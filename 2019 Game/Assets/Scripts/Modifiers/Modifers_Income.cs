using UnityEngine;

public class Modifers_Income : Modifiers
{
	[SerializeField] float percent;

	public override void Modify(bool oppose)
	{
		SpawnManager.i.currency.Income += SpawnManager.i.currency.Income/percent;
		base.Modify(oppose);
	}
}