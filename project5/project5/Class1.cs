using System;
using System.Collections.Generic;

namespace BuilderPattern
{
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
            Console.WriteLine(new string('-', 50));
        }
    }

    public interface IReportBuilder
    {
        IReportBuilder SetHeader(string header);
        IReportBuilder SetContent(string content);
        IReportBuilder SetFooter(string footer);
        Report GetReport();
    }


    public class TextReportBuilder : IReportBuilder
    {
        private Report _report;

        public TextReportBuilder()
        {
            _report = new Report();
        }

        public IReportBuilder SetHeader(string header)
        {
            _report.Header = $"=== {header} ===\n";
            return this;
        }

        public IReportBuilder SetContent(string content)
        {
            _report.Content = $"{content}\n";
            return this;
        }

        public IReportBuilder SetFooter(string footer)
        {
            _report.Footer = $"-- {footer} --\n";
            return this;
        }

        public Report GetReport()
        {
            return _report;
        }
    }

    public class HtmlReportBuilder : IReportBuilder
    {
        private Report _report;

        public HtmlReportBuilder()
        {
            _report = new Report();
        }

        public IReportBuilder SetHeader(string header)
        {
            _report.Header = $"<h1>{header}</h1>\n";
            return this;
        }

        public IReportBuilder SetContent(string content)
        {
            _report.Content = $"<p>{content}</p>\n";
            return this;
        }

        public IReportBuilder SetFooter(string footer)
        {
            _report.Footer = $"<footer>{footer}</footer>\n";
            return this;
        }

        public Report GetReport()
        {
            return _report;
        }
    }

    public class XmlReportBuilder : IReportBuilder
    {
        private Report _report;

        public XmlReportBuilder()
        {
            _report = new Report();
        }

        public IReportBuilder SetHeader(string header)
        {
            _report.Header = $"<header>{header}</header>\n";
            return this;
        }

        public IReportBuilder SetContent(string content)
        {
            _report.Content = $"<content>{content}</content>\n";
            return this;
        }

        public IReportBuilder SetFooter(string footer)
        {
            _report.Footer = $"<footer>{footer}</footer>\n";
            return this;
        }

        public Report GetReport()
        {
            Report xmlReport = new Report
            {
                Header = "<report>\n" + _report.Header,
                Content = _report.Content,
                Footer = _report.Footer + "</report>"
            };
            return xmlReport;
        }
    }

    public class PdfStyleReportBuilder : IReportBuilder
    {
        private Report _report;
        private Dictionary<string, string> _styles;

        public PdfStyleReportBuilder()
        {
            _report = new Report();
            _styles = new Dictionary<string, string>();
        }

        public IReportBuilder SetHeader(string header)
        {
            _report.Header = $"[PDF HEADER] {header} [PDF HEADER]\n";
            return this;
        }

        public IReportBuilder SetContent(string content)
        {
            _report.Content = $"[PDF CONTENT] {content} [PDF CONTENT]\n";
            return this;
        }

        public IReportBuilder SetFooter(string footer)
        {
            _report.Footer = $"[PDF FOOTER] {footer} [PDF FOOTER]\n";
            return this;
        }

        public PdfStyleReportBuilder SetStyle(string element, string style)
        {
            _styles[element] = style;
            return this;
        }

        public Report GetReport()
        {
            if (_styles.Count > 0)
            {
                _report.Content += "\n[Styles applied: " + string.Join(", ", _styles) + "]\n";
            }
            return _report;
        }
    }

    public class ReportDirector
    {
        public void ConstructFullReport(IReportBuilder builder, string title, string content, string footer)
        {
            builder.SetHeader(title)
                   .SetContent(content)
                   .SetFooter(footer);
        }

        public void ConstructReportWithoutFooter(IReportBuilder builder, string title, string content)
        {
            builder.SetHeader(title)
                   .SetContent(content);
        }

        public void ConstructMinimalReport(IReportBuilder builder, string content)
        {
            builder.SetContent(content);
        }

        public Report UpdateReport(Report report, string newContent)
        {
            report.Content = newContent;
            return report;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== ПАТТЕРН BUILDER (Строитель) ===\n");

            var director = new ReportDirector();

            Console.WriteLine("1. Текстовый отчет (полный):");
            var textBuilder = new TextReportBuilder();
            director.ConstructFullReport(textBuilder,
                "Годовой отчет",
                "Прибыль компании составила 1 млн рублей",
                "Отчет подготовлен 01.01.2024");

            var textReport = textBuilder.GetReport();
            textReport.Show();

            Console.WriteLine("2. HTML-отчет (без подвала):");
            var htmlBuilder = new HtmlReportBuilder();
            director.ConstructReportWithoutFooter(htmlBuilder,
                "Новости компании",
                "Запущен новый проект");

            var htmlReport = htmlBuilder.GetReport();
            htmlReport.Show();

            Console.WriteLine("3. XML-отчет:");
            var xmlBuilder = new XmlReportBuilder();
            director.ConstructFullReport(xmlBuilder,
                "Конфигурация",
                "Настройки приложения",
                "Версия 1.0");

            var xmlReport = xmlBuilder.GetReport();
            xmlReport.Show();

            Console.WriteLine("4. PDF-отчет со стилями:");
            var pdfBuilder = new PdfStyleReportBuilder();
            pdfBuilder.SetHeader("Документ")
                     .SetContent("Основной текст документа")
                     .SetFooter("Страница 1")
                     .SetStyle("header", "bold")
                     .SetStyle("content", "italic");

            var pdfReport = pdfBuilder.GetReport();
            pdfReport.Show();

            Console.WriteLine("5. Минимальный отчет (только содержимое):");
            var minimalBuilder = new TextReportBuilder();
            director.ConstructMinimalReport(minimalBuilder, "Просто текст");

            var minimalReport = minimalBuilder.GetReport();
            minimalReport.Show();

            Console.WriteLine("6. Динамическое изменение отчета:");
            var dynamicBuilder = new HtmlReportBuilder();
            director.ConstructFullReport(dynamicBuilder,
                "Исходный отчет",
                "Старое содержимое",
                "Оригинальный подвал");

            var dynamicReport = dynamicBuilder.GetReport();
            Console.WriteLine("ДО ИЗМЕНЕНИЯ:");
            dynamicReport.Show();

            director.UpdateReport(dynamicReport, "НОВОЕ СОДЕРЖИМОЕ (изменено динамически)");
            Console.WriteLine("ПОСЛЕ ИЗМЕНЕНИЯ:");
            dynamicReport.Show();

            Console.WriteLine("7. Создание отчета через Fluent Interface:");
            var fluentReport = new TextReportBuilder()
                .SetHeader("Fluent Interface")
                .SetContent("Отчет создан цепочкой методов")
                .SetFooter("Строитель в действии")
                .GetReport();

            fluentReport.Show();

            Console.WriteLine("Все тесты завершены!");
            Console.ReadLine();
        }
    }
}