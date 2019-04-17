using JDS.PelotonWebAPI.Domain.Enums;

namespace JDS.PelotonWebAPI.Domain
{
    public class PelotonOptions
    {
        public string RootFolder { get; set; }
        public string ProfileName { get; set; }
        public string ProfilePassword { get; set; }
        // ReSharper disable once InconsistentNaming
        public DBMS ConnectionDBMS { get; set; }
        public string ConnectionServer { get; set; }
        public string ConnectionDatabase { get; set; }
        public string DatabaseUsername { get; set; }
        public string DatabasePassword { get; set; }
        public bool Trusted { get; set; }
        public string UnitSetName { get; set; }
        public string DataModel { get; set; }
    }
}
