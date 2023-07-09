using System.Collections.Generic;
using UnityEngine;
using System;

public class Strike : MonoBehaviour
{
	public Stats stats; [System.Serializable] public class Stats
	{
  	  public float damage, velocity, range;
	}
	public LayerMask damageLayer;
	public Collider2D hurtbox;
	public int piercing; protected List<Collider2D> pierceds = new List<Collider2D>();
	public delegate void onContact(Health contacted, Vector2 contactPos); public onContact OnContact;


	protected virtual void OnEnable()
	{
		//Stop ignore collider of entity has been pierced
		foreach (Collider2D pierced in pierceds) {Physics2D.IgnoreCollision(hurtbox, pierced, false);} 
		//Reset tracking of pierced and ignored entity
		pierceds = new List<Collider2D>();
	}
}