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
using Microsoft.Win32;

namespace MediaKeyBinding
{
    class Control
    {
        //====================== DLL Import ======================
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        //====================== Variable definition ======================
        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam); //1. declaration
        private const int WH_KEYBOARD_LL = 13;
        private static IntPtr _hookID = IntPtr.Zero;
        private static LowLevelKeyboardProc Proc = KeyPressCallBack.HookCallback; // 3. instantiate

        private static Keys _keyPlayPause = Keys.None;
        private static Keys _keyStop = Keys.None;
        private static Keys _keyNext = Keys.None;
        private static Keys _keyPrevious = Keys.None;


        //====================== Custom classes ======================
        public class Configuration
        {
            public string PlayPause { get; set; }
            public string Stop { get; set; }
            public string Next { get; set; }
            public string Previous { get; set; }
        }


        //====================== Getter / Setter ======================
        public static IntPtr HookID
        {
            get
            {
                return _hookID;
            }

            set
            {
                _hookID = value;
            }
        }

        public static Keys KeyPlayPause
        {
            get
            {
                return _keyPlayPause;
            }

            set
            {
                _keyPlayPause = value;
            }
        }

        public static Keys KeyStop
        {
            get
            {
                return _keyStop;
            }

            set
            {
                _keyStop = value;
            }
        }

        public static Keys KeyNext
        {
            get
            {
                return _keyNext;
            }

            set
            {
                _keyNext = value;
            }
        }

        public static Keys KeyPrevious
        {
            get
            {
                return _keyPrevious;
            }

            set
            {
                _keyPrevious = value;
            }
        }


        //====================== Functions ======================
        private static IntPtr SetHook(LowLevelKeyboardProc proc)  //4. call delegate
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        public static void StartKeyboardCapture()
        {
            // Set hook, capture keyboard
            _hookID = SetHook(Proc);
        }

        public static void StopKeyboardCapture()
        {
            // Unhook, stop keyboard capture
            UnhookWindowsHookEx(_hookID);
        }

        public static void SetKeyBindings(string PlayPause, string Stop, string Next, string Previous)
        {
            // Set values for the key
            KeyPlayPause = (Keys)Enum.Parse(typeof(Keys), PlayPause, true);
            KeyStop = (Keys)Enum.Parse(typeof(Keys), Stop, true);
            KeyNext = (Keys)Enum.Parse(typeof(Keys), Next, true);
            KeyPrevious = (Keys)Enum.Parse(typeof(Keys), Previous, true);
        }

        public static Configuration LoadConfiguration()
        {
            // Instantiate Configuration class -> PS> Custom Object
            Configuration configuration = new Configuration();

            // Open Regkey under HKCU:\\Software\\MediaKeyBinding
            RegistryKey RegKey = Registry.CurrentUser.OpenSubKey("Software\\MediaKeyBinding");

            // Check if RegKey is present
            if (RegKey != null)
            {
                // Loop each RegValue
                foreach (var ValueName in RegKey.GetValueNames())
                {
                    switch (ValueName.ToString())
                    {
                        case "Play/Pause":
                            configuration.PlayPause = Convert.ToString(RegKey.GetValue(ValueName));
                            break;

                        case "Stop":
                            configuration.Stop = Convert.ToString(RegKey.GetValue(ValueName));
                            break;

                        case "Next":
                            configuration.Next = Convert.ToString(RegKey.GetValue(ValueName));
                            break;

                        case "Previous":
                            configuration.Previous = Convert.ToString(RegKey.GetValue(ValueName));
                            break;
                    }
                }

                // Return configuration 'Object'
                return configuration;
            }

            else
            {
                // return null -> Regkey not found
                return null;
            }
        }


        public static void SaveConfiguration(string PlayPause, string Stop, string Next, string Previous)
        {
            // Save current config into the registry under 'HKCU:\\Software\\MediaKeyBinding'
            RegistryKey SoftwareKey = Registry.CurrentUser.OpenSubKey("Software", true);
            RegistryKey ApplicationKey = SoftwareKey.CreateSubKey("MediaKeyBinding");

            ApplicationKey.SetValue("Play/Pause", PlayPause);
            ApplicationKey.SetValue("Stop", Stop);
            ApplicationKey.SetValue("Next", Next);
            ApplicationKey.SetValue("Previous", Previous);
        }
    }
}
