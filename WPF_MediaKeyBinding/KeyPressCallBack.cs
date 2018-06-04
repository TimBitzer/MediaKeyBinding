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
    class KeyPressCallBack
    {
        //====================== DLL Import ======================
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
        IntPtr wParam, IntPtr lParam);


        //====================== Variable definitions ======================
        private const int WM_KEYDOWN = 0x0100;


        //====================== Functions ======================
        public static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) // 2. delegate method
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN) 
            {
                int vkCode = Marshal.ReadInt32(lParam);

                //Console.WriteLine(vkCode);

                Keys PlayPause = Control.KeyPlayPause;

                if ((Keys)vkCode == Control.KeyPlayPause)
                {
                    VirtualKeyPress.PressPlayPause();
                }

                if((Keys)vkCode == Control.KeyStop)
                {
                    VirtualKeyPress.PressStop();
                }

                if ((Keys)vkCode == Control.KeyNext)
                {
                    VirtualKeyPress.PressNext();
                }

                if ((Keys)vkCode == Control.KeyPrevious)
                {
                    VirtualKeyPress.PressPrevious();
                }
            }

            return CallNextHookEx(Control.HookID, nCode, wParam, lParam);
        }
    }
}
