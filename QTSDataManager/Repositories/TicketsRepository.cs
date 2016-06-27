using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTSDataManager.DB;

namespace QTSDataManager.Repositories
{
    public class TicketsRepository : Repository<DB.Ticket> //TicketRep => TicketsRep
    {
        public override int Create(Ticket item)
        {
            _db.Tickets.Add(item);
            _db.SaveChanges();
            return item.Id;
        }

        public override void Delete(int id)
        {
            var item = _db.Tickets.Single(x => x.Id == id);
            _db.Tickets.Remove(item);
            _db.SaveChanges();
        }

        public override IEnumerable<Ticket> Read()
        {
            return _db.Tickets.OrderBy(x => x.Id);
        }

        public override Ticket Read(int id)
        {
            return _db.Tickets.FirstOrDefault(x => x.Id == id);
        }

        public override object TicketRepository()
        {
            throw new NotImplementedException();
        }

        public override void Update(Ticket item)
        {
            var toUpdate = _db.Tickets.Single(x => x.Id == item.Id);

            toUpdate.TicketType = item.TicketType;
            toUpdate.Price = item.Price;
            toUpdate.ConcertId = item.ConcertId;
            toUpdate.Fan = item.Fan;
            _db.SaveChanges();
        }

        public IEnumerable<Ticket> ReadByConcertId(int id)
        {
            return _db.Tickets.Where(x => x.ConcertId == id);
        }

        public void Create(Ticket item, int count)
        {
            Enumerable.Range(0, count).ToList().ForEach(x =>
            {
                _db.Tickets.Add(item);
                _db.SaveChanges();
            });
        }
    }
}
