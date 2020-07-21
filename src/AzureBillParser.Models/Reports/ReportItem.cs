namespace AzureBillParser.Models.Reports
{
    public class ReportItem
    {
        public string ReportName { get; set; }
        public string Subscription { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }

        public override string ToString()
        {
            return $"{ReportName} - {Name} - {Cost:C2}";
        }
    }
}
