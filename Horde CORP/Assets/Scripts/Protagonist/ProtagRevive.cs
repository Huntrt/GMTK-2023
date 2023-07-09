using UnityEngine;

public class ProtagRevive : MonoBehaviour
{
    public float duration; float durationTimer;
	[SerializeField] TMPro.TextMeshProUGUI reviveText;
	bool reviving;

	void Update()
	{
		durationTimer += Time.deltaTime;
		reviveText.text = "Revive in <b><color=#3399ff>"+ (int)(duration-durationTimer) +"</color></b> seconds";
		if(durationTimer >= duration)
		{
			Protag.i.gameObject.SetActive(true);
			durationTimer -= durationTimer;
			gameObject.SetActive(false);
		}
	}

	public void BeginRevive(Vector2 pos)
	{
		transform.position = pos;
		gameObject.SetActive(true);
	}
}