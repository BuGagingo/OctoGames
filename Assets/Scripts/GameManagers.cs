using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagers : MonoBehaviour
{
    public static GameManagers instance;
    public List<GameObject> cardPrefabs;
    public List<Transform> spawnPoints; // Поинты для спавна
    private Card firstCard, secondCard;
    private int matchedPairs = 0;

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
            card.transform.localRotation = Quaternion.Euler(-90, 0, -180) * Quaternion.Euler(0, 180, 0); // Начальный переворот
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
        if (firstCard == null)
        {
            firstCard = card;
        }
        else
        {
            secondCard = card;
            StartCoroutine(CheckCards());
        }
    }

    IEnumerator CheckCards()
    {
        yield return new WaitForSeconds(2);

        if (firstCard.cardID == secondCard.cardID)
        {
            firstCard.isMatched = true;
            secondCard.isMatched = true;
            matchedPairs++;

            if (matchedPairs == cardPrefabs.Count)
            {
                Debug.Log("Поздравляю! Все пары найдены!");
            }
        }
        else
        {
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
        Quaternion targetRotation = card.transform.parent.rotation * Quaternion.Euler(0, 180, 0);
        float time = 0f;

        while (time < 0.3f)
        {
            card.transform.rotation = Quaternion.Lerp(card.transform.rotation, targetRotation, time / 0.3f);
            time += Time.deltaTime;
            yield return null;
        }
        card.transform.rotation = targetRotation;
    }
}