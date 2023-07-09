using UnityEngine;

public class Formation : MonoBehaviour
{
    [System.Serializable] class Form
	{
		public Transform point;
		public GameObject enemy;
	}
	[SerializeField] Form[] forms;

	void Start()
	{
		foreach (Form form in forms)
		{
			Pooler.i.Create(form.enemy, form.point.position, Quaternion.identity);
		}
		Destroy(gameObject);
	}
}