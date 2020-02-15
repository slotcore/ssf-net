using Helper.Classes;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper.Service
{
    public interface IHumanResourceService
    {
        [Get("/api/human-resource/task-work/")]
        Task<TaskWork> TaskWorkGetList();

        [Get("/api/human-resource/task-work/{id}")]
        Task<TaskWork> TaskWorkGet(string id);

        [Post("/api/human-resource/task-work/")]
        Task<TaskWork> TaskWorkCreate([Body] TaskWork taskWork);

        [Post("/api/human-resource/production-work-v2/")]
        Task<TaskWork> ProductionWorkCreate([Body] ProductionWork productionWork);

        [Get("/api/human-resource/production-work-transmit")]
        Task<List<ProductionWork>> ProductionWorkTransmit();
    }
}
