using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Collaborator: MasterEntity
    {
        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El nombre tiene un límite de 50 caracteres")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "La cédula o pasaporte tiene un límite de 50 caracteres")]
        public string LegalId { get; set; }

        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(75, ErrorMessage = "La dirección tiene un límite de 75 caracteres")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El teléfono tiene un límite de 50 caracteres")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Los campos con asterisco son mandatorios")]
        [MaxLength(50, ErrorMessage = "El área de trabajo tiene un límite de 50 caracteres")]
        public string WorkArea { get; set; }
    }
}
