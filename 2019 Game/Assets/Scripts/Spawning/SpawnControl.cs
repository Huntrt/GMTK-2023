using System.Collections.Generic;
using UnityEngine;

public class SpawnControl : MonoBehaviour
{
	[SerializeField] List<SpawnSlot> slots = new List<SpawnSlot>();
	[SerializeField] int currentlySelect;
	SpawnSlot selected {get => slots[currentlySelect];}

	void Start()
	{
		Selecting(0);
	}

	void Update()
	{
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
	void Selecting(int index)
	{
		//Turn the previous select slot indicator to white
		selected.selectIndicator.color = Color.white;
		//Now select at given index
		currentlySelect = index;
		//Currently select slot indicator to be yellow
		selected.selectIndicator.color = Color.yellow;
	}
	}
}