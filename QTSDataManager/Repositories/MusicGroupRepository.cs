using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTSDataManager.DB;

namespace QTSDataManager.Repositories
{
    public class MusicGroupRepository : Repository<DB.MusicGroup>
    {
        public override int Create(MusicGroup item)
        {
            _db.MusicGroups.Add(item);
            _db.SaveChanges();
            return item.Id;
        }

        public override void Delete(int id)
        {
            var item = _db.MusicGroups.Single(x => x.Id == id);
            _db.MusicGroups.Remove(item);
            _db.SaveChanges();
        }

        public override IEnumerable<MusicGroup> Read()
        {
            return _db.MusicGroups.OrderBy(x => x.Name);
        }

        public override MusicGroup Read(int id)
        {
            return _db.MusicGroups.FirstOrDefault(x => x.Id == id);
        }

        public override object TicketRepository()
        {
            throw new NotImplementedException();
        }

        public override void Update(MusicGroup item)
        {
            var toUpdate = _db.MusicGroups.Single(x => x.Id == item.Id);

            toUpdate.Name = item.Name;
            toUpdate.Annotation = item.Annotation;
            toUpdate.PosterURL = item.PosterURL;
            _db.SaveChanges();
        }

        public void AddGroupToConcert(int concertId, int groupId)
        {
            Debugger.Break();
            _db.ConcertGroups.Add(new ConcertGroup
            {
                ConcertId = concertId,
                GroupId = groupId
            });
            _db.SaveChanges();
        }
    }
}
