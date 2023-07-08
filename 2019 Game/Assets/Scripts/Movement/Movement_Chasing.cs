using UnityEngine;

public class Movement_Chasing : Movement
{
    protected override void FixedUpdate()
	{
		if(Protag.i == null) return;
		//Set move direction toward the protagonist
		moveDir = Protag.i.transform.position - transform.position;
		//Begin moving direction has setted
		base.FixedUpdate();
	}
}