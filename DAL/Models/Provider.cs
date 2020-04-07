using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Provider: MasterEntity
    {
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El RUC tiene un límite de 50 caracteres")]
        public string LegalId { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El nombre tiene un límite de 50 caracteres")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(75, ErrorMessage = "El país tiene un límite de 75 caracteres")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El nombre de contacto tiene un límite de 75 caracteres")]
        public string ContactName { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El teléfono de contacto tiene un límite de 50 caracteres")]
        public string ContactPhone { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El email tiene un límite de 50 caracteres")]
        public string ContactEmail { get; set; }
        public string Notes { get; set; }

        public virtual IList<Product> Products { get; set; }
    }
}
