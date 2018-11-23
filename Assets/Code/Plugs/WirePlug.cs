using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

namespace DCATS.Assets.Plugs
{
    public class WirePlug : BaseUsable
    {
        protected override void OnEnable()
        {
            base.OnEnable();

            Debug.Log("Do something here with the usable object...");
            Debug.LogWarning("Do something here with the usable object...");
        }

        protected override void OnDisable()
        {
            Debug.Log("Do something here with the usable object...");
            Debug.LogWarning("Do something here with the usable object...");

            base.OnDisable();
        }


        protected override void UseStart()
        {
            Debug.Log("Do something here with the usable object...");
            Debug.LogWarning("Do something here with the usable object...");
        }

        protected override void UseEnd()
        {
            Debug.Log("End of Use on UsableObject...");
            Debug.LogWarning("End of use on usable object...");
        }
    }
}
