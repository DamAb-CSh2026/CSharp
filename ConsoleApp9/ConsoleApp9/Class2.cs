using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp9
{
    interface IMediator
    {
        void SendMessage(string message, User sender);
        void SendPrivateMessage(string message, User sender, string receiverName);
        void AddUser(User user);
        void RemoveUser(User user);
    }

    class ChatRoom : IMediator
    {
        private List<User> users = new List<User>();

        public void AddUser(User user)
        {
            users.Add(user);
            user.SetMediator(this);
            Console.WriteLine($"{user.Name} joined the chat");
        }

        public void RemoveUser(User user)
        {
            users.Remove(user);
            Console.WriteLine($"{user.Name} left the chat");
        }

        public void SendMessage(string message, User sender)
        {
            if (!users.Contains(sender))
            {
                Console.WriteLine("Error: user is not in the chat");
                return;
            }

            foreach (var user in users)
            {
                if (user != sender)
                {
                    user.Receive(message, sender.Name);
                }
            }
        }

        public void SendPrivateMessage(string message, User sender, string receiverName)
        {
            if (!users.Contains(sender))
            {
                Console.WriteLine("Error: user is not in the chat");
                return;
            }

            foreach (var user in users)
            {
                if (user.Name == receiverName)
                {
                    user.Receive("(private) " + message, sender.Name);
                    return;
                }
            }

            Console.WriteLine("User not found");
        }
    }

    class User
    {
        public string Name { get; private set; }
        private IMediator mediator;

        public User(string name)
        {
            Name = name;
        }

        public void SetMediator(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public void Send(string message)
        {
            mediator.SendMessage(message, this);
        }

        public void SendPrivate(string message, string receiver)
        {
            mediator.SendPrivateMessage(message, this, receiver);
        }

        public void Receive(string message, string sender)
        {
            Console.WriteLine($"{sender} -> {Name}: {message}");
        }
    }

    class Program
    {
        static void Main()
        {
            ChatRoom chat = new ChatRoom();

            User user1 = new User("Alice");
            User user2 = new User("Bob");
            User user3 = new User("Charlie");

            chat.AddUser(user1);
            chat.AddUser(user2);
            chat.AddUser(user3);

            Console.WriteLine();

            user1.Send("Hello everyone!");
            user2.Send("Hi Alice!");

            Console.WriteLine();

            user3.SendPrivate("Hello Bob, this is private", "Bob");

            Console.WriteLine();

            chat.RemoveUser(user3);

            user3.Send("Am I still here?");
        }
    }
}
