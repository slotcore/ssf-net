using Helper.Classes;
using Refit;
using System.Threading.Tasks;

namespace Helper.Service
{
    public interface IProductionService
    {
        [Post("/api/production/production-output/")]
        Task<ProductionOutput> ProductionOutputCreate([Body] ProductionOutput productionOutput);
    }
}
