using Shared.Kozubenko.IO;
using Shared.Kozubenko.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kozubenko.Protocols.Messages
{
    public class SimpleConsumption
    {
        public const int MIN_MESSAGE_LEN = 8;
        public string ParseError { get; set; } = "";
        public DateTime Timestamp { get; set; }
        public uint Consumption { get; set; }

        public SimpleConsumption() { }
        public SimpleConsumption(IEnumerable<byte> bytes)
        {
            var reader = new BigEndianBinaryReader(bytes);

            if(reader.BytesToRead >= MIN_MESSAGE_LEN)
            {
                var timestamp = reader.ReadUInt32();

                Timestamp = DateTimeUtils.ToUnixDateTime(timestamp);
                Consumption = reader.ReadUInt32();
            }
            else
                ParseError = $"Not enough data for SimpleConsumption message. Requires {MIN_MESSAGE_LEN}, received {reader.BytesToRead}";
        }

        public byte[] ToArray()
        {
            var writer = new BigEndianBinaryWriter();

            uint timestamp = DateTimeUtils.ToUnixTimestamp(Timestamp);

            writer.WriteUInt32(timestamp);
            writer.WriteUInt32(Consumption);

            return writer.ToArray();
        }

        public override string ToString()
        {
            return $"{Consumption}@{Timestamp:o}";
        }
    }
}
