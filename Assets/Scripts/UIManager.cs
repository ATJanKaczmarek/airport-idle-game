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
        _price2.text = "$" + CalculateMoneyShortcut(Constants.QUEUE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.lengthUpgradesOwned));
        _price3.text = "$" + CalculateMoneyShortcut(Constants.QUEUE_TIME_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.waitingTimeUpgradesOwned));

        Button _btn1 = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
        Button _btn2 = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
        Button _btn3 = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>();

        _btn1.onClick.AddListener(() => { queue.UpgradeSpawnrate(); UpdateUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });
        _btn2.onClick.AddListener(() => { queue.UpgradeQueueLength(); UpdateUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });
        _btn3.onClick.AddListener(() => { queue.UpgradeWaitingTime(); UpdateUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });

        panel.SetActive(true);
    }

    private void UpdateUpgradePanel(GameObject panel, Queue queue)
    {
        TMP_Text _price1_txt = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text _price2_txt = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text _price3_txt = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>();

        float _price1 = Constants.QUEUE_SPAWN_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.spawnrateUpgradesOwned);
        float _price2 = Constants.QUEUE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.lengthUpgradesOwned);
        float _price3 = Constants.QUEUE_TIME_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.waitingTimeUpgradesOwned);

        Button _btn1 = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
        Button _btn2 = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
        Button _btn3 = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>();

        CheckButton(_price1, _btn1);
        CheckButton(_price2, _btn2);
        CheckButton(_price3, _btn3);

        _price1_txt.text = "$" + CalculateMoneyShortcut(_price1);
        _price2_txt.text = "$" + CalculateMoneyShortcut(_price2);
        _price3_txt.text = "$" + CalculateMoneyShortcut(_price3);

    }

    private void CheckButton(float price, Button btn)
    {
<<<<<<< HEAD
        if (btn.gameObject.name == "BuyButton:SpawnRate" && queue.spawnrateUpgradesOwned == 39)
        {
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = "Max Level";
            btn.interactable = false;
            return;
        }

        if (btn.gameObject.name == "BuyButton:QueueLength" && queue.lengthUpgradesOwned == 11)
        {
            btn.interactable = false;
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = "Max Level";
            return;
        }

        if (btn.gameObject.name == "BuyButton:WaitingDuration" && queue.waitingTimeUpgradesOwned == 39)
        {
            btn.interactable = false;
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = "Max Level";
            return;
        }

=======
>>>>>>> parent of a67cadf... Finished Queue upgrades
        if (GameManager.coins < price)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

}
