using System;
using System.Collections;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SVNHookGenerator
{
	internal class XMLParser
	{
		public void Save_XML(Dashboard app_set)
		{
			FileStream flStream = new FileStream(dataFileName, FileMode.Create, FileAccess.Write);
			try
			{
				XmlSerializer xmlserializer = new XmlSerializer(typeof(Dashboard));

				xmlserializer.Serialize(flStream, app_set);

				flStream.Close();
			}
			catch (Exception ex)
			{
				_exhaust.exhaust_pipe_big("Save XML", ex);
			}
			finally
			{
				flStream.Close();
			}
		}
		private void Load_Settings(bool isDefault)
		{
			string port = string.Empty;
			int buad = 0;

			// Add Devices.
			

			if (!isDefault)
			{
				if (File.Exists(dataFileName))
				{
					FileStream flStream = new FileStream(dataFileName, FileMode.Open, FileAccess.Read);
					XmlSerializer xmlserializer = new XmlSerializer(typeof(Dashboard));

					app_set = (Dashboard)xmlserializer.Deserialize(flStream);

					flStream.Close();
				}
			}
			else
			{
				app_set = new Dashboard();
			}


			string instrument_type = _m._Menu_ComboBox_Instrument.Text;
			instrument_type = instrument_type.Replace(' ', '_');
			if (instrument_type == Device.Wired_Gas_I.ToString()
				)
			{

			}
			else
			{
				if (app_set.Portable_Intrument_Com_Port != null)
				{
					port = app_set.Portable_Intrument_Com_Port;
				}

				if (app_set.Portable_Instrument_Baud_Rate != 0)
				{
					buad = app_set.Portable_Instrument_Baud_Rate;
				}
			}

			for (int i = 0; i < _ConnectToComboBox.Items.Count; i++)
			{
				if (port == Get_Com_Port_Number(_ConnectToComboBox.Items[i].ToString()))
				{
					_ConnectToComboBox.SelectedIndex = i;
					break;
				}
			}
		}
	}
}