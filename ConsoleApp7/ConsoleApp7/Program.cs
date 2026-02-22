using System;
using System.Text;

public class Report
{
    public string Header { get; set; }
    public string Content { get; set; }
    public string Footer { get; set; }

    public void Show()
    {
        Console.WriteLine(Header);
        Console.WriteLine(Content);
        Console.WriteLine(Footer);
        Console.WriteLine();
    }
}

public interface IReportBuilder
{
    void SetHeader(string text);
    void SetContent(string text);
    void SetFooter(string text);
    Report GetReport();
}

public class TextReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetHeader(string text)
    {
        _report.Header = $"=== {text} ===\n";
    }

    public void SetContent(string text)
    {
        _report.Content = $"{text}\n";
    }

    public void SetFooter(string text)
    {
        _report.Footer = $"\n--- {text} ---";
    }

    public Report GetReport()
    {
        return _report;
    }
}
public class HtmlReportBuilder : IReportBuilder
{
    private Report _report = new Report();

    public void SetHeader(string text)
    {
        _report.Header = $"<h1>{text}</h1>\n";
    }

    public void SetContent(string text)
    {
        _report.Content = $"<p>{text}</p>\n";
    }

    public void SetFooter(string text)
    {
        _report.Footer = $"<hr/><i>{text}</i>";
    }

    public Report GetReport()
    {
        return _report;
    }
}
public class XmlReportBuilder : IReportBuilder
{
    private Report _report = new Report();
    private StringBuilder _xml = new StringBuilder();

    public void SetHeader(string text)
    {
        _xml.AppendLine($"<header>{text}</header>");
        _report.Header = $"<header>{text}</header>";
    }

    public void SetContent(string text)
    {
        _xml.AppendLine($"<content>{text}</content>");
        _report.Content = $"<content>{text}</content>";
    }

    public void SetFooter(string text)
    {
        _xml.AppendLine($"<footer>{text}</footer>");
        _report.Footer = $"<footer>{text}</footer>";
    }

    public Report GetReport()
    {
        _report.Header = "<report>\n" + _report.Header;
        _report.Footer = _report.Footer + "\n</report>";
        return _report;
    }
}
public class ReportDirector
{
    public void ConstructReport(IReportBuilder builder, string header, string content, string footer)
    {
        builder.SetHeader(header);
        builder.SetContent(content);
        builder.SetFooter(footer);
    }
}

class Program
{
    static void Main()
    {
        ReportDirector director = new ReportDirector();

        Console.WriteLine("ТЕКСТОВЫЙ ОТЧЕТ");
        TextReportBuilder textBuilder = new TextReportBuilder();
        director.ConstructReport(textBuilder, "Отчет о продажах", "Продано 1000 единиц", "Конец отчета");
        Report textReport = textBuilder.GetReport();
        textReport.Show();

        Console.WriteLine("HTML ОТЧЕТ");
        HtmlReportBuilder htmlBuilder = new HtmlReportBuilder();
        director.ConstructReport(htmlBuilder, "Продажи", "Товар 500 шт", "2026 год");
        Report htmlReport = htmlBuilder.GetReport();
        htmlReport.Show();

        Console.WriteLine("XML ОТЧЕТ");
        XmlReportBuilder xmlBuilder = new XmlReportBuilder();
        director.ConstructReport(xmlBuilder, "Sales", "Items 500", "End");
        Report xmlReport = xmlBuilder.GetReport();
        xmlReport.Show();

        Console.WriteLine("ИЗМЕНЕННЫЙ ОТЧЕТ");
        Report changedReport = htmlBuilder.GetReport();
        changedReport.Content = "<p>Обновленные данные: 750 шт</p>";
        changedReport.Show();
    }
}
