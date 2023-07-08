﻿using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
	[SerializeField] GameObject slotGUIPrefab;
	[SerializeField] Transform slotGUIGroup;
	[SerializeField] List<SpawnSlot> slots = new List<SpawnSlot>();
	[SerializeField] int currentlySelect;
	SpawnSlot selected {get => slots[currentlySelect];}

	void Start()
	{
		Selecting(0);
	}

	void Update()
	{
		//todo: add keybind for 'single spawn'
		if(Input.GetKeyDown(KeyCode.Mouse0)) Spawning();
		//todo: add keybind for 'rapid spawn'
		if(Input.GetKey(KeyCode.Mouse1)) Spawning();
		#region Select with key
		//todo: replace with keybinds (clean up?)
		if(Input.GetKeyDown(KeyCode.Alpha1)) Selecting(0);
		if(Input.GetKeyDown(KeyCode.Alpha2)) Selecting(1);
		if(Input.GetKeyDown(KeyCode.Alpha3)) Selecting(2);
		if(Input.GetKeyDown(KeyCode.Alpha4)) Selecting(3);
		if(Input.GetKeyDown(KeyCode.Alpha5)) Selecting(4);
		if(Input.GetKeyDown(KeyCode.Alpha6)) Selecting(5);
		if(Input.GetKeyDown(KeyCode.Alpha7)) Selecting(6);
		if(Input.GetKeyDown(KeyCode.Alpha8)) Selecting(7);
		if(Input.GetKeyDown(KeyCode.Alpha9)) Selecting(8);
		if(Input.GetKeyDown(KeyCode.Alpha0)) Selecting(9);
		#endregion
		//Go through all the slot
		for (int s = 0; s < slots.Count; s++)
		{
			//To cool it
			slots[s].Cooling();
		}
	}

	void NewSlot(EnemySpawning enemy)
	{
		//temp: max amount of slot allowing
		if(slots.Count >= 10) {Debug.LogError("Maxxed slot amount"); return;}
		//Create an new slot 
		GameObject newSlotGUI = Instantiate(slotGUIPrefab);
		//Group the new slot
		newSlotGUI.transform.SetParent(slotGUIGroup);
		//Get the spawn slot component of slot gui just create
		SpawnSlot newSlotSpawn = newSlotGUI.GetComponent<SpawnSlot>();
		//Added new slot into list
		slots.Add(newSlotSpawn);
		//Set the given enemy is the enemy this slot will have
		newSlotSpawn.spawning = enemy;
		newSlotSpawn.RefreshInfo();
	}

	void Selecting(int index)
	{
		//Turn the previous select slot indicator to white
		selected.selectIndicator.color = Color.white;
		//Now select at given index
		currentlySelect = index;
		//Currently select slot indicator to be yellow
		selected.selectIndicator.color = Color.yellow;
	}

	void Spawning()
	{
		//Allow to spawn when cooldown over
		if(!selected.isCooled) return;
		//Allow to spawn when have enough cash to spend on it
		if(!SpawnManager.i.currency.Spending(selected.spawning.cost)) return;
		//Reset the selected slot cooldown
		selected.RestartCool();
		//Pooling the selected spawning at mouse position
		Pooler.i.Create(selected.spawning.gameObject, MousePosition.i.World(), Quaternion.identity);
	}
}