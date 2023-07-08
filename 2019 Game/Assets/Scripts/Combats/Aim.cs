using UnityEngine;

public class Aim : MonoBehaviour
{
	[SerializeField] Transform[] firepoints;
	[SerializeField] Caster caster;
	[SerializeField] float detect;
    [SerializeField] Transform target; public Transform Target {get => target; set => target = value;}
	public Vector2 dirTotarget {get {return target.position - transform.position;}}
	bool aimingRight = true;

	void Update()
	{
		//Stop if there no target
		if(target == null) return;
		//Make all firepoint aim toward target direction
		foreach (var firepoint in firepoints) firepoint.right = dirTotarget;
		//Begin auto cast when the target get in detection
		caster.autoCast = Vector2.Distance(transform.position, target.position) <= detect;
		//@ Flip the rotation if the target is behind/infront
		if(dirTotarget.x > 0 && !aimingRight)
		{
			aimingRight = true;
			transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, 0, transform.localEulerAngles.z);
		}
		if(dirTotarget.x < 0 && aimingRight)
		{
			aimingRight = false;
			transform.rotation = Quaternion.Euler(transform.localEulerAngles.x, 180, transform.localEulerAngles.z);
		}
	}
}