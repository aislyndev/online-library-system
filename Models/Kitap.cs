﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KutuphaneSistemi.Models
{
    public class Kitap


    {
        [Key]
        public int Id { get; set; }

        
        public String KitapAdi { get; set; }

        public string Tanim {  get; set; }


        [Required]
        public string Yazar {  get; set; }

        [Required]
        [Range(10, 5000)]
        public double Fiyat {  get; set; }

        [ValidateNever]
        public int KitapTuruId { get; set; }
        [ForeignKey("KitapTuruId")]


        [ValidateNever]
        public KitapTuru KitapTuru { get; set;}
        [ValidateNever]
        public string ResimUrl { get; set; }

    }
}
