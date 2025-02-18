using Microsoft.VisualStudio.TestPlatform.MSTest.TestAdapter.ObjectModel;
using Shared.Kozubenko.IO;
using Shared.Kozubenko.Protocols;
using Shared.Kozubenko.Protocols.Messages;
using Shared.Kozubenko.Utilities;
using System;
using System.Collections;
using System.Diagnostics;
using System.Net;

namespace ProtocolUnitTests
{
    [TestClass]
    public sealed class ServerTests
    {
        [TestMethod]
        public void Server_OnlyAcceptsMessagesWithCorrectMagicNumber()
        {
            byte[] exampleMessage = SampleData.GenerateExampleMeterResponse();

            exampleMessage[0] = 200;

            Assert.ThrowsException<ProtocolViolationException>(() => ServerTryParse(exampleMessage));
        }

        [TestMethod]
        public void Test_SimpleConsumption()
        {
            var consumption = MyRandom.GenerateRandomUInt();
            var serializedMessage = SampleData.GenerateSimpleConsumptionResponse(consumption);

            var response = (SimpleConsumption)ServerTryParse(serializedMessage);

            Assert.AreEqual(consumption, response.Consumption);
        }

        [TestMethod]
        public void Test_UnixTimeMessage()
        {
            var testTime = DateTimeUtils.UnixTimestamp;

            var request = new MyProtocol
            {
                MsgID = MyProtocolMsgID.UnixTime,
                Payload = new UnixTimeMessage(testTime).Serialize()
            };

            var unixTimeReadyToBeShipped = request.Serialize();

            var response = (UnixTimeMessage)ServerTryParse(unixTimeReadyToBeShipped);

            Assert.AreEqual(testTime, response.ToTimestamp());
        }

        [TestMethod]
        public void TestStringMessage()
        {
            string Message = "Hello Server";

            var request = new MyProtocol
            {
                MsgID = MyProtocolMsgID.StringMessage,
                Payload = new StringMessage(Message).Serialize()
            };

            var response = (StringMessage)ServerTryParse(request.Serialize());
            Assert.IsTrue(response.Message == Message);
        }

        public static object ServerTryParse(byte[] MyProtocolSerializedMessage)
        {
            var protocol = new MyProtocol(MyProtocolSerializedMessage);

            if (!protocol.IsValid)
            {
                throw new ProtocolViolationException();
            }

            switch (protocol.MsgID)
            {
                case MyProtocolMsgID.UnixTime:
                    var reader = new BigEndianBinaryReader(protocol.Payload);

                    UInt32 timestamp = reader.ReadUInt32();

                    var dt = DateTimeUtils.ToUnixDateTime(timestamp);

                    return new UnixTimeMessage();

                case MyProtocolMsgID.SimpleConsumption:
                    return new SimpleConsumption(protocol.Payload);

                case MyProtocolMsgID.StringMessage:
                    return new StringMessage(protocol.Payload);

                default:
                    throw new Exception($"Message [{protocol.MsgID}] not supported");
            }

            throw new UnreachableException();
        }
    }
}
