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

        GameObject _tipPoint = null;
        Grabber _tipGrabber = null;

        #endregion


        [SerializeField]
        public ScrewKind Kind = ScrewKind.Phillips;

        [SerializeField]
        public float CutoffDistance = 0.5f;

        protected GameObject AttachedScrew = null;

        protected Transform TipTransform
        {
            get
            {
                if (_tipPoint == null)
                {
                    SetupTip();
                }

                return _tipPoint.transform;
            }
        }

        protected Grabber TipGrabber
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
            _tipPoint = this.gameObject.transform.Find("TipPoint").gameObject;
            var tipGrabber = _tipPoint.GetComponent<Grabber>();
            if (tipGrabber == null)
            {
                tipGrabber = _tipPoint.AddComponent<Grabber>();
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

            //base.OnDisable();
        }


        protected override void UseStart()
        {
            Debug.Log("Screwdriver is being USED.");
            var objs = this.gameObject.scene.GetRootGameObjects();
            Screw closest = null;
            float closestDistance = 0.0f;
            var screws = Component.FindObjectsOfType(typeof(Screw)).OfType<Screw>();

            foreach (var screw in screws)
            {
                Debug.Log("Found a screw!");
                var dist = (screw.transform.position - this.transform.position).magnitude;
                Debug.Log("Distance: " + dist.ToString());
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

            //foreach (var obj in objs)
            //{
            //    var comps = obj.GetComponentsInChildren<Screw>();
            //    if (comps == null || comps.Length == 0)
            //    {
            //        continue;
            //    }

            //    foreach (var screw in comps)
            //    {
            //        Debug.Log("Found a screw!");
            //        var dist = (screw.transform.position - this.transform.position).magnitude;
            //        Debug.Log("Distance: " + dist.ToString());
            //        if (screw.Kind == this.Kind)
            //        {
            //            if (closest == null)
            //            {
            //                closest = screw;
            //                closestDistance = dist;
            //            }
            //            else
            //            {
            //                if (dist < closestDistance)
            //                {
            //                    closestDistance = dist;
            //                    closest = screw;
            //                }
            //            }
            //        }
            //    }
            //}

            

            if (closest != null && closestDistance <= CutoffDistance)
            {
                Debug.Log("Attaching screw...");
                AttachScrew(closest);
            }
            else
            {
                Debug.Log("Too far away or no screw found. Closest screw: " + closestDistance.ToString());
            }
        }

        protected override void UseEnd()
        {
            Debug.Log("Screwdriver is DONE being USED.");
        }


        protected void AttachScrew(Screw screw)
        {
            var grabbable = screw.GetComponent<BaseGrabbable>();
            if (!grabbable.TryGrabWith(TipGrabber))
            {
                Debug.Log("Couldn't grab the screw.");
                // TODO
            }
            else
            {
                Debug.Log("Grabbed the screw!!!");
            }
        }
    }
}
