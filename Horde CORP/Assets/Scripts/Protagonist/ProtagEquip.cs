using System.Collections.Generic;
using UnityEngine;

public class ProtagEquip : MonoBehaviour
{
	public List<ProtagWeapon> unlockedWeapon = new List<ProtagWeapon>();
	public List<ProtagWeapon> lockedWeapon = new List<ProtagWeapon>();
	[SerializeField] SpriteRenderer protagSprite;
	public int currentIndex;
	public ProtagWeapon currentWeapon;
	public Transform currentTarget;

	void Start()
	{
		//Equip the first slot upon start
		Equiping(0);
	}

	public int[] FilterWeapon(ProtagWeapon.Type wanted)
	{
		List<int> tempList = new List<int>();
		//Go through all the unlocked weapon
		for (int w = 0; w < unlockedWeapon.Count; w++)
		{
			ProtagWeapon weapon = unlockedWeapon[w];
			//If this weapon type match the wanted type than save it index
			if(weapon.type == wanted) tempList.Add(w);
		}
		//Return the filterd index
		return tempList.ToArray();
	}

	public void SwitchWeaponType(ProtagWeapon.Type wanted)
	{
		//Filter the index of wanted weapon type
		int[] filtered = FilterWeapon(wanted);
		//Equip an random weapon in index has filterd
		Equiping(filtered[Random.Range(0, filtered.Length)]);
	}

	public void Equiping(int index)
	{
		//If the previous weapon exist and it is not equiping the same weapon
		if(currentWeapon != null && currentWeapon != unlockedWeapon[index])
		{
			//Deactive the previous weapon
			currentWeapon.caster.gameObject.SetActive(false);
		}
		//Set current index and weapon base on given
		currentIndex = index; currentWeapon = unlockedWeapon[index];
		//Update the protag sprite to be
		protagSprite.sprite = currentWeapon.sprite;
		//Current weapon now aim toward current target
		currentWeapon.caster.GetComponent<Aim>().Target = currentTarget;
		//Active the current weapon
		currentWeapon.caster.gameObject.SetActive(true);

	}
}