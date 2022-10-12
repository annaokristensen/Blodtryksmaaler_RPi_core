using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer_RPi
{
    //UDP forbindelse. RPi er Server, PC er client
    class UDPServer
    {
        private const int listenPort = 11000;   //Portnummer 11000

        private static void StartListener()   //Lytter efter porten på PC
        {
            UdpClient listener = new UdpClient(listenPort);
            IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);    //læser porten fra IPadressen

            try
            {
                while (true)
                {
                    Console.WriteLine("Waiting for broadcast"); //venter på at PC sender noget
                    byte[] bytes = listener.Receive(ref groupEP); //Det der modtages lægges ind i bytes

                    Console.WriteLine($"Received broadcast from {groupEP} :");
                    Console.WriteLine(
                        $"{Encoding.ASCII.GetString(bytes, 0, bytes.Length)}"); //tager string og laver den om til Ascii værdi
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                listener.Close();
            }
        }

        public static void Main()
        {
            StartListener();
        }
    }
}
