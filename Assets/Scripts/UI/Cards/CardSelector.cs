using System.Collections.Generic;
using UnityEngine;

public class CardSelector : ICardSelector{
    private List<ICard> selectedCards=new List<ICard>();
    private bool isSelectingRange=false;
    private ICard FirstSelectedCard;
    public void SelectCard(ICard card){
        if(isSelectingRange){
            SelectCardRange(FirstSelectedCard,card);
            isSelectingRange=false;
        }
        else{
            FirstSelectedCard=card;
            isSelectingRange=true;
            selectedCards.Clear();
            selectedCards.Add(card);
            Debug.Log($"Single Card selected : {card.GetCardValue()}");
        }
    }
    public void SelectCardRange(ICard StartCard,ICard EndCard){
        selectedCards.Clear();

        int startIndex=CardSpawner.cards.IndexOf(StartCard);
        int endIndex=CardSpawner.cards.IndexOf(EndCard);
        for(int i=startIndex;i<=endIndex;i++){
            selectedCards.Add(CardSpawner.cards[i]);
        }
        Debug.Log($"Selected cards from range {StartCard.GetCardValue()} to {EndCard.GetCardValue()}");
    }

    public List<ICard> GetSelectedCards()=>selectedCards;
}