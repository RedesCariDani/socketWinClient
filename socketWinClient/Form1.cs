using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace socketWinClient
{
    public partial class Form1 : Form
    {
        System.Net.Sockets.TcpClient clientSocket = new System.Net.Sockets.TcpClient();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs ev)
        {
            textBox2.Text = "";
            byte[] bytes = new byte[1024];

            // Connect to a remote device.
            try
            {
                // Establish the remote endpoint for the socket.
                // This example uses port 11000 on the local computer.
                //IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
                System.Net.IPAddress ipaddress = System.Net.IPAddress.Parse(textBox1.Text);
                IPEndPoint remoteEP = new IPEndPoint(ipaddress, 80);

                // Create a TCP/IP  socket.
                Socket sendi = new Socket(AddressFamily.InterNetwork,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connect the socket to the remote endpoint. Catch any errors.
                try
                {
                    sendi.Connect(remoteEP);

                    textBox2.Text = "Socket connected to {0}" +
                         sendi.RemoteEndPoint.ToString();

                    // Encode the data string into a byte array.
                    byte[] msg = Encoding.ASCII.GetBytes("GET / HTTP/1.0\n\n");

                    // Send the data through the socket.
                    int bytesSent = sendi.Send(msg);

                    // Receive the response from the remote device.
                    int bytesRec = sendi.Receive(bytes);
                    textBox2.Text = "Echoed test = {0} " +
                         Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    // Release the socket.
                    sendi.Shutdown(SocketShutdown.Both);
                    sendi.Close();

                }
                catch (ArgumentNullException ane)
                {
                    textBox2.Text = "ArgumentNullException : {0}" + ane.ToString();
                }
                catch (SocketException se)
                {
                    textBox2.Text = "SocketException : {0}" + se.ToString();
                }
                catch (Exception e)
                {
                    textBox2.Text = "Unexpected exception : {0}" + e.ToString();
                }

            }
            catch (Exception e)
            {
                textBox2.Text = e.ToString();
            }

        }

    }
}
