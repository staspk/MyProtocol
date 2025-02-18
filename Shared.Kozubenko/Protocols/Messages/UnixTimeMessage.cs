using Shared.Kozubenko.IO;
using Shared.Kozubenko.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kozubenko.Protocols.Messages
{
    public class UnixTimeMessage
    {
        public DateTime UnixTime = DateTime.UtcNow;

        public UnixTimeMessage() { }
        public UnixTimeMessage(IEnumerable<byte> Data)
        {
            this.Deserialize(Data.ToArray());
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Timestamp">Seconds since 1970</param>
        public UnixTimeMessage(UInt32 Timestamp)
        {
            UnixTime = DateTimeUtils.ToUnixDateTime(Timestamp);
        }

        /// <summary>
        /// Converts DateTime into 4 bytes, big endian unixtimestamp
        /// </summary>
        /// <returns></returns>
        public byte[] Serialize()
        {
            var timestamp = DateTimeUtils.ToUnixTimestamp(UnixTime);

            var writer = new BigEndianBinaryWriter();

            writer.WriteUInt32(timestamp);

            return writer.ToArray();
        }

        public void Deserialize(byte[] Data)
        {
            if(Data.Length >= 4)
            {
                var reader = new BigEndianBinaryReader(Data);

                var timestamp = reader.ReadUInt32();

                UnixTime = DateTimeUtils.ToUnixDateTime(timestamp);
            }
        }

        public UInt32 ToTimestamp()
        {
            return DateTimeUtils.ToUnixTimestamp(UnixTime);
        }
    }
}
