using System;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Timers;


namespace CookieClicker
{
    class Program
    {
        public const int MOUSEEVENTF_RIGHTDOWN = 0x03;
        public const int MOUSEEVENTF_RIGHTUP = 0x05;

        public static int clicks = 0;
        public static Timer time = new Timer(1000);

        [DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int xpos, int ypos, int dwData, int dwExtraInfo);
        
        [STAThread]
        static void Main()
        {
            time.Elapsed += OnClicksPerSecond;

            Console.WriteLine("Press S to start and again to stop. E to exit");

            while (true)
            {
                if (Keyboard.IsKeyToggled(Key.S))
                {
                    Loop();
                }
                else if (Keyboard.IsKeyDown(Key.E))
                { 
                    break; 
                }               
            }
        }

        public static void Loop()
        {
            Console.WriteLine("Clicking");

            time.Start();
            while (Keyboard.IsKeyToggled(Key.S) ^ Keyboard.IsKeyDown(Key.E))
            {               
                mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
                mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                clicks++;
            }

            Console.WriteLine("Stopped clicking");
            time.Stop();

            if (Keyboard.IsKeyToggled(Key.E))
            {
                Environment.Exit(-1);
            }

        }
        public static void OnClicksPerSecond(object source, ElapsedEventArgs e)
        {
            Console.ForegroundColor = ConsoleColor.Green;

            string output = string.Format("Clicked {0} times in the last second", clicks);
            
            Console.WriteLine(output);
            Console.ForegroundColor = ConsoleColor.White;
            clicks = 0;
        }       
    }


 

}

