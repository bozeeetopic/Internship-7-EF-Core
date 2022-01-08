using Domain.Models;
using Domain.Services;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using Presentation.Helpers;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Presentation.Actions.AppStart
{
    public static class LoginActions
    {
        public static void UsernameInput(List<Template> actions)
        {
            var username = Reader.UserStringInput("Unesi korisničko ime", "", 1);
            UserServices.SetCurrentUserWithUsername(username);
            if (Users.CurrentUser is null)
            {
                Console.WriteLine("Ne postoji korisnik sa unesenim korisničkim imenom!");
                Thread.Sleep(1000);
                return;
            }
            if (Users.CurrentUser.BannedUntil > DateTime.Now)
            {
                Console.WriteLine("Korištenje aplikacije je trenutno zabranjeno navedenom korisniku!");
                Thread.Sleep(1000);
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
            if (Users.CurrentUser.Password != password)
            {
                Console.WriteLine("Krivo unesena lozinka!");
                Thread.Sleep(1000);
                return;
            }
            actions[1].Status = InputStatus.Done;
            actions[2].Status = InputStatus.WaitingForInput;
            actions[3].Status = InputStatus.Warning;
            return;
        }
    }

}
