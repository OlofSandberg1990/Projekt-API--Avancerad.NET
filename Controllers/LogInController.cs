using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Projekt_API__Avancerad.NET.Services.Interfaces;
using System.Threading.Tasks;
using BookingSystemLibrary;

namespace Projekt_API__Avancerad.NET.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthorize _authorizeRepo;

        public AuthController(IAuthorize authorizeRepo)
        {
            _authorizeRepo = authorizeRepo;
        }

        // Metod för att logga in
        [HttpPost("CustomerLogin")]
        public async Task<IActionResult> CustomerLogin([FromBody] LoginModel model)
        {
            try
            {
                //Sparar en ny customer med AuthenticCustomer-metoden från _autorizeRepot som tar mail och password som inparametrar.
                var customer = await _authorizeRepo.AuthenticateCustomer(model.Email, model.Password);
                if (customer == null)
                {
                    return Unauthorized($"Customer with Email {model.Email} and Password {model.Password} not found");
                }

                //Hämtar nuvarande Session och lagrar "UserRole" och "UserEmail" som nycklar, och Customer och customer.Email som value.
                HttpContext.Session.SetString("UserRole", "Customer");
                HttpContext.Session.SetString("UserEmail", customer.Email);


                //Retunerar Ok med kunden ID, namn och email.
                return Ok(new { customer.Id, customer.Name, customer.Email });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("CompanyLogin")]
        public async Task<IActionResult> CompanyLogin([FromBody] LoginModel model)
        {
            try
            {
                var company = await _authorizeRepo.AuthenticateCompany(model.Email, model.Password);
                if (company == null)
                {
                    return Unauthorized($"Company with Email {model.Email} and Password {model.Password} not found");
                }

                // Sätter sessionen för att lagra användarens roll och email
                HttpContext.Session.SetString("UserRole", "Company");
                HttpContext.Session.SetString("UserEmail", company.Email);
                return Ok(new { company.Id, company.Name, company.Email });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost("AdminLogin")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginModel model)
        {
            try
            {
                var admin = await _authorizeRepo.AuthenticateAdmin(model.Email, model.Password);
                if (admin == null)
                {
                    return Unauthorized($"Admin with Email {model.Email} and Password {model.Password} not found");
                }

                HttpContext.Session.SetString("UserRole", "Admin");
                HttpContext.Session.SetString("UserEmail", admin.Email);
                return Ok(new { admin.Id, admin.Email });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        // Metod för att logga ut en användare
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            try
            {
                // Rensar sessionen för att logga ut användaren
                HttpContext.Session.Clear();
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }

}
