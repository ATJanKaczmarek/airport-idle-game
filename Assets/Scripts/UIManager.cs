using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region Singleton
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }
    #endregion

    public TMP_Text money_txt;

    public void UpdateMoney(decimal _money)
    {
        money_txt.text = CalculateMoneyShortcut(_money);
    }

    private string CalculateMoneyShortcut(decimal _money)
    {
        decimal _roundedMoney = System.Math.Round(_money, 2);
        if (_roundedMoney < 1000)
        {
            return "Money: " + _roundedMoney;
        } 
        else if (_roundedMoney < 1000000)
        {
            return "Money: " + System.Math.Round(_roundedMoney / 1000, 2) + "K";
        }
        else if (_roundedMoney < 1000000000)
        {
            return "Money: " + _roundedMoney / 1000000 + "M";
        }
        else
        {
            return "Money: " + _roundedMoney;
        }
    }
}
