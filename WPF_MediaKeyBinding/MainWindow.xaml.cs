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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Manually initialize all controls (usually done with this.show())
            InitializeComponent();

            // Load saved config, if there is any
            LoadBinding();

            // Reposition the window in the bottom right corner
            SetWindowLocation();

            // Start keyboard capture
            Control.StartKeyboardCapture();

            // create context menu for trayicon
            System.Windows.Forms.ContextMenu trayMenu = new System.Windows.Forms.ContextMenu();
            trayMenu.MenuItems.Add("Configure", TrayMenu_Configure_Click);
            trayMenu.MenuItems.Add("Help", TrayMenu_Help_Click);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", TrayMenu_Exit_Click);

            // create notify (systray) icon
            System.Windows.Forms.NotifyIcon trayIcon = new System.Windows.Forms.NotifyIcon();
            trayIcon.Icon = new Icon("AppIcon.ico");
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
            trayIcon.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    this.Show();
                    this.WindowState = WindowState.Normal;
                };
        }

        private void SetWindowLocation()
        {
            // Reposiion the window in the bottom right corner
            Rect workArea = System.Windows.SystemParameters.WorkArea;
            this.Left = (workArea.Width - this.Width) + workArea.Left;
            this.Top = (workArea.Height - this.Height) + workArea.Top;
        }

        private void LoadBinding()
        {
            // get loaded registry values
            Control.Configuration configuration = Control.LoadConfiguration();

            // check if config was successfully loaded
            if (configuration != null)
            {
                // Inizialize current config
                Control.SetKeyBindings(configuration.PlayPause, configuration.Stop, configuration.Next, configuration.Previous);

                // Set text of textboxes
                this.TB_PlayPause.Text = configuration.PlayPause;
                this.TB_Stop.Text = configuration.Stop;
                this.TB_Next.Text = configuration.Next;
                this.TB_Previous.Text = configuration.Previous;

                // Hide window if configuration was found, therfor the capture will start immediately
                this.Hide();
            }

            // Control.LoadConfiguration() returned null -> config/regkey not found
            else
            {
                this.LBL_Status.Content = "Could not load settings. Regkey not found.";
                this.LBL_Status.Visibility = Visibility.Visible;
            }
        }


        private void SaveBinding()
        {
            // Get value of each textbox
            string _playPause = string.Empty;
            string _stop = string.Empty;
            string _next = string.Empty;
            string _previous = string.Empty;

            if (TB_PlayPause.IsEnabled)
            {
                _playPause = TB_PlayPause.Text;
            }

            else
            {
                _playPause = "None";
            }

            if (TB_Stop.IsEnabled)
            {
                _stop = TB_Stop.Text;
            }

            else
            {
                _stop = "None";
            }

            if (TB_Next.IsEnabled)
            {
                _next = TB_Next.Text;
            }

            else
            {
                _next = "None";
            }

            if (TB_Previous.IsEnabled)
            {
                _previous = TB_Previous.Text;
            }

            else
            {
                _previous = "None";
            }

            // Inizialize current config
            Control.SetKeyBindings(_playPause, _stop, _next, _previous);

            // Save current config to registry
            Control.SaveConfiguration(_playPause, _stop, _next, _previous);
        } 


        private void TrayMenu_Help_Click(object sender, EventArgs e)
        {
            // Configure message box
            string Body = "This tool lets you assign the multimedia key functions like Play/Pause, Stop, Next and Previous on any key of your keyboard. Combinations of keys is not yet supported\r\n\r\nPlease notice that the assigned keys wont work while in configuration menu. \r\n\r\n\r\n\r\ncreated by Tim Bitzer - Bechtle GmbH & Co. KG | 2018";
            string Headline = "Help";
            MessageBoxButton Button = MessageBoxButton.OK;

            // Show message box
            MessageBoxResult result = System.Windows.MessageBox.Show(Body, Headline, Button);
        }

        private void TrayMenu_Exit_Click(object sender, EventArgs e)
        {
            // Trigger closing event manually
            System.ComponentModel.CancelEventArgs cancelEvent = new System.ComponentModel.CancelEventArgs();
          
            Window_Closing(sender, cancelEvent);  
        }

        private void TrayMenu_Configure_Click(object sender, EventArgs e)
        {
            // Show window
            this.Show();

            // Hide status label since its not needed at runtime
            this.LBL_Status.Visibility = Visibility.Hidden;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            // Unhook KBHook            
            Control.StopKeyboardCapture();
        }

        private void BTN_Save_Click(object sender, RoutedEventArgs e)
        {
            //Control.SetConfiguration(TB_PlayPause.Text, TB_Stop.Text, TB_Next.Text, TB_Previous.Text);

            // Initialize (and save) the binding and hide the window;
            SaveBinding();
            this.Hide();        
            

            //Control.LoadConfiguration();
        }

        private void TB_ANY_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            System.Windows.Controls.TextBox CurrentTextBox = sender as System.Windows.Controls.TextBox;

            // Check if the pressed key is not a modifier key like "ctrl" or "shift" ...
            if (e.Key != Key.Tab && e.Key != (Key.LeftShift | Key.RightShift) && e.Key != (Key.LeftCtrl | Key.RightCtrl) && e.Key != (Key.LeftAlt | Key.RightAlt))             //(Keyboard.Modifiers & (ModifierKeys.Control | ModifierKeys.Shift)) == (ModifierKeys.Control | ModifierKeys.Shift))
            {
                // check if the pressed key is a system key like "F10" /bc they are toggels
                if (e.Key == Key.System)
                {
                    int hash = 0;
                    switch (e.SystemKey)
                    {
                        case Key.F10:                        
                            hash = e.Key.GetHashCode();
                            CurrentTextBox.Text = Key.F10.ToString();

                            // Press F10 again because its a toggle like he ALT key
                            VirtualKeyPress.PressKeyByByteValue((byte)hash);
                            break;

                        case (Key.LeftAlt):
                            hash = e.Key.GetHashCode();
                            CurrentTextBox.Text = "Alt";

                            // Press LeftAlt again because its a toggle like he ALT key
                            VirtualKeyPress.PressKeyByByteValue((byte)hash);
                            break;
                    }
                }

                else
                {
                    // Write pressed key in textbox
                    CurrentTextBox.Text = e.Key.ToString();
                }  
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            // Stop keyboard hook if GUI is visible, start it if GUI is hidden
            if (this.IsVisible == true)
            {
                Control.StopKeyboardCapture();
            }

            else
            {
                Control.StartKeyboardCapture();
            }
        }

        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject inputObject) where T : DependencyObject
        {
            // Get all WPF controls of a given type, loop through the VisualTreeHelper 
            if (inputObject != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(inputObject); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(inputObject, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    // needed bwcause <Grid> holds all controlls not window directly -> Grid = Child
                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        private void CHB_ANY_CheckChanged(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.CheckBox CurrentCheckBox = sender as System.Windows.Controls.CheckBox;

            // Get all controls form "Window" (inputObject) of the type "TextBox" (<T>) and loop them 
            foreach (System.Windows.Controls.TextBox TB_ANY in FindVisualChildren<System.Windows.Controls.TextBox>(Window))
            {
                // Find corresponding textbox by comparing the name -> remove type identifier like "TB_" or "CHB_"
                if (TB_ANY.Name.Split('_')[1] == CurrentCheckBox.Name.Split('_')[1])
                {
                    if (TB_ANY.IsEnabled == true)
                    {
                        TB_ANY.IsEnabled = false;
                    }
                    
                    else
                    {
                        TB_ANY.IsEnabled = true;
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Check if sender of event is MainWindow or MenuItem; Don´t close application if its from window close (x)
            if ((sender.GetType()).Name == "MainWindow")
            {
                e.Cancel = true;
                this.Hide();
            }

            else
            {
                // Configure message box
                string Body = "You are about to close this application. \nAre your sure?";
                string Headline = "Close application?";
                MessageBoxButton Button = MessageBoxButton.YesNo;

                // Show message box
                MessageBoxResult result = System.Windows.MessageBox.Show(Body, Headline, Button);

                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }

                else
                {
                    System.Windows.Application.Current.Shutdown();
                }
            }
        }
    }
}
