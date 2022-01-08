using System.Collections.Generic;
using System;
using System.Threading;
using System.Media;

namespace Presentation.Helpers.Intro
{
    public static class IntroAnimation
    {
        private static readonly SoundPlayer _amogusSound = new();
        private static readonly string _message = "Intern Bože predstavlja...";
        private static readonly List<string> _welcomeToText = new()
        {
            "                                                                                                                         @@@@@@@@ %@@@@@@@/*@@@@@@@&                         @@@@@@@*                                                                                                         @@@@@@@@                                                                                                                                                   ",
            "                                                                                                                         @@@@@@@@ %@@@@@@@/*@@@@@@@&                         @@@@@@@*                                                                                                         @@@@@@@@                                                                                                                                                   ",
            "                                                                                                                         @@@@@@@@ %@@@@@@@/*@@@@@@@&                         @@@@@@@*                                                                                                         @@@@@@@@                                                                                                                                                   ",
            "                                                                                                                         @@@@@@@@ %@@@@@@@/*@@@@@@@&       &@@@@@@@@@@/      @@@@@@@*      (@@@@@@@@@@(         *@@@@@@@@@@&         ,@@@@@@@@@.*@@@@@@@@@         /@@@@@@@@@@&               @@@@@@@@@@@     .&@@@@@@@@@@,                                                                                                                              ",
            "                                                                                                                         @@@@@@@@ %@@@@@@@/*@@@@@@@&    /@@@@@@@F^&@@@@@@    @@@@@@@*   .@@@@@@@@@@@@@@@@     &@@@@@@@@@@@@@@@*    .@@@@@@@@@@@@@@@@@@@@@@@&     @@@@@@@F^&@@@@@@*            @@@@@@@@@@@   #@@@@@@@@@@@@@@@&                                                                                                                            ",
            "                                                                                                                         @@@@@@@@ %@@@@@@@/*@@@@@@@&   %@@@@@@@,  /@@@@@@@   @@@@@@@*  *@@@@@@@@F^&@@@@@@@,  @@@@@@@@F^&@@@@@@@%   @@@@@@@@@@@@@@@@@@@@@@@@@*  ,@@@@@@@@   @@@@@@@(           @@@@@@@@@@@  &@@@@@@@F^&@@@@@@@@                                                                                                                           ",
            "                                                                                                                         @@@@@@@@ %@@@@@@@/*@@@@@@@&   @@@@@@@(@@@@@@@@@@@/  @@@@@@@*  &@@@@@@@             /@@@@@@@    #@@@@@@@   @@@@@@@# @@@@@@@( @@@@@@@*  #@@@@@@@,@@@@@@@@@@@           @@@@@@@@     @@@@@@@(   *@@@@@@@.                                                                                                                          ",
            "                                                                                                                         @@@@@@@@/@@@@@@@@&&@@@@@@@%   @@@@@@@@              @@@@@@@*  #@@@@@@@%  (@@@@@@@( .@@@@@@@@  (@@@@@@@&   @@@@@@@# @@@@@@@( @@@@@@@*  /@@@@@@@*                      ,@@@@@@@@@#  @@@@@@@@, .@@@@@@@@                                                                                                                           ",
            "                                                                                                                         #@@@@@@@@@@@@@@@@@@@@@@@@@*    @@@@@@@@@@@@@        @@@@@@@*   #@@@@@@@@@@@@@@@@&   .@@@@@@@@@@@@@@@@@    @@@@@@@# @@@@@@@( @@@@@@@*   %@@@@@@@@@@@@,                 /@@@@@@@@@   @@@@@@@@@@@@@@@@@.                                                                                                                           ",
            "                                                                                                                          %@@@@@@@@@@@(@@@@@@@@@@@,       @@@@@@@@@@@@@      @@@@@@@*     #@@@@@@@@@@@@&       *@@@@@@@@@@@@@      @@@@@@@# @@@@@@@( @@@@@@@*     %@@@@@@@@@@@@*                 *@@@@@@@    .@@@@@@@@@@@@@,                                                                                                                             ",
            "                                                                                                                             /&@@@#,     *%@@@#,              ,****                           .****.                ****,                                             .****,                            .         ,***,                                                                                                                                  "
        };
        private static readonly List<string> _internshipAppText = new()
        {
           " @@@@@@@@@@@@@                                 @@@                                                                                      @@@@@@@@@                  @@@@@@@                           ",
           " @@@@@@@@@@@@@                                @@@@                                                                                      @@@@@@@@@*               @@@@@@@@.                           ",
           "  @@@@@@@@@                               @@@@@@@                                                                                       @@@@@@@@*                                                    ",
           "  @@@@@@@@@    @@@@@@@@@  @@@@@@@@@    @@@@@@@@@@@@       #@@@@@@@@@@/      @@@@@@@@  @@@@@  &@@@@@@@@@ /@@@@@@@@@      @@@@@@@@@@@     @@@@@@@@* @@@@@@@@@    @@@@@@@@@@   /@@@@@@@@@ @@@@@@@@@     ",
           "  @@@@@@@@@     @@@@@@@@@@@@@@@@@@@@     @@@@@@@@       @@@@@@    @@@@@      @@@@@@@ @@@@@@@   @@@@@@@@@@@@@@@@@@@@   ,@@@@@   @@@@@    @@@@@@@@@@@@@@@@@@@@     @@@@@@@@     @@@@@@@@@ #@@@@@@@@    ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@      @@@@@@@    @@@@@@     @@@@@@@@@&@@@@@   @@@@@@@@    @@@@@@@@   @@@@@@@   *@@@    @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@   ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@    @@@@@@@@          @@@@@@@@    @@@@@@@@   @@@@@@@@(         @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@   ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@    @@@@@@@@          @@@@@@@@    @@@@@@@@    @@@@@@@@@        @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@@  ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@    @@@@@@@@@@@@@@@@@@@@    @@@@@@@@          @@@@@@@@    @@@@@@@@     @@@@@@@@@       @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@@  ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@    @@@@@@@@@               @@@@@@@@          @@@@@@@@    @@@@@@@@      @@@@@@@@@@     @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@@  ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@    @@@@@@@@@               @@@@@@@@          @@@@@@@@    @@@@@@@@       @@@@@@@@@@    @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@@  ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@     @@@@@@@@               @@@@@@@@          @@@@@@@@    @@@@@@@@         @@@@@@@@    @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@@  ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@     @@@@@@@@       @@@@    @@@@@@@@          @@@@@@@@    @@@@@@@@          @@@@@@@@   @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@   ",
           "  @@@@@@@@@     @@@@@@@@    @@@@@@@@     @@@@@@@@      @@@@@@@       @@@@    @@@@@@@@          @@@@@@@@    @@@@@@@@    @@@/   @@@@@@@   @@@@@@@@*   @@@@@@@@     @@@@@@@@     @@@@@@@@    @@@@@@@@   ",
           "  @@@@@@@@@     @@@@@@@@.   @@@@@@@@     @@@@@@@@       @@@@@@@     @@@@     @@@@@@@@          @@@@@@@@    @@@@@@@@    @@@@@   @@@@@    @@@@@@@@@   @@@@@@@@     @@@@@@@@     @@@@@@@@@@@@@@@@@@@    ",
           "@@@@@@@@@@@@@  @@@@@@@@@@@  @@@@@@@@@    @@@@@@@@@@       @@@@@@@@@@@@      @@@@@@@@@@@      @@@@@@@@@@@   @@@@@@@@@    @@@@@@@@@@@    @@@@@@@@@@@   @@@@@@@@@  @@@@@@@@@@/   @@@@@@@@ @@@@@@@@      ",
           "                                                                                                                                                                              @@@@@@@@               ",
           "                                                                                                                                                                              @@@@@@@@@              ",
           "                                                                                                                                                                            ,@@@@@@@@@@@             ",
           "                                                        ,@@@@@@(                                                                                                                                     ",
           "                                                 @@@@@@@@@%,.(&@@@@@@/                                                                                                                               ",
           "                                              @@@@/,               ,@@@@                                                                                                                             ",
           "                                            @@@@,                    ,@@@                                                                                                                            ",
           "                                           @@@@@@@@@@@@@@@@%,,.      ,,@@@                                                                                                                           ",
           "                                        @@@@&##(*,,,,,*(##%@@@@@,     ,,@@@                                                                                                                          ",
           "                                      @@@%#,          ,,,,,,##@@@     ,,@@@           .@@@@@@@@@  @@@@@@@      @@@@@@@@@@ *@@@@@@&                                                                   ",
           "                                     @@@#,,,,,,,,,,,,,,,,,,####@@.    ,,,@@,,,         #@@@@@@@@@@@@@@@@@@@     @@@@@@@@@@@@@@@@@@@*                                                                 ",
           "                                     @@###,,,,,,,,,,,,,,,######@@     ,,,@@@@@@@@@@     @@@@@@@@   #@@@@@@@@    @@@@@@@@@   @@@@@@@@                                                                 ",
           "                                     @@@######################@@@     ,,,@@@,    #@@    @@@@@@@@    @@@@@@@@    @@@@@@@@@   @@@@@@@@@                                                                ",
           "                                       @@@@#################&@@@      ,,,@@@,,,, ,@@&   @@@@@@@@    @@@@@@@@%   @@@@@@@@@   @@@@@@@@@                                                                ",
           "                                       .@@@@@@@@@@@@@@@@@@@@@&        ,,,@@@,,,,,,@@@   @@@@@@@@    @@@@@@@@@   @@@@@@@@@   @@@@@@@@@                                                                ",
           "                                       .@@,                          ,,,,@@@,,,,,,@@@   @@@@@@@@    @@@@@@@@@   @@@@@@@@@   @@@@@@@@@                                                                ",
           "                                       &@@,                          ,,,,@@@,,,,,,@@@   @@@@@@@@    @@@@@@@@@   @@@@@@@@@   @@@@@@@@@                                                                ",
           "                                       @@@,,                        ,,,,,@@@,,,,,,@@@   @@@@@@@@    @@@@@@@@&   @@@@@@@@@   @@@@@@@@@                                                                ",
           "                                       @@@,,                       ,,,,,,@@@,,,,,,/@@   @@@@@@@@    @@@@@@@@    @@@@@@@@@   @@@@@@@@@                                                                ",
           "                                       @@@,,,,                    ,,,,,,,@@@,,,,,,#@@   @@@@@@@@    @@@@@@@@    @@@@@@@@@   @@@@@@@@@                                                                ",
           "                                       @@@,,,,,,,.           .,,,,,,,,,,,@@@,,,,,,@@@   @@@@@@@@@  @@@@@@@@     @@@@@@@@@*  @@@@@@@@                                                                 ",
           "                                       .@@.,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,@@@,,,,,,@@#   @@@@@@@@%@@@@@@@@@      @@@@@@@@@@@@@@@@@@@                                                                  ",
           "                                        @@@,,,,,,,,,,,,,,,,,,,,,,,,,,,,,,@@%,,,,,@@@    @@@@@@@@   .@@@         @@@@@@@@@   @@@.                                                                     ",
           "                                        *@@,,,,,,@@@@@@@@@@@@.,,,,,,,,,,,@@@@@@@@@      @@@@@@@@                @@@@@@@@@                                                                            ",
           "                                         @@@,,,,,,,,,@@@  .@@,,,,,,,,,,,,@@           @@@@@@@@@@@@             @@@@@@@@@@@*                                                                          ",
           "                                         @@@,,,,,,,,,@@@  &@@,,,,,,,,,,,,@@                                                                                                                          ",
           "                                          @@@,,,,,,,/@@   .@@,,,,,,,,,,,@@@                                                                                                                          ",
           "                                           @@@@@@@@@@@     @@@@,,,,,,,, &@@/                                                                                                                         ",
           "                                                             #@@@@@@@@@@@                                                                                                                            "
        };

        private static void WelcomeToClipped(int sliderCount, int screenWidth)
        {
            if (_welcomeToText[0].Length < sliderCount + screenWidth)
            {
                screenWidth = _welcomeToText[0].Length - sliderCount;
            }
            foreach (var line in _welcomeToText)
            {
                Console.WriteLine(line.Substring(sliderCount, screenWidth));
            }
        }
        public static void PrintAndFade(int speed, ConsoleColor color)
        {
            ConsoleHelpers.ClearNumberOfLinesFromConsole(2);
            ConsoleHelpers.WriteInColor("\t\t\t\t\t\t" + _message, color);
            Thread.Sleep(speed);
        }
        public static void PrintInternshipAppText(ConsoleColor amogusColor, ConsoleColor textColor)
        {
            for (var i = 0; i < 19; i++)
            {
                ConsoleHelpers.WriteInColor("\t" + _internshipAppText[i] + "\n", textColor);
            }
            for (var i = 19; i < 45; i++)
            {
                ConsoleHelpers.WriteInColor("\t" + _internshipAppText[i].Substring(0, 85), amogusColor);
                ConsoleHelpers.WriteInColor(_internshipAppText[i].Substring(85, 50) + "\n", textColor);
            }
        }
        public static void DelayedInternshipPrint(ConsoleColor amogusColor, ConsoleColor textColor, int fadeSpeed)
        {
            PrintInternshipAppText(amogusColor, textColor);
            Thread.Sleep(fadeSpeed);
            ConsoleHelpers.ClearNumberOfLinesFromConsole(5);
            ConsoleHelpers.ClearNumberOfLinesFromConsole(5);
            ConsoleHelpers.ClearNumberOfLinesFromConsole(5);
            ConsoleHelpers.ClearNumberOfLinesFromConsole(5);
            ConsoleHelpers.ClearNumberOfLinesFromConsole(4);
            Console.WriteLine();
        }

        public static void InternPresentingPart(int fadeSpeed,int animationSpeed)
        {
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t");
            foreach (var character in _message)
            {
                ConsoleHelpers.WriteInColor("" + character, ConsoleColor.White);
                Thread.Sleep(animationSpeed);
            }
            Thread.Sleep(fadeSpeed);
            PrintAndFade(fadeSpeed, ConsoleColor.Gray);
            PrintAndFade(fadeSpeed, ConsoleColor.DarkGray);
            Console.Clear();
        }
        public static void WelcomeToPart(int fadeSpeed)
        {
            Console.WriteLine("\n\n\n\n\n\n");
            for (var i = 0; i < 320; i++)
            {
                WelcomeToClipped(i, 120);
                Thread.Sleep(5);
                ConsoleHelpers.ClearNumberOfLinesFromConsole(5);
                ConsoleHelpers.ClearNumberOfLinesFromConsole(2);
            }
            Thread.Sleep(fadeSpeed);
            Console.Clear();
        }
        public static void EnlargeConsolePart(double width, double heigth)
        {
            while (width + 2 < Console.LargestWindowWidth)
            {
                width += 2;
                heigth += 0.4;
                Console.SetWindowSize((int)width, (int)heigth);
                Thread.Sleep(5);
            }
        }
        public static void AmogusPart(int fadeSpeed)
        {
            Console.WriteLine("\n\n");
            DelayedInternshipPrint(ConsoleColor.Black, ConsoleColor.DarkGray, fadeSpeed);
            DelayedInternshipPrint(ConsoleColor.Black, ConsoleColor.Gray, fadeSpeed);
            DelayedInternshipPrint(ConsoleColor.Black, ConsoleColor.White, 3000);
            _amogusSound.SoundLocation = Environment.CurrentDirectory + "/../../../Helpers/Intro/AmongUsIntro.wav";
            _amogusSound.Play();
            Thread.Sleep(300);
            PrintInternshipAppText(ConsoleColor.Red, ConsoleColor.White);
            Thread.Sleep(4400);
            Console.Clear();
            _amogusSound.Stop();
        }

        public static void PrintIntro(int animationSpeed, int fadeSpeed)
        {
            InternPresentingPart(fadeSpeed, animationSpeed);
            WelcomeToPart(fadeSpeed);
            var origWidth = Console.WindowWidth;
            var origHeight = Console.WindowHeight;
            EnlargeConsolePart((double)origWidth, (double)origHeight);
            AmogusPart(fadeSpeed);
            Console.SetWindowSize(origWidth,origHeight);
        }
    }
}
