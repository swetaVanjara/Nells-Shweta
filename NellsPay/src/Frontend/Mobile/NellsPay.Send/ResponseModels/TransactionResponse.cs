using System;
namespace NellsPay.Send.ResponseModels
{ 
    public partial class TransactionResponse
    {
        public Transactionsdata Transactions { get; set; }
    }
    public class Transaction
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string CustomerId { get; set; }
        public string TransactionNumber { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string SenderAddress { get; set; }
        public string SenderCity { get; set; }
        public string SenderCountry { get; set; }
        public string SenderCountryCode { get; set; }
        public string SenderState { get; set; }
        public string SenderZipCode { get; set; }
        public string RecipientFirstName { get; set; }
        public string RecipientLastName { get; set; }
        public string RecipientPhoneNumber { get; set; }
        public string RecipientAddress { get; set; }
        public string RecipientCity { get; set; }
        public string RecipientCountry { get; set; }
        public string RecipientCountryCode { get; set; }
        public string RecipientState { get; set; }
        public string RecipientZipCode { get; set; }
        public int PayOutMethod { get; set; }
        public string PayOutAccount { get; set; }
        public string TransactionReason { get; set; }
        public string SenderCurrency { get; set; }
        public double? SenderAmount { get; set; }
        public string RecipientCurrency { get; set; }
        public double? ReceivedAmount { get; set; }
        public double? ExchangeRate { get; set; }
        public double? TransactionFee { get; set; }
        public string CouponCode { get; set; }
        public string PaymentMethod { get; set; }
        public string SeCustomerId { get; set; }
        public string CardNumber { get; set; }
        public string CardName { get; set; }
        public string Expiration { get; set; }
        public string Cvv { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
    }

    public class Transactionsdata
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public List<Transaction> data { get; set; }
    }
}