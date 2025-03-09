using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;
public class GameManagers : MonoBehaviour
{
    public static GameManagers instance;
    public List<GameObject> cardPrefabs;
    public List<Transform> spawnPoints; // Поинты для спавна
    private Card firstCard, secondCard;
    private int matchedPairs = 0;
    public static bool canFlip = true;

    void Awake()
    {
        instance = this;      
    }

    void Update()
    {
        VariableManager.UpdateVariables();    
        if (VariableManager.game_status == 1)
        {
            SetupCards();
            VariableManager.game_status = 2;
            VariableManager.SaveVariables();
        }
    }
    void SetupCards()
    {
        List<GameObject> cards = new List<GameObject>();
        Shuffle(cardPrefabs);
        foreach (var prefab in cardPrefabs)
        {
            cards.Add(Instantiate(prefab));
            cards.Add(Instantiate(prefab));
            if (cards.Count == spawnPoints.Count)
            {
                break;
            }
        }

        Shuffle(cards);

        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject card = cards[i];
            card.transform.SetParent(spawnPoints[i]);
            card.transform.localPosition = Vector3.zero;
        }
    }

    void Shuffle(List<GameObject> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            GameObject temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void CheckMatch(Card card)
    {
        canFlip = false;
        if (firstCard == null)
        {
            firstCard = card;
            canFlip = true;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckCards());
        }
    }

    IEnumerator CheckCards()
    {
        

        if (firstCard.cardID == secondCard.cardID)
        {
            firstCard.isMatched = true;
            secondCard.isMatched = true;
            matchedPairs++;
            canFlip = true;
            if (matchedPairs == spawnPoints.Count/2)
            {
                VariableManager.game_status = 3;
                VariableManager.SaveVariables();
                var player = Engine.GetService<IScriptPlayer>();
                player.Play(0);
            }
        }
        else
        {
            yield return new WaitForSeconds(1);
            ResetCard(firstCard);
            ResetCard(secondCard);
        }

        firstCard = null;
        secondCard = null;
        
    }

    void ResetCard(Card card)
    {
        StartCoroutine(RotateBack(card));
    }

    IEnumerator RotateBack(Card card)
    {
        Quaternion targetRotation = new Quaternion(0, 0, 0, 0);  // Rotate back to the initial rotation
        float time = 0f;
        float duration = 0.3f;
        var t = card.transform.parent.rotation;

        while (time <= duration)
        {
            card.transform.parent.rotation = Quaternion.Lerp(t, targetRotation, time / duration);
            time += Time.deltaTime;
            yield return null;
        }
        canFlip = true;
        card.transform.parent.rotation = targetRotation;
    }
}