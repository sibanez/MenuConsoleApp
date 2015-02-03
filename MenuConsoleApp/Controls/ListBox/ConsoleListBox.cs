using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;

namespace MenuConsoleApp.Controls.ListBox
{
    public class ConsoleListBox
    {

        public delegate void ChangedEventHandler(string sender, EventArgs e);
        public event ChangedEventHandler Changed;
        protected internal virtual   void OnChanged(int sender,EventArgs e)
        {
            if (Changed != null)
                Changed(_items[sender -1], e);
        }

        private IList<string> _items;
      public    void Start(IList<string > items)
      {
          _items = items;
          
            Console.TreatControlCAsInput = false;
            Console.CancelKeyPress += (BreakHandler);
            Console.Clear();
            Console.CursorVisible = false;

            WriteColorString("Choose Level using down and up arrow keys and press enter", 12, 20, ConsoleColor.Black, ConsoleColor.White);
            ChooseListBoxItem(_items.ToArray(), 5, 3, ConsoleColor.DarkBlue, ConsoleColor.White);
        }
        public  int ChooseListBoxItem(string[] items, int ucol, int urow, ConsoleColor back, ConsoleColor fore)
        {
            int numItems = items.Length;
            int maxLength = items[0].Length;
            for (int i = 1; i < numItems; i++)
            {
                if (items[i].Length > maxLength)
                {
                    maxLength = items[i].Length;
                }
            }
            int[] rightSpaces = new int[numItems];
            for (int i = 0; i < numItems; i++)
            {
                rightSpaces[i] = maxLength - items[i].Length + 1;
            }
            int lcol = ucol + maxLength + 15;
            int lrow = urow + numItems + 2;
            DrawBox(ucol, urow, lcol, lrow, back, fore, true);
            WriteColorString(" " + items[0] + new string(' ', rightSpaces[0]), ucol + 1, urow + 1, fore, back);
            for (int i = 2; i <= numItems; i++)
            {
                WriteColorString(items[i - 1], ucol + 2, urow + i, back, fore);
            }
            var choice = 1;

            while (true)
            {
                var cki = Console.ReadKey(true);
                var key = cki.KeyChar;
                if (key == '\r') // enter 
                {
                    
                    OnChanged( choice, EventArgs.Empty);
                    return choice;
                }
                else if (cki.Key == ConsoleKey.DownArrow)
                {
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, back, fore);
                    if (choice < numItems)
                    {
                        choice++;
                    }
                    else
                    {
                        choice = 1;
                    }
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, fore, back);
                }
                else if (cki.Key == ConsoleKey.UpArrow)
                {
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, back, fore);
                    if (choice > 1)
                    {
                        choice--;
                    }
                    else
                    {
                        choice = numItems;
                    }
                    WriteColorString(" " + items[choice - 1] + new string(' ', rightSpaces[choice - 1]), ucol + 1, urow + choice, fore, back);
                }
            }
        }
        public static void DrawBox(int ucol, int urow, int lcol, int lrow, ConsoleColor back, ConsoleColor fore, bool fill)
        {
           // const char horizontal = '\u2500';
           // const char vertical = '\u2502';
           // const char upperLeftCorner = '\u250c';
          //  const char upperRightCorner = '\u2510';
          //  const char lowerLeftCorner = '\u2514';
           // const char lowerRightCorner = '\u2518';
            string fillLine = fill ? new string(' ', lcol - ucol - 1) : "";
            SetColors(back, fore);
            // draw top edge 
            Console.SetCursorPosition(ucol, urow);
           // Console.Write(upperLeftCorner);
          //  for (int i = ucol + 1; i < lcol; i++)
          //  {
           //     Console.Write(horizontal);
           // }
           // Console.Write(upperRightCorner);
            // draw sides 
            for (int i = urow + 1; i < lrow; i++)
            {
                Console.SetCursorPosition(ucol, i);
              //  Console.Write(vertical);
                if (fill) Console.Write(fillLine);
                Console.SetCursorPosition(lcol, i);
              //  Console.Write(vertical);
            }
            // draw bottom edge 
            Console.SetCursorPosition(ucol, lrow);
            //Console.Write(lowerLeftCorner);
           // for (int i = ucol + 1; i < lcol; i++)
           // {
           //     Console.Write(horizontal);
           // }
          //  Console.Write(lowerRightCorner);
        }
        public static void WriteColorString(string s, int col, int row, ConsoleColor back, ConsoleColor fore)
        {
            SetColors(back, fore);
            // write string 
            Console.SetCursorPosition(col, row);
            Console.Write(s);
        }
        public static void SetColors(ConsoleColor back, ConsoleColor fore)
        {
            Console.BackgroundColor = back;
            Console.ForegroundColor = fore;
        }
        public  void CleanUp()
        {
            Console.ResetColor();
            Console.CursorVisible = true;
            Console.Clear();
        }
        private  void BreakHandler(object sender, ConsoleCancelEventArgs args)
        {
            // exit gracefully if Control-C or Control-Break pressed 
            CleanUp();
        }

        
       
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ConsoleFont
    {
        public uint Index;
        public short SizeX, SizeY;
    }

    public static class ConsoleHelper
    {
        [DllImport("kernel32")]
        public static extern bool SetConsoleIcon(IntPtr hIcon);

        public static bool SetConsoleIcon(Icon icon)
        {
            return SetConsoleIcon(icon.Handle);
        }

        [DllImport("kernel32")]
        private extern static bool SetConsoleFont(IntPtr hOutput, uint index);

        private enum StdHandle
        {
            OutputHandle = -11
        }

        [DllImport("kernel32")]
        private static extern IntPtr GetStdHandle(StdHandle index);

        public static bool SetConsoleFont(uint index)
        {
            return SetConsoleFont(GetStdHandle(StdHandle.OutputHandle), index);
        }

        [DllImport("kernel32")]
        private static extern bool GetConsoleFontInfo(IntPtr hOutput, [MarshalAs(UnmanagedType.Bool)]bool bMaximize,
            uint count, [MarshalAs(UnmanagedType.LPArray), Out] ConsoleFont[] fonts);

        [DllImport("kernel32")]
        private static extern uint GetNumberOfConsoleFonts();

        public static uint ConsoleFontsCount
        {
            get
            {
                return GetNumberOfConsoleFonts();
            }
        }

        public static ConsoleFont[] ConsoleFonts
        {
            get
            {
                ConsoleFont[] fonts = new ConsoleFont[GetNumberOfConsoleFonts()];
                if (fonts.Length > 0)
                    GetConsoleFontInfo(GetStdHandle(StdHandle.OutputHandle), false, (uint)fonts.Length, fonts);
                return fonts;
            }
        }

    }
}
