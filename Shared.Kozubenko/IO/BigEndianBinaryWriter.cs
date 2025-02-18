using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Kozubenko.IO
{
    public class BigEndianBinaryWriter          // HEX MIN VALUE: 0x0; MAX VALUE depends on bit system... 8-bit
    {
        List<byte> Buffer = new List<byte>();   // num == 1739664309    buffer == 103, 177, 43, 181  == 0x67 0xB1 0x2B 0xB5

        /// <summary>
        /// Note: 2500 ==> 0x000009C4 == 32-bit representation, 0x9C4 == 16-bit reprsentation
        /// 
        /// Assume incoming value: 2500
        ///     WriteUInt32 returns => [0x00 0x00 0x09 0xc4], made up of individual bytes.
        ///            decimal repr => [   0    0    9  196]
        ///            binary  repr => 1001 1100 0100
        ///                  
        ///     To get original value back, add right side up:
        ///         9 * 16^2 =  9 * 256 = 2304
        ///        12 * 16^1 = 12 * 16  =  192
        ///         4 * 16^0 =  4 * 1   =    4
        ///                             = 2500
        ///     
        ///  Assume incoming value: 1739664309
        ///     WriteUInt32 returns => [0x67 0xB1 0x2B 0xB5], made up of individual bytes => 0x67B12BB5
        ///            decimal repr => [ 103  177   43  181]
        ///            binary  repr => 0110 0111 1011 0001 0010 1011 1011 0101
        ///
        ///    First byte:  0xB5  =>
        ///            5  * 16^0  = 5
        ///           11  * 16^1  = 176
        ///           176 + 5     => 181
        /// 
        ///    Second byte: 0x09  =>
        ///            11 * 16^0  = 11
        ///             2 * 16^1  = 32
        ///                      += 43
        ///            43 * 16^2  => 11,008
        ///      
        ///    Third byte: 0xB1  =>
        ///            1 * 16^0  = 1
        ///           11 * 16^1  = 176
        ///                     += 177
        ///           177 * 16^4 => 11,599,872
        ///           
        ///    Fourth byte: 0x67 =>
        ///            7 * 16^0 = 7
        ///            6 * 16^1 = 96
        ///                    += 103
        ///          103 * 16^6 => 1,728,053,248
        ///                                                 1739664309
        ///     1,728,053,248 + 11,599,872 + 11,008 + 181 = 1,739,664,309    CHECKS OUT
        /// </summary>
        /// <param name="num"></param>
        public void WriteUInt32(uint num)   // 
        {                                   // example: num == 1739662942, hexdec => 0x67b1265e            Buffer
            Buffer.Add((byte)(num >> 24));  // num >> 24 == 103,     0x00000067;    (byte)(num >> 24) == 103    0x67
            Buffer.Add((byte)(num >> 16));  // num >> 16 == 26545,   0x000067b1;    (byte)(num >> 16) == 177    0xB1
            Buffer.Add((byte)(num >> 8));   // num >> 8  == 6795558  0x0067b126;    (byte)(num >> 8)  == 38     0x26
            Buffer.Add((byte)(num));        //                                      (byte)(num)       == 94     0x5E
        }                                   // Conclusion: we take the incoming stream of characters


        public void WriteUInt16(int num)
        {
            // Example 0x1234
            Buffer.Add((byte)(num >> 8));
            Buffer.Add((byte)(num));
        }
        public void WriteRange(IEnumerable<byte> items)
        {
            Buffer.AddRange(items);
        }

        public byte[] ToArray()
        {
            return Buffer.ToArray();
        }
    }
}
