using UnityEngine.UI;
using UnityEngine;

public class ProtagState : MonoBehaviour
{
    [SerializeField] enum State
	{
		None,
		Melee, //attack close up enemy with crowd control weapon.
		Annoyed, //attack enemy from far away if got got hurt by it.
		Stomping, // when no threat begin stomp he floor.
	}
	[SerializeField] State currentState;
	[SerializeField] ProtagEquip equip;
	[SerializeField] float meleeRange;
	[SerializeField] float stompDuration; float stompDurationTimer;
	[SerializeField] GameObject stompGUI;
	[SerializeField] Image stompBar;
	[SerializeField] TMPro.TextMeshProUGUI stompText;
	[SerializeField] float revivingDuration;
	[SerializeField] LayerMask enemyLayer;

	void OnEnable()
	{
		Protag.i.health.OnDamage += GotAnnoyed;
	}

	void OnDisable()
	{
		Protag.i.health.OnDamage -= GotAnnoyed;
	}

	void SetTarget(Transform target) 
	{
		//Set current target to be given
		equip.currentTarget = target;
		//If there is no target
		if(target == null)
		{
			//Current state are now stomping
			currentState = State.Stomping;
		}
	}

	void Update()
	{
		//If there is target but it is inactive
		if(equip.currentTarget != null) if(!equip.currentTarget.gameObject.activeInHierarchy)
		{
			//No longer have target
			SetTarget(null);
		}
		InMeleeRange();
		Stomping();
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
				SetTarget(scan.collider.gameObject.transform);
				//Switch to an rapid firing weapon
				equip.SwitchWeaponType(ProtagWeapon.Type.rapid);
			}
			//Enter melee stage
			currentState = State.Melee;
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
		//Randomly target an enemy in the list
		if(enemies.Count > 0) SetTarget(enemies[Random.Range(0, enemies.Count)].transform);
	}

	void Stomping()
	{
		if(currentState != State.Stomping) return;
		//@ Basic timer for stomping
		stompDurationTimer += Time.deltaTime;
		if(stompDurationTimer >= stompDuration)
		{
			//Stomp the floor
			Floor.i.Stomped();
			stompDurationTimer -= stompDurationTimer;
		}
		//Display stomp progress
		stompBar.fillAmount = stompDurationTimer/stompDuration;
		//Show stomp text in format: 84%
		stompText.text = (int)((stompDurationTimer/stompDuration) * 100) + "%";
	}
}