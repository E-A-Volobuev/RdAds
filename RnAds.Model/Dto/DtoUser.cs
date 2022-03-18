
using System;

namespace RnAds.Model.Dto
{
    public  class DtoUser
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
    }
}
