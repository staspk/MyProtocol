using Shared.Kozubenko.Protocols;
using Shared.Kozubenko.Protocols.Messages;
using System.Net;

namespace Shared.Kozubenko
{
    public class Test
    {
        public static void Main(string[] args)
        {
            //ServerTryParse(GenerateConsumptionRequest());

            var stream = GenerateConsumptionResponse();

            foreach (var value in stream)
            {
                Console.Write($"{value} ");
                //Console.Write($"{value:X2} ");
            }
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
