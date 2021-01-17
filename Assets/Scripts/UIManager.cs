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
        if (_money < 1000)
        {
            return "Money: " + _money;
        }
        else if (_money < 1000000)
        {
            return "Money: " + System.Math.Round(_money / 1000, 2) + "K";
        }
        else if (_money < 1000000000)
        {
            return "Money: " + System.Math.Round(_money / 1000000, 2) + "Mio";
        }
        else if (_money < 1000000000000)
        {
            return "Money: " + System.Math.Round(_money / 1000000000, 2) + "Mrd";
        }
        else if (_money < 1000000000000000)
        {
            return "Money: " + System.Math.Round(_money / 1000000000000, 2) + "B";
        }
        else if (_money < 1000000000000000000)
        {
            return "Money: " + System.Math.Round(_money / 1000000000000000, 2) + "Brd";
        }
        else
        {
            Debug.Log("Max Money");
            return "Max";
        }
    }
}
