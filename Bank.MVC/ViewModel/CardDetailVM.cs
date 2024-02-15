using Bank.Core.Entities.Models;

namespace Bank.MVC.ViewModel
{
    public class CardDetailVM
    {
        public List<Card> Cards { get; set; }

        public CardDetailVM()
        {
            Cards = new List<Card>();
        }
    }
}
