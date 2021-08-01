
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Dapper_Blog.Models
{
    [Table("[User]")]
    public class User
    {
        public User() =>
            _roles = new List<Role>();

        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Bio { get; set; }
        public string Image { get; set; }
        public string Slug { get; set; }

        private List<Role> _roles { get; set; }
        
        //Significa que eu não quero que o Dapper inclua a lista de Roles no Insert e Update
        [Write(false)]
        public IReadOnlyCollection<Role> Roles { get => _roles; }

        public void AddRoule(Role role)
        {
            if (role != null)
                _roles.Add(role);
        }
    }
}