using UnityEngine;
using TMPro;

public class SpawnCurrency : MonoBehaviour
{
	[SerializeField] float maxCash;
    [SerializeField] float cash; public float Cash {get => cash;}
	[SerializeField] float income; public float Income {get => income; set {income = value; RefreshCashDisplay();}}
	float incomeTimer;
	[Header("GUI")]
	[SerializeField] TextMeshProUGUI cashDisplay;

	void Start()
	{
		RefreshCashDisplay();
	}

	void Update()
	{
		incomeTimer += Time.deltaTime;
		if(incomeTimer >= 1/income)
		{
			Earning(1);
			incomeTimer -= incomeTimer;
		}
	}

	public void Earning(int earned)
	{
		cash += earned;
		cash = Mathf.Clamp(cash, 0, maxCash);
		RefreshCashDisplay();
	}

	public bool Spending(int spended)
	{
		//If have enough cast to spend
		if(spended <= cash)
		{
			cash -= spended;
			RefreshCashDisplay();
			return true;
		}
		//Don't have enough
		return false;
	}

	void RefreshCashDisplay()
	{
		//Display cash in format: $ 553/1000 +12/s
		cashDisplay.text = "$" + cash + "/" + maxCash + " +" + System.Math.Round(1/income, 1) + "/s";
	}
}