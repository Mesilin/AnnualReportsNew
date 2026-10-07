using Incom.Common2.Persistence;

namespace Aviales.Model.Summary
{
    public class MeteoKpoSummary : ModelBase
    {
        public string? StationName { get; set; }
        public Guid MeteoInfoId { get; set; }
        public DateTime MeteoInfoDate { get; set; }
        public decimal? Temperature { get; set; }
        public decimal? DewPoint { get; set; }
        public byte? WindAvg { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public decimal? KpoNesterov { get; set; }
    }
}