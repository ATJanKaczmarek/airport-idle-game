using System.Collections;
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

    public static decimal coins = 10000;

    public void GainMoney(Constants.FlightLevel _level, Constants.FlightClass _class)
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

        coins += (decimal)price * (decimal)multiplier;
        UIManager.Instance.UpdateMoney(coins);
    }

    public void AddMoneyButton()
    {
        coins += 10000;
        UIManager.Instance.UpdateMoney(coins);
    }
}
