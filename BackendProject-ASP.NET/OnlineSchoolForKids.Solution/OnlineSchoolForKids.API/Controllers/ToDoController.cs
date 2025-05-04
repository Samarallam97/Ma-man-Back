using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<ToDo> _ToDo;

        public ToDoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _ToDo = _unitOfWork.Repository<ToDo>();
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToDo(ToDoDTO toDoDTO)
        {
            var toDo = new ToDo()
            {
                CreationDate = toDoDTO.CreationDate,
                Content = toDoDTO.Content,
                Status = toDoDTO.Status,
                UserId = toDoDTO.UserId
            };
            await _unitOfWork.Repository<ToDo>().AddAsync(toDo);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(toDoDTO);
            return BadRequest(400);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteToDo(int ToDoId)
        {
            var toDo = await _ToDo.GetByIdAsync(ToDoId);

            if (toDo is null)
                return NotFound(new BaseErrorResponse(404, $"ToDo with Id {ToDoId} not found."));

            _ToDo.Delete(toDo);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(ToDoId);

            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpPut("update")]
        public async Task<ActionResult<ToDoDTO>> UpdateToDo(ToDoDTO toDoDTO)
        {
            var existingToDo = await _ToDo.GetByIdAsync(toDoDTO.Id);

            if (existingToDo == null)
                return NotFound(new BaseErrorResponse(404, $"Todo with Id {toDoDTO.Id} not found."));

            existingToDo.Content = toDoDTO.Content;

            _ToDo.Update(existingToDo);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(toDoDTO);

            return BadRequest(new BaseErrorResponse(400));
        }


        [HttpGet]
        public async Task<IActionResult> GetToDoByUserId(int UserId)
        {
            var spec = new Specification<ToDo>(T => T.UserId == UserId);

            var result = await _unitOfWork.Repository<ToDo>().GetAllWithSpecAsync(spec);

            if (result.Count() > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        
    }
}
