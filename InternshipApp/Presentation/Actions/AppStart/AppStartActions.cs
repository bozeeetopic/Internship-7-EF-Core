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
            List<Template> InputsList = new()
            {   
                new(){ Status = InputStatus.WaitingForInput,Name= "Unos korisničkog imena", Function = null},
                new() { Status = InputStatus.Error, Name = "Unos šifre", Function = null},
                new() { Status = InputStatus.Error, Name = "Login", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) },
                new() { Status = InputStatus.WaitingForInput, Name = "Izlaz", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions) }
            };
            InputsList[0].Function = () => LoginActions.UsernameInput(InputsList);
            InputsList[1].Function = () => LoginActions.PasswordInput(InputsList);

            ActionsHelper.GenericMenu(InputsList);
        }
        public static void Register()
        {
            List<Template> InputsList = new()
            {
                new() { Status = InputStatus.WaitingForInput, Name = "Unos korisničkog imena", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos šifre", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos imena", Function = null },
                new() { Status = InputStatus.WaitingForInput, Name = "Unos prezimena", Function = null },
                new() { Status = InputStatus.Error, Name = "Register", Function = () => RegisterActions.Register() },
                new() { Status = InputStatus.WaitingForInput, Name = "Izlaz", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions) }
            };
            InputsList[0].Function = () => RegisterActions.UsernameInput(InputsList);
            InputsList[1].Function = () => RegisterActions.SetPassword(InputsList);
            InputsList[2].Function = () => RegisterActions.SetName(InputsList);
            InputsList[3].Function = () => RegisterActions.SetSurname(InputsList);

            CurrentUser.User = new();
            ActionsHelper.GenericMenu(InputsList);
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
