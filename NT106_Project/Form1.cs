using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Resources;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using KAutoHelper;
using System.Net;
namespace NT106_Project
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtMyIP.Text = GetMyIP();
        }
        public static int ScrCount = 0;
        // Lấy IP
        #region IP
        public string GetMyIP()
        {
            string output = GetLocalIPv4(NetworkInterfaceType.Wireless80211);
            if (output == "")
            {
                output = GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }

            return output;
        }
        private string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach (NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach (UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        #endregion
        DisplayScreen rmtScr;
        private void Btn_OpenConnect_Click(object sender, EventArgs e)
        {
            if (ScrCount != 0)//Nếu đã có 1 form mở lên rồi thì k connect nữa
            {
                MessageBox.Show("Can not open more than 1 connection!") ;
                return;
            }
            int port;
            try
            {
                port = int.Parse(txtMyPort.Text);

                
                //chatForm = new ChatForm(0, txtMyIP.Text, port + 1);
                //chatForm.Show();
                 MessageBox.Show("Open connection success!");
                
            }
            catch
            {


                MessageBox.Show("Your port is not correct!");

                return;
            }
            rmtScr = new DisplayScreen(port);



            rmtScr.Show();
            ScrCount++;
        }
        #region Scaling and Connection
        private TcpClient client;
        private NetworkStream ostream;
        private int portNumber;
        private int width;
        private int height;
        // Độ phân giải 
        public Size GetDpiSafeResolution()
        {
            using (Graphics graphics = this.CreateGraphics())
            {
                return new Size((Screen.PrimaryScreen.Bounds.Width * (int)graphics.DpiX) / 96
                  , (Screen.PrimaryScreen.Bounds.Height * (int)graphics.DpiY) / 96);
            }
        }
        private Image CaptureScreen()
        {
            height = (int)(GetDpiSafeResolution().Height*getScalingFactor());
            width = (int)(GetDpiSafeResolution().Width*getScalingFactor());
            Rectangle bounds = new Rectangle(0, 0, width, height);
            Bitmap screenShot = new Bitmap(bounds.Width, bounds.Height, PixelFormat.Format32bppArgb);
            Graphics graphics = Graphics.FromImage(screenShot);
            graphics.CopyFromScreen(bounds.X, bounds.Y, 0, 0, bounds.Size, CopyPixelOperation.SourceCopy);
            return screenShot;
        }

        #region Get Scaling of Screen
        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);
        public enum DeviceCap
        {
            VERTRES = 10,
            DESKTOPVERTRES = 117,

            // http://pinvoke.net/default.aspx/gdi32/GetDeviceCaps.html
        }
        private float getScalingFactor()
        {
            Graphics g = Graphics.FromHwnd(IntPtr.Zero);
            IntPtr desktop = g.GetHdc();
            int LogicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.VERTRES);
            int PhysicalScreenHeight = GetDeviceCaps(desktop, (int)DeviceCap.DESKTOPVERTRES);
            float ScreenScalingFactor = (float)PhysicalScreenHeight / (float)LogicalScreenHeight;
            return ScreenScalingFactor; // 1.25 = 125%
        }

        #endregion
        private void SendDesktopImage()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            ostream = client.GetStream();         
            binFormatter.Serialize(ostream, CaptureScreen());// Xem xét tới vấn đề scaling giữa 2 máy
        }

        //Share screen 
        #endregion
        private bool isConnected = false;
        private void Btn_Connect_Click(object sender, EventArgs e)
        {
            client = new TcpClient();

            try
            {
                portNumber = int.Parse(txtPartnerPort.Text);
               /* IPAddress add1 = IPAddress.Parse(txtMyIP.Text.ToString());
                TcpListener server = new TcpListener(add1, int.Parse(txtMyPort.Text));
                server.Start();*/
                client.Connect(txtPartnerIP.Text, portNumber);
                //
                // chatForm2 = new ChatForm(1, txtPartnerIP.Text, portNumber + 1);
                // chatForm2.Show();
                isConnected = true;
                MessageBox.Show("Connected!");
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());

                isConnected = false;

            }
        }
        private bool isShared = false;
        private void Btn_ShareScreen_Click(object sender, EventArgs e)
        {
            timeOut = 0;

            if (isShared == false) 
            {
                timer1.Start();
                Btn_ShareScreen.Text = "Stop sharing";
                MessageBox.Show("Share successfully");
                StartListenText();//Listen text
            }
            else
            {
                timer1.Stop();
                Btn_ShareScreen.Text = "Share your screen";

                   MessageBox.Show("Stopped share screen");

                //StopListenText();//Stop listen

            }
        }

        private int timeOut = 0;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            {
                try
                {
                    if (client.Connected)
                    {
                        SendDesktopImage();
                    }
                    else
                    {
                        timeOut++;
                        if (timeOut == 200)
                        {
                            try
                            {
                                //chatForm2.StopListening();
                               // chatForm2.Close();
                            }
                            catch { }
                            Disconnect();
                            MessageBox.Show("Connection has been lost!");
                        }
                    }
                }
                catch
                {
                }
            }
        }
        private void Disconnect()
        {
            Btn_ShareScreen.Text = "Share your screen";
            client.Close();
            client.Dispose();
            timer1.Stop();

                MessageBox.Show( "Disconnected");

            isConnected = false;
        }
        #region Receive Click and Keys
        private Thread ListeningToText;
        private NetworkStream instream;

        //Thư viện xử lý click
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, int dx, int dy, uint dwData, int dwExtraInfo);
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        const uint MOUSEEVENTF_MIDDLEUP = 0x0040;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;

        private void StartListenText() 
        {
            ListeningToText = new Thread(ReceiveText);
            ListeningToText.Start();
        }
        private void StopListenText()
        {
            if (ListeningToText != null && ListeningToText.IsAlive)
                ListeningToText.Abort();
        }

        private void ReceiveText()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            while (client.Connected)
            {
                try
                {
                    instream = client.GetStream();
                    string str = (String)binFormatter.Deserialize(instream);
                    string[] strArr = str.Split(':');
                    if (!(strArr[0] == "KU" || strArr[0] == "KD"))
                    {
                        #region Clicks handle
                        int x = (int)((width / getScalingFactor()) * Int32.Parse(strArr[1]) / Int32.Parse(strArr[3]));
                        int y = (int)((height / getScalingFactor()) * Int32.Parse(strArr[2]) / Int32.Parse(strArr[4]));
                        switch (strArr[0])
                        {
                            case "MM"://mouse move
                                {
                                    Cursor.Position = new Point(x, y);
                                }
                                break;
                            case "LD"://left down
                                {
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
                                }
                                break;
                            case "LU"://left up
                                {
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                }
                                break;
                            case "RC"://right click
                                {
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                }
                                break;
                            case "MC"://Middle click
                                {
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_MIDDLEDOWN | MOUSEEVENTF_MIDDLEUP, 0, 0, 0, 0);
                                }
                                break;
                            case "LC": //left click
                                {
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                }
                                break;
                            case "DL"://double left click
                                {
                                    Cursor.Position = new Point(x, y);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
                                }
                                break;
                            case "DR"://double right click
                                {
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                    mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
                                }
                                break;
                            default:
                                break;
                        }

                        #endregion
                    }
                    else
                    {
                        #region Xu ly keys
                        if (strArr[0] == "KU")
                        {
                            AutoControl.SendKeyUp(getKeyCodeFromString(strArr[1]));
                        }
                        else if (strArr[0] == "KD")
                        {
                            AutoControl.SendKeyDown(getKeyCodeFromString(strArr[1]));
                        }
                        #endregion
                    }
                }
                catch { }
            }
        }

        private KeyCode getKeyCodeFromString(string keystr)
        {
            switch (keystr)
            {
                case "3": { return KeyCode.CANCEL; }
                case "8": { return KeyCode.BACKSPACE; }
                case "9": { return KeyCode.TAB; }
                case "13": { return KeyCode.ENTER; }
                case "16": { return KeyCode.SHIFT; }
                case "17": { return KeyCode.CONTROL; }
                case "18": { return KeyCode.ALT; }
                case "20": { return KeyCode.CAPS_LOCK; }
                case "27": { return KeyCode.ESC; }
                case "32": { return KeyCode.SPACE_BAR; }
                case "33": { return KeyCode.PAGE_UP; }
                case "34": { return KeyCode.PAGEDOWN; }
                case "35": { return KeyCode.END; }
                case "36": { return KeyCode.HOME; }
                case "37": { return KeyCode.LEFT; }
                case "38": { return KeyCode.UP; }
                case "39": { return KeyCode.RIGHT; }
                case "40": { return KeyCode.DOWN; }
                case "44": { return KeyCode.SNAPSHOT; }
                case "45": { return KeyCode.INSERT; }
                case "46": { return KeyCode.DELETE; }
                case "48": { return KeyCode.KEY_0; }
                case "49": { return KeyCode.KEY_1; }
                case "50": { return KeyCode.KEY_2; }
                case "51": { return KeyCode.KEY_3; }
                case "52": { return KeyCode.KEY_4; }
                case "53": { return KeyCode.KEY_5; }
                case "54": { return KeyCode.KEY_6; }
                case "55": { return KeyCode.KEY_7; }
                case "56": { return KeyCode.KEY_8; }
                case "57": { return KeyCode.KEY_9; }
                case "65": { return KeyCode.KEY_A; }
                case "66": { return KeyCode.KEY_B; }
                case "67": { return KeyCode.KEY_C; }
                case "68": { return KeyCode.KEY_D; }
                case "69": { return KeyCode.KEY_E; }
                case "70": { return KeyCode.KEY_F; }
                case "71": { return KeyCode.KEY_G; }
                case "72": { return KeyCode.KEY_H; }
                case "73": { return KeyCode.KEY_I; }
                case "74": { return KeyCode.KEY_J; }
                case "75": { return KeyCode.KEY_K; }
                case "76": { return KeyCode.KEY_L; }
                case "77": { return KeyCode.KEY_M; }
                case "78": { return KeyCode.KEY_N; }
                case "79": { return KeyCode.KEY_O; }
                case "80": { return KeyCode.KEY_P; }
                case "81": { return KeyCode.KEY_Q; }
                case "82": { return KeyCode.KEY_R; }
                case "83": { return KeyCode.KEY_S; }
                case "84": { return KeyCode.KEY_T; }
                case "85": { return KeyCode.KEY_U; }
                case "86": { return KeyCode.KEY_V; }
                case "87": { return KeyCode.KEY_W; }
                case "88": { return KeyCode.KEY_X; }
                case "89": { return KeyCode.KEY_Y; }
                case "90": { return KeyCode.KEY_Z; }
                case "91": { return KeyCode.LWIN; }
                case "92": { return KeyCode.RWIN; }
                case "93": { return KeyCode.RightClick; }
                case "96": { return KeyCode.NUMPAD0; }
                case "97": { return KeyCode.NUMPAD1; }
                case "98": { return KeyCode.NUMPAD2; }
                case "99": { return KeyCode.NUMPAD3; }
                case "100": { return KeyCode.NUMPAD4; }
                case "101": { return KeyCode.NUMPAD5; }
                case "102": { return KeyCode.NUMPAD6; }
                case "103": { return KeyCode.NUMPAD7; }
                case "104": { return KeyCode.NUMPAD8; }
                case "105": { return KeyCode.NUMPAD9; }
                case "106": { return KeyCode.MULTIPLY; }
                case "107": { return KeyCode.ADD; }
                case "109": { return KeyCode.SUBTRACT; }
                case "110": { return KeyCode.DECIMAL; }
                case "111": { return KeyCode.DIVIDE; }
                case "112": { return KeyCode.F1; }
                case "113": { return KeyCode.F2; }
                case "114": { return KeyCode.F3; }
                case "115": { return KeyCode.F4; }
                case "116": { return KeyCode.F5; }
                case "117": { return KeyCode.F6; }
                case "118": { return KeyCode.F7; }
                case "119": { return KeyCode.F8; }
                case "120": { return KeyCode.F9; }
                case "121": { return KeyCode.F10; }
                case "122": { return KeyCode.F11; }
                case "123": { return KeyCode.F12; }
                case "124": { return KeyCode.F13; }
                case "125": { return KeyCode.F14; }
                case "126": { return KeyCode.F15; }
                case "127": { return KeyCode.F16; }
                case "128": { return KeyCode.F17; }
                case "129": { return KeyCode.F18; }
                case "130": { return KeyCode.F19; }
                case "131": { return KeyCode.F20; }
                case "132": { return KeyCode.F21; }
                case "133": { return KeyCode.F22; }
                case "134": { return KeyCode.F23; }
                case "135": { return KeyCode.F24; }
                case "144": { return KeyCode.NUMLOCK; }
                case "160": { return KeyCode.LSHIFT; }
                case "161": { return KeyCode.RSHIFT; }
                case "162": { return KeyCode.LCONTROL; }
                case "163": { return KeyCode.RCONTROL; }
                case "166": { return KeyCode.BROWSER_BACK; }
                case "167": { return KeyCode.BROWSER_FORWARD; }
                case "168": { return KeyCode.BROWSER_REFRESH; }
                case "169": { return KeyCode.BROWSER_STOP; }
                case "170": { return KeyCode.BROWSER_SEARCH; }
                case "171": { return KeyCode.BROWSER_FAVORITES; }
                case "172": { return KeyCode.BROWSER_HOME; }
                case "173": { return KeyCode.VOLUME_MUTE; }
                case "174": { return KeyCode.VOLUME_DOWN; }
                case "175": { return KeyCode.VOLUME_UP; }
                case "176": { return KeyCode.MEDIA_NEXT_TRACK; }
                case "177": { return KeyCode.MEDIA_PREV_TRACK; }
                case "178": { return KeyCode.MEDIA_STOP; }
                case "179": { return KeyCode.MEDIA_PLAY_PAUSE; }
                case "180": { return KeyCode.LAUNCH_MAIL; }
                case "181": { return KeyCode.LAUNCH_MEDIA_SELECT; }
                case "182": { return KeyCode.LAUNCH_APP1; }
                case "183": { return KeyCode.LAUNCH_APP2; }
                case "186": { return KeyCode.OEM_1; }
                case "187": { return KeyCode.OEM_PLUS; }
                case "188": { return KeyCode.OEM_COMMA; }
                case "189": { return KeyCode.OEM_MINUS; }
                case "190": { return KeyCode.OEM_PERIOD; }
                case "191": { return KeyCode.OEM_2; }
                case "192": { return KeyCode.OEM_3; }
                case "219": { return KeyCode.OEM_4; }
                case "220": { return KeyCode.OEM_5; }
                case "221": { return KeyCode.OEM_6; }
                case "222": { return KeyCode.OEM_7; }
                case "223": { return KeyCode.OEM_8; }
                case "226": { return KeyCode.OEM_102; }
                case "254": { return KeyCode.OEM_CLEAR; }
            }
            return KeyCode.NUMLOCK;
        }
        #region Done
        private bool checkFormOpen(string name)
        {
            foreach (Form f in Application.OpenForms)
            {
                if (f.Text == name)
                {
                    return true;
                }
            }
            return false;
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {

            try
            {
                timer1.Stop();
                StopListenText();
                try
                {
                    client.Dispose();
                    client.Close();
                }
                catch { }
                if (checkFormOpen("DisplayScreen"))
                {
                    rmtScr.StopListening();
                    rmtScr.Close();
                }
                if (checkFormOpen("ChatForm"))
                {
                    try
                    {
                        try
                        {
                            //chatForm.StopListening();
                        }
                        catch { }
                        try
                        {
                            //chatForm.Close();
                        }
                        catch { }
                        try
                        {
                            //chatForm2.StopListening();
                        }
                        catch { }
                        try
                        {
                            //chatForm2.Close();
                        }
                        catch { }

                    }
                    catch { }
                }
            }
            catch { }
        }
        #endregion

        #endregion

       
    }

}

