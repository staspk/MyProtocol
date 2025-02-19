using Shared.Kozubenko.Protocols;
using Shared.Kozubenko.Protocols.Messages;
using Shared.Kozubenko.Utilities;
using System.Diagnostics;
using System.Net;
using System.Text;

namespace Shared.Kozubenko
{
    public class Test
    {
        class Utf8StringWriter : System.IO.StringWriter
        {
            public override Encoding Encoding
            {
                get { return Encoding.UTF8; }
            }
        }



        public static void Main(string[] args)
        {
            Env.Load();
        }

        public static byte[] GenerateConsumptionResponse()
        {
            var consumptionInfo = new SimpleConsumption()
            {
                Timestamp = DateTime.UtcNow,
                Consumption = 2500
            };

            var payload = consumptionInfo.ToArray();

            var response = new MyProtocol()
            {
                MsgID = MyProtocolMsgID.SimpleConsumption,
                Payload = payload
            };

            return response.Serialize();
        }

        // 218

        public static void ServerTryParse(byte[] MyProtocolSerializedMessage)
        {
            var protocol = new MyProtocol(MyProtocolSerializedMessage);

            if (!protocol.IsValid)
            {
                throw new ProtocolViolationException();
            }

            if (protocol.MsgID == MyProtocolMsgID.SimpleConsumption)
            {
                Console.WriteLine("Got a SimpleConsumption Message sent over the Network");
            }
        }
    }
}
