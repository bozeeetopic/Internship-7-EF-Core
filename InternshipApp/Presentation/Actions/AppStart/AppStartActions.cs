using System;
using System.Media;
using System.Threading;
using Presentation.Helpers;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using System.Collections.Generic;
using Domain.Models;

namespace Presentation.Actions.AppStart
{
    public static class AppStartAction
    {
        public static void Login()
        {
            List<Template> actions = new()
            {   
                new(){ Status = InputStatus.WaitingForInput,Name= "Unos korisničkog imena", Function = null},
                new() { Status = InputStatus.Error, Name = "Unos šifre", Function = null},
                new() { Status = InputStatus.Error, Name = "Login", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) },
                new() { Status = InputStatus.WaitingForInput, Name = "Izlaz", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions) }
            };
            actions[0].Function = () => LoginActions.UsernameInput(actions);
            actions[1].Function = () => LoginActions.PasswordInput(actions);

            ActionsHelper.GenericMenuAndMessage(actions,"");
        }
        public static void Register()
        {
            List<Template> actions = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos korisničkog imena", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos šifre", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos imena", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos prezimena", Function = null },
                new() { Status = InputStatus.Error, Name = "Register", Function = () => RegisterActions.Register() },
                new() { Status = InputStatus.WaitingForInput, Name = "Izlaz", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions) }
            };
            actions[0].Function = () => RegisterActions.UsernameInput(actions);
            actions[1].Function = () => RegisterActions.SetPassword(actions);
            actions[2].Function = () => RegisterActions.SetName(actions);
            actions[3].Function = () => RegisterActions.SetSurname(actions);

            CurrentUser.User = new();
            ActionsHelper.GenericMenuAndMessage(actions,"");
        }
        public static void Exit()
        {
            var message = "Izašli ste iz aplikacije...";
            SoundPlayer amogusSound = new();
            amogusSound.SoundLocation = Environment.CurrentDirectory + "/../../../Helpers/Intro/AmongUs.wav";
            amogusSound.Play();
            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t");
            foreach (var character in message)
            {
                ConsoleHelpers.WriteInColor("" + character, ConsoleColor.Red);
                Thread.Sleep(100);
            }
            Thread.Sleep(3000);
            Environment.Exit(0);
        }
    }
}
