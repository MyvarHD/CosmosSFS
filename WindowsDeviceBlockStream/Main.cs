﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SimpleFileSystem;

namespace WindowsDeviceBlockStream
{
    public partial class Main : Form
    {
        private SimpleFS Fs { get; set; }

        public Main()
        {
            InitializeComponent();
        }

        private void Main_Load(object sender, EventArgs e)
        {
        }


        public void LoadDir()
        {
            listBox1.Items.Clear();
            foreach (var directory in Fs?.GetAllDirectories())
            {
                listBox1.Items.Add(directory);
            }

            foreach (var fl in Fs?.GetAllFiles())
            {
                listBox1.Items.Add(fl + " " + Fs.ReadAllText(fl));
            }
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Fs = new SimpleFS(new WindowsStreamBlockDevice(
                        ofd.FileName));
                    Fs.Load();
                    LoadDir();
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var ofd = new OpenFileDialog())
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Fs = new SimpleFS(new WindowsStreamBlockDevice(ofd.FileName));
                    Fs.Format();
                    LoadDir();
                }
            }
        }

        private void newToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Fs?.CreateDirectory("test");
//
//            Fs?.CreateDirectory(
//                "veryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryvery" +
//                "veryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryvery" +
//                "veryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryvery" +
//                "veryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryveryvery" +
//                "longName/lol/gg");
//            Fs?.CreateDirectory("test/bob");
//            Fs?.CreateDirectory("test/lol/gg");
            LoadDir();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > 0)
            {
                Fs.DeleteDirectory(listBox1.SelectedItem.ToString());
                LoadDir();
            }
        }

        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Fs?.DeleteFile("test.txt");
            // Fs?.DeleteFile("test.jpg");
//            var sw = new Stopwatch();
//           
//            var buf = File.ReadAllBytes("test.jpg");
//            sw.Start();
//            Fs?.WriteAllBytes("test.jpg", buf);
//            sw.Stop();
//            Console.WriteLine("Milliseconds: " + sw.Elapsed.Milliseconds + " |  bytes: " + buf.Length);
//
//
//            Fs?.WriteAllText("bob.txt", "xD");
//            Fs?.WriteAllText("lol.txt", "LOL");
            Fs?.WriteAllText("x.txt", "content of X");
            //Fs?.WriteAllBytes("test.jpg", File.ReadAllBytes("test.jpg"));

            LoadDir();
        }

        static string CalculateMD5(byte[] data)
        {
            using (var md5 = MD5.Create())
            {
                var hash = md5.ComputeHash(data);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }

        private void readTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Console.WriteLine("test.txt:");
            var sw = new Stopwatch();
            sw.Start();
            var buf = File.ReadAllBytes("test.jpg");
            sw.Stop();
            Console.WriteLine("Milliseconds: " + sw.Elapsed.Milliseconds + " |  bytes: " + buf.Length);
            //Console.WriteLine(Fs.ReadAllText("test.txt"));
            Console.WriteLine("test.jpg:");
            Console.WriteLine($"Before Hash: {CalculateMD5(File.ReadAllBytes("test.jpg"))}");
            Console.WriteLine($"After Hash: {CalculateMD5(Fs?.ReadAllBytes("test.jpg"))}");
            File.WriteAllBytes("test1.jpg", Fs?.ReadAllBytes("test.jpg"));


        }

        private void dDToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
    }
}