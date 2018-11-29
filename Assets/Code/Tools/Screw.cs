using DCATS.Assets.Attachable;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Tools
{
    public class Screw : AttachGrabbableBase
    {
        [SerializeField]
        public ScrewKind Kind = ScrewKind.Phillips;

        protected override void StartGrab(BaseGrabber grabber)
        {
            base.StartGrab(grabber);
            transform.position = transform.parent.position;
            transform.Rotate(new Vector3(0, 1, 0), 180);
        }
    }
}
