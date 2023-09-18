// A C# Program for Server
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using SocketOpgave.Models;

namespace Server
{

    class Program
    {
        private static List<SaleItem> SaleItems { get; set; } = new List<SaleItem>();
        // Main Method
        static void Main(string[] args)
        {
            MakeItems();
            ExecuteServer();
        }

        public static void ExecuteServer()
        {
            // Establish the local endpoint
            // for the socket. Dns.GetHostName
            // returns the name of the host
            // running the application.
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = ipHost.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            // Creation TCP/IP Socket using
            // Socket Class Constructor
            Socket listener = new Socket(ipAddr.AddressFamily,
                        SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a
                // network address to the Server Socket
                // All client that will connect to this
                // Server Socket must know this network
                // Address
                listener.Bind(localEndPoint);

                // Using Listen() method we create
                // the Client list that will want
                // to connect to Server
                listener.Listen(10);

                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");

                    // Suspend while waiting for
                    // incoming connection Using
                    // Accept() method the server
                    // will accept connection of client
                    Socket clientSocket = listener.Accept();

                    // Data buffer
                    byte[] bytes = new Byte[1024];
                    string data = null;

                    while (true)
                    {

                        int numByte = clientSocket.Receive(bytes);

                        data += Encoding.ASCII.GetString(bytes,
                                                0, numByte);

                        if (data.IndexOf("<EOF>") > -1)

                            break;
                    }

                    Console.WriteLine("Text received -> {0} ", data);
                    string ListOfItems = "\n";
                    for (int i=0; i<SaleItems.Count(); i++)
                    {
                        ListOfItems += SaleItems[i].Name + " " + SaleItems[i].MinPrice + "\n";
                    }
                    byte[] message = Encoding.ASCII.GetBytes(ListOfItems);

                    // Send a message to Client
                    // using Send() method
                    clientSocket.Send(message);

                    string endOfListMessage = "hvad vil du";
                    byte[] endOfListBytes = Encoding.ASCII.GetBytes(endOfListMessage);
                    clientSocket.Send(endOfListBytes);

                    //byte[] recievedMessage = new byte[1024];
                    //string data2 = "";
                    //int numByte2 = clientSocket.Receive(recievedMessage);
                    //data2 += Encoding.ASCII.GetString(recievedMessage,0, numByte2);


                                     


                    // Close client Socket using the
                    // Close() method. After closing,
                    // we can use the closed Socket
                    // for a new Client Connection
                    clientSocket.Shutdown(SocketShutdown.Both);
                    clientSocket.Close();
                }
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void MakeItems()
        {
            SaleItem si = new SaleItem();
            si.Id = 1;
            si.AuctionLength = TimeSpan.FromSeconds(10);
            si.StartTime = DateTime.Now;
            si.MinPrice = 100f;
            si.Name = "stol";
            SaleItems.Add(si);


            SaleItem si1 = new SaleItem();
            si1.Id = 2;
            si1.AuctionLength = TimeSpan.FromSeconds(10);
            si1.StartTime = DateTime.Now;
            si1.MinPrice = 100f;
            si1.Name = "bord";
            SaleItems.Add(si1);
            
            SaleItem si2 = new SaleItem();
            si2.Id = 3;
            si2.AuctionLength = TimeSpan.FromSeconds(10);
            si2.StartTime = DateTime.Now;
            si2.MinPrice = 100f;
            si2.Name = "seng";
            SaleItems.Add(si2);

        }
    }
}
