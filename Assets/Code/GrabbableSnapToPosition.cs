using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets
{
    [AddComponentMenu("DCATS/Grabbable Snap To Position")]
    public class GrabbableSnapToPosition : GrabbableSnapToOrient
    {
        protected override void StartGrab(BaseGrabber grabber)
        {
            base.StartGrab(grabber);

            transform.position = transform.parent.position;





        }
    }
}
