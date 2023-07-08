using UnityEngine;
using TMPro;

public class SpawnControl : MonoBehaviour
{
	#region Set this class to singleton
	static SpawnControl _i; public static SpawnControl i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<SpawnControl>();
			}
			return _i;
		}
	}
	#endregion
}