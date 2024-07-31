using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FHG.AccessControl
{
    public class CloseVisits
    {
        private readonly ILogger _logger;
         private readonly ApplicationDbContext _context;

        public CloseVisits(ILoggerFactory loggerFactory, ApplicationDbContext context)
        {
            _logger = loggerFactory.CreateLogger<CloseVisits>();
            _context = context;
        }

        [Function("CloseVisits")]
        public void Run([TimerTrigger("0 */5 * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            var accessControls = _context.Visit.Where(c=>c.OriginId == 33).ToList();
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
            //sample change
        }
    }
    public class Visit{
  
        public int Id { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime Date { get; set; }
        public int OriginId { get; set; }
        public int? DurationMinutes { get; set; }
        public DateTime? ExitDate { get; set; }
    }


     public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(){}
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Visit> Visit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            //TODO set certain strings as allowing null on creating

            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
