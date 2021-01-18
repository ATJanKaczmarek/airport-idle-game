using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    private void Start()
    {
        UpdateMoney(GameManager.coins);
    }

    public void UpdateMoney(float _money)
    {
        money_txt.text = "Money: " + CalculateMoneyShortcut(_money);
    }

    private string CalculateMoneyShortcut(float _money)
    {
        if (_money < 1000f)
        {
            return _money.ToString();
        }
        else if (_money < 1000000f)
        {
            return System.Math.Round(_money / 1000f, 2) + "K";
        }
        else if (_money < 1000000000f)
        {
            return System.Math.Round(_money / 1000000f, 2) + "Mio";
        }
        else if (_money < 1000000000000f)
        {
            return System.Math.Round(_money / 1000000000f, 2) + "Mrd";
        }
        else if (_money < 1000000000000000f)
        {
            return System.Math.Round(_money / 1000000000000f, 2) + "B";
        }
        else if (_money < 1000000000000000000f)
        {
            return System.Math.Round(_money / 1000000000000000f, 2) + "Brd";
        }
        else if (_money < 1000000000000000000000f)
        {
            return System.Math.Round(_money / 1000000000000000000f, 2) + "Tri";
        }
        else if (_money < 1000000000000000000000000f)
        {
            return System.Math.Round(_money / 1000000000000000000000f, 2) + "Trd";
        }
        else if (_money < 1000000000000000000000000000f)
        {
            return System.Math.Round(_money / 1000000000000000000000000f, 2) + "Qui";
        }
        else if (_money < 1000000000000000000000000000000f)
        {
            return System.Math.Round(_money / 1000000000000000000000000000f, 2) + "Qrd";
        }
        else if (_money < 1000000000000000000000000000000000f)
        {
            return System.Math.Round(_money / 1000000000000000000000000000000f, 2) + "Qut";
        }
        else
        {
            Debug.Log ("Max Money");
            return System.Math.Round(_money / 1000000000000000000000000000000f, 2) + "Qut";
        }
    }

    public void ActivateUpgradeQueuePanel(GameObject panel, Queue queue)
    {
        TMP_Text _price1 = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text _price2 = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text _price3 = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>();

        _price1.text = "$" + CalculateMoneyShortcut(Constants.QUEUE_SPAWN_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.spawnrateUpgradesOwned));
        _price2.text = "$" + CalculateMoneyShortcut(Constants.QUEUE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.lengthOwned));
        _price3.text = "$" + CalculateMoneyShortcut(Constants.QUEUE_TIME_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.waitingTimeUpgradesOwned));

        Button _btn1 = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
        Button _btn2 = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
        Button _btn3 = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>();

        _btn1.onClick.AddListener(queue.UpgradeSpawnrate);
        _btn2.onClick.AddListener(queue.UpgradeQueueLength);
        _btn3.onClick.AddListener(queue.UpgradeWaitingTime);

        panel.SetActive(true);
    }

}
