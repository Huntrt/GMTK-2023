using System.Collections.Generic;
using UnityEngine;
using System;

public class Pooler : MonoBehaviour
{
	#region Set this class to singleton
	public static Pooler i 
	{
		//Create an instance of the pooler once if it haven't exist
		get {if(_i == null) {_i = GameObject.FindObjectOfType<Pooler>();} return _i; }
	}
	static Pooler _i;
	#endregion
	
	[Serializable] public class Pool 
	{
		public string key; 
		public List<GameObject> objs;

		public Pool(string key)
		{
			this.key = key;
			this.objs = new List<GameObject>();
		}
	}
	[SerializeField] List<Pool> pools = new List<Pool>(); 

	//Create the object needed with wanted position, rotation, does it auto active upon create? and do it need to has parent?
	public GameObject Create(GameObject need, Vector3 position, Quaternion rotation, bool autoActive = true, Transform parent = null)
	{
		//Convert the name of needed object to key
		string neededKey = need.name + "(Clone)";
		
		//Create an new pool for the needed key if it haven't exist
		if(FindPool(neededKey) == null) pools.Add(new Pool(neededKey));

		//Get the pool to be use of needed key
		List<GameObject> use = FindPool(neededKey).objs;

		//If the use pool has object then go through all of it object
		if(use.Count > 0) {for (int o = 0; o < use.Count; o++)
		{
			//Get the current object will use
			GameObject obj = use[o];
			//Remove any using object that is null
			if(obj == null) {use.RemoveAt(o); continue;}
			//If this object is unactive 
			if(!obj.activeInHierarchy)
			{
				//Move this object to last since it will be use
				use.Add(obj); use.RemoveAt(o);
				//Using the last object just move
				obj = use[use.Count-1];
				//Set it position
				obj.transform.position = position;
				//Set it rotation
				obj.transform.rotation = rotation;
				//Set it to given parent
				obj.transform.SetParent(parent);
				//Active it if need to
				obj.SetActive(autoActive);
				//Return it and no need to create new
				return obj;
			}
		}}

		///If there is no unactive object left in pool
		//Create an new needed object at given position and rotation
		GameObject newObj = Instantiate(need, position, rotation);
		//Set the new object to given parent
		newObj.transform.SetParent(parent);
		//Add new object into the use list
		use.Add(newObj);
		//Active the new object if need to
		newObj.SetActive(autoActive);
		//Return the new object
		return newObj;
	}

	//Return the pool of given key
	public Pool FindPool(string poolKey) 
	{
		//Go through all the pool
		for (int p = 0; p < pools.Count; p++)
		{
			//Return the pool that it key match the given key
			if(pools[p].key == poolKey) return pools[p];
		}
		//Return nothing if no key match
		return null;
	}
	
	public void DestroyPool(string poolKey)
	{
		Pool finded = FindPool(poolKey);
		if(finded != null)
		{
			//Destroy all object of the finded pool
			for (int o = 0; o < finded.objs.Count; o++) Destroy(finded.objs[o]);
			//Clear the object list
			finded.objs.Clear();
			//Remove finded pool from pool
			pools.Remove(finded);
		}
		else
		{
			//Send an warning
			Debug.LogWarning("There no pool for key " + poolKey);
		}
	}
}