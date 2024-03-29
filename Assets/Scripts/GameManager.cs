﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Singleton
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

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

    public static float coins = 0;
    public GameObject moneyPopupPrefab;
    public Transform worldspaceCanvas;

    public void GainMoney(Constants.FlightLevel _level, Constants.FlightClass _class, Vector3 _popupPos)
    {
        float multiplier;
        float price;

        switch (_level)
        {
            case Constants.FlightLevel.SIGHTSEEING_FLIGHT:
                price = Constants.PAYMENT_SIGHTSEEING_FLIGHT;
                break;
            case Constants.FlightLevel.SHORT_HAUL_FLIGHT:
                price = Constants.PAYMENT_SHORT_HAUL_FLIGHT;
                break;
            case Constants.FlightLevel.BETTER_SHORT_HAUL_FLIGHT:
                price = Constants.PAYMENT_BETTER_SHORT_HAUL_FLIGHT;
                break;
            case Constants.FlightLevel.MEDIUM_HAUL_FLIGHT:
                price = Constants.PAYMENT_MEDIUM_HAUL_FLIGHT;
                break;
            case Constants.FlightLevel.BETTER_MEDIUM_HAUL_FLIGHT:
                price = Constants.PAYMENT_BETTER_MEDIUM_HAUL_FLIGHT;
                break;
            case Constants.FlightLevel.LONG_HAUL_FLIGHT:
                price = Constants.PAYMENT_LONG_HAUL_FLIGHT;
                break;
            case Constants.FlightLevel.BETTER_LONG_HAUL_FLIGHT:
                price = Constants.PAYMENT_BETTER_LONG_HAUL_FLIGHT;
                break;
            case Constants.FlightLevel.FLIGHT_AROUND_THE_WORLD:
                price = Constants.PAYMENT_AROUND_THE_WORLD_FLIGHT;
                break;
            default:
                price = Constants.PAYMENT_SIGHTSEEING_FLIGHT;
                break;
        }

        switch (_class)
        {
            case Constants.FlightClass.ECONOMY_CLASS:
                multiplier = Constants.ECONOMY_CLASS_MULTIPLIER;
                break;
            case Constants.FlightClass.BUSINESS_CLASS:
                multiplier = Constants.BUSINESS_CLASS_MULTIPLIER;
                break;
            case Constants.FlightClass.FIRST_CLASS:
                multiplier = Constants.FIRST_CLASS_MULTIPLIER;
                break;
            case Constants.FlightClass.LUXURY_FIRST_CLASS:
                multiplier = Constants.LUXURY_FIRST_CLASS_MULTIPLIER;
                break;
            default:
                multiplier = 1.0f;
                break;
        }

        coins += price * multiplier;

        GameObject popup = Instantiate(moneyPopupPrefab, _popupPos, moneyPopupPrefab.transform.rotation, worldspaceCanvas);
        popup.GetComponent<MoneyPopup>().insantiationPosition = _popupPos;
        UIManager.Instance.SetPopUpText(popup, price * multiplier);

        UIManager.Instance.UpdateMoney(coins);
    }

    public void GainMoney(float _moneyGained, Vector3 _popupPos)
    {
        coins += _moneyGained;
        GameObject popup = Instantiate(moneyPopupPrefab, _popupPos, moneyPopupPrefab.transform.rotation, worldspaceCanvas);
        popup.GetComponent<MoneyPopup>().insantiationPosition = _popupPos;
        UIManager.Instance.SetPopUpText(popup, _moneyGained);

        UIManager.Instance.UpdateMoney(coins);
    }

    public void AddMoneyButton()
    {
        coins += 100000000000000;
        UIManager.Instance.UpdateMoney(coins);
    }

    public static Queue QueueFromId(int id)
    {
        Queue[] queues = FindObjectsOfType<Queue>();
        foreach (Queue q in queues)
        {
            if (q.queueId == id)
            {
                return q;
            }
        }
        return null;
    }
}
