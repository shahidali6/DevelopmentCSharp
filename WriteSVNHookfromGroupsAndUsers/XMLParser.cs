using System;
using System.IO;
using System.Xml.Serialization;

namespace SVNHookGenerator
{
    internal class XMLParser
    {
        /// <summary>
        /// Save Application settings in XML file
        /// </summary>
        /// <param name="appSettings"></param>
        /// <param name="fileNameAndPath"></param>
        public void SaveSettingsXMLFile(ApplicationSettings appSettings, string fileNameAndPath)
        {
            FileStream flStream = new FileStream(fileNameAndPath, FileMode.Create, FileAccess.Write);
            try
            {
                XmlSerializer xmlserializer = new XmlSerializer(typeof(ApplicationSettings));

                xmlserializer.Serialize(flStream, appSettings);

                flStream.Close();
                Console.WriteLine("Settings file Saved successfuly!.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in Save XML file: ", ex.Message);
            }
            finally
            {
                flStream.Close();
            }
        }
        /// <summary>
        /// Load Application settings from XML file
        /// </summary>
        /// <param name="fileNameAndPath"></param>
        /// <returns></returns>
        public ApplicationSettings LoadSettingsXMLFile(string fileNameAndPath)
        {
            ApplicationSettings appSettings = new ApplicationSettings();

            try
            {
                if (File.Exists(fileNameAndPath))
                {
                    FileStream flStream = new FileStream(fileNameAndPath, FileMode.Open, FileAccess.Read);
                    XmlSerializer xmlserializer = new XmlSerializer(typeof(ApplicationSettings));

                    appSettings = (ApplicationSettings)xmlserializer.Deserialize(flStream);

                    flStream.Close();
                    Console.WriteLine("Application Settings load successfuly!.");
                }
                else
                {
                    SaveSettingsXMLFile(appSettings, fileNameAndPath);
                    appSettings = new ApplicationSettings();
                    Console.WriteLine("Application Settings Created and Saved successfuly!.");
                }
                return appSettings;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception in load XML file and Application Settings reCreated successfuly!.", ex.Message);
                appSettings = new ApplicationSettings();
                return appSettings;
            }   
        }
    }
}