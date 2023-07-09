using UnityEngine;

public class ProtagState : MonoBehaviour
{
    [SerializeField] enum State
	{
		Idle,// do nothing mostly switch to stomp instantly.
		Melee, //attack close up enemy with crowd control weapon.
		Range, //attack enemy from far away if got got hurt by it.
		Stomping, // when no threat begin stomp he floor.
		Reviving, // 10 second of unable to do anything.
	}
	[SerializeField] State currentState;
	[SerializeField] float meleeRange;
	[SerializeField] float revivingDuration;
	[SerializeField] LayerMask enemyLayer;

	void Update()
	{
		InMeleeRange();
	}

	void InMeleeRange()
	{
		//Scan all enemy in melee range
		RaycastHit2D scan = Physics2D.CircleCast(transform.position, meleeRange, Vector2.zero, 0, enemyLayer);
		//If there enemy in melee range
		if(scan)
		{
			//If this is the initial stateor the target no longer active
			if(currentState != State.Melee || !Protag.i.equip.currentTarget.gameObject.activeInHierarchy)
			{
				//Set current target to be enemy got scan
				Protag.i.equip.currentTarget = scan.collider.gameObject.transform;
			}
			//Change stage
			currentState = State.Melee;
			//Switch to an rapid firing weapon
			Protag.i.equip.SwitchWeaponType(ProtagWeapon.Type.rapid);
		}
	}
}