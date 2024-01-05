public class PaymentModel

{

    public int Id { get; set; }//Ödemenin kimliği
    public string PaymentMethod { get; set; }//Ödeme yöntemi
    public decimal Amount { get; set; }//Ödeme tutarı
    public DateTime Date { get; set; }//Ödeme tarihi
    public string TransactionId { get; set; }//İşlem kimliği
}
