using UnityEngine;

public class ProtagState : MonoBehaviour
{
    [SerializeField] enum State
	{
		Idle,// do nothing mostly switch to stomp instantly.
		CrowdControl, // use fast firirng weapon when weak enemy come near.
		Vengeance, //attack enemy that target it from far away.
		Stomping, // when no threat begin stomp he floor.
		Reviving, // 10 second of unable to do anything.
	}
	[SerializeField] State currentState;

	void Update()
	{
		
	}
}