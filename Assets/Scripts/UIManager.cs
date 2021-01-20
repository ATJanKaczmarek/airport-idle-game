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
    public Scrollbar scrollbar;
    public GameObject panel;
    private Queue _currentQueue;

    private void Start()
    {
        UpdateMoney(GameManager.coins);
    }

    #region Money
    public void UpdateMoney(float _money)
    {
        money_txt.text = "Money: " + CalculateMoneyShortcut(_money);
        if (_currentQueue != null)
        {
            UpdateUpgradePanel(panel, _currentQueue);
        }
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

    #endregion

    #region Upgrades

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

        _btn1.onClick.AddListener(() => { queue.UpgradeSpawnrate(); UpdateUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });
        _btn2.onClick.AddListener(() => { queue.UpgradeQueueLength(); UpdateUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });
        _btn3.onClick.AddListener(() => { queue.UpgradeWaitingTime(); UpdateUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });

        panel.SetActive(true);
    }

    public void UpdateUpgradePanel(GameObject panel, Queue queue)
    {
        _currentQueue = queue;

        TMP_Text _price1_txt = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text _price2_txt = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text _price3_txt = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>();

        float _price1 = Constants.QUEUE_SPAWN_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.spawnrateUpgradesOwned);
        float _price2 = Constants.QUEUE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.lengthOwned);
        float _price3 = Constants.QUEUE_TIME_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.waitingTimeUpgradesOwned);

        Button _btn1 = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
        Button _btn2 = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
        Button _btn3 = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>();

        _price1_txt.text = "$" + CalculateMoneyShortcut(_price1);
        _price2_txt.text = "$" + CalculateMoneyShortcut(_price2);
        _price3_txt.text = "$" + CalculateMoneyShortcut(_price3);

        CheckButton(_price1, _btn1, queue);
        CheckButton(_price2, _btn2, queue);
        CheckButton(_price3, _btn3, queue);
    }

    private void CheckButton(float price, Button btn, Queue queue)
    {
        if (btn.gameObject.name == "BuyButton:SpawnRate" && queue.spawnrateUpgradesOwned == 39)
        {
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = "Max Level";
            btn.interactable = false;
            return;
        }

        if (btn.gameObject.name == "BuyButton:QueueLength" && queue.lengthOwned == 10)
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

        if (GameManager.coins < price)
        {
            btn.interactable = false;
        }
        else
        {
            btn.interactable = true;
        }
    }

    #endregion

    #region Scrollbar
    public void ScollbarChanged(float value)
    {
        Vector3 minPos = new Vector3(0, 0, -10);
        Vector3 maxPos = new Vector3(0, ((QueueCount.queueCount - 1) * -2.5f) + 2.5f, -10);
        Camera.main.transform.position = Vector3.Lerp(minPos, maxPos, value);
    }

    public void ResizeScrollbar(int queueCount)
    {
        scrollbar.size /= queueCount;
        scrollbar.value = 1f;
        ScollbarChanged(1f);
    }

    #endregion
}
