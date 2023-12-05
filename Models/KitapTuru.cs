using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KutuphaneSistemi.Models
{
    public class KitapTuru
    {

        [Key] //primary key
        public int Id { get; set; }

        [Required(ErrorMessage = "Kitap Türü Adı boş bırakılamaz!")] //not null
        [MaxLength]
        [DisplayName("Kitap Türü Gir")]//not null
        public string Ad { get; set; }

    }
}
