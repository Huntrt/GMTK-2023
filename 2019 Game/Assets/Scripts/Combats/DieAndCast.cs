using UnityEngine;

public class DieAndCast : MonoBehaviour
{
    [SerializeField] Caster caster;
	[SerializeField] Health health;
	[SerializeField] bool castUponDie, dieUponCast;

	void OnEnable()
	{
		caster.onCast += Die;
		health.OnDeath += Cast;
	}

	void Die() {StopListen(); health.Die();}
	void Cast() {StopListen(); caster.Casting(true);}

	void OnDisable() {StopListen();}

	void StopListen()
	{
		caster.onCast -= Die;
		health.OnDeath -= Cast;
	}
}