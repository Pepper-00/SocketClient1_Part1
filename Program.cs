using SocketServerProto;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Google.Protobuf;
using Grpc.Core;
using Grpc.Net; 

namespace SocketClient1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //establish tcp client 
                TcpClient client = new TcpClient();
                client.Connect("127.0.0.1", 59065); 
                Console.WriteLine("Client connected to server.");
                

                //input string 
                Console.WriteLine("Enter the message you want to send.");
                string upload_string = Console.ReadLine();
                Stream stream = client.GetStream();

                //set up buffer 
                UTF8Encoding encoding = new UTF8Encoding();
                byte[] bytes = encoding.GetBytes(upload_string);

                //write msg to stream
                stream.Write(bytes, 0, bytes.Length);

                byte[] bytes1 = new byte[1024];
                int y = stream.Read(bytes1);
                string record = Encoding.UTF8.GetString(bytes1);

                //Console.WriteLine($"Received from server1: {record}");

                //close connection 
                client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
