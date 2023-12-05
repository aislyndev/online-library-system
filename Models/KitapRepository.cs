using KutuphaneSistemi.Utulity;

namespace KutuphaneSistemi.Models
{
    public class KitapRepository : Repository<Kitap>, IKitapRepository
    {

        private  UyglmDbContext _uyglmDbContext;
        public KitapRepository(UyglmDbContext uyglmDbContext) : base(uyglmDbContext)
        {

            _uyglmDbContext = uyglmDbContext;
        }

        public void Guncelle(Kitap kitap)
        {
            _uyglmDbContext.Update(kitap);
        }

        public void Kaydet()
        {
            _uyglmDbContext.SaveChanges();
        }
    }
}
