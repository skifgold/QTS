using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTSDataManager.DB;

namespace QTSDataManager.Repositories
{
   public class CustomerRepository : Repository <DB.Customer>
    {
        public override int Create(Customer item)
        {
            _db.Customers.Add(item);
            _db.SaveChanges();
            return item.Id;
        }

        public override void Delete(int id)
        {
            var item = _db.Customers.Single(x => x.Id == id);
            _db.Customers.Remove(item);
            _db.SaveChanges();
        }

        public override IEnumerable<Customer> Read()
        {
            return _db.Customers.OrderBy(x => x.Name);
        }

        public override Customer Read(int id)
        {
            return _db.Customers.FirstOrDefault(x => x.Id == id);
        }

        public override object TicketRepository()
        {
            throw new NotImplementedException();
        }

        public override void Update(Customer item)
        {
            var toUpdate = _db.Customers.Single(x => x.Id == item.Id);

            toUpdate.Name = item.Name;
            toUpdate.Surname = item.Surname;
            toUpdate.Email = item.Email;
            toUpdate.Phone = item.Phone;
            toUpdate.Status = item.Status;
            _db.SaveChanges();
        }

       public IEnumerable<Customer> ReadByTicketType(int concertId,string ticketType, string fan, int ticketId)
       {
           return  _db.Customers.Where(x => x.Ticket.ConcertId == concertId &&  x.Ticket.TicketType == ticketType && x.Ticket.Fan == fan && x.TicketId == ticketId);
            
       } 
    }

}
