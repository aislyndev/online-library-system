using KutuphaneSistemi.Migrations;
using KutuphaneSistemi.Models;
using KutuphaneSistemi.Utulity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http.Headers;

namespace KutuphaneSistemi.Controllers
{
    [Authorize(Roles = UserRoles.Role_Admin)]
    public class KiralamaController : Controller
    {
        private readonly IKiralamaRepository _kiralamaRepository;
        private readonly IKitapRepository _kitapRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;


        public KiralamaController(IKiralamaRepository kiralamaRepository, IKitapRepository kitapRepository,IWebHostEnvironment webHostEnvironment)
        {
            _kiralamaRepository = kiralamaRepository;
            _kitapRepository = kitapRepository;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {

            
            List<Kiralama> objKiralamaList =_kiralamaRepository.GetAll(includeProps:"Kitap").ToList();
            
            return View(objKiralamaList);
        }


        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.KitapAdi,
                    Value = k.Id.ToString()
                });
            ViewBag.KitapList = KitapList;
            //ekle
            if(id==null || id == 0)
            {   

                return View();
            }
            else
            {   //güncelle
                Kiralama? kiralamaVt = _kiralamaRepository.Get(u => u.Id == id);

                if (kiralamaVt == null)
                {
                    return NotFound();
                }


                return View(kiralamaVt);

            }
        }

        [HttpPost]
        public IActionResult EkleGuncelle(Kiralama kiralama)
        {
            
            if (ModelState.IsValid)
            {
                
                
                if (kiralama.Id == 0)
                {
                    TempData["basarili"] = "Yeni Kiralama İşlemi  Başarıyla Oluşturuldu";
                    _kiralamaRepository.Ekle(kiralama);

                }
                else
                {
                    TempData["basarili"] = "Kiralama Kayıt güncelleme başarıyla oluşturuldu";
                    _kiralamaRepository.Guncelle(kiralama);
                }


                //_kitapRepository.Ekle(kitap);
                _kiralamaRepository.Kaydet();
                
                return RedirectToAction("Index", "Kiralama"); //SaveChanges ile bilgiler veritabanına ekleniyor.
            }
            return View();
        }
       
        // GET ACTION
        public IActionResult Sil(int? id)
        {
            IEnumerable<SelectListItem> KitapList = _kitapRepository.GetAll()
               .Select(k => new SelectListItem
               {
                   Text = k.KitapAdi,
                   Value = k.Id.ToString()
               });
            ViewBag.KitapList = KitapList;



            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kiralama? kiralamaVt = _kiralamaRepository.Get(u=>u.Id==id);

            if (kiralamaVt == null)
            {
                return NotFound();
            }


            return View(kiralamaVt);
        }
        
        // POST ACTION
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kiralama? kiralama = _kiralamaRepository.Get(u => u.Id == id);
            if (kiralama == null)
            {
                return NotFound();
            }


            _kiralamaRepository.Sil(kiralama);
            _kiralamaRepository.Kaydet();
            TempData["basarili"] = "Kayıt silme işlemi başarılı!";
            return RedirectToAction("Index", "Kiralama");

        }


    }
}

