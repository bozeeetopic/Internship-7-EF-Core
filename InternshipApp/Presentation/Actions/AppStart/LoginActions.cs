using Domain.Factories;
using Domain.Models;
using Domain.Repositories;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Presentation.Actions.AppStart
{
    public static class LoginActions
    {
        public static void UsernameInput(List<Template> actions)
        {
            var username = Reader.UserStringInput("Unesi korisničko ime", "", 1);
            CurrentUser.User = RepositoryFactory.Create<MemberBase>().GetAll()
                .FirstOrDefault(p => p.Username == username);
            if (CurrentUser.User is null)
            {
                Console.WriteLine("Ne postoji korisnik sa unesenim korisničkim imenom!");
                return;
            }
            if (CurrentUser.User.BannedUntil > DateTime.Now)
            {
                Console.WriteLine("Korištenje aplikacije je trenutno zabranjeno navedenom korisniku!");
                return;
            }
            actions[0].Status = InputStatus.Done;
            actions[1].Status = InputStatus.WaitingForInput;
            actions[3].Status = InputStatus.Warning;
            return;
        }
        public static void PasswordInput(List<Template> actions)
        {
            var password = Reader.UserStringInput("lozinku", "", 0);
            if (CurrentUser.User.Password != password)
            {
                Console.WriteLine("Krivo unesena lozinka!");
                return;
            }
            actions[1].Status = InputStatus.Done;
            actions[2].Status = InputStatus.WaitingForInput;
            actions[3].Status = InputStatus.Warning;
            return;
        }
    }

}
