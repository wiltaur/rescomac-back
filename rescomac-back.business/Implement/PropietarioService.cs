using Microsoft.EntityFrameworkCore;
using rescomac_back.business.Interface;
using rescomac_back.repository.Context;
using rescomac_back.repository.Dto;
using rescomac_back.repository.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rescomac_back.business.Implement
{
    public class PropietarioService : IPropietarioService
    {
        private readonly RescomacDbContext _context;

        public PropietarioService(RescomacDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddPropiedad(PropiedadRequest propiedad)
        {
            Propiedad propiedadNew = new()
            {
                Nombre = propiedad.Nombre,
                Apellido = propiedad.Apellido,
                Apto = propiedad.Apto,
                Celular = propiedad.Celular,
                Correo = propiedad.Correo,
                Identificacion = propiedad.Identificacion,
                Torre = propiedad.Torre,
                EstadoPago = propiedad.EstadoPago
            };
            _context.Propiedads.Add(propiedadNew);
            return await _context.SaveChangesAsync() > 0 ? true : false;
        }

        public async Task<bool> UpdatePropiedad(PropiedadModRequest propiedad)
        {
            var existingPropiedad = (from prop in _context.Propiedads
                                     where prop.Id == propiedad.Id
                                     select prop).FirstOrDefault();

            if (existingPropiedad != null)
            {
                existingPropiedad.Nombre = propiedad.Nombre;
                existingPropiedad.Apellido = propiedad.Apellido;
                existingPropiedad.Celular = propiedad.Celular;
                existingPropiedad.Correo = propiedad.Correo;
                existingPropiedad.Torre = propiedad.Torre;
                existingPropiedad.EstadoPago = propiedad.EstadoPago;
                existingPropiedad.Identificacion = propiedad.Identificacion;
                existingPropiedad.Apto = propiedad.Apto;

                _context.Propiedads.Update(existingPropiedad);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> DeletePropiedad(int id)
        {
            var existingPropiedad = (from prop in _context.Propiedads
                                     where prop.Id == id
                                     select prop).FirstOrDefault();

            if (existingPropiedad != null)
            {
                _context.Propiedads.Remove(existingPropiedad);
                return await _context.SaveChangesAsync() > 0 ? true : false;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<PropiedadModRequest>> GetAllPropiedad()
        {
            List<PropiedadModRequest> propiedades = await (from prop in _context.Propiedads
                                                           select new PropiedadModRequest
                                                           {
                                                               Id = prop.Id,
                                                               Apellido = prop.Apellido,
                                                               Apto = prop.Apto,
                                                               Celular = prop.Celular,
                                                               Correo = prop.Correo,
                                                               EstadoPago = prop.EstadoPago,
                                                               Identificacion = prop.Identificacion,
                                                               Nombre = prop.Nombre,
                                                               Torre = prop.Torre
                                                           }).ToListAsync();
            return propiedades;
        }
    }
}