using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Asset: MasterEntity
    {
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(250, ErrorMessage = "La descripción tiene un límite de 250 caracteres")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El área tiene un límite de 50 caracteres")]
        public string Area { get; set; }
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "La surcural tiene un límite de 50 caracteres")]
        public string Office { get; set; }
    }
}
