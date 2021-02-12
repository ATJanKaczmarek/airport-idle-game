using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DutyFreeShop : MonoBehaviour
{
    public bool makesMoney = false;
    public bool interactable = true;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Person>() == true)
        {
            int rnd = Random.Range(0, 100);
            if (rnd <= 70)
            {
                if (makesMoney == true)
                {
                    GameManager.Instance.GainMoney(100f, new Vector3(
                        collision.transform.position.x, 
                        collision.transform.position.y + (collision.transform.localScale.y / 2), 
                        collision.transform.position.z)
                    );
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Person>() == true && makesMoney == false)
        {
            int rnd = Random.Range(0, 100);
            if (rnd <= 70)
            {
                Person p = collision.GetComponent<Person>();
                p.SetHappiness(HappinessState.Happy);
            }
        }
    }

    private void OnMouseOver()
    {
        if (interactable == true)
            GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f);
    }

    private void OnMouseExit()
    {
       GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnMouseDown()
    {
        if (interactable == true)
        {
            UIManager.Instance.OpenDutyFreeUpgradePanel(this);
        }
    }
}