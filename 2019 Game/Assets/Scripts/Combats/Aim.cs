using UnityEngine;

public class Aim : MonoBehaviour
{
	[SerializeField] Transform body;
	[SerializeField] Transform[] firepoints;
	[SerializeField] Caster caster;
	[SerializeField] float detect;
    [SerializeField] Transform target; public Transform Target {get => target; set => target = value;}
	public Vector2 dirTotarget {get {return target.position - transform.position;}}
	bool aimingRight = true;

	void Awake()
	{
		//Auto get body if havent
		if(body == null) body = transform;
	}

	void Update()
	{
		//Stop if there no target
		if(target == null) {caster.autoCast = false; return;}
		//Stop if there target no longer active
		if(!target.gameObject.activeInHierarchy) {caster.autoCast = false; return;}
		//Make all firepoint aim toward target direction
		foreach (var firepoint in firepoints) firepoint.right = dirTotarget;
		//Begin auto cast when the target get in detection
		caster.autoCast = Vector2.Distance(body.position, target.position) <= detect;
		//@ Flip the rotation if the target is behind/infront
		if(dirTotarget.x > 0 && !aimingRight)
		{
			aimingRight = true;
			body.rotation = Quaternion.Euler(body.localEulerAngles.x, 0, body.localEulerAngles.z);
		}
		if(dirTotarget.x < 0 && aimingRight)
		{
			aimingRight = false;
			body.rotation = Quaternion.Euler(body.localEulerAngles.x, 180, body.localEulerAngles.z);
		}
	}
}