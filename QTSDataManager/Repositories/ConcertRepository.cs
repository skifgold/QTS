using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTSDataManager.DB;

namespace QTSDataManager.Repositories
{
    public class ConcertRepository : Repository <DB.Concert>
    {
        public override int Create(Concert item)
        {
            _db.Concerts.Add(item);
            _db.SaveChanges();
            return item.Id;
        }

        public override void Delete(int id)
        {
            var item = _db.Concerts.Single(x => x.Id == id);
            _db.Concerts.Remove(item);
            _db.SaveChanges();
        }

        public override IEnumerable<Concert> Read()
        {
            return _db.Concerts.OrderBy(x=> x.Name);
        }

        public override Concert Read(int id)
        {
            return _db.Concerts.FirstOrDefault(x => x.Id == id);
        }

        public override object TicketRepository()
        {
            throw new NotImplementedException();
        }

        public override void Update(Concert item)
        {
            var toUpdate = _db.Concerts.Single(x => x.Id == item.Id);

            toUpdate.Name = item.Name;
            toUpdate.Address = item.Address;
            toUpdate.TypeOfPlace = item.TypeOfPlace;
            toUpdate.Date = item.Date;
            toUpdate.PosterURL = item.PosterURL;
            
            
            _db.SaveChanges();
        }

        public IEnumerable<Concert> FilterBy(DateTime? date = null)
        {
            return _db.Concerts.Where(x => (date== null || x.Date == date));
        }
    }
}
