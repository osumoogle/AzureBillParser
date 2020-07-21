namespace AzureBillParser.Models
{
    public class LineItem
    {
        // 9
        public string SubscriptionName { get; set; }
        // 16
        public string MeterCategory { get; set; }
        // 17
        public string MeterSubCategory { get; set; }
        // 20
        public decimal Quantity { get; set; }
        // 21
        public decimal EffectivePrice { get; set; }
        // 22
        public decimal Cost { get; set; }
        // 27
        public string ConsumedService { get; set; }
        // 29
        public string ResourceName { get; set; }
        // 38
        public string ResourceGroup { get; set; }

        public LineItem(string line)
        {
            var parts = line.Split(',');
            SubscriptionName = GetPart(parts, 9);
            MeterCategory = GetPart(parts, 15);
            MeterSubCategory = GetPart(parts, 16);
            Quantity = GetDecimalPart(parts, 19);
            EffectivePrice = GetDecimalPart(parts, 20);
            Cost = GetDecimalPart(parts, 21);
            ConsumedService = GetPart(parts, 26);
            ResourceName = GetPart(parts, 28);
            ResourceGroup = GetPart(parts, 42);
        }

        public static string GetPart(string[] parts, int index)
        {
            return parts.Length >= index ? parts[index] : string.Empty;
        }

        public static decimal GetDecimalPart(string[] parts, int index)
        {
            return parts.Length >= index ? decimal.Parse(parts[index]) : 0;
        }
    }
}
