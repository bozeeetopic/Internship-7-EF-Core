using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Presentation.Actions.User
{
    public static class UserActions
    {
        public static void ChooseUser()
        {
                var chosenUser = Reader.UserNumberInput("Unesi redni broj korisnika", 1, CurrentUser.Users.Count);

                CurrentUser.SearchedUser = CurrentUser.Users[chosenUser];

            while (true)
            {
                List<Template> actions = new()
                {
                    new() { Status = InputStatus.WaitingForInput, Name = "Aktiviraj korisnika", Function = () => ActivateUser(DateTime.Now) },
                    new() { Status = InputStatus.WaitingForInput, Name = "Deaktiviraj korisnika", Function = () => ActivateUser(Reader.UserDateInput()) },
                    new() { Status = InputStatus.WaitingForInput, Name = "Povratak u korisnike", Function = () => Dashboard.DashboardActions.Users() },
                    new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) }
                };
                SetActionCallabilityStatus(actions);

                var userString = CurrentUser.UserToString(CurrentUser.SearchedUser);
                Console.WriteLine(userString);
                ActionsHelper.GenericMenu(actions);
            }
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            if(CurrentUser.SearchedUser.BannedUntil > DateTime.Now)
            {
                actions.RemoveAt(1);
            }
            else
            {
                actions.RemoveAt(0);
            }

            if(CurrentUser.User.ReputationPoints < 100000)
            {
                actions.RemoveAt(0);
            }
        }
        public static void EditName()
        {
            var newName = Reader.UserStringInput("novo ime", "", 1);
            CurrentUser.SearchedUser.Name = newName;
            RepositoryFactory
                .Create<MemberBase>().Edit(CurrentUser.SearchedUser, CurrentUser.SearchedUser.Id);
        }
        public static void EditSurname()
        {
            var newSurame = Reader.UserStringInput("novo prezime", "", 1);
            CurrentUser.SearchedUser.Surname = newSurame;
            RepositoryFactory
                .Create<MemberBase>().Edit(CurrentUser.SearchedUser, CurrentUser.SearchedUser.Id);
        }
        public static void EditPassword()
        {
            var newPassword = Reader.UserStringInput("novu šifru", "", 1);
            CurrentUser.SearchedUser.Password = newPassword;
            RepositoryFactory
                .Create<MemberBase>().Edit(CurrentUser.SearchedUser, CurrentUser.SearchedUser.Id);
        }
        public static void EditUsername()
        {
            var newUsername = Reader.UserStringInput("novo korisničko ime", "", 1);
            var users = RepositoryFactory.Create<MemberBase>().GetAll()
                .FirstOrDefault(p => p.Username == newUsername);
            if (users is not null)
            {
                Console.WriteLine("Postoji korisnik sa unesenim korisničkim imenom!");
                Thread.Sleep(1000);
                return;
            }
            CurrentUser.SearchedUser.Username = newUsername;
            RepositoryFactory
                .Create<MemberBase>().Edit(CurrentUser.SearchedUser, CurrentUser.SearchedUser.Id);
        }
        public static void SearchName()
        {
            var surname = Reader.UserStringInput("pretraživano ime", "", 1);
            CurrentUser.Users = CurrentUser.Users.Where(n => n.Name.Contains(surname)).ToList();
        }
        public static void SearchUsername()
        {
            var username = Reader.UserStringInput("pretraživano korisničko ime", "", 1);
            CurrentUser.Users = CurrentUser.Users.Where(n => n.Username.Contains(username)).ToList();
        }
        public static void SearchSurname()
        {
            var surname = Reader.UserStringInput("pretraživano prezime", "", 1);
            CurrentUser.Users = CurrentUser.Users.Where(n => n.Surname.Contains(surname)).ToList();
        }
        public static void ActivateUser(DateTime date)
        {
            CurrentUser.SearchedUser.BannedUntil = date;
            RepositoryFactory
                .Create<MemberBase>().Edit(CurrentUser.SearchedUser, CurrentUser.SearchedUser.Id);
        }
    }
}
