using KutuphaneSistemi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using System.ComponentModel.DataAnnotations;


//Veri Tabanında EF Tablo Oluşturması için ilgili model sınıflarınızı buraya eklemeliyiz.
namespace KutuphaneSistemi.Utulity
{
    public class UyglmDbContext : IdentityDbContext
    {

        public UyglmDbContext(DbContextOptions<UyglmDbContext> options) : base(options) { }


       
        public DbSet<KitapTuru> KitapTurleri { get; set; }

       
        public DbSet<Kitap> Kitaplar {  get; set; }

       
        public DbSet<Kiralama> Kiralamalar { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }




    }
}
