using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UECP_Decoder
{
    /*
    MIT License

    Copyright (c) 2017 Stéphane Lepin <stephane.lepin@gmail.com>

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
    */
    public enum MEC
    {
        RDS_PI = 0x01,
        RDS_PS = 0x02,
        RDS_PIN = 0x06,
        RDS_DI = 0x04,
        RDS_TA_TP = 0x03,
        RDS_MS = 0x05,
        RDS_PTY = 0x07,
        RDS_PTYN = 0x3E,
        RDS_RT = 0x0A,
        RDS_AF = 0x13,
        RDS_EON_AF = 0x14,
        RDS_SLOW_LABELING = 0x1A,
        RDS_LINKAGE_INFO = 0x2E,

        ODA_CONFIG = 0x40,
        ODA_IDENT = 0x41,
        ODA_FREE_FORMAT = 0x42,
        ODA_PRIORITY = 0x43,
        ODA_BURST_MODE = 0x44,
        ODA_SPIN_WHEEL = 0x45,
        ODA_DATA = 0x46,
        ODA_DATA_ACL = 0x47,

        TDC = 0x26,
        EWS = 0x2B,
        IH = 0x25,
        TMC = 0x30,
        FREE_FORMAT = 0x24,

        // Paging not implemented
        // Starting to implement it
        EPP = 0x31,
        // Starting to implement it

        RTC = 0x0D,
        RTC_CORRECTION = 0x09,
        CT_ON_OFF = 0x19,

        RDS_ON_OFF = 0x1E,
        RDS_PHASE = 0x22,
        RDS_LEVEL = 0x00,

        UECP_ACK = 0x18,
        UECP_REQUEST = 0x17
    }

    public struct MECRules
    {
        public static readonly MEC[] HasMEL = new MEC[] {
            MEC.RDS_RT,
            MEC.RDS_AF,
            MEC.RDS_EON_AF,
            MEC.ODA_IDENT,
            MEC.ODA_PRIORITY,
            MEC.ODA_DATA,
            MEC.TDC,
            MEC.TMC,
            MEC.UECP_REQUEST,
        };

        public static readonly MEC[] HasDSNPSN = new MEC[] {
            MEC.RDS_PI,
            MEC.RDS_PS,
            MEC.RDS_PIN,
            MEC.RDS_DI,
            MEC.RDS_TA_TP,
            MEC.RDS_MS,
            MEC.RDS_PTY,
            MEC.RDS_PTYN,
            MEC.RDS_RT,
            MEC.RDS_AF,
            MEC.RDS_EON_AF,
            MEC.RDS_SLOW_LABELING,
            MEC.RDS_LINKAGE_INFO,
            MEC.ODA_IDENT,
        };

        public static readonly MEC[] ExcludePSN = new MEC[]
        {
            MEC.RDS_SLOW_LABELING,
            MEC.ODA_IDENT,

        };
    }

    public enum PTY
    {
        None = 0,
        News = 1,
        CurrentAffairs = 2,
        Information = 3,
        Sport = 4,
        Education = 5,
        Drama = 6,
        Culture = 7,
        Science = 8,
        Varied = 9,
        PopMusic = 10,
        RockMusic = 11,
        EasyListening = 12,
        LightClassical = 13,
        SeriousClassical = 14,
        OtherMusic = 15,
        Weather = 16,
        Finance = 17,
        Children = 18,
        SocialAffairs = 19,
        Religion = 20,
        PhoneIn = 21,
        Travel = 22,
        Leisure = 23,
        JazzMusic = 24,
        CountryMusic = 25,
        NationalMusic = 26,
        OldiesMusic = 27,
        FolkMusic = 28,
        Documentary = 29,
        AlarmTest = 30,
        Alarm = 31
    }
}
