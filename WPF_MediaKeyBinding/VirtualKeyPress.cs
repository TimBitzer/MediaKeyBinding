using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MediaKeyBinding
{
    class VirtualKeyPress
    {
        //====================== DLL Import ======================
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);



        //====================== Functions ====================== 
        public static void PressPlayPause()
        {
            //This code will press the virtual key "play/pause" button 
            keybd_event((byte)0xB3, 0, 0, UIntPtr.Zero);

            //This code will release the virtual key "play/pause" button 
            keybd_event((byte)0xB3, 0, (uint)0x2, UIntPtr.Zero);
        }

        public static void PressStop()
        {
            //This code will press the virtual key "stop" button 
            keybd_event((byte)0xB2, 0, 0, UIntPtr.Zero);

            //This code will release the virtual key "stop" button 
            keybd_event((byte)0xB2, 0, (uint)0x2, UIntPtr.Zero);
        }

        public static void PressNext()
        {
            //This code will press the virtual key "next" button 
            keybd_event((byte)0xB0, 0, 0, UIntPtr.Zero);

            //This code will release the virtual key "next" button 
            keybd_event((byte)0xB0, 0, (uint)0x2, UIntPtr.Zero);
        }

        public static void PressPrevious()
        {
            //This code will press the virtual key "previous" button 
            keybd_event((byte)0xB1, 0, 0, UIntPtr.Zero);

            //This code will release the virtual key "previous" button 
            keybd_event((byte)0xB1, 0, (uint)0x2, UIntPtr.Zero);
        }

        public static void PressKeyByByteValue(byte ByteValue)
        {
            //This code will press the virtual key "previous" button 
            keybd_event((byte)ByteValue, 0, 0, UIntPtr.Zero);

            //This code will release the virtual key "previous" button 
            keybd_event((byte)ByteValue, 0, (uint)0x2, UIntPtr.Zero);
        }
    }
}
