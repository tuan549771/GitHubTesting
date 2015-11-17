
namespace DMController.ViewModels
{
    class ScriptViewModel
    {
        public int Ordinal { get; set; }
        public string ScriptName { get; set; }
        public int IDScript { get; set; }
        public int ConfigurationID { get; set; }
        public ConfigurationViewModel ConfigurationVM { get; set; }

        public ScriptViewModel()
        {
            ConfigurationVM = new ConfigurationViewModel();
        }

        public ScriptViewModel(string iScriptName, int iID, int iOrdinal, int iConfigurationID)
        {
            ScriptName = iScriptName;
            IDScript = iID;
            Ordinal = iOrdinal;
            ConfigurationID = iConfigurationID;
            ConfigurationVM = new ConfigurationViewModel();
        }
    }
}
