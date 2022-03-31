using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(FishingRod))]
public class FishingRodController : MonoBehaviour
{
    [SerializeField] Hand hand;
    private Interactable interactable;
    private FishingRod fishingRod;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);

    private bool prevGrabEndState;
    //-------------------------------------------------
    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
        fishingRod = GetComponent<FishingRod>();
    }

    //-------------------------------------------------
    // Called every Update() while a Hand is hovering over this object
    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        bool isGrabEnding = hand.IsGrabEnding(this.gameObject);

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            Debug.Log("Grabbed");
            // Call this to continue receiving HandHoverUpdate messages,
            // and prevent the hand from hovering over anything else
            hand.HoverLock(interactable);

            // Attach this object to the hand
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

            return;
        }

        if (interactable.attachedToHand == hand)
        {
            if(startingGrabType != GrabTypes.None)
            {
                Debug.Log("click");
                fishingRod.ReelBobber();
            }
            if (isGrabEnding && isGrabEnding != prevGrabEndState)
            {
                Debug.Log("unclick");
                fishingRod.LaunchBobber();
            }
        }
        prevGrabEndState = isGrabEnding;
    }


    //-------------------------------------------------
    // Called when this GameObject becomes attached to the hand
    //-------------------------------------------------
    private void OnAttachedToHand(Hand hand)
    {
        Debug.Log("Attached");
    }

    //-------------------------------------------------
    // Called when this GameObject is detached from the hand
    //-------------------------------------------------
    private void OnDetachedFromHand(Hand hand)
    {
        Debug.Log("Detatched");
    }
}
