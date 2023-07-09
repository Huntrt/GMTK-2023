using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    #region Set this class to singleton
	static SpawnManager _i; public static SpawnManager i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<SpawnManager>();
			}
			return _i;
		}
	}
	#endregion

	public SpawnCurrency currency;
	public SpawnControl control;
}