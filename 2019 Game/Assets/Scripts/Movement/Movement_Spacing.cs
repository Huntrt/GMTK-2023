using UnityEngine;

public class Movement_Spacing : Movement
{
    public float safeDistance, dangerDistance;

	protected override void FixedUpdate()
	{
		if(Protag.i == null) return;
		//Moving toward protagonist position
		moveDir = Protag.i.transform.position - transform.position;
		//Get the distance to protagonist
		float distToTarget = Vector2.Distance(transform.position, Protag.i.transform.position);
		//Running backward if enter danger zone
		if(distToTarget < dangerDistance) moveDir = -moveDir;
		//Stop moving if in the safe zone
		else if(distToTarget < safeDistance) moveDir = Vector2.zero;
		//Begin basic movement
		base.FixedUpdate();
	}
}