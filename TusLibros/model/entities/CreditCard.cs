using System;

namespace TusLibros.model.entities
{
    public class CreditCard
    {
        public DateTime ExpirationDate { get; protected set; }
        public int CardNumber { get; set; }

        public CreditCard(DateTime expirationDate, int cardNumber)
        {
            ExpirationDate = expirationDate;
            CardNumber = cardNumber;
        }

        public bool IsExpired()
        {
            return ExpirationDate < DateTime.Now;
        }

        public override bool Equals(Object creditCard)
        {
            if (creditCard == null)
            {
                return false;
            }

            CreditCard aCreditCard = creditCard as CreditCard;

            return CardNumber.Equals(aCreditCard.CardNumber);
        }
    }
}
