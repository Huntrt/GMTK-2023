using UnityEngine;

public class Caster_Points : Caster
{
	[SerializeField] GameObject projectile;
    [SerializeField] Transform[] points;

	public override void Casting(bool force = true)
	{
		base.Casting();
		//Go through all the point
		foreach (Transform point in points)
		{
			//Pool projectile at this point position and using point rotation
			GameObject pooled = Pooler.i.Create(projectile, point.position, point.rotation);
			//Overwrite stats of strike pooled
			pooled.GetComponent<Strike>().stats = stats;
		}
		//Cast over
		CastEnded();
	}
}