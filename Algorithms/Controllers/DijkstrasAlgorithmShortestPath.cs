using Algorithms.Algorithms.DijkstrasAlgorithmShortestPath;
using Algorithms.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Algorithms.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DijkstrasAlgorithmShortestPathController : ControllerBase
    {
        [HttpPost]
        public IActionResult RunAlgorithm([FromBody] GraphViewModel vm)
        {
            if (vm.Method == 1)
            {
                var result = AlgorithmV1.Run(vm); // mostra a distancia do vertice inicial ao vertice final;
                return Ok(result);
            }
            if (vm.Method == 2)
            {
                var result = AlgorithmV2.Run(vm); // mostra o caminho do grafo, partindo do vertice inicial ao vertice final;
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
