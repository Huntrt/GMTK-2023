using UnityEngine;

public class ShowModifiers : MonoBehaviour
{
	#region Set this class to singleton
	static ShowModifiers _i; public static ShowModifiers i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<ShowModifiers>();
			}
			return _i;
		}
	}
	#endregion

	[SerializeField] GameObject panel;
	public GameObject[] options;

	public void Show()
	{
		panel.SetActive(true);
		foreach (GameObject option in options) {option.SetActive(true);}
		Time.timeScale = 0;
	}

	public void FinishModify()
	{
		panel.SetActive(false);
		foreach (GameObject option in options) {option.SetActive(false);}
		Time.timeScale = 1;
	}
}