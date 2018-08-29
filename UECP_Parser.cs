using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UECP_Decoder
{
    class UECP_Parser
    {
        public byte[] UECPFrame = new byte[263]; //Our complete UECP Frame

        //Now lets break it into peaces
        public byte[] STA = new byte[1]; //Start (STA) 

        public byte[] ADD = new byte[2];   //Address (ADD) 
        public byte[] SQC = new byte[1];   //Sequence Counter (SQC) 
        public byte[] MFL = new byte[1];   //Message Field Lenght (MFL) 
        public byte[] MSG = new byte[255]; //Message (MSG) 
        public byte[] CRC = new byte[2];   //Cyclic Redundancy Check (CRC) 

        public byte[] STP = new byte[1]; //Stop (STP) 
        //Now lets break it into peaces

        //Nekaj spremenljivk ki jih potrebujemo
        public static bool CRCOK = false;
        //Nekaj spremenljivk ki jih potrebujemo
        public void ParseFrame(byte[] data)
        {
            UECP_Parser UECP = new UECP_Parser(); //We have to initialize our class
            byte[] UECPFrame = RDSTools.RemoveByteStuffing(data);
            //Lets fill out array with data now
            Array.Copy(UECPFrame, 0, UECP.STA, 0, 1); //START

            Array.Copy(UECPFrame, 0 + 1, UECP.ADD, 0, 2);
            Array.Copy(UECPFrame, 0 + 1 + 2, UECP.SQC, 0, 1);
            Array.Copy(UECPFrame, 0 + 1 + 2 + 1, UECP.MFL, 0, 1); //We need this now
            Array.Copy(UECPFrame, 0 + 1 + 2 + 1 + 1, UECP.MSG, 0, UECP.MFL[0]); //We need MFL now
            Array.Copy(UECPFrame, 0 + 1 + 2 + 1 + 1 + UECP.MFL[0], UECP.CRC, 0, 2); //We still need MFL, to know where to start copying

            Array.Copy(UECPFrame, Array.IndexOf(UECPFrame, (byte)0xFF), UECP.STP, 0, 1); //STOP
            //Lets fill out array with data now

            //Check CRC Now
            List<byte> CRC2Calculate = Tools.array2ListAtRange(UECPFrame, 0 + 1, 0 + 1 + 2 + 1 + 1 + (int)UECP.MFL[0]);
            List<byte> CRC2Check = UECP.CRC.ToList();
            CRCOK = RDSTools.check_crc(CRC2Calculate, CRC2Check); //IF CRC IS OK IT SHOULD BE TRUE
            //Console.WriteLine(CRCOK);
            //Check CRC Now
        }
        public void ParseMessage()
        {
            //PI (01)
        }
    }
}
