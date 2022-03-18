using System;

namespace RnAds.Model.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }

        public string Role { get; set; }
    }
    public class AvatarUser
    {
        public Guid Id { get; set; }

        //фотография профиля пользователя
        public string FilePath { get; set; }

        public string UserId { get; set; }
        public User User { get; set; }
    }
}
