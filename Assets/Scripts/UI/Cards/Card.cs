public class Card : ICard{
    private string Value {get;}
    private string Suit {get;}

    internal Card(string Value,string Suit){
        this.Value=Value;
        this.Suit=Suit;
    }

    public string GetCardValue(){
        return $"{Value}{Suit}";
    }
}