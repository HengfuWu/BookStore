using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStoreWithImage.Models
{
    public class UserProvider
    {
        public static UserProvider Instance { get; private set; }
        public List<User> AllUser { get; private set; }
        static UserProvider()
        {
            Instance = new UserProvider();
            Instance.AllUser = new List<User>();

            for (int i = 1; i <= 10; i++)
            {
                User user = new User() { Id = i, Name = String.Format("User {0}", i) };
                Instance.AllUser.Add(user);
            }
        }
    }
}