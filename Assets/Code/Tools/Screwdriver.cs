using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace DCATS.Assets.Tools
{
    public class Screwdriver : BaseUsable
    {
        #region PropertyBackings

        ScrewdriverGrabber _tipGrabber = null;

        #endregion


        [SerializeField]
        public ScrewKind Kind = ScrewKind.Phillips;

        [SerializeField]
        public float CutoffDistance = 0.5f;

        [SerializeField]
        public Transform Tip;

        protected GameObject TipObject
        {
            get
            {
                if (Tip != null)
                {
                    return Tip.gameObject;
                }
                else
                {
                    return null;
                }
            }
        }

        protected GameObject AttachedScrew = null;

        protected ScrewdriverGrabber TipGrabber
        {
            get
            {
                if (_tipGrabber == null)
                {
                    SetupTip();
                }
                return _tipGrabber;
            }
        }

        private void SetupTip()
        {
            var tipGrabber = TipObject.GetComponent<ScrewdriverGrabber>();
            if (tipGrabber == null)
            {
                var screwGrabber = TipObject.AddComponent<ScrewdriverGrabber>();

                

                tipGrabber = screwGrabber;
            }
            this._tipGrabber = tipGrabber;
        }


        public Screwdriver()
        {
            
        }

        public Screwdriver(ScrewKind kind) : this()
        {
            Kind = kind;
        }


        protected override void OnEnable()
        {
            base.OnEnable();

            Debug.Log("Screwdriver is ENABLED.");
        }

        protected override void OnDisable()
        {
            Debug.Log("Screwdriver is DISABLED.");

            base.OnDisable();
        }


        protected override void UseStart()
        {
            Debug.Log("Screwdriver is being USED.");
            Screw closest = null;
            float closestDistance = 0.0f;

            GameObject screwObject = FindClosestScrew(out closest, out closestDistance);

            

            if (closest != null && closestDistance <= CutoffDistance)
            {
                AttachScrew(closest);
            }
        }

        protected override void UseEnd()
        {
            Debug.Log("Screwdriver is DONE being USED.");

            if (this.AttachedScrew != null)
            {
                ReleaseScrew();
            }
        }


        protected void AttachScrew(Screw screw)
        {
            this.TipGrabber.DoGrab(screw);
            this.AttachedScrew = screw.gameObject;
        }

        protected void ReleaseScrew()
        {
            GameObject screw = this.AttachedScrew;

            this.TipGrabber.FinishGrab();

            this.AttachedScrew = null;
        }

        protected GameObject FindClosestScrew(out Screw outScrew, out float distance)
        {
            Screw closest = null;
            float closestDistance = 0.0f;
            var screws = Component.FindObjectsOfType(typeof(Screw)).OfType<Screw>();

            foreach (var screw in screws)
            {
                var dist = (screw.transform.position - this.transform.position).magnitude;
                if (screw.Kind == this.Kind)
                {
                    if (closest == null)
                    {
                        closest = screw;
                        closestDistance = dist;
                    }
                    else
                    {
                        if (dist < closestDistance)
                        {
                            closestDistance = dist;
                            closest = screw;
                        }
                    }
                }
            }

            outScrew = closest;
            distance = closestDistance;
            return closest.gameObject;
        }
    }
}
