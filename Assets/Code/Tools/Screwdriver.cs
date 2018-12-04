using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

namespace DCATS.Assets.Tools
{
    public class Screwdriver : SaneBaseUsable
    {
        #region PropertyBackings

        ScrewdriverTip _tip = null;

        #endregion


        [SerializeField]
        public ScrewKind Kind = ScrewKind.Phillips;

        [SerializeField]
        public float CutoffDistance = 0.5f;

        [SerializeField]
        public Transform TipTransform;

        protected GameObject TipObject
        {
            get
            {
                if (TipTransform != null)
                {
                    return TipTransform.gameObject;
                }
                else
                {
                    return null;
                }
            }
        }

        protected GameObject AttachedScrew = null;

        protected ScrewdriverTip Tip
        {
            get
            {
                if (_tip == null)
                {
                    SetupTip();
                }
                return _tip;
            }
        }

        private void SetupTip()
        {
            var tipGrabber = TipObject.GetComponent<ScrewdriverTip>();
            if (tipGrabber == null)
            {
                var screwGrabber = TipObject.AddComponent<ScrewdriverTip>();

                

                tipGrabber = screwGrabber;
            }
            this._tip = tipGrabber;
        }


        public Screwdriver()
        {
            this.PressType = InteractionSourcePressType.Select;
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


        protected override void InputStart(InteractionSourcePressedEventArgs e)
        {
            Debug.Log("Screwdriver is being USED.");
            //Screw closest = null;
            //float closestDistance = 0.0f;

            //GameObject screwObject = FindClosestScrew(out closest, out closestDistance);



            //if (closest != null && closestDistance <= CutoffDistance)
            //{
            //    AttachScrew(closest);
            //}

            Tip.ActionTriggered();
        }

        protected override void InputEnd(InteractionSourceReleasedEventArgs e)
        {
            Debug.Log("Screwdriver is DONE being USED.");



            //if (this.AttachedScrew != null)
            //{
            //    ReleaseScrew();
            //}
        }

        //protected GameObject FindClosestScrew(out Screw outScrew, out float distance)
        //{
        //    Screw closest = null;
        //    float closestDistance = 0.0f;
        //    var screws = Component.FindObjectsOfType(typeof(Screw)).OfType<Screw>();

        //    foreach (var screw in screws)
        //    {
        //        var dist = (screw.transform.position - this.transform.position).magnitude;
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

        //    outScrew = closest;
        //    distance = closestDistance;
        //    return closest.gameObject;
        //}
    }
}
