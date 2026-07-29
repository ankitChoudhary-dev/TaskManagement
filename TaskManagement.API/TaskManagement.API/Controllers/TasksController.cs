using Microsoft.AspNetCore.Mvc;

namespace TaskManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {


        [HttpGet]
        public IActionResult GetTasks()
        {

            return Ok(new
            {
                message = "Get all tasks endpoint working",
                data = new List<object>()
            });

        }



        [HttpGet("{id}")]
        public IActionResult GetTask(int id)
        {

            return Ok(new
            {
                message = $"Get task {id} endpoint working"
            });

        }



        [HttpPost]
        public IActionResult CreateTask()
        {

            return Ok(new
            {
                message = "Create task endpoint working"
            });

        }



        [HttpPut("{id}")]
        public IActionResult UpdateTask(int id)
        {

            return Ok(new
            {
                message = $"Update task {id} endpoint working"
            });

        }



        [HttpDelete("{id}")]
        public IActionResult DeleteTask(int id)
        {

            return Ok(new
            {
                message = $"Delete task {id} endpoint working"
            });

        }



        [HttpGet("search")]
        public IActionResult SearchTask(string title)
        {

            return Ok(new
            {
                message = $"Search task by title : {title}"
            });

        }



        [HttpGet("filter")]
        public IActionResult FilterTask(
            string status,
            string priority)
        {

            return Ok(new
            {
                message = $"Filter task Status:{status}, Priority:{priority}"
            });

        }

    }
}