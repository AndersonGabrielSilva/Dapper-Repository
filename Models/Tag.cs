using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace Dapper_Blog.Models
{
    [Table("[Tag]")]
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        
        //Significa que eu não quero que o Dapper inclua a lista de Roles no Insert e Update
        [Write(false)]
        public List<Post> Posts { get; set; }
    }
}