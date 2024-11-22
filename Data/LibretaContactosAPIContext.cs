using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LibretaContactosAPI.Models;

namespace LibretaContactosAPI.Data
{
    public class LibretaContactosAPIContext : DbContext
    {
        public LibretaContactosAPIContext (DbContextOptions<LibretaContactosAPIContext> options)
            : base(options)
        {
        }

        public DbSet<LibretaContactosAPI.Models.Contactos> Contactos { get; set; } = default!;
    }
}
