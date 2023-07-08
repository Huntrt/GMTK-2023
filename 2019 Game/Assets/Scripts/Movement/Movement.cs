using UnityEngine;

public class Movement : MonoBehaviour
{
	public Rigidbody2D rb;
    public float speed;
	[SerializeField] protected Vector2 moveDir;

	void Reset()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + (moveDir.normalized * speed) * Time.fixedDeltaTime);
	}
}