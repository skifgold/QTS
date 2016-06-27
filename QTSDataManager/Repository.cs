using System.Collections.Generic;

namespace QTSDataManager
{
    public abstract class Repository<T>
    {
        protected DB.qtsEntities _db = new DB.qtsEntities();

        public abstract int Create(T item);
        public abstract IEnumerable<T> Read();
        public abstract T Read(int id);
        public abstract void Update(T item);
        public abstract object TicketRepository();
        public abstract void Delete(int id);
    }
}