using Data.Entities.Models;
using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using System.Linq;

namespace Domain.Services
{
    public class UserServices
    {
        public static void SetCurrentUserWithUsername(string username)
        {
            Users.CurrentUser = RepositoryFactory.Create<MemberBase>().GetAll()
                .FirstOrDefault(p => p.Username == username);
        }
        public static bool UserExists(string username)
        {
            var user = RepositoryFactory.Create<MemberBase>().GetAll()
                .FirstOrDefault(p => p.Username == username);
            if(user is null)
            {
                return false;
            }
            return true;
        }
        public static void RegisterUser()
        {
            Users.CurrentUser.ReputationPoints = 1;
            RepositoryFactory.Create<MemberBase>().Add(Users.CurrentUser);
            Users.CurrentUser = RepositoryFactory
                .Create<MemberBase>()
                .GetAll()
                .Where(u => u.Username == Users.CurrentUser.Username)
                .FirstOrDefault();
        }
        public static int ReturnUsersReputationPoints(int id)
        {
            return RepositoryFactory
                .Create<MemberBase>()
                .GetAll()
                .Where(ci => ci.Id == id)
                .Select(rp => rp.ReputationPoints)
                .FirstOrDefault();
        }
        public static Member ReturnUserById(int id)
        {
            return RepositoryFactory
                .Create<MemberBase>()
                .GetAll()
                .Where(i => i.Id == id)
                .FirstOrDefault();
        }
        public static Member ReturnUserById()
        {
            return RepositoryFactory
                .Create<MemberBase>().GetAll()
                .Where(i => i.Id == Comments.CurrentComment.AuthorId)
                .FirstOrDefault();
        }
        public static void Edit(Member user)
        {
            RepositoryFactory
                .Create<MemberBase>().Edit(user, user.Id);
        }
        public static void SetUsers()
        {
            Users.UsersList = RepositoryFactory
                        .Create<MemberBase>().GetAll().ToList();
        }
        public static void SearchName(string name)
        {
            Users.UsersList = Users.UsersList.Where(n => n.Name.ToLower().Trim().Contains(name)).ToList();
        }
        public static void SearchSurame(string surname)
        {
            Users.UsersList = Users.UsersList.Where(n => n.Surname.ToLower().Trim().Contains(surname)).ToList();
        }
        public static void SearchUsername(string username)
        {
            Users.UsersList = Users.UsersList.Where(n => n.Username.ToLower().Trim().Contains(username)).ToList();
        }

    }
}
