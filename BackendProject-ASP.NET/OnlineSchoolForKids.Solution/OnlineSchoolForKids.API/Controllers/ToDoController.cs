using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        IGenericRepository<TODO> _TODORepo;

        public ToDoController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _TODORepo = _unitOfWork.Repository<TODO>();
        }

        [HttpPost]
        public async Task<IActionResult> AddToDo(ToDoDTO toDoDTO)
        {
            var ToDo = new TODO()
            {
                Content = toDoDTO.Content,
                Status = toDoDTO.Status,
                User = toDoDTO.User,
                UserId = toDoDTO.UserId
            };
            await _unitOfWork.Repository<TODO>().AddAsync(ToDo);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(toDoDTO);
            return BadRequest(new BaseErrorResponse(400, message: "Error happened while adding to db"));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllToDos()
        {
            var result = await _unitOfWork.Repository<TODO>().GetAllAsync();
            if (result.Count() > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteToDo(int ToDoId)
        {
            var ToDo = await _TODORepo.GetByIdAsync(ToDoId);

            if (ToDo is null)
                return NotFound(new BaseErrorResponse(404, $"ToDo with Id {ToDoId} not found."));

            _TODORepo.Delete(ToDo);
            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateToDO(ToDoDTO toDoDTO)
        {
            var existingToDo = await _TODORepo.GetByIdAsync(toDoDTO.Id);

            if (existingToDo is null)
                return NotFound(new BaseErrorResponse(404, $"ToDo with Id {toDoDTO.Id} not found."));

            existingToDo.Status = toDoDTO.Status;
            existingToDo.Content = toDoDTO.Content;
            existingToDo.CreationDate = DateTime.Now;

            _TODORepo.Update(existingToDo);

            var result = await _unitOfWork.CompleteAsync();

            if (result > 0)
                return Ok(result);

            return BadRequest(new BaseErrorResponse(400));


        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetToDoById(int id)
        {
            var result = await _unitOfWork.Repository<TODO>().GetByIdAsync(id);
            if (result != null)
                return Ok(result);
            return NotFound(new BaseErrorResponse(404));
        }
    }
}
