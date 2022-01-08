using Lab6_OpenCV.Service;
using System;
using System.Threading;

namespace Lab6_OpenCV
{
    internal class Program
    {
        private static SpotifyService spotifyService = new SpotifyService();

        /// <summary>
        /// Main application method to start application
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            Console.WriteLine("Open Spotify app, move to main menu and click expected button depends on account type");
            Console.WriteLine("A - Premium account");
            Console.WriteLine("S - NonPremium account");
            Console.WriteLine("Any other button will invoke premium application implementation");
            ConsoleKey pressedKey = Console.ReadKey().Key;

            if (pressedKey == ConsoleKey.S)
            {
                spotifyService.AttachToApplication(false);
            }
            else
            {
                spotifyService.AttachToApplication(true);
            }

            Console.WriteLine("Successfully attached to application. Remember to leave app in foreground!");


            detectGesture();
            invokeGestureAction(GestureType.StartPlayLikedSongs);
            invokeGestureAction(GestureType.Next);
            invokeGestureAction(GestureType.Pause);
            invokeGestureAction(GestureType.Previous);
            invokeGestureAction(GestureType.Play);
        }

        /// <summary>
        /// Method to detect gesture from video
        /// </summary>
        /// <returns>Enum with available gestures</returns>
        private static GestureType detectGesture()
        {
            //There should be code for gesture detection using
            //MediaPipe but does not found any wrapper for C# :(

            return GestureType.None;
        }


        /// <summary>
        /// Method to invoke expected method from SpotifyService depends on detected gesture
        /// </summary>
        /// <param name="gesture">Enum represent available gestures</param>
        private static void invokeGestureAction(GestureType gesture)
        {
            switch (gesture)
            {
                case GestureType.StartPlayLikedSongs:
                    spotifyService.InvokeStartPlayLikedSongs();
                    break;
                case GestureType.Play:
                    spotifyService.InvokePlayButton();
                    break;
                case GestureType.Previous:
                    spotifyService.InvokePreviousButton();
                    break;
                case GestureType.Next:
                    spotifyService.InvokeNextButton();
                    break;
                case GestureType.Pause:
                    spotifyService.InvokePauseButton();
                    break;
            }

            Thread.Sleep(3000);
        }
    }
}
