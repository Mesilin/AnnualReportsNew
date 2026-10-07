using Incom.Common2.Persistence;

namespace Aviales.Model.Summary
{
    public class FightFireResourceSummary : ModelBase
    {
        public string? FightFireTeamLocalName { get; set; }
        public string? FightFireResourceLocalName { get; set; }
        public short? Amount { get; set; }
    }
}
