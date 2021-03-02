using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

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

    // public
    public TMP_Text money_txt;
    public TMP_Text laneAddingText;
    public TMP_Text queueCountText;
    public Scrollbar scrollbar;
    public Image switchImage;
    public Sprite happy_ico;
    public Sprite dollar_ico;
    public GameObject queueUpgradePanel;
    public GameObject scannerUpgradePanel;
    public GameObject dutyFreeShopUpgradePanel;
    public GameObject airplaneUpgradePanel;
    public GameObject scannerEventPanel;

    public bool hasActiveUIPanel = false;

    public Sprite suitcaseOpen;
    public Sprite suitcaseClosed;
    public Image suitcaseVisual;
    private Constants.ScannerRewards rewardType;

    // public hidden
    [HideInInspector] public Queue currentQueue;
    [HideInInspector] public Scanner currentScanner;
    [HideInInspector] public Airplane currentAirplane;

    // private
    private DutyFreeShop currentShop;
    private Scrollbar dutyFreeSwitch;
    
    private void Start()
    {
        UpdateMoney(GameManager.coins);
        dutyFreeSwitch = dutyFreeShopUpgradePanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Scrollbar>();
    }

    #region Money
    public void UpdateMoney(float _money)
    {
        money_txt.text = "$" + CalculateMoneyShortcut(_money);
        
        if (currentQueue != null)
        {
            UpdateQueueUpgradePanel(queueUpgradePanel, currentQueue);
        }

        if (currentScanner != null)
        {
            UpdateScannerUpgradePanel(scannerUpgradePanel, currentScanner);
        }

        if (currentAirplane != null)
        {
            UpdateAirplaneUpgradePanel(airplaneUpgradePanel, currentAirplane);
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
            scannerUpgrade.interactable = true;
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
        btn.onClick.AddListener(() => { currentScanner.Upgrade(price); UpdateScannerUpgradePanel(panel, scanner); UpdateMoney(GameManager.coins); });
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

    // Airplane upgrades:
    public void ActivateUpgradeAirplanePanel(GameObject panel, Airplane airplane)
    {
        UpdateAirplaneUpgradePanel(panel, airplane);
        panel.SetActive(true);
        DeavtivateTriggers();
    }

    public void UpdateAirplaneUpgradePanel(GameObject panel, Airplane airplane)
    {
        currentAirplane = airplane;
        TMP_Text lengthPriceTxt = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        TMP_Text classPriceTxt = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<TMP_Text>();
        float lengthPrice = Constants.AIRPLANE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, airplane.lengthOwned);
        float classPrice = Constants.AIRPLANE_CLASS_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, airplane.classOwned);

        lengthPriceTxt.text = "$" + CalculateMoneyShortcut(lengthPrice);
        classPriceTxt.text = "$" + CalculateMoneyShortcut(classPrice);

        Button lengthBtn = panel.transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Button>();
        Button classBtn = panel.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Button>();

        lengthBtn.onClick.RemoveAllListeners();
        lengthBtn.onClick.AddListener(() => { currentAirplane.Upgrade(0, lengthPrice); UpdateAirplaneUpgradePanel(panel, airplane); UpdateMoney(GameManager.coins); });
        CheckButton(lengthPrice, lengthBtn, airplane);

        classBtn.onClick.RemoveAllListeners();
        classBtn.onClick.AddListener(() => { currentAirplane.Upgrade(1, classPrice); UpdateAirplaneUpgradePanel(panel, airplane); UpdateMoney(GameManager.coins); });
        CheckButton(classPrice, classBtn, airplane);
    }

    private void CheckButton(float price, Button btn, Airplane airplane)
    {
        if (btn.gameObject.name == "BuyButton:FlightLevel" && airplane.lengthOwned == 7)
        {
            btn.interactable = false;
            btn.transform.GetChild(0).GetComponent<TMP_Text>().text = "Max Level";
            return;
        }
        else if (btn.gameObject.name == "BuyButton:FlightClass" && airplane.classOwned == 3)
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
        Vector3 minPos = new Vector3(Camera.main.transform.position.x, 0, -10);
        Vector3 maxPos = new Vector3(Camera.main.transform.position.x, ((QueueCount.queueCount - 1) * -3f) - 2.25f, -10);
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

    public void SetLaneText(int queueCount)
    {
        queueCountText.text = "Lanes: " + queueCount;
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

        //dutyFreeSwitch = dutyFreeShopUpgradePanel.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<Scrollbar>();
        if (currentShop.makesMoney == true)
            dutyFreeSwitch.value = 1f;
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
        {
            GetCurrentModeText().text = "MONEY";
            dutyFreeSwitch.value = 1f;
        }
        else
        {
            GetCurrentModeText().text = "HAPPINESS";
            dutyFreeSwitch.value = 0f;
        }
    }

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
            else if (trigger.GetComponent<ScannerUpgrade>() == true)
            {
                trigger.GetComponent<ScannerUpgrade>().interactable = false;
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
            else if (trigger.GetComponent<ScannerUpgrade>() == true)
            {
                trigger.GetComponent<ScannerUpgrade>().interactable = true;
            }
        }
        hasActiveUIPanel = false;
    }
    #endregion

    #region Scanner event

    public void OpenScannerEvent(Constants.ScannerRewards reward, GameObject popup)
    {
        if (hasActiveUIPanel == false)
        {
            FadeInSuitcasePanel();
            DeavtivateTriggers();

            rewardType = reward;

            popup.SetActive(false);
        }
    }
    public void FadeInSuitcasePanel()
    {
        LeanTween.moveLocalY(scannerEventPanel, 0f, 1f).setEaseInOutCirc().setOnComplete(() => shakingAllowed = true);
    }

    public void OnSuitcaseOpen()
    {
        if (rewardType == Constants.ScannerRewards.MONEY)
        {
            float money = Random.Range(0f, GameManager.coins / 2);
            GameManager.coins += money;
            UpdateMoney(GameManager.coins);
            suitcaseVisual.sprite = suitcaseOpen;
            shakingAllowed = false;

            FadeOutSuitcasePanel();
            
            suitcaseVisual.transform.parent.GetChild(2).GetChild(0).gameObject.SetActive(true);
        }
    }
    
    public void FadeOutSuitcasePanel()
    {
        LeanTween.moveLocalY(scannerEventPanel, -Screen.height, 1f).setEaseInOutCirc().setOnComplete(ResetScannerEventPanel);
        ActivateTriggers();
    }

    private void ResetScannerEventPanel()
    {
        hasActiveUIPanel = false;
        suitcaseVisual.transform.parent.GetChild(2).GetChild(0).gameObject.SetActive(false);
        suitcaseVisual.sprite = suitcaseClosed;
    }

    private bool shakingAllowed = true;
    public void Shake()
    {
        if (LeanTween.isTweening(suitcaseVisual.gameObject) == false && shakingAllowed == true)
        {
            float rot = suitcaseVisual.transform.rotation.z;
            var seq = LeanTween.sequence();

            seq.append(LeanTween.rotateZ(suitcaseVisual.gameObject, rot - 1f, 0.05f));
            seq.append(LeanTween.rotateZ(suitcaseVisual.gameObject, rot + 1f, 0.05f));
            seq.append(LeanTween.rotateZ(suitcaseVisual.gameObject, rot, 0.025f));
        }
    }

    #endregion
}