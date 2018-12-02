using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DCATS.Assets.Physics
{
	public interface IUnityBody : IUnityObject
	{
		UnityEngine.Rigidbody Body { get; }
		UnityEngine.Vector3 Velocity { get; set; }
		UnityEngine.Vector3 AngularVelocity { get; set; }
	}



	public static class IUnityBodyExtensions
	{
		
	}
}
