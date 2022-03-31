using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(FishingRod))]
public class FishingRodController : MonoBehaviour
{
    private Interactable interactable;
    private FishingRod fishingRod;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

    private bool prevGrabEndState;
    private bool firstGrabEnded;

    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
        fishingRod = GetComponent<FishingRod>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        if (startingGrabType == GrabTypes.Pinch)
        {
            hand.DetachObject(this.gameObject);
            hand.HoverUnlock(interactable);
            return;
        }

        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);
            firstGrabEnded = false;

            return;
        }

        if (interactable.attachedToHand == hand)
        {
            if(startingGrabType != GrabTypes.None)
            {
                fishingRod.ReelBobber();
            }
            if (isGrabEnding && isGrabEnding != prevGrabEndState)
            {
                if (!firstGrabEnded)
                {
                    firstGrabEnded = true;
                }
                else
                {
                    fishingRod.LaunchBobber();
                }
            }
        }
        prevGrabEndState = isGrabEnding;
    }
}
