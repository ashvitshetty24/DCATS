using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;

namespace DCATS.Assets.Plugs
{
    public class WireAutoPlug : WirePlugBase
    {
        public WireAutoPlug() : base()
        {
            
        }

        protected override void Update()
        {
            base.Update();
            if (!IsPluggedIn)
            {
                RecalculateSelected();
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            GameObject otherObj = other.gameObject;
            PlugSlot slot = otherObj.GetComponent<PlugSlot>();
            if (slot != null)
            {
                TryPlug(slot);
            }
        }

        protected override void OnTriggerExit(Collider collider)
        {
            base.OnTriggerExit(collider);
        }
    }
}
