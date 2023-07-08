using UnityEngine;

public class Caster_Points : Caster
{
	[SerializeField] GameObject projectile;
    [SerializeField] Transform[] points;

	public override void Cast()
	{
		base.Cast();
		//Go through all the point
		foreach (Transform point in points)
		{
			//Pool projectile at this point position and using point rotation
			Pooler.i.Create(projectile, point.position, point.rotation);
		}
		//No longer ready
		isReady = false;
	}
}