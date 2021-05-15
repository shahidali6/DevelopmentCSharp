using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ReadWriteXMLSMS
{
    class Program
    {
        //string allSMS = "allsms";
        //string allSMSCountAttribute = "count";
        //string sMS = "sms";
        //string sMSAddressAttribute = "address";
        //string sMSTimeAttribute = "time";
        //string sMSDateAttribute = "date";
        //string sMSTypeAttribute = "type";
        //string sMSBodyAttribute = "body";
        //string sMSReadAttribute = "read";
        //string sMSService_CenterAttribute = "service_center";
        //string sMSNameAttribute = "name";
        static void Main(string[] args)
        {
            string fileName = "sms_20210515084044_8558";
            string xmlReadFilePath = @"C:\Users\Shahid\Downloads\"+fileName+".xml";
            string xmlWriteFilePath = @"C:\Users\Shahid\Downloads\"+fileName+"_Write.xml";
            string csvWriteFilePath = @"C:\Users\Shahid\Downloads\" + fileName + ".csv";
            string specialWriteFilePath = @"C:\Users\Shahid\Downloads\" + fileName + ".txt";
            string delimiter = "|";

            List<XMLAttributeStruct> listMain = new List<XMLAttributeStruct>();

            listMain = ReadXMLFileOfSMS(xmlReadFilePath, false);

            bool status = WriteXMLFileOfSMS(xmlWriteFilePath, listMain);

            //var listDistinct = listBody.Distinct().ToList();

            //using (StreamWriter sr = new StreamWriter(csvWriteFilePath))
            //{
            //    foreach (var item in listDistinct)
            //    {
            //        //sr.WriteLine(item + delimiter + listAddress.Where(s => s.Contains(item)).Count());
            //        sr.WriteLine(item + delimiter + CountValueinList(listBody, item));
            //    }
            //}

            //string textFile = @"C:\Users\msaddique\Documents\sms_20210325112528.xml";
            //string textFileForList = @"C:\Users\msaddique\Documents\ListLines.txt";
            //// Read a text file line by line.  
            //string allString = File.ReadAllText(textFile);
            //List<string> listToRemove = new List<string>();

            //listToRemove = File.ReadAllLines(textFileForList).ToList();

            //foreach (var item in listToRemove)
            //{
            //    allString = allString.Replace(item, string.Empty);
            //}
            //File.WriteAllText(@"C:\Users\msaddique\Documents\sms1_20210325112528.xml", allString);
        }

        private static bool WriteXMLFileOfSMS(string xmlWriteFilePath, List<XMLAttributeStruct> listMain)
        {
            XmlTextWriter xmlWriter = new XmlTextWriter(xmlWriteFilePath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;

            //Strat XML Document
            xmlWriter.WriteStartDocument();

            // Write first element  
            xmlWriter.WriteStartElement(XMLAttribute.allsms.ToString());
            xmlWriter.WriteAttributeString(XMLAttribute.count.ToString(), listMain.Count.ToString());

            for (int i = 0; i < listMain.Count; i++)
            {
                xmlWriter.WriteStartElement(XMLAttribute.sms.ToString());
                xmlWriter.WriteAttributeString(XMLAttribute.address.ToString(), listMain[i].address);
                xmlWriter.WriteAttributeString(XMLAttribute.time.ToString(), listMain[i].time);
                xmlWriter.WriteAttributeString(XMLAttribute.date.ToString(), listMain[i].date);
                xmlWriter.WriteAttributeString(XMLAttribute.type.ToString(), listMain[i].type);
                xmlWriter.WriteAttributeString(XMLAttribute.body.ToString(), listMain[i].body);
                xmlWriter.WriteAttributeString(XMLAttribute.read.ToString(), listMain[i].read);
                xmlWriter.WriteAttributeString(XMLAttribute.service_center.ToString(), listMain[i].service_center);
                xmlWriter.WriteAttributeString(XMLAttribute.name.ToString(), listMain[i].name);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            //Write End document
            xmlWriter.WriteEndDocument();
            //Close Writer instance
            xmlWriter.Close();
            return true;
        }

        private static List<XMLAttributeStruct> ReadXMLFileOfSMS(string xmlReadFilePath, bool debuglog)
        {
            List<XMLAttributeStruct> listMain = new List<XMLAttributeStruct>();
            using (XmlReader xmlReader = XmlReader.Create(xmlReadFilePath))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.AttributeCount == 8)
                        {
                            if (string.IsNullOrEmpty(xmlReader.GetAttribute(XMLAttribute.address.ToString())) ||
                                string.IsNullOrEmpty(xmlReader.GetAttribute(XMLAttribute.time.ToString())) ||
                                string.IsNullOrEmpty(xmlReader.GetAttribute(XMLAttribute.date.ToString())) ||
                                string.IsNullOrEmpty(xmlReader.GetAttribute(XMLAttribute.body.ToString())))
                            {
                                continue;
                            }
                            XMLAttributeStruct xmlStruct = new XMLAttributeStruct();
                            for (int i = 0; i < xmlReader.AttributeCount; i++)
                            {
                                switch (i)
                                {
                                    case 0:
                                        xmlStruct.address = xmlReader.GetAttribute(i).Trim();
                                        if (debuglog)
                                            Console.WriteLine("Address: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 1:
                                        xmlStruct.time = xmlReader.GetAttribute(i).Trim();
                                        if (debuglog)
                                            Console.WriteLine("Time: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 2:
                                        xmlStruct.date = xmlReader.GetAttribute(i).Trim();
                                        if (debuglog)
                                            Console.WriteLine("Date: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 3:
                                        xmlStruct.type = xmlReader.GetAttribute(i).Trim();
                                        if (debuglog)
                                            Console.WriteLine("Type: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 4:
                                        xmlStruct.body = xmlReader.GetAttribute(i).Trim();
                                        if (debuglog)
                                            Console.WriteLine("Body: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 5:
                                        xmlStruct.read = xmlReader.GetAttribute(i).Trim();
                                        if (debuglog)
                                            Console.WriteLine("Read: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 6:
                                        xmlStruct.service_center = xmlReader.GetAttribute(i).Trim();
                                        if (debuglog)
                                            Console.WriteLine("Service Center: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 7:
                                        xmlStruct.name = xmlReader.GetAttribute(i).Trim();
                                        if (debuglog)
                                            Console.WriteLine("Name: " + xmlReader.GetAttribute(i));
                                        break;
                                    default:
                                        break;
                                }
                            }
                            listMain.Add(xmlStruct);
                        }
                    }
                }
            }
            return listMain;
        }

        public static int CountValueinList(List<string> listToCheck, string valueToCheck)
        {
            int count = 0;
            foreach (var item in listToCheck)
            {
                if (item == valueToCheck)
                {
                    count++;
                }
            }
            return count;
        }
    }
    public enum XMLAttribute
    {
        address,
        time,
        date,
        type,
        body,
        read,
        service_center,
        name,
        allsms,
        count,
        sms
    }
    public struct XMLAttributeStruct
    {
        public string address;
        public string time;
        public string date;
        public string type;
        public string body;
        public string read;
        public string service_center;
        public string name;
    }
}