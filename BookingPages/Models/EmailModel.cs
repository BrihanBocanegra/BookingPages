using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;


namespace BookingPages.Models
{
    public class EmailModel
    {

        [Required(ErrorMessage = "El nombre es requerido.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es requerido.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El correo electrónico es obligatorio.")]
        [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
        public string Email { get; set; }
        public string AdditionalContacts { get; set; }
        public string Subject { get; set; }
        public string Company { get; set; }
        public string PostalCode { get; set; }

        [RegularExpression(@"^\d{10}$", ErrorMessage = "El número de teléfono no es válido.")]
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Country { get; set; }
        [BindProperty]
        public List<string> Solution { get; set; }

        public Guid EventId { get; set; }
        public string EventName { get; set; }
        = string.Empty;
        public string EventType { get; set; }
        = string.Empty;
        public string Staff { get; set; }
        public string Message { get; set; }
        
        public string Attendees { get; set; }

        public List<string> EmailList { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }

        public DateTimeOffset CurrentDateTime { get; set; }

        public EmailModel()
        {
            FirstName = string.Empty;
            EventId = Guid.NewGuid();
            // Inicializa la propiedad DateCreate con la fecha y hora actual y la zona horaria local.
            CurrentDateTime = DateTimeOffset.Now;

        }
    }
}
