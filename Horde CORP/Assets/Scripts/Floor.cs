using UnityEngine;

public class Floor : MonoBehaviour
{
    #region Set this class to singleton
	static Floor _i; public static Floor i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<Floor>();
			}
			return _i;
		}
	}
	#endregion

	[SerializeField] int health;
	[SerializeField] SpriteRenderer spriteRenderer;
	[SerializeField] Sprite[] floorBreaks;
	[SerializeField] GameObject gameOverPanel;

	public void Stomped()
	{
		health--;
		if(health > 0) spriteRenderer.sprite = floorBreaks[health-1];
		if(health <= 0)
		{
			gameOverPanel.SetActive(true);
		}
	}
}