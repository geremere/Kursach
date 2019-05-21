using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KursApp
{
    public class User
    {
        public User()
        {
        }

        public User(int id, string name, string login, string password, string position)
        {
            Id = id;
            Name = name;
            Login = login;
            Password = password;
            Position = position;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Position { get; set; }
        public override string ToString() => $"Name:{Name},\nPosition: {Position}";
    }
}
