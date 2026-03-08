using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pr7
{
    internal class Class2
    {

    interface IMediator
    {
        void SendMessage(string message, User user);
        void AddUser(User user);
    }

    class ChatRoom : IMediator
    {
        List<User> users = new List<User>();

        public void AddUser(User user)
        {
            users.Add(user);
            Console.WriteLine(user.Name + " присоединился к чату");
        }

        public void SendMessage(string message, User sender)
        {
            foreach (var user in users)
            {
                if (user != sender)
                    user.Receive(message, sender);
            }
        }
    }

    class User
    {
        IMediator chat;
        public string Name;

        public User(string name, IMediator mediator)
        {
            Name = name;
            chat = mediator;
        }

        public void Send(string message)
        {
            chat.SendMessage(message, this);
        }

        public void Receive(string message, User sender)
        {
            Console.WriteLine(sender.Name + ": " + message + " -> " + Name);
        }
    }

    class Program
    {
        static void Main()
        {
            ChatRoom chat = new ChatRoom();

            User u1 = new User("Али", chat);
            User u2 = new User("Бек", chat);
            User u3 = new User("Сара", chat);

            chat.AddUser(u1);
            chat.AddUser(u2);
            chat.AddUser(u3);

            u1.Send("Привет всем!");
            u2.Send("Привет!");
        }
    }
}
}
