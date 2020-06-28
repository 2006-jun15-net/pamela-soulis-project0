using Microsoft.EntityFrameworkCore;
using pamela_soulis_project0DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using pamelasoulisproject0Library;
using AutoMapper;

namespace pamela_soulis_project0Library.Repositories
{
    public class GenericRepository<TDAL, TBLL> : IGenericRepository<TDAL, TBLL> 
        where TDAL : DataModel, new()
        where TBLL : BaseBusinessModel, new ()
    {
            private pamelasoulisproject0Context _context = null;
            private DbSet<TDAL> table = null;
            private IMapper mapper;

            public GenericRepository()
            {
                this._context = new pamelasoulisproject0Context();
                table = _context.Set<TDAL>();
                  
             
            }

            public GenericRepository(pamelasoulisproject0Context _context)
            {
                this._context = _context;
                table = _context.Set<TDAL>();
                var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<pamela_soulis_project0DataAccess.Model.Customer, pamelasoulisproject0Library.Customer>();
                cfg.CreateMap<pamelasoulisproject0Library.Customer, pamela_soulis_project0DataAccess.Model.Customer>();

                cfg.CreateMap<pamela_soulis_project0DataAccess.Model.Inventory, pamelasoulisproject0Library.Inventory>();
                cfg.CreateMap<pamelasoulisproject0Library.Inventory, pamela_soulis_project0DataAccess.Model.Inventory>();

                cfg.CreateMap<pamela_soulis_project0DataAccess.Model.Location, pamelasoulisproject0Library.Location>();
                cfg.CreateMap<pamelasoulisproject0Library.Location, pamela_soulis_project0DataAccess.Model.Location>();

                cfg.CreateMap<pamela_soulis_project0DataAccess.Model.OrderLine, pamelasoulisproject0Library.OrderLine>();
                cfg.CreateMap<pamelasoulisproject0Library.OrderLine, pamela_soulis_project0DataAccess.Model.OrderLine>();

                cfg.CreateMap<pamela_soulis_project0DataAccess.Model.Orders, pamelasoulisproject0Library.Orders>();
                cfg.CreateMap<pamelasoulisproject0Library.Orders, pamela_soulis_project0DataAccess.Model.Orders>();

                cfg.CreateMap<pamela_soulis_project0DataAccess.Model.Product, pamelasoulisproject0Library.Product>();
                cfg.CreateMap<pamelasoulisproject0Library.Product, pamela_soulis_project0DataAccess.Model.Product>();

            });
            mapper = config.CreateMapper();
        }

            public IEnumerable<TBLL> GetAll()
            {
                var dataObjects = table.ToList();
                var businessObjects = mapper.Map<List<TBLL>>(dataObjects);
                return businessObjects;
            }
         
            public TBLL GetById(int id)
            {
                var dataObjects = table.Find(id);
                var businessObjects = mapper.Map<TBLL>(dataObjects);
                return businessObjects;
            }

            public void Insert(TBLL obj)
            {
                 
                var dataObjects = mapper.Map<TDAL>(obj);
                table.Add(dataObjects);  
                

            }

            public void Update(TBLL obj)
            {
                //var businessObjects = new TBLL();
                var dataObjects = mapper.Map<TDAL>(obj);
                var updatedData = table.Attach(dataObjects);
                _context.Entry(obj).State = EntityState.Modified;
               
        }

            public void Delete(int id)
            {
                TDAL existing = table.Find(id);
                table.Remove(existing);
                //var businessObjects = mapper.Map<TDAL>(existing);
            }

            public void SaveToDB() 
            {
                _context.SaveChanges();
            }

            //public TBLL GetbyId(object id)
            //{
            //    throw new NotImplementedException();
            //}
    }
    
}

