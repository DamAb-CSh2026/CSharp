using System;
using System.IO;
using System.Diagnostics;

interface IReport
{
    string GenerateContent();
}

class PdfReport : IReport
{
    public string GenerateContent()
    {
        string path = Path.GetFullPath("report.pdf");
        File.WriteAllText(path, "PDF Report Content");
        Console.WriteLine("PDF report created: {path}");
        return path;
    }
}

class ExcelReport : IReport
{
    public string GenerateContent()
    {
        string path = Path.GetFullPath("report.xlsx");
        File.WriteAllText(path, "Excel Report Content");
        Console.WriteLine("Excel report created: {path}");
        return path;
    }
}

class HTMLReport : IReport
{
    public string GenerateContent()
    {
        string html =
        @"<html>
            <body>
                <h1>HTML Report</h1>
                <p>This is a report.</p>
            </body>
          </html>";

        string path = Path.GetFullPath("report.html");
        File.WriteAllText(path, html);

        Console.WriteLine("HTML report created: {path}");
        return path;
    }
}

class CSVReport : IReport
{
    public string GenerateContent()
    {
        string csv =
        "Name,Age,City\n" +
        "Ali,25,Almaty\n" +
        "Madi,19,Atyrau";

        string path = Path.GetFullPath("report.csv");
        File.WriteAllText(path, csv);

        Console.WriteLine("CSV report created: {path}");
        return path;
    }
}

class ReportFactory
{
    public static IReport CreateReport(string type)
    {
        switch (type.ToLower())
        {
            case "pdf":
                return new PdfReport();

            case "excel":
                return new ExcelReport();

            case "html":
                return new HTMLReport();

            case "csv":
                return new CSVReport();

            default:
                throw new ArgumentException("Invalid report type");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.Write("Enter report type (pdf, excel, html, csv): ");

        string format = Console.ReadLine();

        try
        {
            IReport report = ReportFactory.CreateReport(format);
            string path = report.GenerateContent();

            OpenFileWithDefaultApp(path);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void OpenFileWithDefaultApp(string path)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = path,
                UseShellExecute = true
            };

            Process.Start(psi);
            Console.WriteLine("Opened file with default application.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Could not open file automatically: {ex.Message}");
            Console.WriteLine("You can find the report at: {path}");
        }
    }
}