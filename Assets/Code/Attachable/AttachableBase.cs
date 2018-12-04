using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;

namespace DCATS.Assets.Attachable
{
    public class AttachableEvent<TSlotType> : UnityEvent<AttachableBase<TSlotType>, TSlotType> where TSlotType : AttachGrabberBase
    {

    }


    public abstract class AttachableBase : BaseUsable
    {
        [SerializeField]
        public bool UnPluggable = true;

        public abstract bool IsPluggedIn();
        public abstract GameObject PluggedInObject();

        public abstract void SetGrabber(BaseGrabber grabber);
    }

    public class AttachableBase<TSlotType> : AttachableBase where TSlotType : AttachGrabberBase
    {
        protected readonly HashSet<Collider> CollidersInRange = new HashSet<Collider>();
        protected TSlotType PluggedSlot = null;
        protected TSlotType SelectedSlot = null;
        public override bool IsPluggedIn()
        {
            //if (PluggedSlot != null)
            //{
            //    if (this.Grabbable().GrabberPrimary == null)
            //    {
            //        throw new Exception("Inconsistent state!");
            //    }
            //}
            return (this.Grabbable().GrabberPrimary != null && PluggedSlot != null);
        }
        public override GameObject PluggedInObject()
        {
            return PluggedSlot.gameObject;
        }
        public override void SetGrabber(BaseGrabber grabber)
        {
            if (grabber is TSlotType)
            {
                PluggedSlot = (grabber as TSlotType);
                IsPluggedInOut = true;
            }
            else
            {
                PluggedSlot = null;
                IsPluggedInOut = false;
            }
        }

        [SerializeField]
        public bool IsPluggedInOut = false;

        

        protected virtual bool CheckKinds(TSlotType other)
        {
            return true;
        }



        public AttachableEvent<TSlotType> OnPlugAttempt;
        public AttachableEvent<TSlotType> OnPlugSuccess;
        public AttachableEvent<TSlotType> OnPlugFail;

        protected AttachableBase()
        {

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

        public bool TryPlug(TSlotType slot)
        {
            if (slot == null)
            {
                return false;
            }


            AttachGrabberBase b = slot;

            if (b.IsOccupied())
            {
                return false;
            }

            //Debug.Log("Not occupied!");
            //var agb = (slot as AttachGrabberBase);



            PlugAttempt(slot);

            if (CheckKinds(slot))
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

        protected void SelectSlot(TSlotType slot)
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

                //throw new NotImplementedException();

            }



        }

        protected void AttachSlot(TSlotType slot)
        {
            //Debug.Log("Matching slot triggered.");





            DeselectSlot(SelectedSlot);
            SelectedSlot = null;

            // - End grab
            // - Start grab from slot to component
            // - Set the "PluggedSlot" property


            var grabbable = this.Grabbable();
            if (grabbable != null)
            {
                
                //grabbable.DetachAllGrabbers();
            }
            grabbable.TransferTo(slot);

            slot.DoGrab(grabbable);
            PluggedSlot = slot;
            IsPluggedInOut = true;
            PlugSuccess(slot);

            var slotEvents = slot as IAttachSlotEvents<TSlotType>;
            if (slotEvents != null)
            {
                if (slotEvents.OnAttachSuccess != null)
                {
                    slotEvents.OnAttachSuccess.Invoke(this, slot);
                }
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
            IsPluggedInOut = false;

            // TODO
            // ...

            throw new NotImplementedException();
        }


        protected void PlugAttempt(TSlotType slot)
        {
            if (OnPlugAttempt != null)
            {
                OnPlugAttempt.Invoke(this, slot);
            }
        }

        protected void PlugSuccess(TSlotType slot)
        {
            if (OnPlugSuccess != null)
            {
                OnPlugSuccess.Invoke(this, slot);
            }
        }

        protected void PlugFail(TSlotType slot)
        {
            if (OnPlugFail != null)
            {
                OnPlugFail.Invoke(this, slot);
            }
        }




        protected virtual TSlotType FindClosestSlot(IEnumerable<TSlotType> slots)
        {
            return slots
                    .OrderBy(s => (this.transform.position - s.transform.position).magnitude)
                    .FirstOrDefault();
        }

        protected TSlotType FindClosestSlot(IEnumerable<GameObject> objects)
        {
            return FindClosestSlot(objects.Where(o => o != null).Select(o => o.GetComponent<TSlotType>()).Where(s => s != null));
        }

        protected TSlotType FindClosestSlot<G>(IEnumerable<G> comps) where G : Component
        {
            return FindClosestSlot(comps.Select(c => c.gameObject));
        }



        private void DeselectSlot(TSlotType slot)
        {
            if (slot == null)
            {
                return;
            }

            // TODO:
            // - Un-Highlight slot


            //throw new NotImplementedException();
        }

        protected AttachGrabbableBase Grabbable()
        {
            return this.GetComponent<AttachGrabbableBase>();
        }
    }



    public class AttachableBase<TSlotType, TKind> : AttachableBase<TSlotType> where TSlotType : AttachGrabberBase, IAttachableKindInfo<TKind>
    {
        [SerializeField]
        public TKind Kind;

        protected override TSlotType FindClosestSlot(IEnumerable<TSlotType> slots)
        {
            return slots
                    .Where(s => s.Kind.Equals(this.Kind))
                    .OrderBy(s => (this.transform.position - s.transform.position).magnitude)
                    .FirstOrDefault();
        }


        protected override bool CheckKinds(TSlotType other)
        {
            return Kind.Equals(other.Kind);
        }
    }
}
