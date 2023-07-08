using UnityEngine;

public class Protag : MonoBehaviour
{
    #region Set this class to singleton
	static Protag _i; public static Protag i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<Protag>();
			}
			return _i;
		}
	}
	#endregion

	
}