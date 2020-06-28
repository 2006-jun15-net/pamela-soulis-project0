using pamela_soulis_project0DataAccess.Model;
using pamelasoulisproject0Library;
using System;
using System.Collections.Generic;
using System.Text;


namespace pamela_soulis_project0Library.Repositories
{
    interface IGenericRepository<TDAL, TBLL>
        where TDAL : DataModel, new()
        where TBLL : BaseBusinessModel, new()
    {
        IEnumerable<TBLL> GetAll();
        TBLL GetById(int id);
        void Insert(TBLL obj);
        void Update(TBLL obj);
        void Delete(int id);
        void SaveToDB();
    }

}