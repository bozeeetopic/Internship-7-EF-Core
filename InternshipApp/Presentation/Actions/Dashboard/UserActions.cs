using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Presentation.Actions.Dashboard
{
    public static class UserActions
    {
        public static void ChooseUser()
        {
                var chosenUser = Reader.UserNumberInput(" redni broj korisnika", 1, Users.UsersList.Count);

                Users.SearchedUser = Users.UsersList[chosenUser];

            while (true)
            {
                List<Template> actions = new()
                {
                    new() { Status = InputStatus.WaitingForInput, Name = "Aktiviraj korisnika", Function = () => ActivateUser(DateTime.Now) },
                    new() { Status = InputStatus.WaitingForInput, Name = "Deaktiviraj korisnika", Function = () => ActivateUser(Reader.UserDateInput()) },
                    new() { Status = InputStatus.WaitingForInput, Name = "Povratak u korisnike", Function = () => DashboardActions.Users() },
                    new() { Status = InputStatus.WaitingForInput, Name = "Povratak u meni", Function = () => ActionsCaller.PrintMenuAndDoAction(DashboardActions.DashboardActionsList) }
                };
                SetActionCallabilityStatus(actions);

                var userString = Users.UserToString(Users.SearchedUser);
                Console.WriteLine(userString);
                ActionsHelper.GenericMenu(actions);
            }
        }
        public static void SetActionCallabilityStatus(List<Template> actions)
        {
            if(Users.SearchedUser.BannedUntil > DateTime.Now)
            {
                actions.RemoveAt(1);
            }
            else
            {
                actions.RemoveAt(0);
            }

            if(Users.CurrentUser.ReputationPoints < 100000)
            {
                actions.RemoveAt(0);
            }
        }
        public static void EditName()
        {
            var newName = Reader.UserStringInput("novo ime", "", 1);
            Users.SearchedUser.Name = newName;
            UserServices.Edit(Users.SearchedUser);
            Users.CurrentUser = UserServices.ReturnUserById(Users.CurrentUser.Id);
        }
        public static void EditSurname()
        {
            var newSurame = Reader.UserStringInput("novo prezime", "", 1);
            Users.SearchedUser.Surname = newSurame;
            UserServices.Edit(Users.SearchedUser);
            Users.CurrentUser = UserServices.ReturnUserById(Users.CurrentUser.Id);
        }
        public static void EditPassword()
        {
            var newPassword = Reader.UserStringInput("novu šifru", "", 1);
            Users.SearchedUser.Password = newPassword;
            UserServices.Edit(Users.SearchedUser);
            Users.CurrentUser = UserServices.ReturnUserById(Users.CurrentUser.Id);
        }
        public static void EditUsername()
        {
            var newUsername = Reader.UserStringInput("novo korisničko ime", "", 1);
            
            if (UserServices.UserExists(newUsername))
            {
                Console.WriteLine("Postoji korisnik sa unesenim korisničkim imenom!");
                Thread.Sleep(1000);
                return;
            }
            Users.SearchedUser.Username = newUsername;
            UserServices.Edit(Users.SearchedUser);
            Users.CurrentUser = UserServices.ReturnUserById(Users.CurrentUser.Id);

        }
        public static void SearchName()
        {
            var name = Reader.UserStringInput("pretraživano ime", "", 1);
            UserServices.SearchName(name);
        }
        public static void SearchUsername()
        {
            var username = Reader.UserStringInput("pretraživano korisničko ime", "", 1);
            UserServices.SearchUsername(username);
        }
        public static void SearchSurname()
        {
            var surname = Reader.UserStringInput("pretraživano prezime", "", 1);
            UserServices.SearchSurame(surname);
        }
        public static void ActivateUser(DateTime date)
        {
            Users.SearchedUser.BannedUntil = date;
            UserServices.Edit(Users.SearchedUser);
        }
    }
}
