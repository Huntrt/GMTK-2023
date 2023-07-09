using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
	[SerializeField] GameObject slotGUIPrefab;
	[SerializeField] Transform slotGUIGroup;
	public List<SpawnSlot> slots = new List<SpawnSlot>();
	[SerializeField] int currentlySelect;
	[SerializeField] LayerMask spawnLimt;
	[SerializeField] TMPro.TextMeshProUGUI selectText;
	SpawnSlot selected {get => slots[currentlySelect];}

	void Start()
	{
		Selecting(0);
	}

	void Update()
	{
		if(Input.GetKeyDown(Game.Operator.SessionOperator.i.keys.SingleSpawn)) Spawning();
		if(Input.GetKey(Game.Operator.SessionOperator.i.keys.RapidSpawn)) Spawning();
		#region Select with key
		if(Input.GetKeyDown(Game.Operator.SessionOperator.i.keys.Slot1)) Selecting(0);
		if(Input.GetKeyDown(Game.Operator.SessionOperator.i.keys.Slot2)) Selecting(1);
		if(Input.GetKeyDown(Game.Operator.SessionOperator.i.keys.Slot3)) Selecting(2);
		if(Input.GetKeyDown(Game.Operator.SessionOperator.i.keys.Slot4)) Selecting(3);
		if(Input.GetKeyDown(Game.Operator.SessionOperator.i.keys.Slot5)) Selecting(4);
		#endregion
		//Go through all the slot
		for (int s = 0; s < slots.Count; s++)
		{
			//To cool it
			slots[s].Cooling();
		}
	}

	public void Hiring(EnemySpawning enemy)
	{
		//Go through all slot
		for (int s = 0; s < slots.Count; s++)
		{
			//If this slot empty
			if(slots[s].spawning == null)
			{
				//@ Added given enemy to slot and refresh it
				slots[s].spawning = enemy;
				slots[s].RefreshInfo();
				slots[s].RestartCool();
				return;
			}
		}
	}

	public void Replace(EnemySpawning enemy, int replacing)
	{
		//@ Replace the given slot imdex with given enemy
		slots[replacing].spawning = enemy;
		slots[replacing].RefreshInfo();
		slots[replacing].RestartCool();
		//Re-select if currently select the slot been replace
		if(currentlySelect == replacing) Selecting(replacing);
	}

	void Selecting(int index)
	{
		//Stop if choose slot out of range
		if(index >= slots.Count) return;
		//Turn the previous select slot indicator to white
		selected.selectIndicator.color = Color.white;
		//Now select at given index
		currentlySelect = index;
		//Currently select slot indicator to be yellow
		selected.selectIndicator.color = Color.yellow;
		if(selected.spawning != null)
		{
			selectText.text = selected.spawning.trueName;
		}
		else
		{
			selectText.text = "empty";
		}
	}

	void Spawning()
	{
		//Spawn at mouse position
		Vector2 mousePos = MousePosition.i.World();
		//Check if spawning position is on the limit
		if(Physics2D.Raycast(mousePos, Vector2.zero, 0, spawnLimt))
		{
			Popup.i.Pop("Cant spawn here", mousePos);
			return;
		}
		//Allow to spawn when cooldown over
		if(!selected.isCooled) return;
		//Allow to spawn when have enough cash to spend on it
		if(!SpawnManager.i.currency.Spending(selected.spawning.cost)) return;
		//Reset the selected slot cooldown
		selected.RestartCool();
		//Pooling the selected spawning at mouse position
		Pooler.i.Create(selected.spawning.gameObject, mousePos, Quaternion.identity);
	}
}