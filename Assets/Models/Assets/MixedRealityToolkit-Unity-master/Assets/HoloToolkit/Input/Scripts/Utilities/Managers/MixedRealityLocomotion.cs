////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//  This is an implementation using the classes from the InputModule namespace provided by the Windows Mixed Reality Toolkit (WMRT) to provide users with a "fade step" 
//  approach to locomotion throughout the DCATS environment. This is a modification and restructuring of some of the functionality of the MixedRealityTeleport.cs script from 
//  the WMRT. This fade step approach will provide the simulation with more limits on user motion than the teleportation provided by the Mixed Reality Toolkit, and will help 
//  users keep grounded during the simulation and be more immersed. Also, the idea of fading the screen in and out quickly between steps (camera translations) is quite 
//  effective in avoiding VR Motion Sickness that many users encounter when trying to move through a VR simulation in the manner of a typical First Person video game 
//  (smoothly between frames with no transitional queues whatsoever). This fade step implementation will be more comfortable for the user as well as less dangerous, as it can
//  be hazardous to become dizzy and lose your balance when wearing a headset that completely takes away your spatial awareness of the real world.
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using UnityEngine;

#if UNITY_2017_2_OR_NEWER
using UnityEngine.XR;
#if UNITY_WSA
using UnityEngine.XR.WSA;
using UnityEngine.XR.WSA.Input;
#endif
#else
using UnityEngine.VR;
#if UNITY_WSA
using UnityEngine.VR.WSA.Input;
#endif
#endif

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// Script provides user with the ability to move more freely through the simulation using the joystick instead of teleporting throughout the simulation.
    /// </summary>
    [RequireComponent(typeof(SetGlobalListener))]
    public class MixedRealityLocomotion : Singleton<MixedRealityLocomotion>, IControllerInputHandler
    {
        // setup controller scheme. as can be seen in InputMappingAxisUtility.cs, input from an Xbox controller and a Motion Tracked Controller are nearly the same
        [Tooltip("Name of the left thumbstick axis to check for horizontal movement.")]
        public XboxControllerMappingTypes LeftHorizontal = XboxControllerMappingTypes.XboxLeftStickHorizontal;

        [Tooltip("Name of the left thumbstick axis to check for movement forwards and backwards.")]
        public XboxControllerMappingTypes LeftVertical = XboxControllerMappingTypes.XboxLeftStickVertical;

        [Tooltip("Name of the right thumbstick axis to check for horizontal movement.")]
        public XboxControllerMappingTypes RightHorizontal = XboxControllerMappingTypes.XboxRightStickHorizontal;

        [Tooltip("Name of the right thumbstick axis to check for movement forwards and backwards.")]
        public XboxControllerMappingTypes RightVertical = XboxControllerMappingTypes.XboxRightStickVertical;

        [Tooltip("Custom Input Mapping for left horizontal")]
        public string LeftThumbstickX = InputMappingAxisUtility.CONTROLLER_LEFT_STICK_HORIZONTAL;

        [Tooltip("Custom Input Mapping for left vertical")]
        public string LeftThumbstickY = InputMappingAxisUtility.CONTROLLER_LEFT_STICK_VERTICAL;

        [Tooltip("Custom Input Mapping for right horizontal")]
        public string RightThumbstickX = InputMappingAxisUtility.CONTROLLER_RIGHT_STICK_HORIZONTAL;

        [Tooltip("Custom Input Mapping for right vertical")]
        public string RightThumbstickY = InputMappingAxisUtility.CONTROLLER_RIGHT_STICK_VERTICAL;

        [Tooltip("Makes sure you don't get put 'on top' of holograms, just on the floor. If true, your height won't change as a result of a teleport.")]
        public bool StayOnTheFloor = true;

        // the amount of a step
        public float StepAmount = 0.3f;

        [SerializeField]
        private Animator animationController;

        // no custom mapping needed since we're using the Motion Tracked Controllers
        [SerializeField]
        private bool useCustomMapping = false;

        /// <summary>
        /// The fade control allows us to fade out and fade in the scene.
        /// This is done to improve comfort when using an immersive display.
        /// 
        /// Note: After testing and doing research on other programs that have implemented free locomotion in VR, navigating as one would in a traditional First Person
        ///         game is very quick to induce nausea and light-headedness, which is not only uncomfortable for the user but can be quite dangerous since the user is
        ///         not fully aware of their surrounding environment. Due to this,
        /// </summary>
        private FadeManager fadeControl;

        private uint currentSourceId;

        private void Start()
        {

            // FadeManager isn't checked unless we're in a
            // setup where it might be supported.
            FadeManager.AssertIsInitialized();

            fadeControl = FadeManager.Instance;

            // If the FadeManager is missing,
            // remove this component.
            if (fadeControl == null)
            {
                Destroy(this);
                return;
            }
        }

        // called once per frame
        private void Update()
        {
#if UNITY_WSA
            if (InteractionManager.numSourceStates > 0)
            {
                return;
            }
#endif      
            HandleGamepad();
        }

        private void HandleGamepad()
        {
            float leftX = 0.0f;
            float leftY = 0.0f;
            float rightX = 0.0f;
            float rightY = 0.0f;

            if (!fadeControl.Busy)
            {
                leftX = Input.GetAxis(useCustomMapping ? LeftThumbstickX : XboxControllerMapping.GetMapping(LeftHorizontal));
                leftY = Input.GetAxis(useCustomMapping ? LeftThumbstickY : XboxControllerMapping.GetMapping(LeftVertical));
                rightX = Input.GetAxis(useCustomMapping ? RightThumbstickX : XboxControllerMapping.GetMapping(RightHorizontal));
                rightY = Input.GetAxis(useCustomMapping ? RightThumbstickY : XboxControllerMapping.GetMapping(RightVertical));
            }

            else
            {
                if (leftX < -0.8 && Math.Abs(leftY) < 0.3)
                {
                    DoStep(Vector3.left * StepAmount);
                }
                else if (leftX > 0.8 && Math.Abs(leftY) < 0.3)
                {
                    DoStep(Vector3.right * StepAmount);
                }
                else if (leftY < -0.8 && Math.Abs(leftX) < 0.3)
                {
                    DoStep(Vector3.back * StepAmount);
                }
                else if (leftY > -0.8 && Math.Abs(leftX) < 0.3)
                {
                    DoStep(Vector3.forward * StepAmount);
                }
                else if (rightX < -0.8 && Math.Abs(rightY) < 0.3)
                {
                    DoStep(Vector3.left * StepAmount);
                }
                else if (rightX > 0.8 && Math.Abs(rightY) < 0.3)
                {
                    DoStep(Vector3.right * StepAmount);
                }
                else if (rightY < -0.8 && Math.Abs(rightX) < 0.3)
                {
                    DoStep(Vector3.back * StepAmount);
                }
                else if (rightY > -0.8 && Math.Abs(rightX) < 0.3)
                {
                    DoStep(Vector3.forward * StepAmount);
                }
            }
        }

        void IControllerInputHandler.OnInputPositionChanged(InputPositionEventData eventData)
        {
            if (eventData.Position.y < -0.8 && Math.Abs(eventData.Position.x) < 0.3)
            {
                DoStep(Vector3.back * StepAmount);
            }

            else if (eventData.Position.y > -0.8 && Math.Abs(eventData.Position.x) < 0.3)
            {
                DoStep(Vector3.forward * StepAmount);
            }

            else if (eventData.Position.x < -0.8 && Math.Abs(eventData.Position.y) < 0.3)
            {
                DoStep(Vector3.left * StepAmount);
            }

            else if (eventData.Position.x > 0.8 && Math.Abs(eventData.Position.y) < 0.3)
            {
                DoStep(Vector3.right * StepAmount);
            }
        }

        public void DoStep(Vector3 StepAmount)
        {
            if (StepAmount.magnitude != 0 && !fadeControl.Busy)
            {
                fadeControl.DoFade(
                    0.15f, // Fade out time
                    0.15f, // Fade in time
                    () => // Action after fade out
                    {
                        Transform transformToRotate = CameraCache.Main.transform;
                        transformToRotate.rotation = Quaternion.Euler(0, transformToRotate.rotation.eulerAngles.y, 0);
                        transform.Translate(StepAmount, CameraCache.Main.transform);
                    }, null); // Action after fade in
            }
        }

        /// <summary>
        /// Places the player in the specified position of the world
        /// </summary>
        /// <param name="worldPosition"></param>
        public void SetWorldPosition(Vector3 worldPosition)
        {
            // There are two things moving the camera: the camera parent (that this script is attached to)
            // and the user's head (which the MR device is attached to. :)). When setting the world position,
            // we need to set it relative to the user's head in the scene so they are looking/standing where 
            // we expect.
            var newPosition = worldPosition - (CameraCache.Main.transform.position - transform.position);

            // If we're Stationary, we'll need to raycast to estimate our height. In RoomScale, that will be accounted for by the offset between the camera and its parent.
            if (XRDevice.GetTrackingSpaceType() == TrackingSpaceType.Stationary && !StayOnTheFloor)
            {
                RaycastHit hitInfo;
                newPosition.y += (Physics.Raycast(CameraCache.Main.transform.position, Vector3.down, out hitInfo, 5.0f) ? hitInfo.distance : 1.7f);
            }
            else
            {
                newPosition.y = StayOnTheFloor ? transform.position.y : worldPosition.y;
            }

            transform.position = newPosition;
        }
    }
}