using System.Collections.Generic;
using System;
using System.Threading;
using System.Media;

namespace Presentation.Helpers
{
    public static class IntroAnimation
    {
        private static readonly SoundPlayer amogusSound = new();
        private static readonly string message = "Intern Bože predstavlja...";
        private static readonly List<string> welcomeToText = new()
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
        private static readonly List<string> InternshipAppText = new()
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
            if (welcomeToText[0].Length < sliderCount + screenWidth)
            {
                screenWidth = welcomeToText[0].Length - sliderCount;
            }
            foreach (var line in welcomeToText)
            {
                Console.WriteLine(line.Substring(sliderCount, screenWidth));
            }
        }
        public static void PrintAndFade(int speed, ConsoleColor color)
        {
            ConsoleHelpers.ClearNumberOfLinesFromConsole(2);
            ConsoleHelpers.WriteInColor("\t\t\t\t\t\t" + message, color);
            Thread.Sleep(speed);
        }
        public static void PrintInternshipAppText(ConsoleColor amogusColor, ConsoleColor textColor)
        {
            for (var i = 0; i < 19; i++)
            {
                ConsoleHelpers.WriteInColor("\t" + InternshipAppText[i] + "\n", textColor);
            }
            for (var i = 19; i < 45; i++)
            {
                ConsoleHelpers.WriteInColor("\t" + InternshipAppText[i].Substring(0, 85), amogusColor);
                ConsoleHelpers.WriteInColor(InternshipAppText[i].Substring(85, 50) + "\n", textColor);
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

        public static void PrintIntro(int animationSpeed, int fadeSpeed)
        {
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\t\t\t\t\t\t");
            foreach (var character in message)
            {
                ConsoleHelpers.WriteInColor("" + character, ConsoleColor.White);
                Thread.Sleep(animationSpeed);
            }
            Thread.Sleep(fadeSpeed);
            PrintAndFade(fadeSpeed, ConsoleColor.Gray);
            PrintAndFade(fadeSpeed, ConsoleColor.DarkGray);
            Console.Clear();

            Console.WriteLine("\n\n\n\n");
            for (var i = 0; i < 320; i++)
            {
                WelcomeToClipped(i, 120);
                Thread.Sleep(5);
                ConsoleHelpers.ClearNumberOfLinesFromConsole(5);
                ConsoleHelpers.ClearNumberOfLinesFromConsole(2);
            }
            Thread.Sleep(fadeSpeed);
            Console.Clear();

            var origWidth = Console.WindowWidth;
            var origHeight = Console.WindowHeight;
            double heigth = origHeight, width = origWidth;
            while (width < 220)
            {
                width += 2;
                heigth += 0.6;
                Console.SetWindowSize((int)width, (int)heigth);
                Thread.Sleep(5);
            }

            Console.WriteLine("\n\n");
            DelayedInternshipPrint(ConsoleColor.Black, ConsoleColor.DarkGray, fadeSpeed);
            DelayedInternshipPrint(ConsoleColor.Black, ConsoleColor.Gray, fadeSpeed);
            DelayedInternshipPrint(ConsoleColor.Black, ConsoleColor.White, 3000);
            PrintInternshipAppText(ConsoleColor.Red,ConsoleColor.White);
            amogusSound.SoundLocation = Environment.CurrentDirectory + "/../../../Helpers/AmongUs.wav";
            amogusSound.Play();
            Thread.Sleep(4000);
            Console.Clear();
            Console.SetWindowSize(origWidth,origHeight);
        }
    }
}
