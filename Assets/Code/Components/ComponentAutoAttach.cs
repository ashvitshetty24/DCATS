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
    public class ComponentAutoAttach : AttachableBase<ComponentSlot, ComponentType>
    {
        protected override void Update()
        {
            base.Update();
            if (!IsPluggedIn())
            {
                RecalculateSelected();
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (IsPluggedIn()/* && this.PluggedSlot.gameObject == other.gameObject*/)
            {
                return;
            }

            GameObject otherObj = other.gameObject;
            ComponentSlot slot = otherObj.GetComponent<ComponentSlot>();
            if (slot != null)
            {
                Debug.LogWarning("[" + name + "] " + "Trying attach...");
                if (!TryPlug(slot))
                {
                    Debug.LogWarning("[" + name + "] " + "Attaching failed.");
                }
            }
        }

        protected override void OnTriggerExit(Collider collider)
        {
            base.OnTriggerExit(collider);
        }
    }
}
