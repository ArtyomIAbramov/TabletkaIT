
using System;

namespace tabletka2.Data
{
    [Serializable]
    public class User
    {
        public string username;
        public string email;
        public string userpassword;
        public User() { }
        public User(string username, string email, string userpassword)
        {
            this.email = email;
            this.username = username;
            this.userpassword = userpassword;
        }
    }
}
