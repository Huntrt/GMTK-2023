using UnityEngine;

public class MousePosition : MonoBehaviour
{
    #region Set this class to singleton
	static MousePosition _i; public static MousePosition i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<MousePosition>();
			}
			return _i;
		}
	}
	#endregion

	Camera cam;

	void Awake()
	{
		cam = Camera.main;
	}

	public Vector2 World()
	{
		return cam.ScreenToWorldPoint(Input.mousePosition);
	}

	public Vector2 Screen()
	{
		return Input.mousePosition;
	}
}