// Modified version DebugPanelControllerInfo.cs from Windows Mixed Reality Toolkit
// This is simply using their code for extracting the state of the controller as input is read from it
/*

using UnityEngine.XR.WSA.Input;
using UnityEngine;
using System.Collections.Generic;

namespace DCATS.Assets.Attachable
{
    public partial class SimLogic
    {
        public class ControllerState
        {
            public InteractionSourceHandedness Handedness;
            public Vector3 PointerPosition;
            public Quaternion PointerRotation;
            public Vector3 GripPosition;
            public Quaternion GripRotation;
            public bool Grasped;
            public bool MenuPressed;
            public bool SelectPressed;
            public float SelectPressedAmount;
            public bool ThumbstickPressed;
            public Vector2 ThumbstickPosition;
            public bool TouchpadPressed;
            public bool TouchpadTouched;
            public Vector2 TouchpadPosition;
        }

        public Dictionary<uint, ControllerState> controllers;

        public void Awake()
        {
            controllers = new Dictionary<uint, ControllerState>();

            InteractionManager.InteractionSourceDetected += InteractionManager_InteractionSourceDetected;

            InteractionManager.InteractionSourceLost += InteractionManager_InteractionSourceLost;
            InteractionManager.InteractionSourceUpdated += InteractionManager_InteractionSourceUpdated;
        }

        public void InteractionManager_InteractionSourceDetected(InteractionSourceDetectedEventArgs obj)
        {
            Debug.LogFormat("{0} {1} Detected", obj.state.source.handedness, obj.state.source.kind);

            if (obj.state.source.kind == InteractionSourceKind.Controller && !controllers.ContainsKey(obj.state.source.id))
            {
                controllers.Add(obj.state.source.id, new ControllerState { Handedness = obj.state.source.handedness });
            }
        }

        public void InteractionManager_InteractionSourceLost(InteractionSourceLostEventArgs obj)
        {
            Debug.LogFormat("{0} {1} Lost", obj.state.source.handedness, obj.state.source.kind);

            controllers.Remove(obj.state.source.id);
        }

        public void InteractionManager_InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
        {
            ControllerState controllerState;
            if (controllers.TryGetValue(obj.state.source.id, out controllerState))
            {
                obj.state.sourcePose.TryGetPosition(out controllerState.PointerPosition, InteractionSourceNode.Pointer);
                obj.state.sourcePose.TryGetRotation(out controllerState.PointerRotation, InteractionSourceNode.Pointer);
                obj.state.sourcePose.TryGetPosition(out controllerState.GripPosition, InteractionSourceNode.Grip);
                obj.state.sourcePose.TryGetRotation(out controllerState.GripRotation, InteractionSourceNode.Grip);

                controllerState.Grasped = obj.state.grasped;
                controllerState.MenuPressed = obj.state.menuPressed;
                controllerState.SelectPressed = obj.state.selectPressed;
                controllerState.SelectPressedAmount = obj.state.selectPressedAmount;
                controllerState.ThumbstickPressed = obj.state.thumbstickPressed;
                controllerState.ThumbstickPosition = obj.state.thumbstickPosition;
                controllerState.TouchpadPressed = obj.state.touchpadPressed;
                controllerState.TouchpadTouched = obj.state.touchpadTouched;
                controllerState.TouchpadPosition = obj.state.touchpadPosition;
            }
        }
    }
}
*/