using UnityEngine;
using System;

public class Caster : MonoBehaviour
{
	public bool autoCast, isReady;
	public float firerate; float firerateTimer;
	public event Action onCast;

	void Update()
	{
		//When has reached the timer for firerate
		if(firerateTimer >= 1/firerate)
		{
			//Is now ready
			isReady = true;
			//Instantly cast if autoing
			if(autoCast) Casting();
		}
		//Timing firerate when not ready
		if(!isReady) firerateTimer += Time.deltaTime;
	}

    public virtual void Casting()
	{
		//Don't cast if not ready
		if(!isReady) return;
	}

	protected void CastEnded()
	{
		onCast?.Invoke();
		isReady = false;
		firerateTimer -= firerateTimer;
	}
}
