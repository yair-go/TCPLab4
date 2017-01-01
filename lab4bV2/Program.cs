using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace lab4bV2
{
    class Program
    {
        const int PORT_NO = 1234;
        const string SERVER_IP = "127.0.0.1";

        static void Main(string[] args)
        {
            //---listen at the specified IP and port no.---
            IPAddress localAdd = IPAddress.Parse(SERVER_IP);
            TcpListener listener = new TcpListener(localAdd, PORT_NO);
            Console.WriteLine("Listening...");
            listener.Start();

            //---incoming client connected---
            TcpClient client = listener.AcceptTcpClient();

            //---get the incoming data through a network stream---
            NetworkStream nwStream = client.GetStream();
            byte[] buffer = new byte[client.ReceiveBufferSize];

            //---read incoming stream---
            int bytesRead = nwStream.Read(buffer, 0, client.ReceiveBufferSize);

            //---convert the data received into a string---
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received : " + dataReceived);
            string writeString = @"<html><body><hr>This is a response <b>message</b> in HTML <br/> <font color = red> Wow!</font><hr></ body ></ html>";
            buffer = Encoding.Unicode.GetBytes(writeString);
            bytesRead = writeString.Length;
            //---write back the text to the client---
            Console.WriteLine("Sending back : " + writeString);
            nwStream.Write(buffer, 0, buffer.Length);
            client.Close();
            listener.Stop();
            Console.ReadLine();
        }
    }
}
