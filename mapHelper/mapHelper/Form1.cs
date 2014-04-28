using System;
<<<<<<< HEAD
=======
using System.Collections;
>>>>>>> 375b6da8952ce6171e832bd9a69bc4fcfb1eed88
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
<<<<<<< HEAD
=======
using System.Xml;
>>>>>>> 375b6da8952ce6171e832bd9a69bc4fcfb1eed88

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
<<<<<<< HEAD
            FieldDef fd = new FieldDef("Name");
            var fd1 = new List<FieldDef>();
          //  fd1.Add("Name1");
            //var fd1 = new FieldDef["name1","name2"];
            WriteFile wf = new WriteFile(fd);
            if (result == DialogResult.OK) // Test result.
            {
                //string directory = Directory.GetCurrentDirectory();
                //Console.WriteLine(directory);
                //fileName = openFileDialog1.FileName;
                //List<Segment> Sections = Parse(fileName);
                //foreach (Segment seg in Sections)
                //{
                //    Console.WriteLine();
                //    Console.Write(seg.Name + " ");
                //    for (int i = 0; i < seg.elements.Count; i++)
                //    {
                //        Console.Write(seg.elements[i].number + " " );
                //        Console.Write(seg.elements[i].Value + " ");
                //    }
                //}
               
            }
            Console.WriteLine(fileName); // <-- For debugging use.
        }
        public static List<Segment> Parse(string path) //метод парс, принимает путь к файлу возвращает лист
        {
            var reg2 = new Regex(@"(?<=<section )[\w\d]+(?=\>)"); //регулярное выражение для поиска начала секции
         //   var reg = new Regex(@"(?<=S)?=\>)"); //регулярное выражение для поиска начала секции
            var list = new List<Segment>(); // лист объектов класса секция
            string j="000";
            Segment sec = null; // создаем новую ссылку на объект типа секция
            foreach (string s in File.ReadAllLines(path)) // здесь в посте выше ошибка - должно быть path вместо пути, копировал просто, цикл выполняется для каждой строки в файле
                if (s.StartsWith("S")) // если найдено совпадение (начало секции)
                {
                    if (sec != null) // если у нас уже есть объект секции т.е. ссылка не null
                        list.Add(sec); //записываем в лист
                    sec = new Segment(); // создаем новый объект секции
                    //sec.Name = reg.Match(s).Value; //записываем имя секции
                    sec.Name = s.Substring(1);
                    Console.WriteLine(sec.Name);
                }
                else // если нет совпадения (не начало секции
                    if (s.Trim(' ').StartsWith("E")) // и не строка обозначающая ее конец
                    {

                      //  sec.elements.Add(new element { number = s.Split('0')[0], Value = s.Split('0')[1] }); //добавляем параметр в лист
                        sec.elements.Add(new element { number = s.Substring(1,3), Value = s.Substring(7) });
                        Console.WriteLine(s.Substring(1, 3));
                        Console.WriteLine(s.Substring(7));
                    }
            if (sec != null) // это нужно для файла без секций т.е. пустого
                list.Add(sec); //записываем последнюю секцию
            return list; //возвращаем лист
        }
    }
}
=======
            if (result == DialogResult.OK) // Test result.
            {
                string dataElementPath = Directory.GetCurrentDirectory();
                dataElementPath = dataElementPath + "\\Repository\\EDI\\004010\\standard\\dataelement.xsd";
                string segmentPath = Directory.GetCurrentDirectory();
                segmentPath = segmentPath + "\\Repository\\EDI\\004010\\standard\\segment.xsd";
                Console.WriteLine(segmentPath);
                fileName = openFileDialog1.FileName;
                InputDataParser inputData = new InputDataParser(segmentPath, dataElementPath, fileName);
            }

            Console.WriteLine(fileName); // <-- For debugging use.
        }
    
   
        }
    }


>>>>>>> 375b6da8952ce6171e832bd9a69bc4fcfb1eed88
