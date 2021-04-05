using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp6ReadWriteXML
{
    class Program
    {
        static void Main(string[] args)
        {
            string xmlReadFilePath = @"C:\Users\msaddique\Documents\sms_20210325112528.xml";
            string xmlWriteFilePath = @"C:\Users\msaddique\Documents\smsWrite_20210325112528.xml";
            string csvWriteFilePath = @"C:\Users\msaddique\Documents\sms_20210325112528.csv";
            string specialWriteFilePath = @"C:\Users\msaddique\Documents\sms_20210325112528.txt";
            string delimiter = "|";

            List<string> listAddress = new List<string>();
            List<string> listTime = new List<string>();
            List<string> listDate = new List<string>();
            List<string> listType = new List<string>();
            List<string> listBody = new List<string>();
            List<string> listRead = new List<string>();
            List<string> listServiceCenter = new List<string>();
            List<string> listName = new List<string>();
            List<string> listSpecial = new List<string>();

            string writeAllSMS = "allsms";
            string writeAllSMSCountAttribute = "count";
            string writeSMS = "sms";
            string writeSMSAddressAttribute = "address";
            string writeSMSTimeAttribute = "time";
            string writeSMSDateAttribute = "date";
            string writeSMSTypeAttribute = "type";
            string writeSMSBodyAttribute = "body";
            string writeSMSReadAttribute = "read";
            string writeSMSService_CenterAttribute = "service_center";
            string writeSMSNameAttribute = "name";

            using (XmlReader xmlReader = XmlReader.Create(xmlReadFilePath))
            {
                while (xmlReader.Read())
                {
                    if (xmlReader.NodeType == XmlNodeType.Element)
                    {
                        if (xmlReader.AttributeCount > 5)
                        {

                            if (string.IsNullOrEmpty(xmlReader.GetAttribute(AttributeSequence.address.ToString())) ||
                                string.IsNullOrEmpty(xmlReader.GetAttribute(AttributeSequence.time.ToString())) ||
                                string.IsNullOrEmpty(xmlReader.GetAttribute(AttributeSequence.date.ToString())) ||
                                string.IsNullOrEmpty(xmlReader.GetAttribute(AttributeSequence.body.ToString())))
                            {
                                continue;
                            }

                            for (int i = 0; i < xmlReader.AttributeCount; i++)
                            {


                                switch (i)
                                {
                                    case 0:
                                        listAddress.Add(xmlReader.GetAttribute(i).Trim());
                                        if (xmlReader.GetAttribute(i) == "321")
                                        {
                                            listSpecial.Add(xmlReader.GetAttribute(i).Trim());
                                        }
                                        //Console.WriteLine("Address: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 1:
                                        listTime.Add(xmlReader.GetAttribute(i).Trim());
                                        //Console.WriteLine("Time: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 2:
                                        listDate.Add(xmlReader.GetAttribute(i).Trim());
                                        //Console.WriteLine("Date: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 3:
                                        listType.Add(xmlReader.GetAttribute(i).Trim());
                                        //Console.WriteLine("Type: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 4:
                                        listBody.Add(xmlReader.GetAttribute(i).Trim());
                                        //Console.WriteLine("Body: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 5:
                                        listRead.Add(xmlReader.GetAttribute(i).Trim());
                                        //Console.WriteLine("Read: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 6:
                                        listServiceCenter.Add(xmlReader.GetAttribute(i).Trim());
                                        //Console.WriteLine("Service Center: " + xmlReader.GetAttribute(i));
                                        break;
                                    case 7:
                                        listName.Add(xmlReader.GetAttribute(i).Trim());
                                        //Console.WriteLine("Name: " + xmlReader.GetAttribute(i));
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                    }
                }


                while (xmlReader.Read())
                {
                    //Console.WriteLine("Name: " + xmlReader.Name + " Value: " + xmlReader.Value);

                    if (xmlReader.IsStartElement())
                    {
                        //Console.WriteLine(xmlReader.Name);
                    }
                }
            }

            XmlTextWriter xmlWriter = new XmlTextWriter(xmlWriteFilePath, Encoding.UTF8);
            xmlWriter.Formatting = Formatting.Indented;

            //Strat XML Document
            xmlWriter.WriteStartDocument();

            // Write first element  
            xmlWriter.WriteStartElement(writeAllSMS);
            xmlWriter.WriteAttributeString(writeAllSMSCountAttribute, listAddress.Count.ToString());

            for (int i = 0; i < listAddress.Count; i++)
            {
                xmlWriter.WriteStartElement(writeSMS);

                xmlWriter.WriteAttributeString(writeSMSAddressAttribute, listAddress[i]);
                xmlWriter.WriteAttributeString(writeSMSTimeAttribute, listTime[i]);
                xmlWriter.WriteAttributeString(writeSMSDateAttribute, listDate[i]);
                xmlWriter.WriteAttributeString(writeSMSTypeAttribute, listType[i]);
                xmlWriter.WriteAttributeString(writeSMSBodyAttribute, listBody[i]);
                xmlWriter.WriteAttributeString(writeSMSReadAttribute, listRead[i]);
                xmlWriter.WriteAttributeString(writeSMSService_CenterAttribute, listServiceCenter[i]);
                xmlWriter.WriteAttributeString(writeSMSNameAttribute, listName[i]);
                xmlWriter.WriteEndElement();
            }

            xmlWriter.WriteEndElement();
            xmlWriter.WriteEndDocument();
            xmlWriter.Close();

            var listDistinct = listBody.Distinct().ToList();

            using (StreamWriter sr = new StreamWriter(csvWriteFilePath))
            {
                foreach (var item in listDistinct)
                {
                    //sr.WriteLine(item + delimiter + listAddress.Where(s => s.Contains(item)).Count());
                    sr.WriteLine(item + delimiter + CountValueinList(listBody, item));
                }
            }

            string textFile = @"C:\Users\msaddique\Documents\sms_20210325112528.xml";
            string textFileForList = @"C:\Users\msaddique\Documents\ListLines.txt";
            // Read a text file line by line.  
            string allString = File.ReadAllText(textFile);
            List<string> listToRemove = new List<string>();

            listToRemove = File.ReadAllLines(textFileForList).ToList();

            foreach (var item in listToRemove)
            {
                allString = allString.Replace(item, string.Empty);
            }
            File.WriteAllText(@"C:\Users\msaddique\Documents\sms1_20210325112528.xml", allString);


            using (StreamWriter sw = new StreamWriter(specialWriteFilePath))
            {
                foreach (var item in listSpecial)
                {
                    sw.WriteLine(item);
                }
            }

            //Console.ReadLine();
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
    public enum AttributeSequence
    {
        address,
        time,
        date,
        type,
        body,
        read,
        service_center,
        name
    }
}
