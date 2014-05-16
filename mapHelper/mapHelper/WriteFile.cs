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
using System.Xml;
using System.Collections;


namespace mapHelper
{
    public class WriteFile
    {
        String elementName;
   
      public WriteFile(List<FieldDef> all, string docPath)
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

            XmlDocument segment = new XmlDocument(); // создаем новый xml документ 
            FileStream fs = new FileStream(docPath, FileMode.Open);
            segment.Load(fs);
            XmlNodeList complexTypes = segment.GetElementsByTagName("complexType"); // Создаем список узлов по тегу "complexType"  
            XmlNodeList childNotesInComplexType;
           XmlNode currentSequenceInComplexType;
           XmlNode currentAnnotationInComplexType;
           XmlNode currentAppInfoChildInComplexType;
            int count = 0;
            bool isGroup = false;
            bool isloop = false;
            bool wasFound = false;
            Boolean flag1 = false;
          foreach ( FieldDef field in all)
           {

               foreach (XmlNode currentType in complexTypes) // проходим по всем тегам "complexType"  
               {
                    childNotesInComplexType = currentType.ChildNodes; // получаем дочерние узлы "complexType"
                   if (count == 0)
                   {
                       if (currentType.Attributes["me:nodetype"].Value.ToString().Equals("MAP"))
                       {

                           foreach (XmlNode childNoteInComplexType in childNotesInComplexType) //проходим по всем дочерним узлам "complexType"
                           {
                               if (childNoteInComplexType.Name.ToString().Equals("sequence")) // если имя узла  = sequence и в теге annotation значение тэга "me:id" совпало  именем сегмента в тестовых данных
                               {

                                   if (childNoteInComplexType.HasChildNodes)
                                   {
                                       IEnumerator ienum = childNoteInComplexType.GetEnumerator();
                                       while (ienum.MoveNext())
                                       {
                                           currentSequenceInComplexType = (XmlNode)ienum.Current;
                                           if (currentSequenceInComplexType.Attributes["me:nodetype"].Value.Equals("SEG") &
                                               currentSequenceInComplexType.Attributes["type"].Value.Substring(5).Equals(field.segmentTag))//сравниваем значение аттрибута name тэга element с номером элемента в тестовых данных
                                           {
                                               wasFound = true;
                                               if ( currentSequenceInComplexType.Attributes["maxOccurs"].Value.Equals("1"))
                                               {
                                                   write_text.WriteLine(" <FIELDDEF name=\"" + field.name + "\" "
                                                         + " javaName=\"" + field.javaName + "\""
                                                         + " minLength=\"" + field.minLength + "\""
                                                         + " maxLength=\"" + field.maxLength + "\""
                                                         + " dataType=\"" + field.datatype + "\" rounding=\"2\"");
                                                        if (field.precision == 2) write_text.Write(" precision=\"2\"");
                                                            write_text.Write(
                                                        " mandatory=\"N\" exclude=\"N\" display=\"Y\" editable=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" keyType=\"NONE\" persistent=\"Y\" present=\"Y\" templatable=\"Y\" dtdRequired=\"N\""
                                                        + " edi=\"Y\""
                                                        + " segmentTag=\"" + field.segmentTag + "\""
                                                        + " position=\"" + field.position + "\""
                                                        + " referenceNum=\"" + field.refenerceNum + "\""
                                                        + " includeInTestFile=\"Y\">"
                                                        + "</FIELDDEF>");
                                               }
                                               else if (!currentSequenceInComplexType.Attributes["maxOccurs"].Value.Equals("1"))
                                               {
                                                   isGroup = true;
                                                   write_text.WriteLine("<GROUPDEF "
                                                   + " name=\"" + field.segmentTag + "\""
                                                   + " javaName=\"" + field.segmentTag.ToLower() + "\""
                                                   +" min=\"0\"");
                                                   if (currentSequenceInComplexType.Attributes["maxOccurs"].Value.Equals("unbounded")) 
                                                   {
                                                       write_text.Write(" max=\"100\"");
                                                   }
                                                   else 
                                                   {
                                                      write_text.Write( " max=\"" + currentSequenceInComplexType.Attributes["maxOccurs"].Value + "\"");
                                                   }
                                                   write_text.Write(" exclude=\"N\" justification=\"Left\" display=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" sourceFilter=\"No Filter\" isRecord=\"N\" persistent=\"Y\" present=\"Y\" includeInTestFile=\"Y\">");
                                                  write_text.WriteLine("<GROUPDEF "
                                                   +" name=\"" + field.segmentTag + " Rep\""
                                                   +" javaName=\"" + field.segmentTag.ToLower() + "Rep\""
                                                   +" min=\"1\" max=\"1\" exclude=\"N\" justification=\"Left\" display=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" sourceFilter=\"No Filter\" isRecord=\"N\" persistent=\"Y\" present=\"Y\">");
                                                   write_text.WriteLine(" <FIELDDEF name=\"" + field.name + "\" "
                                                         + " javaName=\"" + field.javaName + "\""
                                                         + " minLength=\"" + field.minLength + "\""
                                                         + " maxLength=\"" + field.maxLength + "\""
                                                         + " dataType=\"" + field.datatype + "\" rounding=\"2\"");
                                                   if (field.precision == 2) write_text.Write(" precision=\"2\"");
                                                   write_text.Write(
                                               " mandatory=\"N\" exclude=\"N\" display=\"Y\" editable=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" keyType=\"NONE\" persistent=\"Y\" present=\"Y\" templatable=\"Y\" dtdRequired=\"N\""
                                               + " edi=\"Y\""
                                               + " segmentTag=\"" + field.segmentTag + "\""
                                               + " position=\"" + field.position + "\""
                                               + " referenceNum=\"" + field.refenerceNum + "\""
                                               + " includeInTestFile=\"Y\">"
                                               + "</FIELDDEF>");
                                                   write_text.WriteLine(" </GROUPDEF>");
                                               }
                                               Console.WriteLine(field.segmentTag);
                                               break;
                                           }
                                           else 
                                              if (currentSequenceInComplexType.Attributes["me:nodetype"].Value.Equals("") &
                                               !currentSequenceInComplexType.Attributes["type"].Value.Substring(5).Equals(field.segmentTag))//сравниваем значение аттрибута name тэга element с номером элемента в тестовых данных
                                              {

                                              }
                                       }
                                   }
                               }
                           
                           }
                       }
                       break;
                   }
                   else if (count != 0 & !elementName.Equals(field.segmentTag))
                   {
                       wasFound = false;
                       if (isGroup)
                       {

                           write_text.WriteLine(" </GROUPDEF>");
                           write_text.WriteLine(" </GROUPDEF>");
                           isGroup = false;
                       }
                       if (currentType.Attributes["me:nodetype"].Value.ToString().Equals("MAP"))
                       {
                           foreach (XmlNode childNoteInComplexType in childNotesInComplexType) //проходим по всем дочерним узлам "complexType"
                           {
                               if (childNoteInComplexType.Name.ToString().Equals("sequence")) // если имя узла  = sequence и в теге annotation значение тэга "me:id" совпало  именем сегмента в тестовых данных
                               {

                                   if (childNoteInComplexType.HasChildNodes)
                                   {
                                       IEnumerator ienum = childNoteInComplexType.GetEnumerator();
                                       while (ienum.MoveNext())
                                       {
                                           currentSequenceInComplexType = (XmlNode)ienum.Current;
                                           if (currentSequenceInComplexType.Attributes["me:nodetype"].Value.Equals("SEG") &
                                               currentSequenceInComplexType.Attributes["type"].Value.Substring(5).Equals(field.segmentTag))//сравниваем значение аттрибута name тэга element с номером элемента в тестовых данных
                                           {
                                               wasFound = true;
                                               if (currentSequenceInComplexType.Attributes["maxOccurs"].Value.Equals("1"))
                                               {
                                                   write_text.WriteLine(" <FIELDDEF name=\"" + field.name + "\" "
                                                         + " javaName=\"" + field.javaName + "\""
                                                         + " minLength=\"" + field.minLength + "\""
                                                         + " maxLength=\"" + field.maxLength + "\""
                                                         + " dataType=\"" + field.datatype + "\" rounding=\"2\"");
                                                   if (field.precision == 2) write_text.Write(" precision=\"2\"");
                                                   write_text.Write(
                                               " mandatory=\"N\" exclude=\"N\" display=\"Y\" editable=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" keyType=\"NONE\" persistent=\"Y\" present=\"Y\" templatable=\"Y\" dtdRequired=\"N\""
                                               + " edi=\"Y\""
                                               + " segmentTag=\"" + field.segmentTag + "\""
                                               + " position=\"" + field.position + "\""
                                               + " referenceNum=\"" + field.refenerceNum + "\""
                                               + " includeInTestFile=\"Y\">"
                                               + "</FIELDDEF>");
                                               }
                                               else if (!currentSequenceInComplexType.Attributes["maxOccurs"].Value.Equals("1"))
                                               {
                                                   isGroup = true;
                                                   write_text.WriteLine("<GROUPDEF "
                                                   + " name=\"" + field.segmentTag + "\""
                                                   + " javaName=\"" + field.segmentTag.ToLower() + "\""
                                                   + " min=\"0\"");
                                                   if (currentSequenceInComplexType.Attributes["maxOccurs"].Value.Equals("unbounded"))
                                                   {
                                                       write_text.Write(" max=\"100\"");
                                                   }
                                                   else
                                                   {
                                                       write_text.Write(" max=\"" + currentSequenceInComplexType.Attributes["maxOccurs"].Value + "\"");
                                                   }
                                                   write_text.Write(" exclude=\"N\" justification=\"Left\" display=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" sourceFilter=\"No Filter\" isRecord=\"N\" persistent=\"Y\" present=\"Y\" includeInTestFile=\"Y\">");
                                                   write_text.WriteLine("<GROUPDEF "
                                                  + " name=\"" + field.segmentTag + " Rep\""
                                                  + " javaName=\"" + field.segmentTag.ToLower() + "Rep\""
                                                  + " min=\"1\" max=\"1\" exclude=\"N\" justification=\"Left\" display=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" sourceFilter=\"No Filter\" isRecord=\"N\" persistent=\"Y\" present=\"Y\">");
                                                 
                                                   write_text.WriteLine(" <FIELDDEF name=\"" + field.name + "\" "
                                                         + " javaName=\"" + field.javaName + "\""
                                                         + " minLength=\"" + field.minLength + "\""
                                                         + " maxLength=\"" + field.maxLength + "\""
                                                         + " dataType=\"" + field.datatype + "\" rounding=\"2\"");
                                                   if (field.precision == 2) write_text.Write(" precision=\"2\"");
                                                   write_text.Write(
                                               " mandatory=\"N\" exclude=\"N\" display=\"Y\" editable=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" keyType=\"NONE\" persistent=\"Y\" present=\"Y\" templatable=\"Y\" dtdRequired=\"N\""
                                               + " edi=\"Y\""
                                               + " segmentTag=\"" + field.segmentTag + "\""
                                               + " position=\"" + field.position + "\""
                                               + " referenceNum=\"" + field.refenerceNum + "\""
                                               + " includeInTestFile=\"Y\">"
                                               + "</FIELDDEF>");
                                                 
                                               }
                                               Console.WriteLine(field.segmentTag);
                                               break;
                                           }
                                       }
                                   }
                               }

                           }
                       }
                   
                       break;
                   }
                   else if (elementName.Equals(field.segmentTag) && wasFound)
                   {
                       write_text.WriteLine(" <FIELDDEF name=\"" + field.name + "\" "
                                                         + " javaName=\"" + field.javaName + "\""
                                                         + " minLength=\"" + field.minLength + "\""
                                                         + " maxLength=\"" + field.maxLength + "\""
                                                         + " dataType=\"" + field.datatype + "\" rounding=\"2\"");
                       if (field.precision == 2) write_text.Write(" precision=\"2\"");
                       write_text.Write(
                   " mandatory=\"N\" exclude=\"N\" display=\"Y\" editable=\"Y\" enable=\"Y\" print=\"Y\" nextRow=\"N\" keyType=\"NONE\" persistent=\"Y\" present=\"Y\" templatable=\"Y\" dtdRequired=\"N\""
                   + " edi=\"Y\""
                   + " segmentTag=\"" + field.segmentTag + "\""
                   + " position=\"" + field.position + "\""
                   + " referenceNum=\"" + field.refenerceNum + "\""
                   + " includeInTestFile=\"Y\">"
                   + "</FIELDDEF>");
                     

                       break;
                   }
                   if (!wasFound)
                   {
                       foreach (XmlNode currentType2 in complexTypes) // проходим по всем тегам "complexType"  
                       {
                           XmlNodeList childNotesInComplexType2 = currentType2.ChildNodes; // получаем дочерние узлы "complexType"
                         //  Console.WriteLine(field.segmentTag + " NOT FOUND");
                           if (currentType2.Attributes["me:nodetype"].Value.ToString().Equals("LOOP"))
                           {

                               foreach (XmlNode childNoteInComplexType in childNotesInComplexType2) //проходим по всем дочерним узлам "complexType"
                               {
                                   if (childNoteInComplexType.Name.ToString().Equals("annotation")) // если имя узла  = annotation
                                   {
                                       IEnumerator ienum3 = childNoteInComplexType.GetEnumerator(); //получаем дочерние узлы annotation
                                       while (ienum3.MoveNext()) // цикл по дочерним узлам тега annotation
                                       {
                                           currentAnnotationInComplexType = (XmlNode)ienum3.Current;
                                           if (currentAnnotationInComplexType.Name.ToString().Equals("appinfo") && currentAnnotationInComplexType.HasChildNodes) // заходим в тэг appinfo
                                           {
                                               IEnumerator ienum4 = currentAnnotationInComplexType.GetEnumerator();//получаем дочерние узлы appinfo
                                               while (ienum4.MoveNext())
                                               {
                                                   currentAppInfoChildInComplexType = (XmlNode)ienum4.Current; // текущий дочерний узел appinfo
                                                   if (currentAppInfoChildInComplexType.Name.ToString().Equals("me:id") & currentAppInfoChildInComplexType.FirstChild.Value.ToString().Equals(field.segmentTag))//проверяем совпадает ли значение тега me:id с именем сегмента в тестовых данных
                                                   {
                                                       flag1 = true;
                                                       break;
                                                   }

                                               }
                                           }
                                       }
                                   }
                                   if (childNoteInComplexType.Name.ToString().Equals("sequence") & flag1) // если имя узла  = sequence и в теге annotation значение тэга "me:id" совпало  именем сегмента в тестовых данных
                                   {
                                       flag1 = false;
                                       Console.WriteLine("loop " + field.segmentTag + currentType2.Attributes["name"].Value);
                                       break;
                                   }

                               }

                           }
                           // break;
                       }
                   }
               }

               count++;
               elementName = field.segmentTag;
         
           }
            write_text.WriteLine(" </GROUPDEF>");
            write_text.WriteLine("</DOCUMENTDEF>");
            write_text.WriteLine("</SPSFILE>");

            fs.Close();
           write_text.Close(); // Закрываем файл

       }

      
       

     
        

    }
    
    
          
      
}


