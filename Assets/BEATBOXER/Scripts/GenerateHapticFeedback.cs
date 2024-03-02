using UnityEngine;
using Tilia.CameraRigs.TrackedAlias;

//This script is attached to  the Haptics game obejct in the hierarchy.
//You would ideally used this script when you use a SteamVR device as the Haptics against the UnityXR camera Rig are not good enough.
public class GenerateHapticFeedback : MonoBehaviour
{
    [SerializeField] TrackedAliasFacade haptics; //Drag the CameraRigs.TrackedAlias from the hierarchy into this slot.
    [SerializeField] Punch punchLeft; //Access to the Punch class, so you can listen for its OnCubeHit event.
    [SerializeField] Punch punchRight; //Access to the Punch class, so you can listen for its OnCubeHit event.



    private void OnEnable()
    {
        punchLeft.OnCubeHitHaptics += GenerateHaptics;
        punchRight.OnCubeHitHaptics += GenerateHaptics;

    }

    private void OnDisable()
    {
        punchLeft.OnCubeHitHaptics -= GenerateHaptics;
        punchRight.OnCubeHitHaptics -= GenerateHaptics;

    }

    void GenerateHaptics(string ctrl)
    {
        //Ensure that your Interactions.Interactor_L and Interactions.Interactor_R have been tagged appropriately as 'LeftCtrl' and 'RightCtrl' respectively.

        if (ctrl == "LeftCtrl") //if its the Left controller
            haptics.BeginHapticProcessOnLeftController();
        else // its the Right controller
            haptics.BeginHapticProcessOnRightController();
    }

}
