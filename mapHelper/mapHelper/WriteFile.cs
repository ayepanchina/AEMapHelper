using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.ObjectModel;


namespace mapHelper
{
    public class WriteFile
    {
        String someName;


        public WriteFile(FieldDef fd1)
       {
           System.Console.WriteLine("Write Block:");
          // this.someName = name;


           StreamReader streamReader = new StreamReader("C:\\temp\\test.xtl");
           string str = "";

   /*        while (!streamReader.EndOfStream)
           {
               //str += streamReader.ReadLine();
               System.Console.WriteLine("Data file: " + streamReader.ReadLine());
           }
            */
           FileInfo file = new FileInfo("C:\\temp\\test1.xtl");
           if (file.Exists == true) //Если файл существует
           {
               file.Delete(); //Удаляем
           }
           else MessageBox.Show("Файла не существует!!");


           StreamWriter write_text;  //Класс для записи в файл
           file = new FileInfo("C:\\temp\\test1.xtl");
           write_text = file.AppendText(); //Дописываем инфу в файл, если файла не существует он создастся
           write_text.WriteLine("<!DOCTYPE SPSFILE SYSTEM '../form/Xtencil.dtd'>"); 
            write_text.WriteLine(" <SPSFILE name=\"SPS Commerce Xtencil\" date=\"4/23/2014\" fileType=\"FORM\" contents=\"NORM\">"); 
            write_text.WriteLine("<DOCUMENTDEF name=\"MotorCarrierFreightDetail\" javaName=\"motorCarrierFreightDetail\" javaPackageName=\"Standard\" xtencilType=\"XML\" designDate=\"4/23/2014\" resolverFetch=\"Y\" lastModifiedBy=\"\">");
            write_text.WriteLine(""+fd1.name); 
           
             
           write_text.Close(); // Закрываем файл
       //    System.Console.WriteLine("Data file: " + str);
       }
       
     //  FieldDef test = new FieldDef("S");

     
        

    }
    
    
          
      
}


