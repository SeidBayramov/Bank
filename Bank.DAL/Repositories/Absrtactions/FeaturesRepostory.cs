using Bank.Core.Entities.Models;
using Bank.DAL.Context;
using Bank.DAL.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.DAL.Repositories.Absrtactions
{
    public class FeaturesRepostory : Repository<Feature>, IFeatureRepository
    {
        public FeaturesRepostory(AppDbContext context) : base(context)
        {
        }
    }
}
