using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

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
    public Image switchImage;
    public Sprite happy_ico;
    public Sprite dollar_ico;
    public GameObject queueUpgradePanel;
    public GameObject scannerUpgradePanel;
    public GameObject dutyFreeShopUpgradePanel;
    [HideInInspector] public Queue currentQueue;
    [HideInInspector] public Scanner currentScanner;
    private DutyFreeShop currentShop;

    public bool hasActiveUIPanel = false;

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

        BuyDutyFree[] dutyFreeButtons = FindObjectsOfType<BuyDutyFree>();
        foreach (BuyDutyFree button in dutyFreeButtons)
        {
            button.CheckBuyable();
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
        DeavtivateTriggers();
    }

    public void UpdateQueueUpgradePanel(GameObject panel, Queue queue)
    {
        currentQueue = queue;

        TMP_Text _price1_txt = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text _price2_txt = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text _price3_txt = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        
        TMP_Text _spawnrateLvl_txt = panel.transform.GetChild(1).GetChild(0).GetChild(2).GetComponent<TMP_Text>();
        TMP_Text _queuelenLvl_txt = panel.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<TMP_Text>();
        TMP_Text _waitingdurLvl_txt = panel.transform.GetChild(1).GetChild(2).GetChild(2).GetComponent<TMP_Text>();

        float _price1 = Constants.QUEUE_SPAWN_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.spawnrateUpgradesOwned);
        float _price2 = Constants.QUEUE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.lengthOwned);
        float _price3 = Constants.QUEUE_TIME_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, queue.waitingTimeUpgradesOwned);

        Button _btn1 = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
        Button _btn2 = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();
        Button _btn3 = panel.transform.GetChild(1).GetChild(2).GetChild(0).GetComponent<Button>();

        _price1_txt.text = "$" + CalculateMoneyShortcut(_price1);
        _price2_txt.text = "$" + CalculateMoneyShortcut(_price2);
        _price3_txt.text = "$" + CalculateMoneyShortcut(_price3);

        _spawnrateLvl_txt.text = queue.spawnrateUpgradesOwned + " / 38";
        _queuelenLvl_txt.text = queue.lengthOwned + " / 10";
        _waitingdurLvl_txt.text = queue.waitingTimeUpgradesOwned + " / 39";

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
        if (btn.gameObject.name == "BuyButton:SpawnRate" && queue.spawnrateUpgradesOwned == 38)
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
            queueUpgrade.interactable = true;
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
        DeavtivateTriggers();
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
    public void ScrollbarChanged(float value)
    {
        Vector3 minPos = new Vector3(0, 0, -10);
        Vector3 maxPos = new Vector3(0, ((QueueCount.queueCount - 1) * -3f) + 3f, -10);
        Camera.main.transform.position = Vector3.Lerp(minPos, maxPos, value);
    }

    public void ResizeScrollbar(int queueCount)
    {
        scrollbar.size /= queueCount;
        scrollbar.value = 1f;
        ScrollbarChanged(1f);
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
            AudioManager.instance.Play("BuyLane");
        }
        else
        {
            laneAddingText.transform.parent.GetComponent<Button>().interactable = false;
        }
    }

    #endregion

    #region Duty-Free
    
    public bool BuyDutyFree(int pos, int queueId)
    {
        if (GameManager.coins > Constants.DUTY_FREE_SHOP_PRICE)
        {
            Queue q = GameManager.QueueFromId(queueId);
            q.ActivateDutyFreeShop(pos);
            GameManager.coins -= Constants.DUTY_FREE_SHOP_PRICE;
            UpdateMoney(GameManager.coins);
            return true;
        }
        else
        {
            return false;
        }
    }

    public void OpenDutyFreeUpgradePanel(DutyFreeShop dutyFreeShop)
    {
        currentShop = dutyFreeShop;
        dutyFreeShopUpgradePanel.SetActive(true);

        DeavtivateTriggers();

        Scrollbar modeSwitch = dutyFreeShopUpgradePanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Scrollbar>();
        if (currentShop.makesMoney == true)
            modeSwitch.value = 1f;
        else
            UpdateShopMode();
    }

    private TMP_Text GetCurrentModeText()
    {
        return dutyFreeShopUpgradePanel.transform.GetChild(1).GetChild(1).GetChild(2).GetComponent<TMP_Text>();
    }

    private void UpdateShopMode()
    {
        if (currentShop.makesMoney == true)
            GetCurrentModeText().text = "MONEY";
        else
            GetCurrentModeText().text = "HAPPINESS";
    }

    #endregion

    #region Duty Free Switch

    public void SwitchImage(float value)
    {
        if (value == 1f)
        {
            switchImage.sprite = dollar_ico;
            currentShop.makesMoney = true;
        }
        else if (value == 0f)
        {
            switchImage.sprite = happy_ico;
            currentShop.makesMoney = false;
        }
        UpdateShopMode();
    }

    #endregion

    #region Triggers
    private GameObject[] GetAllUITriggers()
    {
        GameObject[] triggers = GameObject.FindGameObjectsWithTag("Trigger");
        return triggers;
    }

    private void DeavtivateTriggers()
    {
        GameObject[] triggers = GetAllUITriggers();
        foreach (GameObject trigger in triggers)
        {
            if (trigger.GetComponent<QueueUpgrade>() == true)
            {
                trigger.GetComponent<QueueUpgrade>().interactable = false;
            }
            else if (trigger.GetComponent<DutyFreeShop>() == true)
            {
                trigger.GetComponent<DutyFreeShop>().interactable = false;
            }
            else if (trigger.GetComponent<BuyDutyFree>() == true)
            {
                trigger.GetComponent<BuyDutyFree>().interactable = false;
            }
        }
        hasActiveUIPanel = true;
    }

    public void ActivateTriggers()
    {
        GameObject[] triggers = GetAllUITriggers();
        foreach (GameObject trigger in triggers)
        {
            if (trigger.GetComponent<QueueUpgrade>() == true)
            {
                trigger.GetComponent<QueueUpgrade>().interactable = true;
            }
            else if (trigger.GetComponent<DutyFreeShop>() == true)
            {
                trigger.GetComponent<DutyFreeShop>().interactable = true;
            }
            else if (trigger.GetComponent<BuyDutyFree>() == true)
            {
                trigger.GetComponent<BuyDutyFree>().interactable = true;
            }
        }
        hasActiveUIPanel = false;
    }
    #endregion
}
