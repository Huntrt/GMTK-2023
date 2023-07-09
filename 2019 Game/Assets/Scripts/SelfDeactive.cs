using UnityEngine;

public class SelfDeactive : MonoBehaviour
{
	public void Deactive()
	{
		gameObject.SetActive(false);
	}
}