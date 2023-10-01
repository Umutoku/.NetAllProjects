﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PlinqFormApp
{
    public partial class Form1 : Form
    {
        CancellationTokenSource cts;

        public Form1()
        {
            InitializeComponent();
            cts = new CancellationTokenSource();
        }

        private bool Hesapla(int x)
        {
            Thread.SpinWait(50000); //iterasyon kadar kod döndürüyor.
            return x % 12 == 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                try
                {
                    Enumerable.Range(1, 100000).AsParallel()
                    .WithCancellation(cts.Token)
                    .Where(Hesapla)
                    .ToList().ForEach(x =>
                    {
                        listBox1.Invoke((MethodInvoker)delegate          { listBox1.Items.Add(x); });
                    });
                }
                catch (OperationCanceledException ex)
                {

                    MessageBox.Show("işlem iptal");
                }
                catch (Exception ex2)
                {

                    throw;
                }

            }
            );

        }

        private void button2_Click(object sender, EventArgs e)
        {
            cts.Cancel();
        }
    }
}
