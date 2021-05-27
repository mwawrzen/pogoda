using System;
using System.IO.Ports;

namespace pogoda.Services
{
    public class CommunicationService
    {

        private static String[]? Ports;
        private static SerialPort? Port;

        public static void Connect(string message)
        {
            Ports = SerialPort.GetPortNames();

            if (Ports.Length != 0)
            {
                foreach (var port in Ports)
                {
                    Console.WriteLine(port);
                }

                Port = new SerialPort(Ports[0], 9600, Parity.None, 8, StopBits.One);
                Port.Open();
                Port.Write(message);
            }

            else
            {
                Console.WriteLine("Nie znaleziono zadnych portow");
            }
        }
    }
}

