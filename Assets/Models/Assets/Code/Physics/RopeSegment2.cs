//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using UnityEngine;

//namespace DCATS.Assets.Physics
//{
//	public class RopeSegment2 : BodyBehaviour, IRope
//	{

//		public Vector3 Top
//		{
//			get
//			{
//				return new Vector3(0, 0.5f * Length, 0);
//			}
//		}

//		public Vector3 Bottom
//		{
//			get
//			{
//				return new Vector3(0, -0.5f * Length, 0);
//			}
//		}

//		//private float _Length;
//		public float Length
//		{
//			get
//			{
//				return this.Transform.localScale.y;
//			}
//			set
//			{
//				var scaleVec = this.Transform.localScale;
//				scaleVec.y = value;
//				this.Transform.localScale = scaleVec;
//				var anchor1 = new Vector3(0, value/2, 0);
//				var anchor2 = new Vector3(0, -value/2, 0);
//				//TopJoint.anchor = anchor1;
//				//BottomJoint.anchor = anchor2;
//			}
//		}
//		public float Radius
//		{
//			get
//			{
//				return Collider.radius;
//			}
//			set
//			{
//				this.Collider.radius = value;
//				var scale = this.Transform.localScale;
//				scale.z = value;
//				scale.x = value;
//				this.Transform.localScale = scale;
//			}
//		}
//		public float Density
//		{
//			get
//			{
//				throw new NotImplementedException();
//			}
//			set
//			{
//				throw new NotImplementedException();
//			}
//		}
//		public float SpringConstant
//		{
//			get
//			{
//				throw new NotImplementedException();
//			}
//			set
//			{
//				throw new NotImplementedException();
//			}
//		}

//		public Joint TopJoint
//		{
//			get
//			{
//				return this.GetComponents<Joint>()[0];
//			}
//		}

//		public Joint BottomJoint
//		{
//			get
//			{
//				var joints = this.GetComponents<Joint>();
//				if (joints.Length >= 2)
//				{
//					return joints[1];
//				}
//				else
//				{
//					return null;
//				}
//			}
//		}



//		public CapsuleCollider Collider
//		{
//			get
//			{
//				return this.GetComponent<CapsuleCollider>();
//			}
//		}

//		public MeshRenderer Renderer
//		{
//			get
//			{
//				return this.GetComponent<MeshRenderer>();
//			}
//		}

//		public MeshFilter MeshFilter
//		{
//			get
//			{
//				return this.GetComponent<MeshFilter>();
//			}
//		}

//		public RopeSegment2() : base()
//		{

//		}

//		~RopeSegment2()
//		{
//			//GameObject.Destroy(this.gameObject);
//		}

//		protected override void Init()
//		{
//			base.Init();

//			//Renderer.widthCurve = AnimationCurve.Constant(0, 0, 1);
//			//Renderer.positionCount = 2;


			

//			this.Collider.radius = 0.5f;
//			this.Collider.height = 2;
//			TopJoint.anchor = new Vector3(0, 1, 0);
//			//this.TopJoint.autoConfigureConnectedAnchor = false;
//			//Destroy(BottomJoint);
//		}

//		protected override void _Update()
//		{
//			base._Update();


//		}

//		public void ConnectTop(IRope nextSeg)
//		{
//			this.TopJoint.connectedBody = nextSeg.Body;
//			this.TopJoint.connectedAnchor = nextSeg.Bottom;
//		}
//	}
//}
