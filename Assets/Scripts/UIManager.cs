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

    public void UpdateMoney(float _money)
    {
        money_txt.text = CalculateMoneyShortcut(_money);
    }

    private string CalculateMoneyShortcut(float _money)
    {
        if (_money < 1000f)
        {
            return "Money: " + _money;
        } 
        else if (_money < 1000000)
        {
            return "Money: " + _money / 1000 + "K";
        }
        else
        {
            return "";
        }
    }
}
