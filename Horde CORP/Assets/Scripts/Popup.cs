using UnityEngine;

public class Popup : MonoBehaviour
{
	#region Set this class to singleton
	static Popup _i; public static Popup i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<Popup>();
			}
			return _i;
		}
	}
	#endregion

	[SerializeField] GameObject popupPrefab;

    public void Pop(string message, Vector2 pos)
	{
		GameObject poped = Pooler.i.Create(popupPrefab, pos, Quaternion.identity);
		popupPrefab.transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text = message;
	}
}