using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using HoloToolkit.Unity.InputModule.Examples.Grabbables;

namespace DCATS.Assets.Plugs.Internal
{
    /// <summary>
    /// Effectively a tag for the logic scripts to locate the correct child objects
    /// </summary>
    public class VisualSlot : MonoBehaviour
    {
        #region Property Backing Fields
        private MeshRenderer _Renderer = null;
        #endregion
        public MeshRenderer Renderer
        {
            get
            {
                if (_Renderer == null)
                {
                    _Renderer = GetComponent<MeshRenderer>();
                }

                if (_Renderer != null)
                {
                    if (_Renderer.gameObject != this.gameObject)
                    {
                        _Renderer = null;
                        return Renderer;
                    }
                }

                return _Renderer;
            }
        }
        public Material SlotMaterial
        {
            get
            {
                var renderer = Renderer;
                if (renderer != null)
                {
                    return renderer.material;
                }
                else
                {
                    return null;
                }
            }
        }

    }
}
