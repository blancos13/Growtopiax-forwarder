using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management;

namespace Growtopiax.Extensions
{
    public class HardwareID
    {
        private static string fingerPrint = string.Empty;
        public static string Value()
        {
            if (string.IsNullOrEmpty(fingerPrint))
            {
                fingerPrint = GetHash("CPU >> " + cpuId() + "\nBIOS >> " + biosId() + "\nBASE >> " + baseId());
            }
            return fingerPrint;
        }
        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier
        (string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        if (mo[wmiProperty] != null)
                            result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
                "MACAddress", "IPEnabled");
        }
        #endregion
    }

    public class CSharpPasswordDecoding
    {
        static unsafe uint decrypt(byte* data, uint size, int key)
        {
            uint checksum = 0;
            for (uint i = 0; i < size; i++)
            {
                checksum += data[i] + (uint)(key + i);
                data[i] = (byte)(data[i] - (2 + key + i));
            }
            return checksum;
        }

        static unsafe uint hash_str(char* str, int len)
        {
            if (str == null) return 0;
            var n = str;
            uint acc = 0x55555555;
            for (int i = 0; i < len; i++)
            {
                acc = (acc >> 27) + (acc << 5) + *n++;
            }
            return acc;
        }

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetVolumeInformation(string Volume, StringBuilder VolumeName,
        uint VolumeNameSize, out uint SerialNumber, uint SerialNumberLength,
        uint flags, StringBuilder fs, uint fs_size);

        static string get_identifier()
        {
            uint dwDiskSerial;
            if (!GetVolumeInformation("C:\\", null, 0, out dwDiskSerial, 0, 0, null, 0))
                if (!GetVolumeInformation("D:\\", null, 0, out dwDiskSerial, 0, 0, null, 0))
                    if (!GetVolumeInformation("E:\\", null, 0, out dwDiskSerial, 0, 0, null, 0))
                        if (!GetVolumeInformation("F:\\", null, 0, out dwDiskSerial, 0, 0, null, 0))
                            if (!GetVolumeInformation("G:\\", null, 0, out dwDiskSerial, 0, 0, null, 0))
                                return "";
            return dwDiskSerial.ToString();
        }

        public static string Get()
        {
            string save = File.ReadAllBytes((string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Growtopia", "path", null) + "\\save.dat").Byte2String();
            Regex pattern = new Regex(@"[^\x20-\x7E\w\x10-\x14\cA-\cz]+");
            string raw = pattern.Replace(save, "|").TrimStart('|').TrimEnd('|');
            string[] data = raw.Split('|');
            int index = Array.FindIndex(data, var => !var.Contains("chk") && var.Contains("tankid_password"));
            string found = data[index + 1].TrimEnd('\t', '\u0005');

            uint len = Convert.ToUInt32(found.Length);
            byte[] bytes = Encoding.Default.GetBytes(found);
            var pass = new byte[len];
            Array.Copy(bytes, pass, len);
            string device_id = get_identifier();
            unsafe
            {
                fixed (byte* b = pass)
                {
                    fixed (char* p = device_id)
                    {
                        uint hash = hash_str(p, device_id.Length);
                        var output = decrypt(b, len, (int)hash);
                        string decrypted = Encoding.Default.GetString(pass);
                        return decrypted;
                    }
                }
            }
        }
    }

    public static class StringExtension
    {
        public static string Base64Encode(this string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }

        public static string Base64Decode(this string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public static string CreateMD5(this string input)
        {
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString().ToLower();
            }
        }

        public static string Hash(this string str)
        {
            SHA1Managed sha1 = new SHA1Managed();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sb = new StringBuilder(hash.Length * 2);

            foreach (byte b in hash)
            {
                sb.Append(b.ToString("X2"));

            }
            return sb.ToString();
        }

        public static string Byte2String(this byte[] str)
        {
            return Encoding.Default.GetString(str);
        }

        public static double NextDouble(this Random RandGenerator, double MinValue, double MaxValue)
        {
            return RandGenerator.NextDouble() * (MaxValue - MinValue) + MinValue;
        }

        public static string DecompressString(this string compressedText)
        {
            byte[] array = Convert.FromBase64String(compressedText);
            string @string;
            using (MemoryStream memoryStream = new MemoryStream())
            {
                int num = BitConverter.ToInt32(array, 0);
                memoryStream.Write(array, 4, array.Length - 4);
                byte[] array2 = new byte[num];
                memoryStream.Position = 0L;
                using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Decompress))
                {
                    gzipStream.Read(array2, 0, array2.Length);
                }
                @string = Encoding.UTF8.GetString(array2);
            }
            return @string;
        }
    }

    public static class Rainbow
    {
        const float speed = 0.005f;
        const float speed_other = speed * 20f;
        static bool mode1 = false, mode2 = false;
        static Vector3 hsl = new Vector3(0f, 0.5f, 0.5f);


        public static void change_color(params Control[] controls)
        {
            hsl.x += speed;
            if (hsl.x > 1f)
            {
                hsl.x = 0f;
                hsl.y += mode1 ? -speed_other : speed_other;
            }

            if (hsl.y > 1f || hsl.y < 0.4f)
            {
                if (hsl.y > 1f)
                {
                    mode1 = true;
                    hsl.y -= speed_other;
                }
                else
                {
                    mode1 = false;
                    hsl.y += speed_other;
                }
                hsl.z += mode2 ? -speed_other : speed_other;

            }

            if (hsl.z > 0.7f || hsl.z < 0.3f)
            {
                if (hsl.z > 0.7f)
                {
                    mode2 = true;
                    hsl.z -= speed_other;
                }
                else
                {
                    mode2 = false;
                    hsl.z += speed_other;
                }
            }

            var cl = hsl.to_rgb();
            foreach (Control c in controls)
            {
                if (c is Panel)
                {
                    c.BackColor = cl;
                }
                else
                {
                    c.ForeColor = cl;
                }
            }

        }

        public class Vector3
        {
            public float x, y, z;

            public Vector3(float x2, float y2, float z2)
            {
                this.x = x2;
                this.y = y2;
                this.z = z2;
            }

            public override string ToString() => $"{Math.Round(x, 2)}, {Math.Round(y, 2)}, {Math.Round(z, 2)}";
            public Color to_rgb()
            {
                double h = this.x;
                double sl = this.y;
                double l = this.z;

                if (h < 0f)
                    h = 1f - -h;
                else if (h > 1f)
                    h = -(-h + 1f);

                double v = (l <= 0.5) ? (l * (1.0 + sl)) : (l + sl - l * sl), r = l, g = l, b = l;

                if (v > 0)
                {
                    int sextant;
                    double m, sv, fract, vsf, mid1, mid2;

                    m = l + l - v;
                    sv = (v - m) / v;
                    h *= 6.0;
                    sextant = (int)h;
                    fract = h - sextant;
                    vsf = v * sv * fract;
                    mid1 = m + vsf;
                    mid2 = v - vsf;
                    switch (sextant)
                    {
                        case 0:
                            r = v;
                            g = mid1;
                            b = m;
                            break;
                        case 1:
                            r = mid2;
                            g = v;
                            b = m;
                            break;
                        case 2:
                            r = m;
                            g = v;
                            b = mid1;
                            break;
                        case 3:
                            r = m;
                            g = mid2;
                            b = v;
                            break;
                        case 4:
                            r = mid1;
                            g = m;
                            b = v;
                            break;
                        case 5:
                            r = v;
                            g = m;
                            b = mid2;
                            break;
                    }
                }

                return Color.FromArgb(Convert.ToByte(r * 255.0f), Convert.ToByte(g * 255.0f), Convert.ToByte(b * 255.0f));
            }

        }
    }
}
