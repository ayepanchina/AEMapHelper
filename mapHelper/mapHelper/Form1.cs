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
        }
        string fileName = "";
        private void button1_Click(object sender, EventArgs e)
        {
            // Show the dialog and get result.
            DialogResult result = openFileDialog1.ShowDialog();
            var fd1 = new List<FieldDef>();
           
          
            Console.WriteLine(fileName); // <-- For debugging use.
       
            if (result == DialogResult.OK) // Test result.
            {
                string dataElementPath =  Directory.GetCurrentDirectory();
                string bin = Directory.GetParent(dataElementPath).ToString();
                dataElementPath = Directory.GetParent(bin) + "\\Repository\\EDI\\004010\\standard\\dataelement.xsd";
                Console.WriteLine(dataElementPath);
                string segmentPath = Directory.GetParent(bin) + "\\Repository\\EDI\\004010\\standard\\segment.xsd";
                Console.WriteLine(segmentPath);
                fileName = openFileDialog1.FileName;
                InputDataParser inputData = new InputDataParser(segmentPath, dataElementPath, fileName);
                WriteFile wf = new WriteFile(inputData.allFields);
               
            }

            Console.WriteLine(fileName); // <-- For debugging use.
        }
    
   
        }
    }
