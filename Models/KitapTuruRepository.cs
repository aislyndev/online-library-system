using KutuphaneSistemi.Utulity;

namespace KutuphaneSistemi.Models
{
    public class KitapTuruRepository : Repository<KitapTuru>, IKitapTuruRepository
    {

        private  UyglmDbContext _uyglmDbContext;
        public KitapTuruRepository(UyglmDbContext uyglmDbContext) : base(uyglmDbContext)
        {

            _uyglmDbContext = uyglmDbContext;
        }

        public void Guncelle(KitapTuru kitapTuru)
        {
            _uyglmDbContext.Update(kitapTuru);
        }

        public void Kaydet()
        {
            _uyglmDbContext.SaveChanges();
        }
    }
}
