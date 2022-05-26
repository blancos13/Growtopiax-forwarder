using Growtopiax.Extensions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Growtopiax
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private void DragMove(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }


        private void exitB_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void minimizeB_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void rgb_Tick(object sender, EventArgs e)
        {
            Rainbow.change_color(upperRgb);
        }

        private void loginB_Click(object sender, EventArgs e)
        {
            NameValueCollection register = new NameValueCollection();

            string hwid = HardwareID.Value();

            register["username"] = userBox.Text;
            register["password"] = passBox.Text;
            register["hwid"] = hwid;

            byte[] rawresponse = Program.client.UploadValues("http://127.0.0.1/api/login/login.php", register);
            string response = Encoding.Default.GetString(rawresponse);
            MessageBox.Show(response);
            string[] split = response.Split('|');
            Console.WriteLine(response);

            if (response.Contains("success"))
            {
                Variables.PCoinAddress = split[1];

                Variables.Username = userBox.Text;
                Variables.Password = passBox.Text;
                Variables.Hwid = hwid;

                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default).CreateSubKey("Growtopiax"))
                {
                    if (key != null)
                    {
                        key.SetValue("Username", userBox.Text);
                        key.SetValue("Password", passBox.Text);
                        if (key.GetValueNames().Contains("Teams"))
                        {
                            string[] val = (string[])key.GetValue("Teams");
                            Variables.Teams.AddRange(val);
                        }
                    }
                }

                rgb.Stop();

                Hide();
                var mainForm = new Growtopiax();
                mainForm.Closed += (s, args) => Close();
                mainForm.Show();
            }
            else
            {
                switch (split[0])
                {
                    case "ban":
                        MessageBox.Show("Sorry but you're banned/blacklisted.", "Your account has been banned.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "fail":
                        MessageBox.Show("There is an error with our database. Please contact the developers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "stolen":
                        MessageBox.Show("This is not your account or you are using a different device.", "Unknown device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("There is an error with our database. Please contact the developers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void regB_Click(object sender, EventArgs e)
        {
            if (userBox.Text.Length < 3 || passBox.Text.Length < 8)
            {
                MessageBox.Show("Minimun length for username is 3 and password is 8.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            NameValueCollection register = new NameValueCollection();
            register["HTTP_X_FORWARDED_FOR"] = "127.0.0.1";
            register["username"] = userBox.Text;
            register["password"] = passBox.Text;
            register["hwid"] = HardwareID.Value();

            byte[] rawresponse = Program.client.UploadValues("http://127.0.0.1/api/login/register.php", register);
            string response = Encoding.Default.GetString(rawresponse);
            MessageBox.Show(response);

            switch (response)
            {
                case "success":
                    MessageBox.Show("Registered Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "overuse":
                    MessageBox.Show("The username is taken or your HWID already exists in the database.", "Multiple accounts are not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "fail":
                    MessageBox.Show("There is an error with our database. Please contact the developers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "vpn":
                    MessageBox.Show("VPN/Any type of proxy are not allowed!", "VPN/Proxy are Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("There is an error with our database. Please contact the developers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default).OpenSubKey("Growtopiax"))
            {
                if (key != null)
                {
                    userBox.Text = key.GetValue("Username").ToString();
                    passBox.Text = key.GetValue("Password").ToString();
                }
            }
        }

        private void userBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void darkLabel1_Click(object sender, EventArgs e)
        {

        }

        private void regB_Click_1(object sender, EventArgs e)
        {
            if (userBox.Text.Length < 3 || passBox.Text.Length < 8)
            {
                MessageBox.Show("Minimun length for username is 3 and password is 8.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            NameValueCollection register = new NameValueCollection();
            register["HTTP_X_FORWARDED_FOR"] = "127.0.0.1";
            register["username"] = userBox.Text;
            register["password"] = passBox.Text;
            register["hwid"] = HardwareID.Value();

            byte[] rawresponse = Program.client.UploadValues("http://127.0.0.1/api/login/register.php", register);
            string response = Encoding.Default.GetString(rawresponse);
            MessageBox.Show(response);

            switch (response)
            {
                case "success":
                    MessageBox.Show("Registered Successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                case "overuse":
                    MessageBox.Show("The username is taken or your HWID already exists in the database.", "Multiple accounts are not allowed.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "fail":
                    MessageBox.Show("There is an error with our database. Please contact the developers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                case "vpn":
                    MessageBox.Show("VPN/Any type of proxy are not allowed!", "VPN/Proxy are Prohibited", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
                default:
                    MessageBox.Show("There is an error with our database. Please contact the developers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    break;
            }
        }

        private void loginB_Click_1(object sender, EventArgs e)
        {
            NameValueCollection register = new NameValueCollection();

            string hwid = HardwareID.Value();

            register["username"] = userBox.Text;
            register["password"] = passBox.Text;
            register["hwid"] = hwid;

            byte[] rawresponse = Program.client.UploadValues("http://127.0.0.1/api/login/login.php", register);
            string response = Encoding.Default.GetString(rawresponse);
            MessageBox.Show(response);
            string[] split = response.Split('|');
            Console.WriteLine(response);

            if (response.Contains("success"))
            {
                Variables.PCoinAddress = split[1];

                Variables.Username = userBox.Text;
                Variables.Password = passBox.Text;
                Variables.Hwid = hwid;

                using (RegistryKey key = RegistryKey.OpenBaseKey(RegistryHive.CurrentUser, RegistryView.Default).CreateSubKey("Growtopiax"))
                {
                    if (key != null)
                    {
                        key.SetValue("Username", userBox.Text);
                        key.SetValue("Password", passBox.Text);
                        if (key.GetValueNames().Contains("Teams"))
                        {
                            string[] val = (string[])key.GetValue("Teams");
                            Variables.Teams.AddRange(val);
                        }
                    }
                }

                rgb.Stop();

                Hide();
                var mainForm = new Main();
                mainForm.Closed += (s, args) => Close();
                mainForm.Show();
            }
            else
            {
                switch (split[0])
                {
                    case "ban":
                        MessageBox.Show("Sorry but you're banned/blacklisted.", "Your account has been banned.", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "fail":
                        MessageBox.Show("There is an error with our database. Please contact the developers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    case "stolen":
                        MessageBox.Show("This is not your account or you are using a different device.", "Unknown device", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                    default:
                        MessageBox.Show("There is an error with our database. Please contact the developers!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        break;
                }
            }
        }

        private void rgb_Tick_1(object sender, EventArgs e)
        {
            Rainbow.change_color(upperRgb);
        }

        private void rgb2_Paint(object sender, PaintEventArgs e)
        {
    
        }

        private void rgb3_Tick(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void exitB_Click_1(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void minimizeB_Click_1(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void upperRgb_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}