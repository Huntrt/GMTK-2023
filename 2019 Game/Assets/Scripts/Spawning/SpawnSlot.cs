using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class SpawnSlot : MonoBehaviour
{
	public EnemySpawning spawning;
	public Image iconImage, selectIndicator, cooldownBar;
	public TextMeshProUGUI costText;
	public float cooldownTimer;
	public bool isCooled;

	void OnEnable()
	{
		RefreshInfo();
	}

	public void Cooling()
	{
		//Stop if this slot are already cool
		if(isCooled) return;
		//Timing cooldown timer until it got cooled
		if(cooldownTimer < spawning.cooldown) cooldownTimer += Time.deltaTime; else isCooled = true;
		//Refresh the cooldown bar fill percent
		cooldownBar.fillAmount = cooldownTimer / spawning.cooldown;
	}
	
	public void RestartCool()
	{
		isCooled = false;
		cooldownTimer -= cooldownTimer;
	}

	public void RefreshInfo()
	{
		iconImage.sprite = spawning.icon;
		costText.text = "$" + spawning.cost;
	}
}