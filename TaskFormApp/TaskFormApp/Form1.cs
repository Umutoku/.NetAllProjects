using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskFormApp
{
    public partial class BtnReadFile : Form
    {
        public int counter { get; set; } = 0;
        public BtnReadFile()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            string data = string.Empty;
            Task<string> okuma = ReadFileAsync();
            richTextBox2.Text = await new HttpClient().GetStringAsync("http://www.google.com");

            data = await okuma;
            richTextBox1.Text = data;
        }

        private void btnCounter_Click(object sender, EventArgs e)
        {
            textBoxCounter.Text = counter++.ToString();
        }

        private string ReadFile()
        {
            string data = string.Empty;
            using(StreamReader s = new StreamReader("isimler.txt"))
            {
                Thread.Sleep(5000);
                data=s.ReadToEnd();
            }
            return data;
        }
        //void

        private async Task<string> ReadFileAsync()
        {
            string data = string.Empty;
            using (StreamReader s = new StreamReader("isimler.txt"))
            {
                Task<string> mytask = s.ReadToEndAsync();
                //10 saniye süren işlem

                await Task.Delay(5000);
                data = await mytask;
            }
            return data;
        }

        private Task<string> ReadFileAsync2()
        {
            StreamReader s = new StreamReader("isimler.txt");
            
                return s.ReadToEndAsync();
            
        }

        }
}
