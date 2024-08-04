using API_NoiThat.Data;
using API_NoiThat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static WebAsemly_NoiThat.Service.LoginService;

namespace API_NoiThat.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public LoginController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            //Đăng xuất
            HttpContext.Session.Clear();
            return Ok();
        }

       
    }
}
