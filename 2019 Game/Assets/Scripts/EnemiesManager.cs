using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    #region Set this class to singleton
	static EnemiesManager _i; public static EnemiesManager i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<EnemiesManager>();
			}
			return _i;
		}
	}
	#endregion

	public List<Transform> enemies = new List<Transform>();
}