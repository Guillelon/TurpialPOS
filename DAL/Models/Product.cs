using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Product:MasterEntity
    {
        public string CodeBar { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal Cost { get; set; }
        public decimal Tax { get; set; }

        //iFarmaLabs needs
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El código tiene un límite de 50 caracteres")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "La orden tiene un límite de 50 caracteres")]
        public string OrderNumber { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El número de catalogo tiene un límite de 50 caracteres")]
        public string CatalogNumber { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El codigo de sanidad tiene un límite de 50 caracteres")]
        public string SanitaryCode { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "La marca tiene un límite de 50 caracteres")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        public int Stock { get; set; }

        public virtual Provider Provider { get; set; }
        public int ProviderId { get; set; }
    }
}
