using UnityEngine.UI;
using UnityEngine;

public class Protag : MonoBehaviour
{
    #region Set this class to singleton
	static Protag _i; public static Protag i
	{
		get
		{
			if(_i==null)
			{
				_i = GameObject.FindObjectOfType<Protag>();
			}
			return _i;
		}
	}
	#endregion

	public ProtagEquip equip;
	public ProtagState state;
	public Health health;
	[SerializeField] Image healthBar;
	[SerializeField] TMPro.TextMeshProUGUI healthCount;

	void OnEnable()
	{
		health.OnDamage += RefreshHealthGUI;
	}

	void OnDisable()
	{
		health.OnDamage -= RefreshHealthGUI;
	}

	void Start()
	{
		RefreshHealthGUI(0);
	}

	void RefreshHealthGUI(float taken)
	{
		healthBar.fillAmount = health.CurHealth/health.MaxHealth;
		healthCount.text = health.CurHealth.ToString();
	}
}