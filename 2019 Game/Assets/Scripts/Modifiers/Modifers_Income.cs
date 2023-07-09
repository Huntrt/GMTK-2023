using UnityEngine;

public class Modifers_Income : Modifiers
{
	[SerializeField] float percent;

	public override void Modify(bool oppose)
	{
		SpawnManager.i.currency.Income += (percent/100) * SpawnManager.i.currency.Income;
		base.Modify(oppose);
	}
}