using InTheHand.Net;
using InTheHand.Net.Sockets;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MockAHolyServer
{
    public partial class Form1 : Form
    {
        BackgroundWorker bg;
        BackgroundWorker bgInputServer;
        List<BackgroundWorker> bgSendApp = new List<BackgroundWorker>();
        List<BackgroundWorker> bgComunicate = new List<BackgroundWorker>();
        List<Device> detectedDevices = new List<Device>();

        TcpListener server = null;

        public Form1()
        {
            InitializeComponent();

            bg = new BackgroundWorker();

            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);

            bgInputServer.DoWork += new DoWorkEventHandler(bgInput_DoWork);
        }

        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listBox1.DataSource = detectedDevices;
            pb.Visible = false;
            btnFind.Enabled = true;
        }

        void bgInput_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // Set the TcpListener on port 13000.
                Int32 port = 27015;
                IPAddress localAddr = IPAddress.Parse("127.0.0.1");

                // TcpListener server = new TcpListener(port);
                server = new TcpListener(localAddr, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.Write("Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected!");

                    data = null;

                    // Get a stream object for reading and writing
                    NetworkStream stream = client.GetStream();

                    

                    // Shutdown and end connection
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                
            }

        }

        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            InTheHand.Net.Sockets.BluetoothClient bc = new InTheHand.Net.Sockets.BluetoothClient();
            InTheHand.Net.Sockets.BluetoothDeviceInfo[] array = bc.DiscoverDevices();
            int count = array.Length;
            for (int i = 0; i < count; i++)
            {
                Device device = new Device(array[i]);
                detectedDevices.Add(device);
            }
        }

        void bgSendApp_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result != null)
            {
                dataSetDevices.Devices.AddDevicesRow(((Device)e.Result).DeviceName, ((Device)e.Result).DeviceInfo.ToByteArray());
            }
        }

        void bgSendApp_DoWork(object sender, DoWorkEventArgs e)
        {
            Device device = (Device)e.Argument;
            ObexStatusCode response_status = SendModule.SendFile(device.DeviceInfo, Path.GetFullPath("MockAHolyClient.apk"));
            e.Result = (response_status != ObexStatusCode.InternalServerError ? device : null);
        }

        public class MyConsts
        {
            public static readonly Guid MyServiceUuid
              = new Guid("{00112233-4455-6677-8899-aabbccddeeff}");
        }

        //private void button_Click(object sender, RoutedEventArgs e)
        //{
        //   

        //    string lgphone = "88:30:8A:7E:1A:45"; // The MAC address of my phone, lets assume we know it

        //    BluetoothAddress addr = BluetoothAddress.Parse(lgphone);
        //    var btEndpoint = new BluetoothEndPoint(addr, MyConsts.MyServiceUuid);
        //    var btClient = new BluetoothClient();
        //    btClient.Connect(btEndpoint);

        //    Stream peerStream = btClient.GetStream();


        //    StreamReader sr = new StreamReader(peerStream);
        //    string line;
        //    // Read and display lines from the file until the end of 
        //    // the file is reached.
        //    while ((line = sr.ReadLine()) != null)
        //    {
        //        Console.WriteLine(line);
        //    }


        //    StreamWriter sw = new StreamWriter(peerStream);
        //    sw.WriteLine("Hello World");
        //    sw.Flush();
        //    sw.Close();

        //    btClient.Close();
        //    btClient.Dispose();
        //    btEndpoint = null;
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            btnFind_Click(sender, null);
        }

        private void btnFind_Click(object sender, EventArgs e)
        {
            if (!bg.IsBusy)
            {
                btnFind.Enabled = false;
                pb.Visible = true;
                bg.RunWorkerAsync();
            }
        }

        private void btnInvite_Click(object sender, EventArgs e)
        {
            var notPresentDevices = from rowDevice in (List<Device>)listBox1.DataSource
                                    where dataSetDevices.Devices.FindByDeviceName(rowDevice.DeviceName) == null
                                    select rowDevice;

            foreach (Device device in notPresentDevices)
            {
                bgSendApp.Add(new BackgroundWorker());

                bgSendApp.Last().DoWork += new DoWorkEventHandler(bgSendApp_DoWork);
                bgSendApp.Last().RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgSendApp_RunWorkerCompleted);
                bgSendApp.Last().RunWorkerAsync(device);
            }
        }

        private void btnConnection_Click(object sender, EventArgs e)
        {
            //foreach (DataSet.DevicesRow rowDevice in dataSetDevices.Devices.Rows)
            {
                Device device = (Device)listBox1.SelectedItem;
                bgComunicate.Add(new BackgroundWorker());

                bgComunicate.Last().DoWork += new DoWorkEventHandler(bgCominucate_DoWork);
                bgComunicate.Last().RunWorkerAsync(device.DeviceInfo);
            }
        }

        void bgCominucate_DoWork(object sender, DoWorkEventArgs e)
        {
            using (var bluetoothClient = new BluetoothClient())
            {
                try
                {
                    var ep = new BluetoothEndPoint((BluetoothAddress)e.Argument, MyConsts.MyServiceUuid);

                    // connecting  
                    bluetoothClient.Connect(ep);

                    Stream peerStream = bluetoothClient.GetStream();

                    StreamReader sr = new StreamReader(peerStream);
                    int line;
                    // Read and display lines from the file until the end of 
                    // the file is reached.
                    while (true)//(line = sr.ReadLine()) != null)
                    {
                        line = sr.Read();
                        Console.WriteLine(line);
                    }
                }
                catch
                {
                    // the error will be ignored and the send data will report as not sent  
                    // for understood the type of the error, handle the exception  
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            server.Stop();
        }
    }
}
