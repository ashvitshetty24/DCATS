using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCATS.Assets
{
	public interface IUnityObject
	{
		UnityEngine.Transform Transform { get; }
	}


	public static class IUnityObjectExtensions
	{
		public static void Rotate(this IUnityObject obj, float x, float y, float z)
		{
			obj.Transform.Rotate(x, y, z);
		}

		public static void Rotate(this IUnityObject obj, UnityEngine.Vector3 qty)
		{
			obj.Transform.Rotate(qty);
		}
	}
}
