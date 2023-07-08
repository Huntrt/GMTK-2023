using UnityEngine;

public class Aim : MonoBehaviour
{
	[SerializeField] Transform[] firepoints;
	[SerializeField] Caster caster;
	[SerializeField] float detect;
    [SerializeField] Transform target; public Transform Target {get => target; set => target = value;}

	void Update()
	{
		//Stop if there no target
		if(target == null) return;
		//Make all firepoint aim toward target
		foreach (var firepoint in firepoints) firepoint.right = target.position - transform.position;
		//Begin auto cast when the target get in detection
		caster.autoCast = Vector2.Distance(transform.position, target.position) <= detect;
	}
}