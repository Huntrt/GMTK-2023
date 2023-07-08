using System.Collections.Generic;
using UnityEngine;
using System;

public class Strike : MonoBehaviour
{
	public LayerMask damageLayer;
	public Collider hurtbox;
    public float damage, velocity, range;
	public int piercing; protected List<Collider> pierceds = new List<Collider>();
	public delegate void onContact(Health contacted, Vector2 contactPos); public onContact OnContact;
}