
namespace WebApis.Models
{
    public class RankBonus
    {
        public int RankID { get; set; }
        public int RankNo { get; set; }
        public string? RankTitle { get; set; }
        public decimal TeamTurnover { get; set; }
        public decimal Reward { get; set; }
    }
}
