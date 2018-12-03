using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace DCATS.Assets
{
    public class SaneBaseUsable : MonoBehaviour
    {
        [SerializeField]
        public InteractionSourcePressType PressType;

        [SerializeField]
        public InteractionSourceHandedness Handedness;

        public bool Active { get; protected set; }


        protected virtual void OnEnable()
        {
#if UNITY_WSA && UNITY_2017_2_OR_NEWER
            InteractionManager.InteractionSourcePressed += InputStartCallback;
            InteractionManager.InteractionSourceReleased += InputEndCallback;
#endif
        }

        protected virtual void OnDisable()
        {
#if UNITY_WSA && UNITY_2017_2_OR_NEWER
            InteractionManager.InteractionSourcePressed -= InputStartCallback;
            InteractionManager.InteractionSourceReleased -= InputEndCallback;
#endif
        }

        

        private bool EventIsRelevant(InteractionSourceState state, InteractionSourcePressType pressType)
        {
            if (pressType == this.PressType)
            {
                if (Handedness == InteractionSourceHandedness.Unknown || Handedness == state.source.handedness)
                {
                    var grabbable = GetComponent<BaseGrabbable>();
                    if (grabbable != null && grabbable.GrabState == GrabStateEnum.Single)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool EventIsRelevant(InteractionSourcePressedEventArgs e)
        {
            return EventIsRelevant(e.state, e.pressType);
        }

        private bool EventIsRelevant(InteractionSourceReleasedEventArgs e)
        {
            return EventIsRelevant(e.state, e.pressType);
        }

        private void InputStartCallback(InteractionSourcePressedEventArgs e)
        {
            if (e.pressType == this.PressType)
            {
                if (Handedness == InteractionSourceHandedness.Unknown || Handedness == e.state.source.handedness)
                {
                    var grabbable = GetComponent<BaseGrabbable>();
                    if (grabbable != null && grabbable.GrabState == GrabStateEnum.Single)
                    {
                        
                    }
                }
            }

            if (EventIsRelevant(e))
            {
                InputStart(e);
                Active = true;
            }
        }

        private void InputEndCallback(InteractionSourceReleasedEventArgs e)
        {
            if (EventIsRelevant(e))
            {
                InputEnd(e);
                Active = false;
            }
        }

        protected virtual void InputStart(InteractionSourcePressedEventArgs e)
        {

        }

        protected virtual void InputEnd(InteractionSourceReleasedEventArgs e)
        {

        }
    }
}
