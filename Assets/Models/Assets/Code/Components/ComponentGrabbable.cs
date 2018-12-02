using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;
using DCATS.Assets.Attachable;

namespace DCATS.Assets.Components
{
    public class ComponentGrabbable : AttachGrabbableBase
    {
        protected override void StartGrab(BaseGrabber grabber)
        {
            base.StartGrab(grabber);
            //transform.position = transform.parent.position;
        }
    }
}
