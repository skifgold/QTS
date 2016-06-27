using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTSDataManager.DB;

namespace QTSDataManager.Repositories
{
<<<<<<< HEAD
    //TicketRepository //add s
    public class TicketsRepository : Repository<DB.Ticket>
    {
        public override int Create(Ticket item)
        {
            throw new NotImplementedException();
=======
    public class TicketRepository : Repository<DB.Ticket>
    {
        public override int Create(Ticket item)
        {
            _db.Tickets.Add(item);
            _db.SaveChanges();
            return item.Id;
>>>>>>> 7129246f2e6203ae5bd842e9eac335a8ef8fe6d9
        }

        public override void Delete(int id)
        {
<<<<<<< HEAD
            throw new NotImplementedException();
=======
            var item = _db.Tickets.Single(x => x.Id == id);
            _db.Tickets.Remove(item);
            _db.SaveChanges();
>>>>>>> 7129246f2e6203ae5bd842e9eac335a8ef8fe6d9
        }

        public override IEnumerable<Ticket> Read()
        {
            return _db.Tickets.OrderBy(x => x.Id);
        }

        public override Ticket Read(int id)
        {
<<<<<<< HEAD
            throw new NotImplementedException();
=======
            return _db.Tickets.FirstOrDefault(x => x.Id == id);
>>>>>>> 7129246f2e6203ae5bd842e9eac335a8ef8fe6d9
        }

        public override object TicketRepository()
        {
            throw new NotImplementedException();
        }

        public override void Update(Ticket item)
        {
<<<<<<< HEAD
            throw new NotImplementedException();
=======
            var toUpdate = _db.Tickets.Single(x => x.Id == item.Id);

            toUpdate.TicketType = item.TicketType;
            toUpdate.Price = item.Price;
            toUpdate.ConcertId = item.ConcertId;
            _db.SaveChanges();
>>>>>>> 7129246f2e6203ae5bd842e9eac335a8ef8fe6d9
        }

    }

}
