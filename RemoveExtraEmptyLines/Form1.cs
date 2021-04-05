using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RemoveExtraEmptyLines
{
    public partial class Form1 : Form
    {
        string fileName = "MainWindow.xaml";
        string[] fileTypeArray =
        {
            "*.txt",
            "*.cs",
            "*.xaml",
            "*.csv"
        };
        public Form1()
        {
            InitializeComponent();
        }

        public enum FileTypes
        {

        }

        private void btnRemoveEmptyLines_Click(object sender, EventArgs e)
        {
            File.Delete(fileName);
            //File.Move("MainWindow - Orignal.xaml", fileName);
            File.Copy("MainWindow - Orignal.xaml", fileName);

            string tempFile = Path.GetTempFileName();

            using (var sr = new StreamReader(fileName))
            using (var sw = new StreamWriter(tempFile))
            {
                string line;
                int emptyLineCounter = 0;
                int limitNumber = int.Parse(tbRemoveLine.Text);
                while ((line = sr.ReadLine()) != null)
                {
                    if (String.IsNullOrWhiteSpace(line) || String.IsNullOrEmpty(line))
                    {
                        emptyLineCounter++;
                        continue;
                    }
                    else
                    {
                        if (emptyLineCounter > 0)
                        {
                            sw.WriteLine(String.Empty);
                        }
                        sw.WriteLine(line);
                        emptyLineCounter = 0;
                    }
                }
            }

            File.Delete(fileName);
            File.Move(tempFile, fileName);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (var item in fileTypeArray)
            {
                cbFileType.Items.Add(item);
                cbFileType.SelectedIndex = 0;
                //cbFileType.SelectedItem = item[0];
            }
        }

        private void folderBrowserDialog1_HelpRequest(object sender, EventArgs e)
        {

        }
    }
}