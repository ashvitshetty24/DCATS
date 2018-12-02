using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;
using DCATS.Assets.Plugs.Internal;

namespace DCATS.Assets.Plugs
{
    /// <summary>
    /// Attach to a Slot Parent Object
    /// </summary>
    public class PlugSlotLogic : MonoBehaviour
    {
        #region Property Backing Fields
        private VisualSlot _VisualSlot = null;
        #endregion

        [SerializeField]
        public bool UseColorCoding = true;

        public Color SlotColor { get; protected set; }
        protected VisualSlot VisualSlot
        {
            get
            {
                if (_VisualSlot == null)
                {
                    _VisualSlot = this.GetComponentInChildren<VisualSlot>();
                }

                return _VisualSlot;
            }
        }
        protected Material SlotMaterial
        {
            get
            {
                var visual = VisualSlot;
                if (visual != null)
                {
                    return visual.SlotMaterial;
                }
                else
                {
                    return null;
                }
            }
        }
        public PlugSlot Slot
        {
            get
            {
                return this.GetComponentInChildren<PlugSlot>();
            }
        }


        protected virtual void Start()
        {
            if (UseColorCoding)
            {
                var slot = Slot;
                if (slot != null)
                {
                    var kind = slot.Kind;
                    Color color = ColorCoding.ColorForPlug(kind);
                    SlotColor = color;
                }
            }
            else
            {
                SlotColor = ColorCoding.Default;
            }
        }

        protected virtual void Update()
        {
            var material = this.SlotMaterial;
            if (material != null)
            {
                if (material.color != SlotColor)
                {
                    material.color = SlotColor;
                }
            }
        }
    }
}
