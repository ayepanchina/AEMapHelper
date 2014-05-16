using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mapHelper
{
    class InputDataParser
    {
        public List<FieldDef> allFields;
        public InputDataParser (string segmentPath, string dataelementPath, string testFilePath)
        {
            allFields = xmlParse(segmentPath, dataelementPath, Parse(testFilePath));
            
            foreach (Segment seg in Parse(testFilePath))
            {
                Console.WriteLine();
                Console.Write(seg.Name + " ");
                for (int i = 0; i < seg.elements.Count; i++)
                {
                    Console.Write(seg.elements[i].number + " ");
                    Console.Write(seg.elements[i].Value + " ");
                }
            }
            foreach (FieldDef field in xmlParse(segmentPath, dataelementPath, Parse(testFilePath)))
            {
                Console.WriteLine();
                Console.Write(field.segmentTag + " ");
                Console.Write(field.position + " ");
                Console.Write(field.refenerceNum + " ");
                Console.Write(field.name + " ");
                Console.Write(field.javaName + " ");
                Console.Write(field.minLength + " ");
                Console.Write(field.maxLength + " ");
                Console.Write(field.datatype + " ");
                Console.Write(field.precision + " ");

            }
        }
         public static List<Segment> Parse(string testFilePath) //метод парс, принимает путь к файлу возвращает лист
        {

            var list = new List<Segment>(); // лист объектов класса сегментов
            Segment sec = null; // создаем новую ссылку на объект типа сегмент
            foreach (string s in File.ReadAllLines(testFilePath)) // проходим по всем строкам в файле
                if (s.StartsWith("S")) // если найдено совпадение (начало сегмента)
                {
                    if (sec != null) // если уже есть объект сегмента т.е. ссылка не null
                        list.Add(sec); //записываем в лист
                    sec = new Segment(); // создаем новый объект сегмента
                    sec.Name = s.Substring(1);//записываем значение в переменную Name поля сегмент
                  //  Console.WriteLine(sec.Name);
                }
                else if (s.Trim(' ').StartsWith("E")) // если найдено совпадение (начало элемента)
                    {

                        sec.elements.Add(new element { number = s.Substring(1,3), Value = s.Substring(7) }); // добавляем в список элементов класса сегмент номер и значение элемента 
                       // Console.WriteLine(s.Substring(1, 3));
                      //  Console.WriteLine(s.Substring(7));
                    }
            if (sec != null) // это нужно для файла без сегмента т.е. пустого
                list.Add(sec); //записываем последний сегмент
            return list; //возвращаем лист
        }

        public static List<FieldDef> xmlParse(string segmentPath, string dataelementPath, List<Segment> testData) //метод парс, принимает путь к файлам и коллекцию тестовых данных возвращает лист
        {
            List<FieldDef> allFields = new List<FieldDef>();
            FieldDef field = null;

            XmlDocument segment = new XmlDocument(); // создаем новый xml документ 
            FileStream fs = new FileStream(segmentPath, FileMode.Open); 
            segment.Load(fs); 
            XmlNodeList complexTypes = segment.GetElementsByTagName("complexType"); // Создаем список узлов по тегу "complexType"  

            XmlDocument dataelement = new XmlDocument();// создаем новый xml документ 
            FileStream fs2 = new FileStream(dataelementPath, FileMode.Open);
            XmlNodeList simpleTypes = dataelement.GetElementsByTagName("simpleType"); // Создаем список узлов по тегу "simpleType"                     
            dataelement.Load(fs2);

            XmlNode currentAnnotationChildInSimpleType; // узел annotation
            XmlNode currentAppInfoInSimpleType;
            XmlNode currentRestrictionInSimpleType;
            XmlNodeList childNotesInComplexType;
            XmlNode currentAnnotationInComplexType;
            XmlNode currentAppInfoChildInComplexType;
            XmlNode currentSequenceInComplexType;
            XmlNodeList simpleTypeChildNotes;
            Boolean flag = false;
            Boolean flag1 = false;
            foreach (Segment seg in testData) //цикл по всем сегментам в тестовых данных
            {
                foreach (XmlNode currentType in complexTypes) // проходим по всем тегам "complexType"  
                {
                    childNotesInComplexType = currentType.ChildNodes; // получаем дочерние узлы "complexType"
                    foreach (XmlNode childNoteInComplexType in childNotesInComplexType) //проходим по всем дочерним узлам "complexType"
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
                                                if (currentAppInfoChildInComplexType.Name.ToString().Equals("me:id") & currentAppInfoChildInComplexType.FirstChild.Value.ToString().Equals(seg.Name))//проверяем совпадает ли значение тега me:id с именем сегмента в тестовых данных
                                                {
                                                    flag1 = true; 

                                                }
                                               
                                            }
                                        }
                                    }
                        }
                        if (childNoteInComplexType.Name.ToString().Equals("sequence") & flag1) // если имя узла  = sequence и в теге annotation значение тэга "me:id" совпало  именем сегмента в тестовых данных
                                        {
                                           
                                            if (childNoteInComplexType.HasChildNodes)
                                            {
                                                IEnumerator ienum5 = childNoteInComplexType.GetEnumerator();
                                                while (ienum5.MoveNext())
                                                {
                                                 currentSequenceInComplexType = (XmlNode)ienum5.Current;
                                                 for (int j = 0; j < seg.elements.Count; j++) // цикл по всем элементам сегмента из тестовых данных
                                                 {
                                                    if (currentSequenceInComplexType.Name.ToString().Equals("element") & flag1)
                                                    {

                                                        if (currentSequenceInComplexType.Attributes["name"].Value.Substring(1).Equals(seg.elements[j].number.Substring(2)))//сравниваем значение аттрибута name тэга element с номером элемента в тестовых данных
                                                            {
                                                                if (field != null) // если у нас уже есть объект поля т.е. ссылка не null
                                                                    allFields.Add(field); //записываем в лист
                                                                field = new FieldDef(); // создаем новый объект поля
                                                                field.segmentTag = seg.Name; //присваиваем имя сегмента
                                                                field.refenerceNum = currentSequenceInComplexType.Attributes["type"].Value.Substring(5);//присваиваем значение номера элемента в стандарте
                                                                field.position = seg.elements[j].number.Substring(1);//присваиваем позицию элемента
                                                           //     Console.WriteLine(currentSequenceInComplexType.Attributes["name"].Value);
                                                          //      Console.WriteLine(currentSequenceInComplexType.Attributes["type"].Value);
                                                                foreach (XmlNode simpleType in simpleTypes) // цикл по всем тэгам simpleType
                                                                {
                                                                    simpleTypeChildNotes = simpleType.ChildNodes;
                                                                    foreach (XmlNode simpleTypeChildNote in simpleTypeChildNotes)
                                                                    {
                                                                        if (simpleTypeChildNote.Name.ToString().Equals("annotation")) // если имя узла  = annotation
                                                                        {
                                                                            if (simpleTypeChildNote.HasChildNodes)
                                                                            {
                                                                                IEnumerator ienum = simpleTypeChildNote.GetEnumerator();
                                                                                while (ienum.MoveNext())
                                                                                {
                                                                                    currentAnnotationChildInSimpleType = (XmlNode)ienum.Current;
                                                                                    if (currentAnnotationChildInSimpleType.Name.ToString().Equals("appinfo") && currentAnnotationChildInSimpleType.HasChildNodes) // ищем тэг appinfo
                                                                                    {
                                                                                        IEnumerator ienum2 = currentAnnotationChildInSimpleType.GetEnumerator();
                                                                                        while (ienum2.MoveNext())
                                                                                        {
                                                                                            currentAppInfoInSimpleType = (XmlNode)ienum2.Current;
                                                                                            if (currentAppInfoInSimpleType.Name.ToString().Equals("me:id") & currentAppInfoInSimpleType.FirstChild.Value.ToString().Equals(currentSequenceInComplexType.Attributes["type"].Value.Substring(5)))//проверяем что совпадают id
                                                                                            {
                                                                                                flag = true;

                                                                                            }
                                                                                            if (flag & currentAppInfoInSimpleType.Name.ToString().Equals("me:title")) //если id совпали и тэг называется me:title
                                                                                            {
                                                                                                field.name = currentAppInfoInSimpleType.FirstChild.Value.ToString();//присваиваем имя полю
                                                                                                field.javaName = currentAppInfoInSimpleType.FirstChild.Value.ToString().ToLower().Trim().Replace(" ", ""); // присваиваем джава имя полю как имя в нижнем регистре и без пробелов
                                                                                                break;
                                                                                            }
                                                                                        }
                                                                                    }
                                                                                }
                                                                            }

                                                                        }
                                                                        if (simpleTypeChildNote.Name.ToString().Equals("restriction") & flag) //если дочерний узел называется restriction
                                                                        {
                                                                            DataTypes dt = new DataTypes (simpleTypeChildNote.Attributes["base"].Value.Substring(3));//конвертируем тип данных
                                                                            field.datatype = dt.outputData;
                                                                            if (simpleTypeChildNote.Attributes["base"].Value.Substring(3).Equals("N2"))//устанавливаем precision для типа N2
                                                                            {
                                                                                field.precision = 2;
                                                                            }
                                                                            if (simpleTypeChildNote.HasChildNodes)
                                                                            {
                                                                                IEnumerator ienum = simpleTypeChildNote.GetEnumerator();
                                                                                while (ienum.MoveNext())
                                                                                {
                                                                                    currentRestrictionInSimpleType = (XmlNode)ienum.Current;
                                                                                    if (currentRestrictionInSimpleType.Name.ToString().Equals("minLength") & flag) //если  id совпали и тэг называется minLength
                                                                                    {
                                                                                        field.minLength = currentRestrictionInSimpleType.Attributes["value"].Value;

                                                                                    }
                                                                                    else if (currentRestrictionInSimpleType.Name.ToString().Equals("maxLength") & flag)//если  id совпали и тэг называется maxLength
                                                                                    {
                                                                                        flag = false;
                                                                                        field.maxLength = currentRestrictionInSimpleType.Attributes["value"].Value;
                                                                                        break;
                                                                                    }
                                                                                }
                                                                            }

                                                                        }

                                                                    }

                                                                }
                                                                break;
                                                            }
                                                               }

                                                    }
                                                   
                                                }
                                                flag1 = false;
                                            }

                                        }
                                    }
                        

                    
                }

                    }
            if (field != null) // если у нас уже есть объект поля т.е. ссылка не null
                allFields.Add(field); //записываем в лист
            fs.Close(); //закрваем потоки
            fs2.Close();
             return allFields;
                }

        }
    }
