using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardID;
    public bool isMatched = false;
    private Quaternion initialRotation;
    private bool isFlipping = false;

    void Start()
    {
        initialRotation = Quaternion.Euler(0, 0, 0); // Ротация для спавна
        transform.rotation = initialRotation;
    }

    void OnMouseDown()
    {
        if (!isMatched && !isFlipping)
        {
            StartCoroutine(FlipCard());
        }
    }

    IEnumerator FlipCard()
    {
        isFlipping = true;
        var t = transform.rotation;
        Quaternion targetRotation = (transform.rotation == initialRotation) ? initialRotation * Quaternion.Euler(0, 180, 0) : initialRotation;
        float time = 0f;

        while (time <= 1f)
        {
            transform.rotation = Quaternion.Lerp(t, targetRotation, time);
            time += Time.deltaTime/10;
            yield return null;
        }
        transform.rotation = targetRotation;
        GameManagers.instance.CheckMatch(this);
        isFlipping = false;
    }
}
