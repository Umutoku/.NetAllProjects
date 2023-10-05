namespace CovidChart.API.Models
{
    public enum ECity
    {
        Istanbul=1,
        Manisa=2,
        Yozgat=3,
        Izmir=4,
        Ankara=5
    }
    public class Covid
    {
        public int Id { get; set; }
        public ECity City { get; set; }
        public int Count { get; set; }
        public DateTime CovidDate { get; set; }
    }
}
