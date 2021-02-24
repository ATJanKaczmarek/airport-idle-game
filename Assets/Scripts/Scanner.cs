using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scanner : MonoBehaviour
{
    public Sprite green;
    public Sprite red;
    private SpriteRenderer _spriteRenderer;

    public int upgradesOwned = 0;
    private int probability = 100;

    public GameObject scannerEventIcon;

    private void Awake() { _spriteRenderer = GetComponent<SpriteRenderer>(); scannerEventIcon.SetActive(false); }

    public void Upgrade(float price) { if (upgradesOwned < 10) { probability -= 10; upgradesOwned++; GameManager.coins -= price; } else { Debug.Log("Maxed Scanner"); } }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Person>() == true)
        {

            int rnd = Random.Range(0, probability);
            if (rnd == 0)
            {
                scannerEventIcon.SetActive(true);
                AudioManager.instance.Play("BodyScanner");
            }

            _spriteRenderer.sprite = green;
            StartCoroutine(SetRed());
        }
    }

    private IEnumerator SetRed()
    {
        yield return new WaitForSeconds(0.25f);
        _spriteRenderer.sprite = red;
    }
}
