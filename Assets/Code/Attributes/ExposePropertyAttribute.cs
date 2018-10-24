using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace DCATS.Assets.Attributes
{
	[AttributeUsage(AttributeTargets.Property)]
	class ExposePropertyAttribute : Attribute
	{

	}

	public class PropertyField
	{
		object _Instance;
		PropertyInfo _Info;
		SerializedPropertyType _Type;
		MethodInfo _Getter;
		MethodInfo _Setter;


		public SerializedPropertyType Type
		{
			get
			{
				return _Type;
			}
		}

		public string Name
		{
			get
			{
				return ObjectNames.NicifyVariableName(_Info.Name);
			}
		}

		public PropertyField(object Instance, PropertyInfo Info, SerializedPropertyType Type)
		{
			_Instance = Instance;
			_Info = Info;
			_Type = Type;

			_Getter = _Info.GetGetMethod();
			_Setter = _Info.GetSetMethod();
		}

		public object GetValue()
		{
			return _Getter.Invoke(_Instance, null);
		}

		public void SetValue(object value)
		{
			if (GetValue() != value)
			{
				_Setter.Invoke(_Instance, new object[] { value });
			}
		}

		public Type GetPropertyType()
		{
			return _Info.PropertyType;
		}

		public static bool GetPropertyType(PropertyInfo info, out SerializedPropertyType propertyType)
		{
			propertyType = SerializedPropertyType.Generic;
			Type type = info.PropertyType;

			if (type == typeof(int))
			{
				propertyType = SerializedPropertyType.Integer;
				return true;
			}

			if (type == typeof(float))
			{
				propertyType = SerializedPropertyType.Float;
				return true;
			}

			if (type == typeof(bool))
			{
				propertyType = SerializedPropertyType.Boolean;
				return true;
			}

			if (type == typeof(string))
			{
				propertyType = SerializedPropertyType.String;
				return true;
			}

			if (type == typeof(Vector2))
			{
				propertyType = SerializedPropertyType.Vector2;
				return true;
			}

			if (type == typeof(Vector3))
			{
				propertyType = SerializedPropertyType.Vector3;
				return true;
			}

			if (type == typeof(Material))
			{
				propertyType = SerializedPropertyType.ObjectReference;
				return true;
			}

			if (type.IsEnum)
			{
				propertyType = SerializedPropertyType.Enum;
				return true;
			}
			// COMMENT OUT to NOT expose custom objects/types
			propertyType = SerializedPropertyType.ObjectReference;
			return true;

			//return false;
		}
	}

	public static class ExposeProperty
	{
		public static void Expose(PropertyField[] properties)
		{
			GUILayoutOption[] empty_options = new GUILayoutOption[0];

			EditorGUILayout.BeginVertical(empty_options);

			foreach (var field in properties)
			{
				EditorGUILayout.BeginHorizontal(empty_options);

				switch (field.Type)
				{
					case SerializedPropertyType.Integer:
						field.SetValue(EditorGUILayout.IntField(field.Name, (int)field.GetValue(), empty_options));
						break;

					case SerializedPropertyType.Float:
						field.SetValue(EditorGUILayout.FloatField(field.Name, (float)field.GetValue(), empty_options));
						break;

					case SerializedPropertyType.Boolean:
						field.SetValue(EditorGUILayout.Toggle(field.Name, (bool)field.GetValue(), empty_options));
						break;

					case SerializedPropertyType.String:
						field.SetValue(EditorGUILayout.TextField(field.Name, (String)field.GetValue(), empty_options));
						break;

					case SerializedPropertyType.Vector2:
						field.SetValue(EditorGUILayout.Vector2Field(field.Name, (Vector2)field.GetValue(), empty_options));
						break;

					case SerializedPropertyType.Vector3:
						field.SetValue(EditorGUILayout.Vector3Field(field.Name, (Vector3)field.GetValue(), empty_options));
						break;



					case SerializedPropertyType.Enum:
						field.SetValue(EditorGUILayout.EnumPopup(field.Name, (Enum)field.GetValue(), empty_options));
						break;

					case SerializedPropertyType.ObjectReference:
						field.SetValue(EditorGUILayout.ObjectField(field.Name, (UnityEngine.Object)field.GetValue(), field.GetPropertyType(), true, empty_options));
						break;

					default:

						break;
				}

				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.EndVertical();
		}


		public static PropertyField[] GetProperties(object obj)
		{

			List<PropertyField> fields = new List<PropertyField>();

			PropertyInfo[] infos = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

			foreach (PropertyInfo info in infos)
			{

				if (!(info.CanRead && info.CanWrite))
					continue;

				object[] attributes = info.GetCustomAttributes(true);

				bool isExposed = false;

				foreach (object o in attributes)
				{
					if (o.GetType() == typeof(ExposePropertyAttribute))
					{
						isExposed = true;
						break;
					}
				}

				if (!isExposed)
					continue;

				SerializedPropertyType type = SerializedPropertyType.Integer;

				if (PropertyField.GetPropertyType(info, out type))
				{
					PropertyField field = new PropertyField(obj, info, type);
					fields.Add(field);
				}

			}

			return fields.ToArray();

		}

	}
}
