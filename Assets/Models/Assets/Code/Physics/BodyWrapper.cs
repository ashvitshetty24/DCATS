using System.Collections.Generic;
using System.Linq;
using System.Text;
using DCATS.Assets.Physics;
using UnityEngine;

namespace DCATS.Assets.Physics
{
	class BodyWrapper : IUnityBody
	{
		protected readonly GameObject Obj;

		public BodyWrapper(GameObject obj)
		{
			Obj = obj;
		}

		public Rigidbody Body
		{
			get
			{
				return Obj.GetComponent<Rigidbody>();
			}
		}

		public Vector3 Velocity
		{
			get
			{
				return Body.velocity;
			}
			set
			{
				Body.velocity = value;
			}
		}

		public Vector3 AngularVelocity
		{
			get
			{
				return Body.angularVelocity;
			}
			set
			{
				Body.angularVelocity = value;
			}
		}

		public Transform Transform
		{
			get
			{
				return Obj.transform;
			}
		}

		public static BodyWrapper Wrap(GameObject obj)
		{
			if (obj != null)
			{
				return new BodyWrapper(obj);
			}
			else
			{
				return null;
			}
		}


		public static BodyWrapper Wrap<T>(T comp) where T : Component
		{
			if (comp != null)
			{
				return new BodyWrapper<T>(comp);
			}
			else
			{
				return null;
			}
		}


		public static PhysicsBodyWrapper WrapPhysics(GameObject obj)
		{
			if (obj != null)
			{
				return new PhysicsBodyWrapper(obj);
			}
			else
			{
				return null;
			}
		}

		public static PhysicsBodyWrapper WrapPhysics<T>(T comp) where T : Component
		{
			if (comp != null)
			{
				return new PhysicsBodyWrapper<T>(comp);
			}
			else
			{
				return null;
			}
		}

	}

	class BodyWrapper<T> : BodyWrapper where T : Component
	{

		public BodyWrapper(T t) : base(t.gameObject)
		{
			
		}
	}






	class PhysicsBodyWrapper : BodyWrapper, IUnityBody
	{
		private readonly ConstantForce ForceComponent;
		public PhysicsBodyWrapper(GameObject obj) : base(obj)
		{
			ForceComponent = obj.GetComponent<ConstantForce>();
			if (ForceComponent == null)
			{
				ForceComponent = obj.AddComponent<ConstantForce>();
			}
		}

		public Vector3 ConstantForce
		{
			get
			{
				return ForceComponent.force;
			}

			set
			{
				ForceComponent.force = value;
			}
		}
	}

	class PhysicsBodyWrapper<T> : PhysicsBodyWrapper where T : Component
	{

		public PhysicsBodyWrapper(T t) : base(t.gameObject)
		{

		}
	}
}
