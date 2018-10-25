//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;
//using System.Linq;

//namespace DCATS.Assets.Physics
//{
//	public class RopeBody2 : BodyBehaviour, IRope
//	{
//		#region HiddenMembers
//		private IUnityBody _obj1;
//		private IUnityBody _obj2;
//		private float _springConstant;
//		private float _density;
//		#endregion

//		public const int DefaultSegmentDensity = 100;
//		public IUnityBody Object1
//		{
//			get
//			{
//				return _obj1;
//			}
//			set
//			{
//				// TODO
//				_obj1 = value;
//				OnObjectSet(value, true);
//			}
//		}
//		public IUnityBody Object2
//		{
//			get
//			{
//				return _obj2;
//			}
//			set
//			{
//				// TODO
//				_obj2 = value;
//				OnObjectSet(value, false);
//			}
//		}
//		public float SpringConstant
//		{
//			get
//			{
//				return _springConstant;
//			}
//			set
//			{
//				// TODO
//				_springConstant = value;
//			}
//		}
//		public float Density
//		{
//			get
//			{
//				return _density;
//			}
//			set
//			{
//				// TODO
//				_density = value;
//			}
//		}
//		public float Radius
//		{
//			get
//			{
//				Debug.Assert(Renderer.startWidth == Renderer.endWidth);
//				return Renderer.startWidth;
//			}
//			set
//			{
//				if (Renderer != null)
//				{
//					Renderer.widthCurve = AnimationCurve.Constant(0, 0, 1);
//					Renderer.widthMultiplier = value;
//				}

//				foreach (var segment in Segments)
//				{
//					segment.Radius = value;
//				}
//			}
//		}
//		public float Length
//		{
//			get
//			{
//				return Segments.Select(s => s.Length).Sum();
//				if (Object1 != null && Object2 != null)
//				{
//					return (Object1.Transform.position - Object2.Transform.position).magnitude;
//				}
//				else
//				{
//					return 0.0f;
//				}
//			}
//		}
//		public double Volume
//		{
//			get
//			{
//				return Length * (Radius * Radius * Math.PI);
//			}
//		}
//		public double RopeMass
//		{
//			get
//			{
//				return Volume * Density;
//			}
//		}

//		public List<RopeSegment2> Segments
//		{
//			get;
//			private set;
//		}

//		private LineRenderer Renderer
//		{
//			get
//			{
//				return this.GetComponent<LineRenderer>();
//			}
//		}

//		public int SegmentCount
//		{
//			get
//			{
//				if (Segments != null)
//				{
//					return Segments.Count;
//				}
//				else
//				{
//					return 0;
//				}
//			}
//			set
//			{
//				ReCalculateSegments(value);
//			}
//		}

//		public Vector3 Top
//		{
//			get
//			{
//				if (SegmentCount > 0)
//				{
//					return Segments.First().Top;
//				}
//				else
//				{
//					return new Vector3();
//				}
//			}
//		}

//		public Vector3 Bottom
//		{
//			get
//			{
//				if (SegmentCount > 0)
//				{
//					return Segments.Last().Bottom;
//				}
//				else
//				{
//					return new Vector3();
//				}
//			}
//		}




//		// Use this for initialization
//		protected override void Init()
//		{
//			base.Init();
//			Segments = new List<RopeSegment2>();

//		}

//		// Update is called once per frame
//		protected override void _Update()
//		{
//			UpdateForces();
//			UpdateSegments();
//		}


//		private void ReCalculateSegments(int newCount)
//		{
//			if (newCount == this.Segments.Count)
//			{
//				return;
//			}
//			int segCount = 0;

//			if (newCount >= 0)
//			{
//				segCount = (int)(DefaultSegmentDensity * this.gameObject.transform.localScale.magnitude);
//			}
//			else
//			{
//				segCount = Renderer.positionCount;
//			}

//			if (segCount == this.Segments.Count)
//			{
//				return;
//			}

//			if (segCount == 0)
//			{
//				// TODO
//				throw new NotImplementedException();
//			}


//			Debug.Assert(segCount > 0);
//			var newSegmentLength = this.Length / segCount;

//			if (segCount < Segments.Count)
//			{
//				// TODO
//				throw new NotImplementedException();
//			}
//			else
//			{
//				for (int i = 0; i < Segments.Count; ++i)
//				{
//					Segments[i].Length = newSegmentLength;
//				}

//				int delta = segCount - Segments.Count;
//				RopeSegment2[] newSegments = new RopeSegment2[delta];
//				for (int i = 0; i < delta; ++i)
//				{
//					var seg = newSegments[i] = PrefabHelper.Create<RopeSegment2>();
//					seg.Length = newSegmentLength;
//					seg.Radius = Radius;
//					seg.Density = Density;
//					if (i > 0)
//					{
//						seg.TopJoint.connectedBody = newSegments[i - 1].Body;
//					}
//				}

//				if (Segments.Count > 0)
//				{
//					newSegments[0].BottomJoint.connectedBody = null;
//					newSegments[0].TopJoint.connectedBody = Segments.Last().Body;
//				}

//				newSegments.Last().BottomJoint.connectedBody = this.Object2.Body;
//				Segments.AddRange(newSegments);
//				return;
//			}

			
//			for (int i = 0; i < newCount; ++i)
//			{
//				// TODO
//			}

//			// TODO

//			throw new NotImplementedException();
//		}

//		private void OnObjectSet(IUnityBody obj, bool startObject)
//		{
//			if (Length > 0)
//			{
//				if (SegmentCount == 0)
//				{
//					ReCalculateSegments((int)(DefaultSegmentDensity * Length));
//				}
//				else
//				{
//					ReCalculateSegments(-1);
//				}
//			}
//			else
//			{
//				ReCalculateSegments(0);
//			}
//		}

//		private void UpdateSegments()
//		{

//		}

//		private void UpdateForces()
//		{
//			// TODO




//		}
//	}
//}

