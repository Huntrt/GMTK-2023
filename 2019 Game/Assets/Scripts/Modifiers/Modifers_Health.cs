using UnityEngine;

public class Modifers_Health : Modifiers
{
	public override void Modify(bool oppose)
	{
		Protag.i.health.MaxHealth += 10;
		base.Modify(oppose);
	}
}