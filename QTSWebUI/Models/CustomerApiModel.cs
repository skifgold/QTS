using QTSDataManager.DB;

namespace QTSWebUI.Models
{
    public class CustomerApiModel
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public CustomerStatus Status { get; set; }
    }
}