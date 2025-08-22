using System;
namespace NellsPay.Send.ResponseModels
{
    public partial class CreateTransactionResponse : ApiErrorResponse
    {
        public string Id { get; set; }
        public string TransactionNumber { get; set; }
    }

    public partial class GetTransactionResponse : ApiErrorResponse
    {
        public Transactions transactions { get; set; }
    }

    public class Transactions
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public List<Transaction> data { get; set; }
    }


    
}