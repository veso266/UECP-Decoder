using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Net;      //required
using System.Net.Sockets;    //required

namespace UECP_Decoder
{
    public class UECP
    {
        /*
        Create a new decoder from bytes containing a
        complete UECP frame, from STA to STO.
        See 2.2.1 General Frame Format
        */
        //public byte[] UECPFrame = new byte[19];
        //public byte[] UECPFrame = new byte[263];

        public void Decode (byte[] data)
        {
            //Now lets start parsing our Message Field

            //Now lets start parsing our Message Field


            //Console.WriteLine(Tools.ByteArrayToString(UECPFrame));
            Console.WriteLine("Break Here");

        } 
    }
    class Program
    {
        static void Main(string[] args)
        {
            UECP_Parser uecp = new UECP_Parser();
            TcpListener server = new TcpListener(IPAddress.Any, 4002);
            // we set our IP address as server's address, and we also set the port: 9999

            server.Start();  // this will start the server

            while (true)   //we wait for a connection
            {
                TcpClient client = server.AcceptTcpClient();  //if a connection exists, the server will accept it

                NetworkStream ns = client.GetStream(); //networkstream is used to send/receive messages

                byte[] hello = new byte[100];   //any message must be serialized (converted to byte array)
                hello = Encoding.Default.GetBytes("hello world");  //conversion string => byte array

                ns.Write(hello, 0, hello.Length);     //sending the message

                while (client.Connected)  //while the client is connected, we look for incoming messages
                {
                    byte[] msg = new byte[1024];     //the messages arrive as byte array
                    //ns.Read(msg, 0, msg.);   //the same networkstream reads the message sent by the client
                    //ns.Read(uecp.UECPFrame, 0, 19);
                    ns.Read(uecp.UECPFrame, 0, uecp.UECPFrame.Length);
                    uecp.ParseFrame(uecp.UECPFrame);
                    //Console.WriteLine(Encoding.Default.GetString(uecp.UECPFrame).Trim());
                    //Array.Copy(msg, uecp.UECPFrame, 255);
                    //Console.WriteLine("Break Here");
                    //Console.WriteLine(Encoding.Default.GetString(msg).Trim()); //now , we write the message as string
                }
            }
        }
    }
}
