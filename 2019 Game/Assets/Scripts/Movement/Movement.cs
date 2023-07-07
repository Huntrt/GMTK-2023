using UnityEngine;

public class Movement : MonoBehaviour
{
	public Rigidbody2D rb;
    public float speed;
	[SerializeField] protected Vector2 moveDir;

	void FixedUpdate()
	{
		rb.MovePosition(rb.position + (moveDir.normalized * speed) * Time.fixedDeltaTime);
	}
}