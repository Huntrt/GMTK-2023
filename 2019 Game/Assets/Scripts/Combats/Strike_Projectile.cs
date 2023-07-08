using UnityEngine;

public class Strike_Projectile : Strike
{
    float travelled;
	Vector3 prePos;
	[SerializeField] Rigidbody2D rb;
	[SerializeField] TrailRenderer trail;

	void OnEnable()
	{
		//Reseting previous position
		prePos = rb.position;
		//Reseting travelled distance
		travelled = 0;
		//If there is an trail
		if(trail != null)
		{
			//Set trail size base on the height of strike
			trail.widthMultiplier = transform.localScale.y;
			//Reseting trail position
			trail.transform.position = transform.position;
			//Added trail back to be this projectile child
			trail.transform.SetParent(transform);
			//Clear trail
			trail.Clear();
		}
	}

	void FixedUpdate()
	{
		//Moving right with set velocity
		rb.velocity = transform.right * velocity;
		//Get how long has travelled this frame
		travelled += Vector3.Distance(prePos, rb.position);
		//If has reached max range
		if(travelled >= range)
		{
			//End the projectile
			End();
		}
		//Update previous position
		prePos = rb.position;
	}

	void OnCollisionEnter2D(Collision2D other)
	{
		//Does hit anything entity?
		Health hitted = null;
		//If collision with an object that on layer to damage
		if(damageLayer == (damageLayer | (1 << other.collider.gameObject.layer)))
		{
			//Get the health of entity got hitted
			hitted = other.collider.GetComponent<Health>();
			//Dealing damage to hitted entity
			hitted.Damaging(damage);
			//Static ignore the collision object
			Physics2D.IgnoreCollision(hurtbox, other.collider);
			//Has pierced this entity
			pierceds.Add(other.collider);
			//End the static when out of pierced
			if(pierceds.Count >= piercing) End();
		}
		//End the projectile when collide with non damaging layer
		else End();
		//Call on hit event along with any contacted entity hitted and contact position
		OnContact?.Invoke(hitted, other.collider.bounds.ClosestPoint(transform.position));
	}

	void End()
	{
		//Deactive the projectile
		gameObject.SetActive(false);
	}
}