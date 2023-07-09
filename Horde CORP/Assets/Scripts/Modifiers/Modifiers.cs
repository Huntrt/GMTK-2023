using UnityEngine;

public class Modifiers : MonoBehaviour
{
	[SerializeField] protected Modifiers oppose;

    public virtual void Modify(bool isOpposed)
	{
		print("Modify" + gameObject.name);
		//Oppose it modify if this is the caller
		if(!isOpposed)
		{
			print("Oppse" + oppose.gameObject.name);
			oppose.Modify(true);
			ShowModifiers.i.FinishModify();
		}
	}
}