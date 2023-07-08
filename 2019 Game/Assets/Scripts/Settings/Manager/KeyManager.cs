using System.Collections;
using System.Reflection;
using UnityEngine;
using System;

public class KeyManager : MonoBehaviour
{
	//---------------------------------------------------------------------
	/// Add the action you want below (enum and keycode NEED TO BE MATCH)
	public enum Binds {Up, Down, Left, Right}
	public KeyCode Up, Down, Left, Right;
	//---------------------------------------------------------------------
	[Tooltip("Will replace key label text when binding it")]
	[SerializeField] string waitingMessage;
	public bool areBinding;
	[SerializeField] KeyBinder currentBinder;
	public Action RefreshAllBinderKeyLabel;
	
	#region Set this class to singleton
	static KeyManager _i; public static KeyManager i 
	{
		get
		{
			//If this class is not static
			if(_i == null)
			{
				//Find object with this class to make it static
				_i = GameObject.FindObjectOfType<KeyManager>();
			}
			return _i;
		}
	}
	#endregion
	
	public void BeginBind(KeyBinder binder)
	{
		//Stop if currently binding
		if(areBinding) return;
		//Get the binder given
		currentBinder = binder;
		//Begining key binding
		areBinding = true; StartCoroutine("Binding");
	}

	public FieldInfo ReflectActionKeycode(string actionName)
	{
		//? Relied on reflection to find keycode variable of given action
		return this.GetType().GetField(actionName);
	}
	
	IEnumerator Binding()
	{
		//If currently binding
		while(areBinding)
		{
			//Display the binder key label to waiting message
			currentBinder.DisplayKeyLabel(waitingMessage);
            //! Go though ALL the key to check if there is currently any input (PERFORMANCE HEAVY)
			foreach(KeyCode pressedKey in System.Enum.GetValues(typeof(KeyCode)))
			{
				//If an key has been press
				if(Input.GetKey(pressedKey))
				{
					//Set keycode of action currently binded to key got press
					ReflectActionKeycode(currentBinder.BindedAction).SetValue(this, pressedKey);
					//Refresh current label's key label
					currentBinder.RefreshKeyLabel();
					//? Delay to prevent re-bind after binding left mouse
					if(pressedKey == KeyCode.Mouse0) yield return new WaitForSeconds(0.55f);
					//Stop assigning
					areBinding = false;
				}
			}
			//Clear the current binder if no longer binding
			if(!areBinding) currentBinder = null;
			yield return null;
		}
	}
}