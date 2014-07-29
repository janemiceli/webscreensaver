using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Diagnostics;

namespace MiceliMatrix
{
    public partial class ScreensaverForm : Form
    {
        private DateTime StartTime = DateTime.Now;

        public ScreensaverForm()
        {
            GlobalUserEventHandler gueh = new GlobalUserEventHandler();
            gueh.Event += new GlobalUserEventHandler.UserEvent(CloseAfter1Second);
            Application.AddMessageFilter(gueh);

            InitializeComponent();
        }

        private void ScreensaverForm_Load(object sender, EventArgs e)
        {
            ProcessStartInfo startInfo = new ProcessStartInfo("chrome.exe");
            RegistryKey reg = Registry.CurrentUser.CreateSubKey(Program.KEY);
            webBrowser.Navigate((string)reg.GetValue("Url", "http://janemiceli.com"));
            String url = (string)reg.GetValue("Url", "http://janemiceli.com");
            startInfo.Arguments = @" --incognito --kiosk http://janemiceli.github.io/matrix";
            Process.Start(startInfo);
            reg.Close();
        }

        private void CloseAfter1Second()
        {
            if (StartTime.AddSeconds(1) < DateTime.Now)
                Close();
        }

        private void webBrowser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

    }

    public class GlobalUserEventHandler : IMessageFilter
    {
        public delegate void UserEvent();
        
        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_MBUTTONDBLCLK = 0x209;
        private const int WM_KEYDOWN = 0x100;
        private const int WM_KEYUP = 0x101;

        public event UserEvent Event;

        public bool PreFilterMessage(ref Message m)
        {
            if ( (m.Msg >= WM_MOUSEMOVE && m.Msg <= WM_MBUTTONDBLCLK) 
                || m.Msg == WM_KEYDOWN || m.Msg == WM_KEYUP)
            {
                if (Event != null)
                {
                    Event();
                }
            }
            // Always allow message to continue to the next filter control
            return false;
        }
    }
}