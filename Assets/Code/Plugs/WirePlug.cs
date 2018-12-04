#define VERBOSE_DEBUG

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using UnityEngine.Events;
using DCATS.Assets.Connectable;
using DCATS.Assets.Extensions;


namespace DCATS.Assets.Plugs
{
    [AddComponentMenu("DCATS/Plugs/Plug")]
    public class WirePlug : ConnectableAttachment<PlugType>
    {
        [SerializeField]
        public bool UseColorCoding = true;

        public Color SlotColor { get; protected set; }

        private void SetSlotColor()
        {
            if (UseColorCoding)
            {
                Color color = ColorCoding.ColorForPlug(this.Kind);
                SlotColor = color;
            }
            else
            {
                SlotColor = ColorCoding.Default;
            }
        }


        public void SetColorCoding(bool state)
        {
            SetSlotColor();
            if (UseColorCoding != state)
            {
                UseColorCoding = state;
                UpdateColorCoding();
            }
        }

        public void UpdateColorCoding()
        {
            if (this.Kind != PlugType.CPUFan)
            {
                return;
            }
            var renderer = this.GetComponent<Renderer>();
            if (renderer != null)
            {
                var material = renderer.material;
                if (material != null)
                {
                    

                    if (UseColorCoding)
                    {
                        if (!material.name.Contains(ColorCoding.IndicatorMaterialName))
                        {
                            renderer.material = GetIndicatorMaterial();
                        }

                        if (material.color != SlotColor)
                        {
                            material.color = SlotColor;
                        }
                    }
                    else if (material.name.Contains(ColorCoding.IndicatorMaterialName))
                    {
                        if (material.color != ColorCoding.Default)
                        {
                            material.color = ColorCoding.Default;
                        }
                    }
                }
            }
#if VERBOSE_DEBUG
            else
            {
                this.ObjectLog("Renderer was null!");
            }
#endif
        }

        private static Material GetIndicatorMaterial()
        {
            return Resources.Load<Material>("Materials/Plug Slot Indicator");
        }

        public void OnEnable()
        {
            SetSlotColor();
            UpdateColorCoding();
        }

        protected override void Start()
        {
            base.Start();

            SetSlotColor();
        }

        protected override void Update()
        {
            base.Update();
            UpdateColorCoding();
        }
    }
}
