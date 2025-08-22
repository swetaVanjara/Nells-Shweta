namespace NellsPay.Send.Models
{
   public class Grouped_list : List<Transactions>
    {
        public string Name { get; set; } = default!;

        public Grouped_list(string name, List<Transactions> transactions) : base(transactions)
        {
            Name = name;     
        }
    }

}
