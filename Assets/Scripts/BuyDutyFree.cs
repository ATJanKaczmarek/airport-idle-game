using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BuyDutyFree : MonoBehaviour
{
    private bool interactable = true;

    private void Start()
    {
        CheckBuyable();
    }

    private void OnMouseEnter()
    {
        if (interactable == true)
            GetComponent<SpriteRenderer>().color = Color.green;
    }

    private void OnMouseExit()
    {
        if (interactable == true)
            GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDown()
    {
        if (interactable)
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
            interactable = true;
            GetComponent<SpriteRenderer>().color = Color.white;
        }
        else
        {
            interactable = false;
            GetComponent<SpriteRenderer>().color = new Color(200f, 200f, 200f, 0.5f);
        }
    }
}