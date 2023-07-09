using UnityEngine;

public class EnemySpawning : MonoBehaviour
{
    public int cost;
	public float cooldown;
	public float exp;
	public Sprite icon;
	public string trueName {get => gameObject.name.Replace("Enemy_", "");}

	void OnEnable()
	{
		EnemiesManager.i.enemies.Add(transform);
	}

	void OnDisable()
	{
		if(Leveling.i!= null) Leveling.i.GainExp(exp);
		if(EnemiesManager.i != null) EnemiesManager.i.enemies.Remove(transform);
	}
}