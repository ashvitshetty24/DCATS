using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DCATS.Assets
{
	public static class PrefabHelper
	{
		private static Dictionary<string, GameObject> LoadedPrefabs = new Dictionary<string, GameObject>();

		public static GameObject GetPrefab(string name)
		{
			GameObject prefab;
			if (LoadedPrefabs.TryGetValue(name, out prefab))
			{
				if (prefab != null)
				{
					return prefab;
				}
			}

			prefab = Resources.Load<GameObject>("Prefabs/" + name);
			if (prefab != null)
			{
				LoadedPrefabs.Add(name, prefab);
			}

			return prefab;
		}

		public static T Create<T>() where T : Object
		{
			var prefab = GetPrefab(typeof(T).Name);
			if (prefab != null)
			{
				var obj = GameObject.Instantiate(prefab);
				if (obj != null)
				{
					return obj.GetComponent<T>();
				}
			}

			return null;
		}

		public static T Create<T>(Vector3 position) where T : Object
		{
			var prefab = GetPrefab(typeof(T).Name);
			if (prefab != null)
			{
				var obj = GameObject.Instantiate(prefab, position, Quaternion.identity);
				Debug.Assert(obj.transform.parent == null);
				if (obj != null)
				{
					return obj.GetComponent<T>();
				}
			}

			return null;
		}
	}
}
