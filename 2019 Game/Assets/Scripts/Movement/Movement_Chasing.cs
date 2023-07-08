using UnityEngine;

public class Movement_Chasing : Movement
{
    void Update()
	{
		//if(Protag.i == null) return;
		moveDir = Protag.i.transform.position - transform.position;
	}
}