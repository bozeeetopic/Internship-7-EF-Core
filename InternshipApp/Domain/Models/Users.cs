using Data.Entities.Models;
using System.Collections.Generic;

namespace Domain.Models
{
    public static class Users
    {
        public static Member CurrentUser { get; set; }
        public static Member SearchedUser { get; set; }
        public static List<Member> UsersList { get; set; }
        public static string UserToString(Member member)
        {
            return $"Name: {member.Name}\tSurname: {member.Surname}\tUsername: {member.Username}\tRP: {member.ReputationPoints}\n";
        }
        public static string UserToStringWithPassword(Member member)
        {
            return $"Name: {member.Name}\tSurname: {member.Surname}\tUsername: {member.Username} \tPassword: {member.Password}\tRP: {member.ReputationPoints}\n";
        }
    }
}
