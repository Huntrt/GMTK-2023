using Game.Settings;
using UnityEngine;

namespace Game.Operator
{
public class SessionOperator : MonoBehaviour
{
	public AudioManager audios;
	public KeyManager keys;
	public DisplayManager displays;
	public static SessionOperator i;

	void Awake()
	{
		//Only set to "don't destroy on load" if haven't then destroy any duplicate
		if(i == null) {i = this; DontDestroyOnLoad(this);} else {Destroy(gameObject);}
	}
}
}