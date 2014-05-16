using System;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace mapHelper
{
    public partial class Form1 : Form
    {
        public Form1()
        {

            InitializeComponent();

            List<String> globalCompaniesList = new List<String>();
            string dataElementPath = Directory.GetCurrentDirectory();
            System.Console.WriteLine("dataElementPath:" + dataElementPath);
            string bin = Directory.GetParent(dataElementPath).ToString();
            System.Console.WriteLine("bin:" + bin);
            dataElementPath = Directory.GetParent(bin) + "\\Repository\\EDI";
            globalCompaniesList.AddRange(Directory.GetDirectories(dataElementPath));
            foreach (string item in globalCompaniesList)
            {

                item.IndexOf("EDI\"");
                System.Console.WriteLine("item:" + item.Substring(item.IndexOf("EDI") + 4));
                comboBox2.Items.Add(item.Substring(item.IndexOf("EDI") + 4));

            }
            //try
            //{
            //    foreach (String d in Directory.GetDirectories(dataElementPath))
            //    {
            //        Console.WriteLine("my"+Directory.GetDirectories(d).Distinct().ToList());
            //        comboBox1.Items.Add(Directory.GetDirectories(d).Distinct().ToList());

            //    }
            //}
            //catch { }




        }
        string fileName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();
            if (result == DialogResult.OK) // Test result.
            {
                textBox3.Text = openFileDialog1.FileName;
            }

        }
        private void button3_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.

            var fd1 = new List<FieldDef>();


            Console.WriteLine(fileName); // <-- For debugging use.
            if (comboBox2.Text == "")
            {
                MessageBox.Show("Please set EDI version");
            }
            else if (textBox1.Text == "")
            {
                textBox1.BackColor = Color.Yellow;
                MessageBox.Show("Please set Folder for xtl.");
                textBox1.BackColor = Color.White;
            }
            else if (textBox2.Text == "")
            {
                textBox2.BackColor = Color.Yellow;
                MessageBox.Show("XTL name is required");
                textBox2.BackColor = Color.White;
            }
            else if (textBox3.Text == "")
            {
                textBox3.BackColor = Color.Yellow;
                MessageBox.Show("Choose test file");
                textBox3.BackColor = Color.White;
            }
            else
            {

                string dataElementPath = Directory.GetCurrentDirectory();
                string bin = Directory.GetParent(dataElementPath).ToString();
                dataElementPath = Directory.GetParent(bin) + "\\Repository\\EDI\\" + comboBox2.Text + "\\standard\\dataelement.xsd";
                Console.WriteLine(dataElementPath);
                string segmentPath = Directory.GetParent(bin) + "\\Repository\\EDI\\" + comboBox2.Text + "\\standard\\segment.xsd";
                Console.WriteLine(segmentPath);

                InputDataParser inputData = new InputDataParser(segmentPath, dataElementPath, textBox3.Text);
                WriteFile wf = new WriteFile(inputData.allFields, Directory.GetParent(bin) + "\\Repository\\EDI\\004010\\850.xsd");


            }

            //      Console.WriteLine(fileName); // <-- For debugging use.
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Show the FolderBrowserDialog.
            FolderBrowserDialog folderBrowserDialog1 = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                String folderName = folderBrowserDialog1.SelectedPath;
                System.Console.WriteLine("folderName:" + folderName);
                textBox1.Text = folderName;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }


    }
}
