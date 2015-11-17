using DMController.Models.ConfigurationInfo;
using DMController.Models.QueueTesting;
using ErrorHandling;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Xml.Linq;

namespace DMController.Models
{
    class XMLParsers
    {
        private const string XML_PATH_CONFIGURATION = @"Configuration.xml";
        private const string XML_PATH_GEN_CONFIGURATION = @"Gen_Configuration.xml";
        private const string CONFIGURATIONS_INFORMATION = "ConfigurationsInformation";
        private const string CONFIGURATION = "Configuration";
        private const string NAME_SCRIPT_CONFIGURATION = "NameScriptConfiguaration";
        private const string OPTION_TESTING = "OptionTesting";
        private const string OPTION_TITLE = "OptionTitle";
        private const string OPTION_NAME = "OptionName";
        private const string MODE_VALUE = "ModeValue";
        private const string MODE = "Mode";
        private const string ID = "ID";
        private const string OPTION = "Option";
        private const string OPTIONS = "Options";
        private const string TITLE = "Title";
        private const string LABLE = "Lable";
        private const string TEXT = "Text";
        private const string EmptyString = "";
        private const string SELECTED_MODE_DEFAULT_VALUE = "SelectedModeDefaultValue";


        public Configuration ParseConfiguration(string iScriptName)
        {
            Configuration configuration = new Configuration();
            try
            {
                XDocument doc = XDocument.Load(XML_PATH_CONFIGURATION);
                IEnumerable<XElement> listConfiguration = doc.Descendants(CONFIGURATION);
                foreach (XElement config in listConfiguration)
                {
                    if (iScriptName == config.Attribute(NAME_SCRIPT_CONFIGURATION).Value)
                    {
                        IEnumerable<XElement> listOption = config.Descendants(OPTION_TESTING);
                        foreach (XElement option in listOption)
                        {
                            configuration.ConfigurationName = string.Format("Configuration {0}", iScriptName);
                            configuration.ListOptionTesting.Add(new OptionTesting()
                            {
                                OptionTitle = option.Attribute(OPTION_TITLE).Value,
                                OptionName = option.Attribute(OPTION_NAME).Value,
                                SelectedModeDefaultValue = Convert.ToInt16(option.Attribute(SELECTED_MODE_DEFAULT_VALUE).Value),
                                Modes = (from modevalue in option.Descendants(MODE_VALUE)
                                         where modevalue.Value != EmptyString
                                         select new ModeTesting(modevalue.Value, modevalue.Attribute(MODE).Value)).ToList<ModeTesting>(),
                            });
                        }
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                string logger = LogTextInformation.CreateErrorMessage(ex);
                LogTextInformation.LogFileWrite(string.Format("[ERROR]: XMLParsers Parse Configuration : {0} ", logger));
            }
            return configuration;
        }

        // Write down XML
        public static void GenerateConfiguaration(List<ItemScript> ListScript)
        {
            try
            {
                XDocument doc = new XDocument(new XElement(CONFIGURATIONS_INFORMATION,
                    from script in ListScript
                    select new XElement(CONFIGURATION,
                        new XAttribute(ID, script.IDConfig),
                        new XElement(OPTIONS,
                            from opt in script.Configuration.ListOptionTesting
                            select new XElement(OPTION,
                                new XAttribute(TITLE, opt.OptionTitle),
                                new XAttribute(LABLE, opt.OptionName),
                                new XAttribute(TEXT, opt.Modes[opt.SelectedModeDefaultValue].ModeValueAttribute)
                                )))));
                doc.Save(XML_PATH_GEN_CONFIGURATION);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                string logger = LogTextInformation.CreateErrorMessage(ex);
                LogTextInformation.LogFileWrite(string.Format("[ERROR]: XMLParsers Generate Configuaration : {0} ", logger));
            }
        }
    }
}
