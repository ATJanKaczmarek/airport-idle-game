using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyDutyFree : MonoBehaviour
{
    public bool interactable = false;

    private void Start()
    {
        CheckBuyable();
    }

    private void OnMouseOver()
    {
        if (interactable == true && UIManager.Instance.hasActiveUIPanel == false)
            GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnMouseExit()
    {
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDown()
    {
        if (interactable == true && UIManager.Instance.hasActiveUIPanel == false)
        {
            if (gameObject.name == "DutyFree1:Buy")
            {
                if (UIManager.Instance.BuyDutyFree(1, gameObject.transform.parent.parent.GetComponent<Queue>().queueId) == true)
                {
                    AudioManager.instance.Play("Build");
                    gameObject.SetActive(false);
                }
            
            }
            else if (gameObject.name == "DutyFree2:Buy") 
            {
                if (UIManager.Instance.BuyDutyFree(2, gameObject.transform.parent.parent.GetComponent<Queue>().queueId) == true)
                {
                    AudioManager.instance.Play("Build");
                    gameObject.SetActive(false);
                }
            }
        }
    }

    public void CheckBuyable()
    {
        if (GameManager.coins > Constants.DUTY_FREE_SHOP_PRICE)
        {
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            GetComponent<SpriteRenderer>().color = new Color(200f, 200f, 200f, 0.5f);
        }
    }
}