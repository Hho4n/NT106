using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NT106_Project
{
    public partial class DisplayScreen : Form
    {
        #region Connect and Receive Image
        private readonly int port;
        private TcpClient client;
        private TcpListener server;
        private NetworkStream mainStream;
        private readonly Thread Listening;
        private readonly Thread GetImage;
        private Size receivedImageSize;
        private bool gotten = false;
        public DisplayScreen(int Port)
        {
            port = Port;
            client = new TcpClient();
            Listening = new Thread(StartListening);
            GetImage = new Thread(ReceiveImage);
            //MessageBox.Show("Open connection success!");
            InitializeComponent();
            this.ActiveControl = MainScreen;
        }
        private void StartListening()
        {
            try
            {
                
                while (!client.Connected)
                {
                    server.Start();
                    client = server.AcceptTcpClient();
                }
                GetImage.Start();
            }
            catch
            {
                MessageBox.Show("Listening failed!");
                StopListening();
            }

        }

        public void StopListening()
        {
            try
            {
                server.Stop();
                client.Close();
                client = null;
                if (Listening.IsAlive) Listening.Abort();


                if (GetImage.IsAlive) GetImage.Abort();
                MessageBox.Show("Disconnect success!");
            }
            catch { }
        }

        private void ReceiveImage()
        {
            BinaryFormatter binFormatter = new BinaryFormatter();
            while (client.Connected)
            {
                try
                {

                    mainStream = client.GetStream();
                    if (gotten == true)
                    {
                        MainScreen.Image = (Image)binFormatter.Deserialize(mainStream);
                    }
                    else
                    {
                        Image receivedImage = (Image)binFormatter.Deserialize(mainStream);
                        MainScreen.Image = receivedImage;
                        receivedImageSize.Height = receivedImage.Height;
                        receivedImageSize.Width = receivedImage.Width;
                        gotten = true;
                    }
                }
                catch { }
            }
            //Thêm để thoong báo khi bên client out
            MessageBox.Show("Connection has been lost!");
        }
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            server = new TcpListener(IPAddress.Any, port);
            Listening.Start();
            //Show size on statusbar
          
        }

        #endregion
        #region SendClick

        private NetworkStream ostream;
        private void sendText(string str)
        {
            if (client.Connected)
            {
                BinaryFormatter binFormatter = new BinaryFormatter();
                ostream = client.GetStream();
                binFormatter.Serialize(ostream, str);
            }
        }

        private void MainScreen_MouseClick(object sender, MouseEventArgs e)
        {
            if (!cb_Mouse_ct.Checked)
                return;
            //Lay toa do
            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;

            if (e.Button == MouseButtons.Right)
            {

                sendText("RC:" + posX + ":" + posY + ":" + MainScreen.Width + ":" + MainScreen.Height);
            }
            else if (e.Button == MouseButtons.Middle)
            {

                sendText("MC:" + posX + ":" + posY + ":" + MainScreen.Width + ":" + MainScreen.Height);
            }
        }

        private void MainScreen_MouseUp(object sender, MouseEventArgs e)
        {
            if (!cb_Mouse_ct.Checked)
                return;
            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;
            if (e.Button == MouseButtons.Left)
            {
                
                sendText("LU:" + posX + ":" + posY + ":" + MainScreen.Width + ":" + MainScreen.Height);

            }
        }
        //Giu chuot

        private void MainScreen_MouseDown(object sender, MouseEventArgs e)
        {
            if (!cb_Mouse_ct.Checked)
                return;

            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;
            if (e.Button == MouseButtons.Left)
            {
                
                sendText("LD:" + posX + ":" + posY + ":" + MainScreen.Width + ":" + MainScreen.Height);

            }
        }
        private void MainScreen_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (!cb_Mouse_ct.Checked)
                return;
            int posX = this.PointToClient(Cursor.Position).X;
            int posY = this.PointToClient(Cursor.Position).Y;

            if (e.Button == MouseButtons.Right)
            {
               
                sendText("DR:" + posX + ":" + posY + ":" + MainScreen.Width + ":" + MainScreen.Height);
            }
        }
        #endregion
        #region SendKey

        private void DisplayScreen_KeyUp(object sender, KeyEventArgs e)
        {
            //If dont allow
            if (cb_Keycb.Checked == false)
                return;

            //If allow
            try
            {
                string keystr = "KU:" + e.KeyValue.ToString();
                //richTextBox1.Text += keystr + "\n";
                sendText(keystr);
            }
            catch { }
        }
        private void DisplayScreen_KeyDown(object sender, KeyEventArgs e)
        {
            //Nếu không cho điều khiển phím thì return
            if (cb_Keycb.Checked == false)
                return;

            //Nếu cho phép điều khiển
            try
            {
                string keystr = "KD:" + e.KeyValue.ToString();
                //richTextBox1.Text += keystr + "\n";
                sendText(keystr);
            }
            catch { }
        }
        #endregion


        private void cb_Mouse_ct_CheckedChanged(object sender, EventArgs e)
        {
            this.ActiveControl = MainScreen;
        }

        private void cb_Keycb_CheckedChanged(object sender, EventArgs e)
        {
            this.ActiveControl = MainScreen;
        }

       
    }
}
