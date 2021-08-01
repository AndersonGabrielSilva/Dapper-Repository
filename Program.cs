using System;
using Dapper_Blog.Models;
using Microsoft.Data.SqlClient;
using Dapper.Contrib.Extensions;
using Dapper_Blog.Repositories;
using System.Collections.Generic;

namespace Dapper_Blog
{

    public class Program
    {
        private const string CONNECTION_STRING = @"Server=(localdb)\MSSQLLocalDB;DataBase=Blog;Integrated Security=true;";

        private static Repository<User> repository { get; set; }

        public static void Main(string[] args)
        {
            // DataBase.Connection = new SqlConnection(CONNECTION_STRING);
            using var connection = new SqlConnection(CONNECTION_STRING);
            var repository = new Repository<User>(connection);

            // CreateUser(repository);
            // UpdateUser(repository);
            // DeleteUser(repository);
            // ReadUser(repository);
            // ReadUsers(repository);
            ReadWithRoles(connection);

            Console.ReadKey();
        }
        private static void CreateUser(Repository<User> repository)
        {
            var user = new User
            {
                Bio = "8x Microsoft MVP",
                Email = "andre@balta.io",
                Image = "https://balta.io/andrebaltieri.jpg",
                Name = "André Baltieri",
                Slug = "andre-baltieri",
                PasswordHash = Guid.NewGuid().ToString()
            };

            repository.Create(user);
        }

        private static void ReadUsers(Repository<User> repository)
        {
            var users = repository.Read();
            foreach (var item in users)
                Console.WriteLine(item.Name);
        }

        private static void ReadUser(Repository<User> repository)
        {
            var user = repository.Read(2);
            Console.WriteLine(user?.Email);
        }

        private static void UpdateUser(Repository<User> repository)
        {
            var user = repository.Read(2);
            user.Email = "hello@balta.io";
            repository.Update(user);

            Console.WriteLine(user?.Email);
        }

        private static void DeleteUser(Repository<User> repository)
        {
            var user = repository.Read(2);
            repository.Delete(user);
        }

        private static void ReadWithRoles(SqlConnection connection)
        {
            var repository = new UserRepository(connection);
            var users = repository.ReadWithRole();

            foreach (var user in users)
            {
                Console.WriteLine(user.Email);
                foreach (var role in user.Roles)
                     Console.WriteLine($" - {role.Slug}");

                     short.Parse(Console.ReadLine()!);
            }
        }

    }
}
