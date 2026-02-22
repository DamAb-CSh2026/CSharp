using System;
using System.Collections.Generic;
using System.Linq;

namespace LibrarySimple
{
    public class Book
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string ISBN { get; set; }
        public int Count { get; set; } 
        public int Available { get; set; } 
        public Book(string title, string author, string isbn, int count)
        {
            Title = title;
            Author = author;
            ISBN = isbn;
            Count = count;
            Available = count;
        }

        public bool CanBorrow() => Available > 0;

        public void BorrowOne() => Available--;

        public void ReturnOne() => Available++;

        public override string ToString()
        {
            return $"{Title} - {Author} (в наличии: {Available}/{Count})";
        }
    }

    public class Reader
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public List<Book> Books { get; set; } = new List<Book>();

        public Reader(string name)
        {
            Name = name;
            Id = Guid.NewGuid().ToString();
        }

        public override string ToString()
        {
            return $"{Name} (книг: {Books.Count})";
        }
    }

    public class Library
    {
        private List<Book> books = new List<Book>();
        private List<Reader> readers = new List<Reader>();

        public void AddBook(Book book)
        {
            books.Add(book);
            Console.WriteLine($"Книга добавлена: {book.Title}");
        }

        public void RemoveBook(string title)
        {
            var book = books.FirstOrDefault(b => b.Title == title);
            if (book != null && book.Available == book.Count)
            {
                books.Remove(book);
                Console.WriteLine($"Книга удалена: {title}");
            }
            else
            {
                Console.WriteLine("Нельзя удалить книгу (есть на руках)");
            }
        }

        public Book FindBook(string title)
            => books.FirstOrDefault(b => b.Title.ToLower().Contains(title.ToLower()));

        public void AddReader(Reader reader)
        {
            readers.Add(reader);
            Console.WriteLine($"Читатель добавлен: {reader.Name}");
        }

        public Reader FindReader(string name)
            => readers.FirstOrDefault(r => r.Name.ToLower().Contains(name.ToLower()));

        public void BorrowBook(string readerName, string bookTitle)
        {
            var reader = FindReader(readerName);
            var book = FindBook(bookTitle);

            if (reader == null || book == null)
            {
                Console.WriteLine("Читатель или книга не найдены");
                return;
            }

            if (!book.CanBorrow())
            {
                Console.WriteLine("Книга недоступна");
                return;
            }

            book.BorrowOne();
            reader.Books.Add(book);
            Console.WriteLine($"Книга '{bookTitle}' выдана {readerName}");
        }

        public void ReturnBook(string readerName, string bookTitle)
        {
            var reader = FindReader(readerName);
            var book = reader?.Books.FirstOrDefault(b => b.Title == bookTitle);

            if (reader == null || book == null)
            {
                Console.WriteLine("У читателя нет такой книги");
                return;
            }

            book.ReturnOne();
            reader.Books.Remove(book);
            Console.WriteLine($"Книга '{bookTitle}' возвращена");
        }

        public void ShowAllBooks()
        {
            Console.WriteLine("\n=== КНИГИ В БИБЛИОТЕКЕ ===");
            foreach (var book in books)
                Console.WriteLine(book);
        }

        public void ShowAllReaders()
        {
            Console.WriteLine("\n=== ЧИТАТЕЛИ ===");
            foreach (var reader in readers)
            {
                Console.Write(reader + " - книги: ");
                if (reader.Books.Any())
                    Console.WriteLine(string.Join(", ", reader.Books.Select(b => b.Title)));
                else
                    Console.WriteLine("нет книг");
            }
        }
    }

    class Program
    {
        static void Main()
        {
            var library = new Library();

            Console.WriteLine("=== ДОБАВЛЕНИЕ КНИГ ===");
            library.AddBook(new Book("Война и мир", "Толстой", "123", 3));
            library.AddBook(new Book("Преступление и наказание", "Достоевский", "456", 2));
            library.AddBook(new Book("Мастер и Маргарита", "Булгаков", "789", 1));

            Console.WriteLine("\n=== ДОБАВЛЕНИЕ ЧИТАТЕЛЕЙ ===");
            library.AddReader(new Reader("Иван Петров"));
            library.AddReader(new Reader("Мария Сидорова"));

            library.ShowAllBooks();

            Console.WriteLine("\n=== ВЫДАЧА КНИГ ===");
            library.BorrowBook("Иван Петров", "Война и мир");
            library.BorrowBook("Иван Петров", "Преступление и наказание");
            library.BorrowBook("Мария Сидорова", "Мастер и Маргарита");

            Console.WriteLine("\n=== ПОПЫТКА ВЗЯТЬ НЕДОСТУПНУЮ КНИГУ ===");
            library.BorrowBook("Иван Петров", "Мастер и Маргарита");

            library.ShowAllReaders();

            Console.WriteLine("\n=== ДОСТУПНЫЕ КНИГИ ПОСЛЕ ВЫДАЧИ ===");
            library.ShowAllBooks();

            Console.WriteLine("\n=== ВОЗВРАТ КНИГ ===");
            library.ReturnBook("Иван Петров", "Война и мир");
            library.ReturnBook("Мария Сидорова", "Мастер и Маргарита");

            Console.WriteLine("\n=== ИТОГ ===");
            library.ShowAllBooks();
            library.ShowAllReaders();

            Console.WriteLine("\n=== УДАЛЕНИЕ КНИГИ ===");
            library.RemoveBook("Война и мир");
            library.ShowAllBooks();

            Console.WriteLine("\nНажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}