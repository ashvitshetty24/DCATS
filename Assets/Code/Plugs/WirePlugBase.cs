using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;

namespace DCATS.Assets.Plugs
{
    [Serializable]
    public class WirePluggedEvent : UnityEvent<WirePlugBase, PlugSlot>
    {

    }

    public class WirePlugBase : BaseUsable
    {
        protected readonly HashSet<Collider> CollidersInRange = new HashSet<Collider>();
        protected PlugSlot PluggedSlot = null;
        protected PlugSlot SelectedSlot = null;
        public bool IsPluggedIn
        {
            get
            {
                return PluggedSlot != null;
            }
        }


        protected bool _RequireInteraction { get; set; }

        [SerializeField]
        public PlugType Kind;

        [SerializeField]
        public bool UnPluggable = true;

        public WirePluggedEvent OnPlugAttempt;
        public WirePluggedEvent OnPlugSuccess;
        public WirePluggedEvent OnPlugFail;

        protected WirePlugBase()
        {
            this._RequireInteraction = false;
        }

        protected virtual void Update()
        {

        }

        protected override void OnEnable()
        {
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
        }


        protected override void UseStart()
        {
            base.UseStart();
            
        }

        protected override void UseEnd()
        {
            base.UseEnd();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            CollidersInRange.Add(other);
        }

        protected virtual void OnTriggerExit(Collider collider)
        {
            CollidersInRange.Remove(collider);
        }

        protected virtual void RecalculateSelected()
        {
            if (CollidersInRange.Count == 0)
            {
                if (SelectedSlot != null)
                {
                    DeselectSlot(SelectedSlot);
                    SelectedSlot = null;
                }
                return;
            }

            var closest = FindClosestSlot(CollidersInRange);
            if (closest != SelectedSlot)
            {
                if (closest != null)
                {
                    SelectSlot(closest);
                }
                else
                {
                    if (SelectedSlot != null)
                    {
                        DeselectSlot(SelectedSlot);
                        SelectedSlot = null;
                    }
                }
            }
        }

        public bool TryPlug(PlugSlot slot)
        {
            if (slot == null)
            {
                return false;
            }


            PlugAttempt(slot);

            if (slot.Kind == this.Kind)
            {
                AttachSlot(slot);
                return true;
            }
            else
            {
                PlugFail(slot);
                return false;
            }
        }

        protected void SelectSlot(PlugSlot slot)
        {
            if (this.SelectedSlot != null && this.SelectedSlot != slot)
            {
                DeselectSlot(SelectedSlot);
                SelectedSlot = null;
            }

            if (slot != null)
            {
                this.SelectedSlot = slot;

                // TODO:
                // - Highlight selected object
                // ...

                throw new NotImplementedException();
                
            }



        }

        private void AttachSlot(PlugSlot slot)
        {
            Debug.Log("Matching slot triggered.");



            

            DeselectSlot(SelectedSlot);
            SelectedSlot = null;

            // - End grab
            // - Start grab from slot to component
            // - Set the "PluggedSlot" property


            var grabbable = this.Grabbable();
            if (grabbable != null)
            {
                grabbable.TransferTo(slot);
                //grabbable.DetachAllGrabbers();
            }

            slot.DoGrab(grabbable);
            PluggedSlot = slot;
            PlugSuccess(slot);

            if (slot.OnPlugSuccess != null)
            {
                slot.OnPlugSuccess.Invoke(this, slot);
            }
        }

        public bool TryDetach()
        {
           if (PluggedSlot == null)
            {
                return false;
            }



            // TODO (?)
            // ...
            DetachSlot();
            return true;
        }

        private void DetachSlot()
        {
            Grabbable().DetachAllGrabbers();
            PluggedSlot = null;

            // TODO
            // ...

            throw new NotImplementedException();
        }


        protected void PlugAttempt(PlugSlot slot)
        {
            if (OnPlugAttempt != null)
            {
                OnPlugAttempt.Invoke(this, slot);
            }
        }

        protected void PlugSuccess(PlugSlot slot)
        {
            if (OnPlugSuccess != null)
            {
                OnPlugSuccess.Invoke(this, slot);
            }
        }

        protected void PlugFail(PlugSlot slot)
        {
            if (OnPlugFail != null)
            {
                OnPlugFail.Invoke(this, slot);
            }
        }




        protected PlugSlot FindClosestSlot(IEnumerable<PlugSlot> slots)
        {
            return slots
                    .Where(s => s.Kind == this.Kind)
                    .OrderBy(s => (this.transform.position - s.transform.position).magnitude)
                    .FirstOrDefault();
        }

        protected PlugSlot FindClosestSlot(IEnumerable<GameObject> objects)
        {
            return FindClosestSlot(objects.Where(o => o != null).Select(o => o.GetComponent<PlugSlot>()).Where(s => s != null));
        }

        protected PlugSlot FindClosestSlot<G>(IEnumerable<G> comps) where G : Component
        {
            return FindClosestSlot(comps.Select(c => c.gameObject));
        }



        private void DeselectSlot(PlugSlot slot)
        {
            if (slot == null)
            {
                return;
            }

            // TODO:
            // - Un-Highlight slot


            throw new NotImplementedException();
        }

        protected AttachGrabbableBase Grabbable()
        {
            return this.GetComponent<AttachGrabbableBase>();
        }
    }
}
