using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QTSDataManager.DB;

namespace QTSDataManager.Repositories
{
    public class GenreRepository : Repository<DB.Genre>
    {
        public override int Create(Genre item)
        {
            _db.Genres.Add(item);
            _db.SaveChanges();
            return item.Id;
        }

        public override void Delete(int id)
        {
            var item = _db.Genres.Single(x => x.Id == id);
            _db.Genres.Remove(item);
            _db.SaveChanges();
        }

        public override IEnumerable<Genre> Read()
        {
            return _db.Genres.OrderBy(x => x.Name);
        }

        public override Genre Read(int id)
        {
            return _db.Genres.FirstOrDefault(x => x.Id == id);
        }

        public override object TicketRepository()
        {
            throw new NotImplementedException();
        }

        public override void Update(Genre item)
        {
            var toUpdate = _db.Genres.Single(x => x.Id == item.Id);

            toUpdate.Name = item.Name;
            _db.SaveChanges(); 
        }

        public void AddGenreToMusicGroup(int musicGroupId, int genreId)
        {
            _db.MusicGroupGenres.Add(new MusicGroupGenre
            {
                GenreId = genreId,
                GroupId = musicGroupId
            });
            _db.SaveChanges();
        }
    }


}
