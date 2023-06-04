
using System;

namespace tabletka2.Data
{
    [Serializable]
    public class User
    {
        public string username;
        public string email;
        public string userpassword;
        public string ID;
        public User() { }
        public User(string username, string email, string userpassword, string ID )
        {
            this.email = email;
            this.username = username;
            this.userpassword = userpassword;
            this.ID = ID;
        }
    }
}
