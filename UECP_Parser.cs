using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UECP_Decoder
{
    class UECP_Parser
    {       
        /*
        Create a new decoder from bytes containing a
        complete UECP frame, from STA to STO.
        See 2.2.1 General Frame Format
        */
        public byte[] UECPFrame = new byte[263]; //Our complete UECP Frame
        public UECP_Parser()
        {

        }

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
            //UECP_Parser UECP = new UECP_Parser(); //We have to initialize our class
            UECPFrame = RDSTools.RemoveByteStuffing(data);
            //Lets fill out array with data now
            Array.Copy(UECPFrame, 0, STA, 0, 1); //START

            Array.Copy(UECPFrame, 0 + 1, ADD, 0, 2);
            Array.Copy(UECPFrame, 0 + 1 + 2, SQC, 0, 1);
            Array.Copy(UECPFrame, 0 + 1 + 2 + 1, MFL, 0, 1); //We need this now
            Array.Copy(UECPFrame, 0 + 1 + 2 + 1 + 1, MSG, 0, MFL[0]); //We need MFL now
            Array.Copy(UECPFrame, 0 + 1 + 2 + 1 + 1 + MFL[0], CRC, 0, 2); //We still need MFL, to know where to start copying

            Array.Copy(UECPFrame, Array.IndexOf(UECPFrame, (byte)0xFF), STP, 0, 1); //STOP
            //Console.WriteLine(Encoding.Default.GetString(UECPFrame).Trim());
            //Console.WriteLine(Tools.ByteArrayToString(UECPFrame));
            //Lets fill out array with data now

            //Check CRC Now
            List<byte> CRC2Calculate = Tools.array2ListAtRange(UECPFrame, 0 + 1, 0 + 1 + 2 + 1 + 1 + (int)MFL[0]);
            List<byte> CRC2Check = CRC.ToList();
            CRCOK = RDSTools.check_crc(CRC2Calculate, CRC2Check); //IF CRC IS OK IT SHOULD BE TRUE
            Console.WriteLine(CRCOK);
            ParseMessage();
            //Check CRC Now
        }

        //Now lets parse our message
        public byte[] MEC = new byte[1]; //Message Element Code
        public byte[] DSN = new byte[1]; //Data Set Number
        public byte[] PSN = new byte[1]; //Program Service Number
        public byte[] MEL = new byte[1]; //Message Element Data Lenght
        public byte[] MED = new byte[255]; //Message Element Data
        //Now lets parse our message

        //Spremenljivke (vse kar ima S pred imenom ni byteArray)
        public string sMEC; //MEC 
        public string bMEC; //MEC (HEX)
        public int dDSN; //DSN (Decimal)
        public string sDSN; //DSN (Decimal)
        public int dPSN; //PSN (Decimal)
        public string sPSN; //PSN (Decimal)
        public int dMEL; //MEL (Decimal)
        //Spremenljivke (vse kar ima S pred imenom ni byteArray)

        public void ParseMessage()
        {
            
            UECP_Parser UECP = new UECP_Parser();
            //MEC
            /*
            The Message Element Code identifies a particular
            message, as defined in Section 3. 
            The Message Element Codeis within the range 01..FD. 
            The codes 00, FE and FF are not permitted for defining a MEC, 
            as they are used in this specification with a special meaning, 
            always defined in the conventions for a specific command.            
            */

            Array.Copy(MSG, 0, MEC, 0, 1);
            sMEC = RDSTools.FormatMEC(((MEC)MEC[0]).ToString());
            bMEC = MEC[0].ToString("X2");
            //Console.WriteLine("MEC: " + sMEC + " (" + bMEC + ")");
            //MEC

            //PI (01)

            //DSN
            /*
            The Data Set Number (DSN) permits a message to be targeted to the following within an encoder:
            -  a specific data set,
            -  the current data set,
            -  all data sets.            
            */

            Array.Copy(MSG, 1, DSN, 0, 1);
            dDSN = DSN[0];
            if (dDSN == 0)
            {
                sDSN = "Current data set";
            }
            else if (dDSN >= 1 && dDSN <= 253)
            {
                sDSN = "Specific data set";
            }
            else if (dDSN == 254)
            {
                sDSN = "All data sets except the current data set";
            }
            else if (dDSN == 255)
            {
                sDSN = "All data sets";
            }
            //DSN

            //PSN
            /*
            The Programme Service Number (PSN) permits a message element to operate a number of 
            services within one or more data sets and the corresponding addressing is shown in Table 6            
            */
            Array.Copy(MSG, 2, PSN, 0, 1);
            dPSN = PSN[0];
            if (dPSN == 0)
            {
                sPSN = "Special PSN for main service of specified data set(s)";
            }
            else if (dPSN >= 1 && dPSN <= 255)
            {
                sPSN = "Specific service within data set(s)";
            }
            //PSN

            //MEL
            Array.Copy(MSG, 3, MEL, 0, 1);
            dMEL = MEL[0];
            //MEL
            Array.Copy(MSG, 3, MED, 0, dMEL);
            //MED

            //MED
            //Console.WriteLine("MEC: " + sMEC + " (" + bMEC + ")");
            //Because we will have more than 5 conditions we will use switch instead of if/else
            switch (sMEC)
            {
                case "PI":
                    ParsePI(dMEL, MED);
                    break;
                case "PS":
                    ParsePS(dMEL, MED);
                    break;
            }
        }

        public string ParsePI(int MEL, byte[] MED)
        {
            Console.WriteLine("MEL: " + MEL);
            Console.WriteLine("MED: " + '\n' + Tools.ByteArrayToString(MED));
            return "";
        }
        public string ParsePS(int MEL, byte[] MED)
        {
            Console.WriteLine("MEL: " + MEL);
            Console.WriteLine("MED: " + '\n' + Tools.ByteArrayToString(MED));
            return "";
        }
    }
}
