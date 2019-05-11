using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Models
{
    public class HabitsContext: DbContext
    {
        public HabitsContext (DbContextOptions<HabitsContext> dbContextOptions) : base(dbContextOptions) {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

    public DbSet<Habit> Habits { get; set; }
    }

}
