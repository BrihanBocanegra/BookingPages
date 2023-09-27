using BookingPages.Models;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Diagnostics;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Net.Mail;
using System;


namespace BookingPages.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> SendMessage(EmailModel model)
        {
            try
            {
                // Crear el mensaje de correo electrónico con los datos del formulario
                var MyEmail = "brihan.bocanegra@nexsysla.info";
                var Password = "Bb.2022.";
                // Procesar los datos del formulario aquí, como enviar un correo electrónico o guardarlos en una base de datos.
                var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress("Brihan Bocanegra", MyEmail));
                emailMessage.To.Add(new MailboxAddress("Receiver", model.Staff));
                emailMessage.Subject = "Mensaje desde Booking Pages";

                // Agregar lista de correos adicionales en la vista y enviarlos por correo
                var emailModel = new EmailModel();
                var listaCorreos = model.EmailList.SelectMany(e => e.Split(',')).Select(e => e.Trim()).ToList();
                emailModel.EmailList = listaCorreos;
                string emails = string.Join(", ", listaCorreos);


                string Solution = "";
                if (model.Solution != null)
                {
                    
                    foreach (var solution in model.Solution)
                    {
                        Solution += solution + ", ";
                    }                    
                }
                
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<p>Nombre: {model.FirstName}</p>" +
                                       $"<p>Apellido: {model.LastName}</p>" +
                                       $"<p>Email: {model.Email}</p>" +
                                       $"<p>Compañía: {model.Company}</p>" +
                                       $"<p>Código Postal: {model.PostalCode}</p>" +
                                       $"<p>Número de teléfono: {model.PhoneNumber}</p>" +
                                       $"<p>Dirección: {model.Address}</p>" +
                                       $"<p>País: {model.Country}</p>" +
                                       $"<p>Área de solución: {Solution}</p>" +
                                       $"<p>ID de evento: {model.EventId}</p>" +
                                       $"<p>Nombre de evento: {model.EventName}</p>" +
                                       $"<p>Tipo de evento: {model.EventType}</p>" +
                                       $"<p>Personal: {model.Staff}</p>" +
                                       $"<p>Mensaje: {model.Message}</p>" +
                                       $"<p>Asistentes: {model.Attendees}</p>" +
                                       $"<p>Contactos adicionales: {emails}</p>" +
                                       $"<p>Fecha: {model.Date.ToShortDateString()}</p>" +
                                       $"<p>Hora: {model.Time.ToString()}</p>" +
                                       $"<p>La fecha actual es: {model.CurrentDateTime.ToString("dd/MM/yyyy HH:mm:ss zzz")}</p>";

                emailMessage.Body = bodyBuilder.ToMessageBody();

                // Configurar y enviar el mensaje de correo electrónico
                using (var smtpClient = new MailKit.Net.Smtp.SmtpClient())
                {
                    await smtpClient.ConnectAsync("smtp.office365.com", 587);
                    await smtpClient.AuthenticateAsync(MyEmail, Password);
                    await smtpClient.SendAsync(emailMessage);
                    await smtpClient.DisconnectAsync(true);
                }

                TempData["SuccessMessage"] = "El correo electrónico se envió correctamente.";

                return RedirectToAction("Index", "Home");
            }
            catch (System.Exception)
            {
                TempData["ErrorMessage"] = "Ocurrió un error al enviar el correo electrónico.";

                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}