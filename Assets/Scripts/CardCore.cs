using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using AssemblyCSharp.Assets.Scripts;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardCore : MonoBehaviour
{
    public int maxCardNumber = 3;
    public int initCost = 3;
    public float cycleTime;
    [HideInInspector]
    public int nextCost = 3;

    public TextMesh costText;
    public GameObject[] cardSlots = new GameObject[3];
    public GameObject cardArrow;
    public GameObject cycleIndicator;
    [HideInInspector] public int selecting = 0;
    private int cost;
    private float currentCycleTime;
    private List<GameCard> drawDeck = new List<GameCard>()
    {
        BasicCards.backup, BasicCards.backup, BasicCards.lazer, BasicCards.lazer, BasicCards.portableBomb,
        BasicCards.portableBomb, BasicCards.speedUp, BasicCards.speedUp
    };

    private List<GameCard> usedDeck = new List<GameCard>();
    private List<GameCard> handCards = new List<GameCard>();

    private Dictionary<string, Texture2D> cardTexCache = new Dictionary<string, Texture2D>();

    public void nextSelect(bool forward = true)
    {
        if (forward)
        {
            if (selecting == maxCardNumber - 1) selecting = 0;
            else selecting++;
        }
        else
        {
            if (selecting == 0) selecting = maxCardNumber - 1;
            else selecting--;
        }
    }

    public void use(int n)
    {
        if (n < handCards.Count)
        {
            if (cost >= ((GameCard) handCards[n]).cost)
            {
                bool save = ((GameCard) handCards[n]).callback("use", gameObject);
                cost -= ((GameCard) handCards[n]).cost;
                if (save)
                {
                    usedDeck.Add(handCards[n]);
                }

                handCards.RemoveAt(n);
            }
        }
    }

    // Use this for initialization
    void Start()
    {
        // Add cards in deck to cache
        for (var i = 0; i != drawDeck.Count; ++i)
        {
            getCardTex(((GameCard) drawDeck[i]).name);
        }

        DrawOne();
        DrawOne();
        DrawOne();
        cost = initCost;
        currentCycleTime = cycleTime;
    }

    Texture2D getCardTex(string name)
    {
        if (cardTexCache.ContainsKey(name))
        {
            return cardTexCache[name];
        }
        else
        {
            var t = Resources.Load<Texture2D>("Images/" + name);
            cardTexCache.Add(name, t);
            return t;
        }
    }

    public void DrawOne()
    {
        if (drawDeck.Count == 0)
        {
            for (var i = 0; i != usedDeck.Count; ++i)
            {
                drawDeck.Add(usedDeck[i]);
            }

            usedDeck.Clear();
        }

        handCards.Add(drawDeck[Random.Range(0, drawDeck.Count - 1)]);
    }


    // Update is called once per frame
    void Update()
    {
        currentCycleTime -= Time.deltaTime;
        if (currentCycleTime < 0)
        {
            newCycle();
        }

        cycleIndicator.transform.localScale = new Vector3(cycleIndicator.transform.localScale.x, cycleIndicator.transform.localScale.y, 0.18f * currentCycleTime / cycleTime);
        costText.text = "Energy:" + cost.ToString();
        cardArrow.transform.position = new Vector3(cardArrow.transform.position.x, cardArrow.transform.position.y,
            cardSlots[selecting].transform.position.z);
        for (var i = 0; i != maxCardNumber; ++i)
        {
            if (i < cardSlots.Length)
            {
                if (i < handCards.Count)
                {
                    cardSlots[i].GetComponent<MeshRenderer>().material.mainTexture =
                        getCardTex(((GameCard) handCards[i]).name);
                }
                else
                {
                    cardSlots[i].GetComponent<MeshRenderer>().material.mainTexture =
                        getCardTex("cardback");
                }
            }
        }
    }

    void newCycle()
    {
        cost = initCost;
        usedDeck.AddRange(handCards);
        handCards.Clear();
        for(var i = 0; i < maxCardNumber; ++i) DrawOne();
        currentCycleTime = cycleTime;
        GetComponent<PlayerBuffManager>().onNewCycle();
    }
}