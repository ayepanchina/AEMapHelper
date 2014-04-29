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
   
      public WriteFile(List<FieldDef> all)
       {
           System.Console.WriteLine("Write Block:");
           
           FileInfo file = new FileInfo("C:\\temp\\test1.xtl");
           if (file.Exists == true) //Если файл существует
           {
               file.Delete(); //Удаляем
           }
          // else MessageBox.Show("Файла не существует!!");


           StreamWriter write_text;  //Класс для записи в файл
           file = new FileInfo("C:\\temp\\test1.xtl");
           write_text = file.AppendText(); //Дописываем инфу в файл, если файла не существует он создастся
           write_text.WriteLine("<?xml version='1.0' encoding='UTF-8'?>");
           write_text.WriteLine("<!DOCTYPE SPSFILE SYSTEM '../form/Xtencil.dtd'>"); 
            write_text.WriteLine(" <SPSFILE name=\"SPS Commerce Xtencil\" date=\"4/23/2014\" fileType=\"FORM\" contents=\"NORM\">"); 
            write_text.WriteLine("<DOCUMENTDEF name=\"MotorCarrierFreightDetail\" javaName=\"motorCarrierFreightDetail\" javaPackageName=\"Standard\" xtencilType=\"XML\" designDate=\"4/23/2014\" resolverFetch=\"Y\" lastModifiedBy=\"\">");
            write_text.WriteLine("<GROUPDEF name=\"Header\" javaName=\"header\" min=\"1\" max=\"1\" exclude=\"N\" justification=\"Left\" display=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" sourceFilter=\"No Filter\" isRecord=\"N\" persistent=\"Y\" present=\"Y\" includeInTestFile=\"Y\">");
            write_text.WriteLine(" <FIELDDEF name=\"Transaction Type\" javaName=\"setPurpose\" minLength=\"2\" maxLength=\"2\" dataType=\"JMappedSet\" rounding=\"2\" choices=\"00:Original,01:Cancellation,05:Replace\" mandatory=\"N\" exclude=\"N\" display=\"Y\" editable=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" keyType=\"NONE\" persistent=\"Y\" present=\"Y\" templatable=\"Y\" dtdRequired=\"N\" edi=\"Y\" segmentTag=\"BEG\" position=\"01\" referenceNum=\"353\" autoQualifier=\"Y\" includeInTestFile=\"Y\"></FIELDDEF>");
           foreach ( FieldDef field in all)
           {
                write_text.WriteLine(" <FIELDDEF name=\""+ field.name +
                    "\" "+"javaName=\"" + field.javaName + "\"" + " minLength=\"2\" maxLength=\"2\" dataType=\"JMappedSet\" rounding=\"2\" choices=\"00:Original,01:Cancellation,05:Replace\" mandatory=\"N\" exclude=\"N\" display=\"Y\" editable=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" keyType=\"NONE\" persistent=\"Y\" present=\"Y\" templatable=\"Y\" dtdRequired=\"N\" edi=\"Y\" segmentTag=\"BEG\" position=\"01\" referenceNum=\"353\" autoQualifier=\"Y\" includeInTestFile=\"Y\"></FIELDDEF>");
         
           }
            write_text.WriteLine(" </GROUPDEF>");
            write_text.WriteLine("</DOCUMENTDEF>");
            write_text.WriteLine("</SPSFILE>");
                 
             
           write_text.Close(); // Закрываем файл

       }
       

     
        

    }
    
    
          
      
}


