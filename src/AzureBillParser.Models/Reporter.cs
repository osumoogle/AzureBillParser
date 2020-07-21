using System;
using System.Collections.Generic;
using System.Linq;
using AzureBillParser.Models.Reports;

namespace AzureBillParser.Models
{
    public static class Reporter
    {
        public static List<ReportItem> GetReports(List<LineItem> lineItems)
        {
            var reports = new List<ReportItem>();
            reports.AddRange(CostPerSubscription(lineItems));
            reports.AddRange(CostPerResourceGroup(lineItems));
            return reports;
        }

        public static IEnumerable<ReportItem> CostPerSubscription(List<LineItem> lineItems)
        {
            if (lineItems == null) throw new ArgumentNullException(nameof(lineItems));
            var report = from l in lineItems
                group  l by l.SubscriptionName into subs
                select new ReportItem
                {
                    ReportName = "Total Cost",
                    Subscription = subs.Key,
                    Name = subs.Key,
                    Cost = subs.Sum(s => s.Cost)
                };
            return report.ToList();
        }

        public static IEnumerable<ReportItem> CostPerResourceGroup(IEnumerable<LineItem> lineItems)
        {
            var report = from l in lineItems
                group l by new 
                {
                    l.SubscriptionName,
                    l.ResourceGroup
                } into resourceGroups
                select new ReportItem
                {
                    ReportName = "Resource Group Cost",
                    Subscription = resourceGroups.Key.SubscriptionName,
                    Name = resourceGroups.Key.ResourceGroup,
                    Cost = resourceGroups.Sum(r => r.Cost)
                };
            return report;
        }

        public static IEnumerable<ReportItem> CostPerResource(IEnumerable<LineItem> lineItems)
        {
            var report = from l in lineItems
                group l by new
                {
                    l.SubscriptionName,
                    l.ResourceName
                }
                into resources
                select new ReportItem
                {
                    ReportName = "Resource Cost",
                    Subscription = resources.Key.SubscriptionName,
                    Name = resources.Key.ResourceName,
                    Cost = resources.Sum(r => r.Cost)
                };
            return report;
        }

        public static IEnumerable<ReportItem> CostPerMeter(IEnumerable<LineItem> lineItems)
        {
            var report = from l in lineItems
                group l by new
                {
                    l.SubscriptionName,
                    l.MeterCategory,
                    l.MeterSubCategory
                }
                into meters
                select new ReportItem
                {
                    ReportName = "Meter Cost",
                    Subscription = meters.Key.SubscriptionName,
                    Name = $"{meters.Key.MeterCategory} - {meters.Key.MeterSubCategory}",
                    Cost = meters.Sum(m => m.Cost)
                };
            return report;
        }
    }
}
