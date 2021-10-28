using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Salvo.Models
{
    public class SalvoContext : DbContext
    {
        //constructor de la clase
        public SalvoContext(DbContextOptions<SalvoContext> options) : base(options)
        {

        }
    }
}
