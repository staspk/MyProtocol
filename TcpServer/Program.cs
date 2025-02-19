namespace TcpServer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TcpServer tcpServer = new TcpServer("127.0.0.1", 8080);

            //tcpServer.SendHttpRequest();

            //tcpServer.SendHttpRequestAsync().Wait();

            tcpServer.SendTcpPacket();
        }
        
    }
}
