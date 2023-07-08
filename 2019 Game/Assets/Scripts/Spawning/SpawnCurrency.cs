using UnityEngine;
using TMPro;

public class SpawnCurrency : MonoBehaviour
{
    [SerializeField] int cash; public int Cash {get => cash;}
	[Header("GUI")]
	[SerializeField] TextMeshProUGUI cashDisplay;

	public void Earning(int earned)
	{
		cash += earned;
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
		cashDisplay.text = "$" + cash;
	}
}