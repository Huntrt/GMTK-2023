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

	protected virtual void FixedUpdate()
	{
		//Moving to direction has set with speed
		rb.MovePosition(rb.position + (moveDir.normalized * speed) * Time.fixedDeltaTime);
	}
}