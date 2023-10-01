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

namespace ParallelForForEachCancellationApp
{
    public partial class Form1 : Form
    {
        CancellationTokenSource ct = new CancellationTokenSource();
        public Form1()
        {
            InitializeComponent();
            ct = new CancellationTokenSource();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ct = new CancellationTokenSource();
            List<string> list = new List<string>()
            {
                "https://www.google.com",
                "https://www.amazon.com",
                "https://www.microsoft.com",
                 "https://www.google.com",
                "https://www.amazon.com",
                "https://www.microsoft.com",
                "https://www.google.com",
                "https://www.amazon.com",
                "https://www.microsoft.com",
                 "https://www.google.com",
                "https://www.amazon.com",
                "https://www.microsoft.com"
            };

            HttpClient client = new HttpClient();

            ParallelOptions parallelOptions = new ParallelOptions();
            parallelOptions.CancellationToken = ct.Token;

            Task.Run(() => { //taska alma sebebimiz threadler çakışmasın diye

                try
                {
                    Parallel.ForEach<string>(list, parallelOptions, (url) =>
                    {
                        string content = client.GetStringAsync(url).Result;

                        string data = $"{url} {content.Length}";

                        parallelOptions.CancellationToken = ct.Token;

                        ct.Token.ThrowIfCancellationRequested();

                        listBox1.Invoke((MethodInvoker)delegate { listBox1.Items.Add(data); });
                    });
                }
                catch (Exception ex)
                {

                    MessageBox.Show("İşlem iptal edildi" + ex.Message);
                }

            });


        }

        private void button3_Click(object sender, EventArgs e)
        {
            ct.Cancel();
        }
    }
}
