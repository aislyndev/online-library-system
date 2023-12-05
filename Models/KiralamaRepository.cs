using KutuphaneSistemi.Utulity;

namespace KutuphaneSistemi.Models
{
    public class KiralamaRepository : Repository<Kiralama>, IKiralamaRepository
    {

        private  UyglmDbContext _uyglmDbContext;
        public KiralamaRepository(UyglmDbContext uyglmDbContext) : base(uyglmDbContext)
        {

            _uyglmDbContext = uyglmDbContext;
        }

        public void Guncelle(Kiralama kiralama)
        {
            _uyglmDbContext.Update(kiralama);
        }

        public void Kaydet()
        {
            _uyglmDbContext.SaveChanges();
        }
    }
}
