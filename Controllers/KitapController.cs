using KutuphaneSistemi.Migrations;
using KutuphaneSistemi.Models;
using KutuphaneSistemi.Utulity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using System.Net.Http.Headers;

namespace KutuphaneSistemi.Controllers
{
    
    public class KitapController : Controller
    {

        private readonly IKitapRepository _kitapRepository;
        private readonly IKitapTuruRepository _kitapTuruRepository;
        public readonly IWebHostEnvironment _webHostEnvironment;


        public KitapController(IKitapRepository kitapRepository, IKitapTuruRepository kitapTuruRepository,IWebHostEnvironment webHostEnvironment)
        {
            _kitapRepository = kitapRepository;
            _kitapTuruRepository = kitapTuruRepository;
            _webHostEnvironment = webHostEnvironment;
        }

        [Authorize(Roles = "Admin, Ogrenci")]
        public IActionResult Index()
        {

            //List<Kitap> objKitapList = _kitapRepository.GetAll().ToList();
            List<Kitap> objKitapList =_kitapRepository.GetAll(includeProps:"KitapTuru").ToList();
            
            return View(objKitapList);
        }

        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult EkleGuncelle(int? id)
        {
            IEnumerable<SelectListItem> KitapTuruList = _kitapTuruRepository.GetAll()
                .Select(k => new SelectListItem
                {
                    Text = k.Ad,
                    Value = k.Id.ToString()
                });
            ViewBag.KitapTuruList = KitapTuruList;
            //ekle
            if(id==null || id == 0)
            {   

                return View();
            }
            else
            {   //güncelle
                Kitap? kitapVt = _kitapRepository.Get(u => u.Id == id);

                if (kitapVt == null)
                {
                    return NotFound();
                }


                return View(kitapVt);

            }
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        [HttpPost]
        public IActionResult EkleGuncelle(Kitap kitap,IFormFile? file)
        {
            var errors = ModelState.Values.SelectMany(x => x.Errors);
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                string kitapPath = Path.Combine(wwwRootPath,@"img");

                if (file != null)
                {
                    using (var fileStream = new FileStream(Path.Combine(kitapPath, file.FileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    kitap.ResimUrl = @"\img\" + file.FileName;
                }
                if (kitap.Id == 0)
                {
                    TempData["basarili"] = "Yeni Kitap  Başarıyla Oluşturuldu";
                    _kitapRepository.Ekle(kitap);

                }
                else
                {
                    TempData["basarili"] = "Kitap güncelleme başarıyla oluşturuldu";
                    _kitapRepository.Guncelle(kitap);
                }


                //_kitapRepository.Ekle(kitap);
                _kitapRepository.Kaydet();
                
                return RedirectToAction("Index", "Kitap"); //SaveChanges ile bilgiler veritabanına ekleniyor.
            }
            return View();
        }
        /*
        public IActionResult Guncelle(int? id)
        {
            if(id== null || id==0)
            {
                return NotFound();
            }

            Kitap? kitapVt= _kitapRepository.Get(u=>u.Id==id);
            
            if(kitapVt == null)
            {
                return NotFound();
            }
                
            
            return View(kitapVt);
        }
        
        [HttpPost]
        public IActionResult Guncelle(Kitap kitap)
        {
            if (ModelState.IsValid)
            {
                _kitapRepository.Guncelle(kitap);
                _kitapRepository.Kaydet();
                TempData["basarili"] = "Yeni Kitap Türü Başarıyla Güncellendi";
                return RedirectToAction("Index", "Kitap"); //SaveChanges ile bilgiler veritabanına ekleniyor.
            }
            return View();
        }
        */
        // GET ACTION
        [Authorize(Roles = UserRoles.Role_Admin)]
        public IActionResult Sil(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Kitap? kitapVt = _kitapRepository.Get(u=>u.Id==id);

            if (kitapVt == null)
            {
                return NotFound();
            }


            return View(kitapVt);
        }
        [Authorize(Roles = UserRoles.Role_Admin)]
        // POST ACTION
        [HttpPost, ActionName("Sil")]
        public IActionResult SilPOST(int? id)
        {
            Kitap? kitap = _kitapRepository.Get(u => u.Id == id);
            if (kitap == null)
            {
                return NotFound();
            }


            _kitapRepository.Sil(kitap);
            _kitapRepository.Kaydet();
            TempData["basarili"] = "Kayıt silme işlemi başarılı!";
            return RedirectToAction("Index", "Kitap");

        }


    }
}

