using UnityEngine;
using System;

public class Caster : MonoBehaviour
{
	[SerializeField] protected Strike.Stats stats;
	public float firerate; float firerateTimer;
	[HideInInspector]public bool autoCast, isReady;
	public event Action onCast;

	void OnEnable()
	{
		//Reset firerate when it enable
		isReady = false; firerateTimer -= firerateTimer;
	}

	void Update()
	{
		//When has reached the timer for firerate
		if(firerateTimer >= 1/firerate)
		{
			//Is now ready
			isReady = true;
			//Instantly cast if autoing
			if(autoCast) {Casting();}
		}
		//Timing firerate when not ready
		if(!isReady) firerateTimer += Time.deltaTime;
	}

    public virtual void Casting(bool force = false)
	{
		//Don't cast if not ready while not need to force it
		if(!isReady && !force) return;
	}

	protected void CastEnded()
	{
		onCast?.Invoke();
		isReady = false;
		firerateTimer -= firerateTimer;
	}
}
