using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPopup : MonoBehaviour
{
    public Vector3 insantiationPosition;

    private void Start()
    {
        StartCoroutine(Timer());
        Destroy(gameObject, 3f);
    }

    private IEnumerator Timer()
    {
        Animation();
        yield return new WaitForSeconds(1.5f);
        FadeOut();
        yield return new WaitForSeconds(0.5f);
    }

    private void Animation()
    {
        MovePosition();
        FadeIn();
    }

    private void MovePosition()
    {
        LeanTween.moveY(gameObject, insantiationPosition.y + 0.5f, 1f).setEaseOutCirc();
        float randomX = Random.Range(-1f, 1f);
        LeanTween.moveX(gameObject, insantiationPosition.x + randomX, 1f);
    }

    private void FadeIn()
    {
        LeanTween.value(gameObject, 0.5f, 1f, 0.5f).setOnUpdate((float val) =>
        {
            Color c = gameObject.GetComponent<TMP_Text>().color;
            c.a = val;
            gameObject.GetComponent<TMP_Text>().color = c;
        }).setEaseInCirc();
    }

    private void FadeOut()
    {
        LeanTween.value(gameObject, 1, 0, 0.5f).setOnUpdate((float val) =>
        {
            Color c = gameObject.GetComponent<TMP_Text>().color;
            c.a = val;
            gameObject.GetComponent<TMP_Text>().color = c;
        }).setEaseOutCirc();
    }
}
