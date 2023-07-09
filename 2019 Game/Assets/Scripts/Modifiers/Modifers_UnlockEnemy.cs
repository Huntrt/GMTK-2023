using UnityEngine;

public class Modifers_UnlockEnemy : Modifiers
{
	[SerializeField] TMPro.TextMeshProUGUI textDisplay;
	[SerializeField] EnemySpawning hiring;
	int replace;
	bool isThereEmptySlot;

	void OnEnable()
	{
		var slots = SpawnManager.i.control.slots;
		//Check if atleast on slot is empty
		isThereEmptySlot = false;
		for (int s = 0; s < slots.Count; s++)
		{
			if(slots[s].spawning == null)
			{
				isThereEmptySlot = true;
				break;
			}
		}
		RandomHiring();
		if(isThereEmptySlot)
		{
			textDisplay.text = "Hire <b><color=yellow>" + hiring.trueName + "</color></b> to your deck";
		}
		else
		{
			textDisplay.text = "Replace <b><color=#yellow>" + slots[replace].spawning.trueName + "</color></b> with <b><color=#yellow>" + hiring.trueName + "</color></b> ";
		}
	}

	void RandomHiring()
	{
		var unlockable = EnemiesManager.i.unlockable;
		//Randomly choose which enemy this modifier will hire
		hiring = unlockable[Random.Range(0, unlockable.Count)];
		//Choosed an random slot that will be replace
		if(!isThereEmptySlot) replace = Random.Range(0, SpawnManager.i.control.slots.Count);
	}

	public override void Modify(bool oppose)
	{
		if(isThereEmptySlot)
		{
			SpawnManager.i.control.Hiring(hiring);
		}
		else
		{
			SpawnManager.i.control.Replace(hiring, replace);
		}
		base.Modify(oppose);
	}
}