using System;
using System.Media;
using System.Threading;
using Presentation.Helpers;
using Presentation.Actions.ActionHelpers;
using Presentation.Enums;
using System.Collections.Generic;
using Domain.Repositories;
using Domain.Factories;
using Domain.Models;
using System.Linq;

namespace Presentation.Actions.AppStart
{
    public static class AppStartAction
    {
        public static void Login()
        {
            List<Template> InputsList = new()
            { new(){ Status = InputStatus.WaitingForInput,Name= "Unos korisničkog imena", Function = null },
                new() { Status = InputStatus.Error, Name = "Unos šifre", Function = null},
                new() { Status = InputStatus.Error, Name = "Login", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.DashboardActions) },
                new() { Status = InputStatus.WaitingForInput, Name = "Izlaz", Function = () => ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions) }
            };

            static void LoginSwitch(List<Template> actions, int index)
            {
                switch (index)
                {
                    case 0:
                        LoginActions.UsernameInput(actions);
                        break;
                    case 1:
                        LoginActions.PasswordInput(actions);
                        break;
                    default:
                        Console.Clear();
                        actions[index].Function();
                        break;
                }
            }

            ActionsHelper.GenericMenu(InputsList, LoginSwitch);
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

            static void LoginSwitch(List<Template> actions, int index)
            {
                switch (index)
                {
                    case 0:
                        RegisterActions.UsernameInput(actions);
                        break;
                    case 1:
                        CurrentUser.User.Password = RegisterActions.UserPropertiesInput(actions, index);
                        break;
                    case 2:
                        CurrentUser.User.Name = RegisterActions.UserPropertiesInput(actions, index);
                        break;
                    case 3:
                        CurrentUser.User.Surname = RegisterActions.UserPropertiesInput(actions, index);
                        break;
                    default:
                        Console.Clear();
                        actions[index].Function();
                        break;
                }
            }

            CurrentUser.User = new();
            ActionsHelper.GenericMenu(InputsList, LoginSwitch);
        }
        public static Action Exit()
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
            return null; 
        }
    }
}
