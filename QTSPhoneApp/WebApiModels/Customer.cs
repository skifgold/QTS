namespace QTSPhoneApp.WebApiModels
{
    public class Customer
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int Status { get; set; }
        public int Count { get; set; }
        public string Fan { get; set; }
        public int ConcertId { get; set; }
        public string TicketType { get; set; }
    }
}