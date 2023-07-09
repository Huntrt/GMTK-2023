using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public int cost;
	public float cooldown;
	public Sprite icon;

	void OnEnable()
	{
		EnemiesManager.i.enemies.Add(transform);
	}

	void OnDisable()
	{
		if(EnemiesManager.i != null) EnemiesManager.i.enemies.Remove(transform);
	}
}