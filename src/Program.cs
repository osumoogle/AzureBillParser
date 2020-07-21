using System;
using System.IO;
using System.Linq;
using System.Text;
using AzureBillParser.Models;

namespace AzureBillParser
{
    public static class Program
    {
        static void Main(string[] args)
        {
            var sb = new StringBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "Bills");

            var lineItems = LineParser.Parse(path);
            var subscriptionReports = Reporter.CostPerSubscription(lineItems);
            var resourceReports = Reporter.CostPerResource(lineItems);
            var meterReports = Reporter.CostPerMeter(lineItems);
            var subscriptions = subscriptionReports.Select(sr => sr.Subscription).ToList();
            foreach (var sub in subscriptions)
            {
                sb.AppendLine($"--------------SUBSCRIPTION: {sub}--------------");
                var subReport = subscriptionReports.Single(r => r.Subscription == sub);
                sb.AppendLine(subReport.ToString());
                var resources = resourceReports.Where(rr => rr.Subscription == sub).ToList();
                var resourceSum = resources.Sum(r => r.Cost);
                sb.AppendLine($"Resource Sum: {resourceSum:C2}");
                foreach (var resource in resources)
                    sb.AppendLine(resource.ToString());

                sb.AppendLine("--------------METERS--------------");
                var meters = meterReports.Where(mr => mr.Subscription == sub);
                foreach (var meter in meters)
                    sb.AppendLine(meter.ToString());
                sb.AppendLine($"--------------SUBSCRIPTION: {sub}--------------");

                sb.AppendLine();
            }

            File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "Report.txt"), sb.ToString());
            Console.WriteLine(sb.ToString());
            Console.Write("Press enter to exit:");
            Console.ReadLine();
        }
    }
}
