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
            Debug.Log("[" + name + "] " + "Starting grab!");
            Debug.Log("Before: " + GrabPoint);
            base.StartGrab(grabber);

            transform.position = transform.parent.position;




            if (this.GrabPoint != null)
            {
                transform.localPosition -= GrabPoint;
                //transform.position += new Vector3(transform.localScale.x * GrabPoint.x, transform.localScale.y * GrabPoint.y, transform.localScale.z * GrabPoint.z);
                transform.position += this.GrabPoint;
            }

        }
    }
}
