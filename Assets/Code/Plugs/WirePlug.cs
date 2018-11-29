using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;

namespace DCATS.Assets.Plugs
{

    public class WirePlug : WirePlugBase
    {
        protected readonly HashSet<PlugSlot> SlotsDetected = new HashSet<PlugSlot>();

        protected bool Interacting = false;

        public WirePlug() : base()
        {
            
        }

        protected override void UseStart()
        {
            base.UseStart();
            Interacting = true;

            foreach (var collider in this.CollidersInRange)
            {
                var slot = collider.GetComponent<PlugSlot>();
                if (slot != null)
                {
                    SlotsDetected.Add(slot);
                }
            }

            PlugSlot closest = FindClosestSlot(SlotsDetected);


            if (closest != null)
            {
                SelectSlot(closest);
            }
        }

        protected override void UseEnd()
        {
            Interacting = false;
            if (SelectedSlot != null)
            {
                this.TryPlug(SelectedSlot);
            }
            SlotsDetected.Clear();
            base.UseEnd();
        }

        protected override void Update()
        {
            base.Update();
            if (Interacting)
            {
                RecalculateSelected();
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (Interacting)
            {
                GameObject otherObj = other.gameObject;
                PlugSlot slot = otherObj.GetComponent<PlugSlot>();

                if (slot != null)
                {
                    SlotsDetected.Add(slot);

                    if (SlotsDetected.Count > 1)
                    {
                        var closest = FindClosestSlot(SlotsDetected);
                        if (closest != null && closest != SelectedSlot)
                        {
                            SelectSlot(closest);
                        }
                    }
                    else
                    {
                        SelectSlot(slot);
                    }
                }
            }
        }

        protected override void OnTriggerExit(Collider collider)
        {
            base.OnTriggerExit(collider);


            if (Interacting)
            {
                var slot = collider.gameObject.GetComponent<PlugSlot>();
                if (slot != null)
                {
                    SlotsDetected.Remove(slot);
                    if (SelectedSlot == slot)
                    {
                        var newSelected = FindClosestSlot(SlotsDetected);
                        SelectSlot(newSelected);
                    }
                }
            }
        }

        
    }
}
