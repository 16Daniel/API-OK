using biz.rebel_wings.Repository.Implementacion;
using dal.rebel_wings.DBContext;
using dal.rebel_wings.Repository.Generic;
using biz.rebel_wings.Models.Implementacion;
using Microsoft.EntityFrameworkCore;

namespace dal.rebel_wings.Repository.Implementacion
{

    public class MermaSucsRepository : GenericRepository<biz.rebel_wings.Entities._MermaSuc>, IMermaSucRepository
    {
        public MermaSucsRepository(Db_Rebel_WingsContext context) : base(context)
        {
        }

        
    }
}
