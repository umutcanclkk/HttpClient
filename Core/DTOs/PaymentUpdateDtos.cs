namespace Core.DTOs
{
    public  class PaymentUpdateDtos
    {

        public string PaymentMethod { get; set; } // Method of payment used for the transaction
        public decimal Amount { get; set; } // The amount of money involved in the transaction
        public DateTime Date { get; set; } // The date when the transaction occurred
        public string TransactionId { get; set; } // An external transaction identifier, such as a bank transaction ID
    }
}
