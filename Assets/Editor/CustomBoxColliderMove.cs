using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;
using DCATS.Assets.Utility;

namespace DCATS.Assets.Editor
{
#if UNITY_EDITOR
    [CustomEditor(typeof(CustomBoxColliderMove))]
    public class CustomBoxColliderMoveEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Calculate"))
            {
                var script = target as CustomBoxColliderMove;
                if (script != null)
                {
                    script.Go();
                }
            }
        }
    }
#endif
}
