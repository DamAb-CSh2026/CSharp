using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T9
{
    abstract class FileSystemComponent
    {
        public string Name;

        public FileSystemComponent(string name)
        {
            Name = name;
        }

        public abstract void Display(int space);
        public abstract int GetSize();
    }
    class File : FileSystemComponent
    {
        int size;

        public File(string name, int size) : base(name)
        {
            this.size = size;
        }

        public override void Display(int space)
        {
            Console.WriteLine(new string(' ', space) + Name + " (" + size + " KB)");
        }

        public override int GetSize()
        {
            return size;
        }
    }
    class Directory : FileSystemComponent
    {
        List<FileSystemComponent> items = new List<FileSystemComponent>();

        public Directory(string name) : base(name) { }

        public void Add(FileSystemComponent c)
        {
            if (!items.Contains(c))
                items.Add(c);
            else
                Console.WriteLine("Exist: " + c.Name);
        }

        public void Remove(FileSystemComponent c)
        {
            if (items.Contains(c))
                items.Remove(c);
            else
                Console.WriteLine("Don't exist: " + c.Name);
        }

        public override void Display(int space)
        {
            Console.WriteLine(new string(' ', space) + "[" + Name + "]");

            foreach (var i in items)
                i.Display(space + 2);
        }

        public override int GetSize()
        {
            int sum = 0;
            foreach (var i in items)
                sum += i.GetSize();
            return sum;
        }
    }
    class Program
    {
        static void Main()
        {
            var root = new Directory("Root");
            var folder = new Directory("Docs");

            var f1 = new File("a.txt", 100);
            var f2 = new File("b.txt", 200);

            root.Add(f1);
            root.Add(folder);
            folder.Add(f2);

            folder.Add(f2);     
            root.Remove(f2);    

            Console.WriteLine("Structure:");
            root.Display(0);

            Console.WriteLine("\n Root Size: " + root.GetSize() + " KB");
        }
    }
}
