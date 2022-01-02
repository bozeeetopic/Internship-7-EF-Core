using System;
using System.Media;
using System.Threading;
using Presentation.Helpers;

namespace Presentation.Actions.AppStart
{
    public class AppStartAction
    {
        public static Action Login() { return null; }
        public static Action Register() { return null; }
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
            return null; 
        }
    }
}
