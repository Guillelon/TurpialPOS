using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Proceeding : MasterEntity
    {
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El Número de orden tiene un límite de 50 caracteres")]
        public string OrderNumber { get; set; }

        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "Nombre de la institución tiene un límite de 50 caracteres")]
        public string InstitutionName { get; set; }

        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(250, ErrorMessage = "Descripción del producto tiene un límite de 250 caracteres")]
        public string ProductDescription { get; set; }

        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "Código sanitario del producto tiene un límite de 50 caracteres")]
        public string ProductSanitaryCode { get; set; }

        public bool Paid { get; set; }

        [MaxLength(250, ErrorMessage = "Las notas tiene un límite de 250 caracteres")]
        public string Notes { get; set; }

    }
}
