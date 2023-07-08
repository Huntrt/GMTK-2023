using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float maxHealth, curHealth;
	public float MaxHealth {get => maxHealth; set{maxHealth = value; Damaging(0);}}
	public float CurHealth {get => curHealth;}
	public delegate void onDamage(float taken); public onDamage OnDamage;
	public delegate void onDeath(); public onDeath OnDeath;

	void OnEnable()
	{
		//Use max health as current health upon spawn
		curHealth = maxHealth;
	}

	public void Damaging(float taken)
	{
		//Current health got decrease by damage taken
		curHealth -= taken;
		//Prevent health from going out of bounds
		curHealth = Mathf.Clamp(curHealth,0,maxHealth);
		//Send taken damage event
		OnDamage?.Invoke(taken);
		//Got hurt when take positive damage
		if(taken > 0) Hurted(taken);
		//Get heal when take negative damage
		if(taken < 0) Healed(taken);
		//Die when run out of health and no longer consider hurt
		if(curHealth <= 0) {Die(); return;}
	}

	void Hurted(float damaged)
	{
		//... Do something after hurted
	}

	void Healed(float recovered)
	{
		//... Do something after got heal
	}

	public void Die()
	{
		//Send death event
		OnDeath?.Invoke();
		//Deactive the object upon death
		gameObject.SetActive(false);
	}
}
