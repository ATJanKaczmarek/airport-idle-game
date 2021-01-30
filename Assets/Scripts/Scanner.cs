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

    private void Awake() => _spriteRenderer = GetComponent<SpriteRenderer>();

    public void Upgrade() { if (upgradesOwned < 10) { probability -= 10; upgradesOwned++; } else { Debug.Log("Maxed Scanner"); } }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Person>() == true)
        {

            int rnd = Random.Range(0, probability);
            if (rnd == 0)
            {
                Debug.Log("Scanner Event!");
            }

            _spriteRenderer.sprite = green;
            AudioManager.instance.Play("BodyScanner");
            StartCoroutine(SetRed());
        }
    }

    private IEnumerator SetRed()
    {
        yield return new WaitForSeconds(0.25f);
        _spriteRenderer.sprite = red;
    }
}
