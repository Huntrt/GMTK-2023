using UnityEngine.UI;
using UnityEngine;

public class Leveling : MonoBehaviour
{
	#region Set this class to singleton
	static Leveling _i; public static Leveling i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<Leveling>();
			}
			return _i;
		}
	}
	#endregion

	[SerializeField] int level;
    public float curExp, expNeeded;
	[SerializeField] Image expBar;
	[SerializeField] TMPro.TextMeshProUGUI expCount;

	void Start()
	{
		RefreshGUI();
	}

	public void GainExp(float amount)
	{
		curExp += amount;
		//If reached exp need for next level
		if(curExp >= expNeeded)
		{
			//Reset to the exp leftover after level up
			curExp = curExp - expNeeded;
			//Level up
			level++;
			//Show modifer to choose when level up
			ShowModifiers.i.Show();
		}
		RefreshGUI();
	}

	void RefreshGUI()
	{
		expBar.fillAmount = curExp/expNeeded;
		expCount.text = curExp + "/" + expNeeded;
	}
}