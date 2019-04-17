using JDS.PelotonWebAPI.Domain.Enums;

namespace JDS.PelotonWebAPI.Domain
{
    public class WConnectionDefinition
    {
        public string Database { get; set; }
        public string Password { get; set; }
        public DBMS Provider { get; set; }
        public string Server { get; set; }
        public bool Trusted { get; set; }
        public string User { get; set; }
    }
}
