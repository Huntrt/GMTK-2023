using System.Collections.Generic;
using UnityEngine;
using System;

public class Strike : MonoBehaviour
{
	public LayerMask damageLayer;
	public Collider2D hurtbox;
    public float damage, velocity, range;
	public int piercing; protected List<Collider2D> pierceds = new List<Collider2D>();
	public delegate void onContact(Health contacted, Vector2 contactPos); public onContact OnContact;
}