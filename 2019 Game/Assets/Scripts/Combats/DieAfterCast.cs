using UnityEngine;

public class DieAfterCast : MonoBehaviour
{
    [SerializeField] Caster caster;
	[SerializeField] Health health;

	void OnEnable()
	{
		caster.onCast += CastDie;
	}

	void CastDie()
	{
		health.Die();
	}

	void OnDisable()
	{
		caster.onCast -= CastDie;
	}
}