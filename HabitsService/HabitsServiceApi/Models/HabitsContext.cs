using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HabitsServiceApi.Models
{
    public class HabitsContext: DbContext
    {
        public DbSet<Habit> Habits { get; set; }
    }
}
