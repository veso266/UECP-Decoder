using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UECP_Decoder
{
    class Tools
    {
        //Nekaj orodij ki so uporabna
        public static List<byte> array2ListAtRange(byte[] array, int startIndex, int endIndex)
        {
            List<byte> bundleData = new List<byte>();
            byte[] transferredData = new byte[endIndex - startIndex];
            Array.Copy(array, startIndex, transferredData, 0, transferredData.Length);
            bundleData.AddRange(transferredData);
            return bundleData;
        }
        public static string ByteArrayToString(byte[] ba)
        {
            /*
            if (BitConverter.IsLittleEndian)
                Array.Reverse(ba);
            //Console.WriteLine("Its LittleEndian");
            */

            string hex = BitConverter.ToString(ba);
            return hex;
        }
        //Nekaj orodij ki so uporabna

    }
}
