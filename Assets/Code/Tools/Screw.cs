using DCATS.Assets.Attachable;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DCATS.Assets.Connectable;

namespace DCATS.Assets.Tools
{
    public class Screw : ConnectableAttachment<ScrewKind>
    {
        //protected override void StartGrab(BaseGrabber grabber)
        //{
        //    base.StartGrab(grabber);
        //    transform.position = transform.parent.position;
        //    transform.Rotate(new Vector3(0, 1, 0), 180);
        //}
    }
}
