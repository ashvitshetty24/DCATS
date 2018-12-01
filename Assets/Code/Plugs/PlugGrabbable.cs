using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

namespace DCATS.Assets.Plugs
{
    public class PlugGrabbable : AttachGrabbableBase
    {
        #region Property Fields
        private WirePlugBase _Base = null;
        #endregion
        

        protected WirePlugBase Base
        {
            get
            {
                if (_Base == null)
                {
                    _Base = this.gameObject.GetComponent<WirePlugBase>();
                }

                return _Base;
            }
        }


        protected override void StartGrab(BaseGrabber grabber)
        {
            base.StartGrab(grabber);
            if (grabber is PlugSlot)
            {
                var slot = (grabber as PlugSlot);
                transform.position = transform.parent.position;
            }
        }

        protected override void DetachFromGrabber(BaseGrabber grabber)
        {
            base.DetachFromGrabber(grabber);
        }

        public override bool TryGrabWith(BaseGrabber grabber)
        {

            if (Base.UnPluggable || !Base.IsPluggedIn)
            {
                return base.TryGrabWith(grabber);
            }
            else
            {
                return false;
            }
        }
    }
}
