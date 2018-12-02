using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;



namespace DCATS.Assets.Utility
{
    [AddComponentMenu("DCATS/Editor/Move Box Colliders")]
    public class CustomBoxColliderMove : MonoBehaviour
    {
#if !OBI_UNAVAILABLE
        [Tooltip("Adds an Obi collider to each generated child object")]
        public bool addObiColliders = false;
#endif

        [Tooltip("Preserve enabled/disabled")]
        public bool preserveEnabledOrDisabled = true;

        public void Go()
        {
            var colliders = this.gameObject.GetComponents<BoxCollider>();

            var children = this.transform.childCount;
            int nameIndex = children + 1;

            for (int i = 0; i < colliders.Length; ++i)
            {
                var collider = colliders[i];

                // You never *really* know...
                if (collider == null)
                {
                    continue;
                }


                var obj = new GameObject("Collider [" + (i + nameIndex) + "]");

                obj.transform.parent = this.transform;
                obj.transform.localRotation = Quaternion.Euler(Vector3.zero);
                obj.transform.localPosition = Vector3.zero;
                obj.layer = this.gameObject.layer;

                var newCollider = obj.AddComponent<BoxCollider>();
                CopyBoxCollider(collider, newCollider);

                

                AddObi(newCollider);

                if (preserveEnabledOrDisabled)
                {
                    if (!collider.enabled)
                    {
                        obj.SetActive(false);
                    }
                }
                
                DestroyImmediate(collider);
            }

            
        }

        public void AddObi(Collider collider)
        {
#if !OBI_UNAVAILABLE
            if (addObiColliders)
            {
                var obi = collider.gameObject.AddComponent<Obi.ObiCollider>();
                obi.SourceCollider = collider;
            }
#endif
        }
        

        public static void CopyBoxCollider(BoxCollider source, BoxCollider destination)
        {
            destination.isTrigger = source.isTrigger;
            destination.material = source.material;
            destination.sharedMaterial = source.sharedMaterial;
            destination.center = source.center;
            destination.size = source.size;
        }
    }
}
