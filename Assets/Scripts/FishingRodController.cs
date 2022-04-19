using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(FishingRod))]
public class FishingRodController : MonoBehaviour
{
    private Interactable interactable;
    private FishingRod fishingRod;
    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers) & (~Hand.AttachmentFlags.VelocityMovement);
    public SteamVR_Action_Boolean teleportAction = SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport");

    void Awake()
    {
        interactable = this.GetComponent<Interactable>();
        fishingRod = GetComponent<FishingRod>();
    }

    private void HandHoverUpdate(Hand hand)
    {
        GrabTypes startingGrabType = hand.GetGrabStarting();
        if (startingGrabType == GrabTypes.Pinch || Input.GetKeyDown(KeyCode.Escape))
        {
            hand.DetachObject(this.gameObject);
            hand.HoverUnlock(interactable);
            return;
        }

        if (interactable.attachedToHand == null && startingGrabType != GrabTypes.None)
        {
            hand.HoverLock(interactable);
            hand.AttachObject(gameObject, startingGrabType, attachmentFlags);

            return;
        }

        if (WasTeleportButtonReleased(hand))
        {
            Debug.Log("Launch");
            fishingRod.LaunchBobber();
        }
        else if(IsTeleportButtonDown(hand))
        {
            Debug.Log("reel");
            fishingRod.ReelBobber();
        }
    }

    private bool WasTeleportButtonReleased(Hand hand)
    {
        if (hand.noSteamVRFallbackCamera != null)
        {
            return Input.GetKeyUp(KeyCode.T);
        }
        else
        {
            return teleportAction.GetStateUp(hand.handType);
        }
    }

    private bool IsTeleportButtonDown(Hand hand)
    {
        if (hand.noSteamVRFallbackCamera != null)
        {
            return Input.GetKey(KeyCode.T);
        }
        else
        {
            return teleportAction.GetState(hand.handType);
        }
    }
}
