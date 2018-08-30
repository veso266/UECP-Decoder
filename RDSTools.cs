using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UECP_Decoder
{
    class RDSTools
    {
        public static byte[] RemoveByteStuffing(byte[] bytes2Process)
        {
            /*
            Unescapes characters as defined in 2.2.9
            Returns True if more data is needed
            */

            List<byte> data = bytes2Process.ToList();
            UECP uecp = new UECP();
            for (int i = 0; i < data.Count; i++)
            {
                if (data[i] == 0xFD && data[i + 1] == 0x00)
                {
                    data.RemoveAt(i);
                    data[i] = 0xFD;
                }
                if (data[i] == 0xFD && data[i + 1] == 0x01)
                {
                    data.RemoveAt(i);
                    data[i] = 0xFE;
                }
                if (data[i] == 0xFD && data[i + 1] == 0x02)
                {
                    data.RemoveAt(i);
                    data[i] = 0xFF;
                }
            }

            return data.ToArray();
        }
        public static bool check_crc(List<byte> CRC2Checkdata, List<byte> CRC2Check)
        {
            // CRC16-CCITT : x^16 + x^12 + x^5 + 1
            // Code from UoC-Radio/rds-control on GitHub
            // Link : https://github.com/UoC-Radio/rds-control/blob/master/uecp.c#L50-L65
            // Because I have no idea what i'm doing

            int crc = 0xFFFF;

            for (int i = 0; i < CRC2Checkdata.Count; i++)
            {
                crc = (byte)(crc >> 8) | (crc << 8);
                crc ^= CRC2Checkdata[i];
                crc ^= (byte)(crc & 0xff) >> 4;
                crc ^= (crc << 8) << 4;
                crc ^= ((crc & 0xff) << 4) << 1;
            }

            ushort CalculatedCRC = (ushort)((crc ^= 0xFFFF) & 0xFFFF);
            ushort ComparedCRC = BitConverter.ToUInt16(new byte[2] { (byte)CRC2Check[1], (byte)CRC2Check[0] }, 0);

            if (ComparedCRC == CalculatedCRC)
            {
                return true;
            }
            return false;
        }
        public static string FormatMEC(string MEC)
        {
            return MEC.Replace("RDS_", "");
        }
    }
}
