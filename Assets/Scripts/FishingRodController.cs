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
        if (startingGrabType == GrabTypes.Pinch || Input.GetKeyDown(KeyCode.Escape))
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
        prevGrabEndState = isGrabEnding;
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

            //return hand.controller.GetPressUp( SteamVR_Controller.ButtonMask.Touchpad );
        }

        return false;
    }

    //-------------------------------------------------
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

        return false;
    }
}
