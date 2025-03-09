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
        if (!isMatched && !isFlipping && GameManagers.canFlip)
        {
            GameManagers.canFlip = false;
            StartCoroutine(FlipCard());
        }
    }

    IEnumerator FlipCard()
    {
        print("FlipCard");
        isFlipping = true;
        var t = transform.parent.rotation;
        Quaternion targetRotation = (transform.parent.rotation == initialRotation) 
            ? initialRotation * Quaternion.Euler(0, 0, -180) 
            : initialRotation;
        float time = 0f;
        float duration = 0.3f;

        while (time <= duration)
        {
            transform.parent.rotation = Quaternion.Lerp(t, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        GameManagers.canFlip = true;
        transform.parent.rotation = targetRotation;
        GameManagers.instance.CheckMatch(this);
        isFlipping = false;
    }
}
