using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using DCATS.Assets.Attachable;

namespace DCATS.Assets.Plugs
{
    public class PlugGrabbable : AttachGrabbableBase
    {
        protected override void StartGrab(BaseGrabber grabber)
        {
            base.StartGrab(grabber);
            //transform.position = transform.parent.position;
        }
    }
}
