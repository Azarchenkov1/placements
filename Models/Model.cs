using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace placements.Models
{
    public class Model : DbContext
    {
        bool IsInitialize = false;
        bool IsRecreate = false;
        public DbSet<Placement> PlasementList { get; set; }
        public Placement placement = new Placement();
        public Model(DbContextOptions<Model> options) : base(options)
        {
            if(IsRecreate)
            {
                Recreate();
            }
            if(IsInitialize)
            {
                Initialize();
            }
        }
        public static Model ModelFactory()
        {
            var optionsBuilder = new DbContextOptionsBuilder<Model>();
            var connectionString = "Server=LAPTOP-73MHSR7G\\SQLEXPRESS;Database=PlacementsDataBase;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connectionString);
            return new Model(optionsBuilder.Options);
        }
        void Recreate()
        {
            this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }
        void Initialize()
        {
            PlasementList.Add(placement1);
            PlasementList.Add(placement2);
            PlasementList.Add(placement3);
            SaveChanges();
        }

        //Initialize data
        public Placement placement1 = new Placement { header = "website placement", type = "website", location = "website.net" };
        public Placement placement2 = new Placement { header = "street bigbord placement", type = "street", location = "Kiev, Saksaganskogo str." };
        public Placement placement3 = new Placement { header = "sky mall placements", type = "building", location = "Kiev, Generala Vatutina str." };
    }
}