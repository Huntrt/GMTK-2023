using UnityEngine;

public class ProtagState : MonoBehaviour
{
    [SerializeField] enum State
	{
		Idle,// do nothing mostly switch to stomp instantly.
		Melee, //attack close up enemy with crowd control weapon.
		Annoyed, //attack enemy from far away if got got hurt by it.
		Stomping, // when no threat begin stomp he floor.
		Reviving, // 10 second of unable to do anything.
	}
	[SerializeField] State currentState;
	[SerializeField] float meleeRange;
	[SerializeField] float revivingDuration;
	[SerializeField] LayerMask enemyLayer;
	[SerializeField] Health protagHealth;
	[SerializeField] ProtagEquip equip;

	void OnEnable()
	{
		protagHealth.OnDamage += GotAnnoyed;
	}

	void OnDisable()
	{
		protagHealth.OnDamage -= GotAnnoyed;
	}

	void Update()
	{
		//If there is target but it is inactive
		if(equip.currentTarget != null) if(!equip.currentTarget.gameObject.activeInHierarchy)
		{
			//No longer have target
			equip.currentTarget = null;
		}
		InMeleeRange();
	}

	void InMeleeRange()
	{
		//Scan all enemy in melee range
		RaycastHit2D scan = Physics2D.CircleCast(transform.position, meleeRange, Vector2.zero, 0, enemyLayer);
		//If there enemy in melee range
		if(scan)
		{
			//If initial melee or have no target
			if(currentState != State.Melee || equip.currentTarget == null)
			{
				//Set current target to be enemy got scan
				equip.currentTarget = scan.collider.gameObject.transform;
			}
			//Change stage
			currentState = State.Melee;
			//Switch to an rapid firing weapon
			equip.SwitchWeaponType(ProtagWeapon.Type.rapid);
		}
	}

	void GotAnnoyed(float taken)
	{
		//Dont get annoy if currently melee
		if(currentState == State.Melee) return;
		if(taken <= 0) return;
		//Protag got annoyed
		currentState = State.Annoyed;
		//Switch to an sniping firing weapon
		equip.SwitchWeaponType(ProtagWeapon.Type.sniping);
		//Get the list of enemy
		var enemies = EnemiesManager.i.enemies;
		//Randomly an enemy in the list
		equip.currentTarget = enemies[Random.Range(0, enemies.Count)].transform;
	}
}