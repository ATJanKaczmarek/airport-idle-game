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
    public TMP_Text laneAddingText;
    public Scrollbar scrollbar;
    public GameObject queueUpgradePanel;
    public GameObject scannerUpgradePanel;
    [HideInInspector] public Queue currentQueue;
    [HideInInspector] public Scanner currentScanner;

    private void Start()
    {
        UpdateMoney(GameManager.coins);
    }

    #region Money
    public void UpdateMoney(float _money)
    {
        money_txt.text = "Money: " + CalculateMoneyShortcut(_money);
        
        if (currentQueue != null)
        {
            UpdateQueueUpgradePanel(queueUpgradePanel, currentQueue);
        }

        if (currentScanner != null)
        {
            UpdateScannerUpgradePanel(scannerUpgradePanel, currentScanner);
        }

        if (CalculateNewLanePrice() < GameManager.coins)
        {
            laneAddingText.transform.parent.GetComponent<Button>().interactable = true;
        }
        else
        {
            laneAddingText.transform.parent.GetComponent<Button>().interactable = false;
        }

    }

    public string CalculateMoneyShortcut(float _money)
    {
        if (_money < 1000f)
        {
            return System.Math.Round(_money, 2).ToString();
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

    public void SetPopUpText(GameObject _popup, float _money)
    {
        TMP_Text _popupText = _popup.GetComponent<TMP_Text>();
        _popupText.text = "+$" + CalculateMoneyShortcut(_money);
    }

    #endregion

    #region Upgrades
    
    // Queue upgrades:
    public void ActivateUpgradeQueuePanel(GameObject panel, Queue queue)
    {
        UpdateQueueUpgradePanel(panel, queue);
        panel.SetActive(true);
    }

    public void UpdateQueueUpgradePanel(GameObject panel, Queue queue)
    {
        currentQueue = queue;

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

        _btn1.onClick.RemoveAllListeners();
        _btn2.onClick.RemoveAllListeners();
        _btn3.onClick.RemoveAllListeners();

        _btn1.onClick.AddListener(() => { currentQueue.UpgradeSpawnrate(); UpdateQueueUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });
        _btn2.onClick.AddListener(() => { currentQueue.UpgradeQueueLength(); UpdateQueueUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });
        _btn3.onClick.AddListener(() => { currentQueue.UpgradeWaitingTime(); UpdateQueueUpgradePanel(panel, queue); UpdateMoney(GameManager.coins); });

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

    public void ResetHoverListeners()
    {
        QueueUpgrade[] queueUpgrades = FindObjectsOfType<QueueUpgrade>();
        foreach (QueueUpgrade queueUpgrade in queueUpgrades)
        {
            queueUpgrade.canBeTriggered = true;
        }

        ScannerUpgrade[] scannerUpgrades = FindObjectsOfType<ScannerUpgrade>();
        foreach (ScannerUpgrade scannerUpgrade in scannerUpgrades)
        {
            scannerUpgrade.canBeTriggered = true;
        }
    }

    // Scanner upgrades:
    public void ActivateUpgradeScannerPanel(GameObject panel, Scanner scanner)
    {
        UpdateScannerUpgradePanel(panel, scanner);
        panel.SetActive(true);
    }

    public void UpdateScannerUpgradePanel(GameObject panel, Scanner scanner)
    {
        currentScanner = scanner;
        TMP_Text priceTxt = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        float price = Constants.SCANNER_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, scanner.upgradesOwned);
        Button btn = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
        priceTxt.text = "$" + CalculateMoneyShortcut(price);
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => { currentScanner.Upgrade(); UpdateScannerUpgradePanel(panel, scanner); UpdateMoney(GameManager.coins); });
        CheckButton(price, btn, scanner);
    }

    private void CheckButton(float price, Button btn, Scanner scanner)
    {
        if (btn.gameObject.name == "BuyButton:ScannerUpgrade" && scanner.upgradesOwned == 10)
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

    #region Lanes
    
    public float CalculateNewLanePrice()
    {
        float price = Constants.LANE_BASE_PRICE * Mathf.Pow(Constants.MULTIPLIER, QueueCount.queueCount);
        laneAddingText.text = "Buy new Lane for: " + "$" + CalculateMoneyShortcut(price);
        return price;
    }

    public void BuyNewLane()
    {
        float price = CalculateNewLanePrice();

        if (price < GameManager.coins)
        {
            GameManager.coins -= price;
            UpdateMoney(GameManager.coins);
            laneAddingText.transform.parent.GetComponent<LaneAdding>().AddLane();
        }
        else
        {
            laneAddingText.transform.parent.GetComponent<Button>().interactable = false;
        }
    }

    #endregion
}
