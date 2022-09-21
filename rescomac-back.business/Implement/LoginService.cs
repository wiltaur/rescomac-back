using Microsoft.EntityFrameworkCore;
using rescomac_back.business.Interface;
using rescomac_back.repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rescomac_back.business.Implement
{
    public class LoginService : ILoginService
    {
        private readonly RescomacDbContext _context;

        public LoginService(RescomacDbContext context)
        {
            _context = context;
        }
        public async Task<bool> ValidarUsuario(string email, string password)
        {
            var existeUsuario = await (from u in _context.Usuarios
                                       where u.Correo.ToUpper() == email.ToUpper() && u.Contresena == password
                                       select u).FirstOrDefaultAsync();
            return existeUsuario != null ? true : false;
        }
    }
}