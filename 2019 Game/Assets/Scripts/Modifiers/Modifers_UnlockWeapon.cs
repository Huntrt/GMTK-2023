using UnityEngine;

public class Modifers_UnlockWeapon : Modifiers
{
	[SerializeField] TMPro.TextMeshProUGUI textDisplay;
	[SerializeField] int unlockIndex;

	void OnEnable()
	{
		var locked = Protag.i.equip.lockedWeapon;
		if(locked.Count > 0)
		{
			unlockIndex = Random.Range(0, locked.Count);
			textDisplay.text = "Protagonist will get <b><color=yellow>" + locked[unlockIndex].gameObject.name + "</b></color> weapon";
		}
		else
		{
			textDisplay.text = "There no more weapon left to unlock";
		}
	}

	public override void Modify(bool oppose)
	{
		var equip = Protag.i.equip;
		equip.unlockedWeapon.Add(equip.lockedWeapon[unlockIndex]);
		base.Modify(oppose);
	}
}