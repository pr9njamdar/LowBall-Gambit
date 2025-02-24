using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private Transform cardContainer;

    internal static readonly List<ICard> cards=new List<ICard>();
    private readonly string [] suits={"♠️","♦️","♣️","♥️"};
    private readonly string [] values={"A","2","3","4","5","6","7","8","9","10","J","Q","K"};
    private ICardSelector cardSelector;

    private void Start(){
        cardSelector=new CardSelector();
        GenerateCards();
    }

    private void GenerateCards(){
        foreach(string suit in suits){
            foreach(string value in values){
                ICard card = new Card(value,suit);
                cards.Add(card);
                CreateCardUI(card);
            }
        }
    }


    private void CreateCardUI (ICard card){
        GameObject newCard =Instantiate(cardPrefab,cardContainer);        

        Button cardButton = newCard.GetComponent<Button>();
        TextMeshProUGUI ButtonText=cardButton.GetComponentInChildren<TextMeshProUGUI>();
        ButtonText.text=card.GetCardValue();
        cardButton.onClick.AddListener(()=>HandleCardSelection(card));
    }

    private void HandleCardSelection(ICard card){
        if(Input.GetKey(KeyCode.LeftShift)||Input.GetKey(KeyCode.RightShift)){
            cardSelector.SelectCardRange(cards[0],card);
        }
        else{
            cardSelector.SelectCard(card);
        }

        HighlightSelectedCards();
    }

    private void HighlightSelectedCards(){
        foreach(Transform child in cardContainer){
            child.GetComponent<Image>().color=Color.white;
        }

        foreach(ICard selected in ((CardSelector)cardSelector).GetSelectedCards()){
            int index =cards.IndexOf(selected);
            cardContainer.GetChild(index).GetComponent<Image>().color=Color.green;
        }
    }

}
