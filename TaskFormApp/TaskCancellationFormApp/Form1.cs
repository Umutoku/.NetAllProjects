using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TaskCancellationFormApp
{
    public partial class Form1 : Form
    {
        CancellationTokenSource ct = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
        }

        private async void Start_Click(object sender, EventArgs e)
        {
            try
            {
                Task<HttpResponseMessage> myTask;

                myTask = new HttpClient().GetAsync("https://localhost:5001/home", ct.Token);

                await myTask;

                var content = await myTask.Result.Content.ReadAsStringAsync();

                richTextBox1.Text = content;
            }
            catch (TaskCanceledException ex)
            {
                MessageBox.Show(ex.Message);
            }        
        }

        private void Stop_Click(object sender, EventArgs e)
        {
            ct.Cancel();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
