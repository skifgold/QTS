namespace QTSWebUI.Models
{
    public class TicketApiModel
    {
        public int Id { get; set; }
        public string TicketType { get; set; }
        public int Price { get; set; }
        public string Fan { get; set; }
        public int ConcertId { get; set; }
    }
}