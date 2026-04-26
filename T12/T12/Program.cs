using System;

class Program
{
    static void Main()
    {
        Console.Write("Введите название вакансии: ");
        string vacancy = Console.ReadLine();

        Console.Write("HR одобрил заявку? (да/нет): ");
        string approved = Console.ReadLine();

        if (approved.ToLower() != "да")
        {
            Console.WriteLine("Заявка отправлена на доработку руководителю.");
        }
        else
        {
            Console.WriteLine("Вакансия опубликована.");

            Console.Write("Введите имя кандидата: ");
            string candidate = Console.ReadLine();

            Console.Write("Анкета подходит? (да/нет): ");
            string form = Console.ReadLine();

            if (form.ToLower() != "да")
            {
                Console.WriteLine("Анкета отклонена.");
            }
            else
            {
                Console.WriteLine("Кандидат приглашен на собеседование.");

                Console.Write("HR интервью пройдено? (да/нет): ");
                string hr = Console.ReadLine();

                Console.Write("Техническое интервью пройдено? (да/нет): ");
                string tech = Console.ReadLine();

                if (hr.ToLower() == "да" && tech.ToLower() == "да")
                {
                    Console.WriteLine("Кандидату отправлен оффер.");

                    Console.Write("Кандидат принял оффер? (да/нет): ");
                    string offer = Console.ReadLine();

                    if (offer.ToLower() == "да")
                    {
                        Console.WriteLine("Сотрудник добавлен в базу данных.");
                        Console.WriteLine("IT-отдел уведомлен о настройке рабочего места.");
                    }
                    else
                    {
                        Console.WriteLine("Кандидат отказался от оффера.");
                    }
                }
                else
                {
                    Console.WriteLine("Кандидат получил отказ.");
                }
            }
        }
        Console.ReadKey();
    }
}