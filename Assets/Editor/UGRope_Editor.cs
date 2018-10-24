using UnityEditor;
using UnityEngine;
using System.Collections;
using DCATS.Assets.Attributes;

namespace DCATS.Assets.Editor
{
	[CustomEditor(typeof(Physics.UGRope))]
	class UGRope : UnityEditor.Editor
	{
		Physics.UGRope Instance;
		PropertyField[] Fields;

		public void OnEnable()
		{
			Instance = target as Physics.UGRope;
			Fields = ExposeProperty.GetProperties(Instance);
		}

		public override void OnInspectorGUI()
		{
			EditorGUI.BeginChangeCheck();
			//base.OnInspectorGUI();
			if (Instance == null)
			{
				return;
			}

			this.DrawDefaultInspector();
			ExposeProperty.Expose(Fields);
			
		}
	}
}
