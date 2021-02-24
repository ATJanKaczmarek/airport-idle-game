using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Airplane : MonoBehaviour
{
    public int lengthOwned = 0;
    public int classOwned = 0;

    public void Upgrade(int lenghtOrClass, float  price)
    {
        if (lenghtOrClass == 0)
        {
            // Upgrade length
            if (lengthOwned < 8)
            {
                lengthOwned++;
                Constants.FlightLevel lvl = (Constants.FlightLevel)lengthOwned;
                //GameManager.coins -= (float)Math.Round(Constants.AIRPLANE_LENGTH_UPGRADE_BASE_COST * Mathf.Pow(Constants.MULTIPLIER, lengthOwned), 2);
                UIManager.Instance.UpdateMoney(GameManager.coins);

                Debug.Log(lvl);
            }
        }
        else if (lenghtOrClass == 1)
        {
            // Upgrade class
            if (classOwned < 4)
            {
                classOwned++;
                Constants.FlightClass lvl = (Constants.FlightClass)classOwned;
                Debug.Log(lvl);
            }
        }
        else
        {
            return;
        }

        GameManager.coins -= price;
    }

    public void ClickedAirplane()
    {
        UIManager.Instance.ActivateUpgradeAirplanePanel(GameObject.Find("OverlayCanvas").transform.GetChild(5).gameObject, this);
    }
}
