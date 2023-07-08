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

	void Die() {health.Die();}
	void Cast() {caster.Casting(true);}

	void OnDisable()
	{
		caster.onCast -= Die;
		health.OnDeath -= Cast;
	}
}