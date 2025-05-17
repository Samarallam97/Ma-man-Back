
using OnlineSchoolForKids.Core.Repositories.Interfaces;

namespace OnlineSchoolForKids.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        IGenericRepository<Comment> _commentRepo;

        public CommentController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _commentRepo = _unitOfWork.Repository<Comment>();
            _mapper = mapper;
        }

        [HttpPost("add")]
        public async Task<ActionResult<CommentDTO>> AddComment(CommentDTO commentDTO)
        {
            //var comment = _mapper.Map<CommentDTO, Comment>(commentDTO);

            var comment = new Comment
            {
                Id = commentDTO.Id,
                Content = commentDTO.Content,
                UserId = commentDTO.UserId,
                ContentId = commentDTO.ContentId
            };

            await _commentRepo.AddAsync(comment);
            var result = await _unitOfWork.CompleteAsync();
            
            if (result > 0)
                return Ok("Comment Added Successfully");
            return BadRequest(new BaseErrorResponse(400, "An ERROR Happened while save to DB"));
        }

        [HttpPut("update/{id}")]
        public async Task<ActionResult<CommentDTO>> UpdateComment(int id, CommentDTO commentDTO)
        {
            var existingComment = await _commentRepo.GetByIdAsync(id);

            if (existingComment == null)
                return NotFound(new BaseErrorResponse(404, $"Comment with Id {id} not found."));
            
            existingComment.Content = commentDTO.Content;
            existingComment.UserId = commentDTO.UserId;
            existingComment.ContentId = commentDTO.ContentId;
           
            _commentRepo.Update(existingComment);
            var result = await _unitOfWork.CompleteAsync();
            
            if (result > 0)
                return Ok(commentDTO);
            return BadRequest(new BaseErrorResponse(400));
        }

        [HttpDelete("delete/{id}")]
        public async Task<ActionResult<CommentDTO>> DeleteComment(int id)
        {
            var comment = await _commentRepo.GetByIdAsync(id);
              
            if (comment is null)
                return NotFound(new BaseErrorResponse(404, $"Comment with Id {id} not found."));
            
            _commentRepo.Delete(comment);
            var result = await _unitOfWork.CompleteAsync();
            
            if (result > 0)
                return Ok("Comment Deleted Successfully");
            return BadRequest(new BaseErrorResponse(400, "An ERROR Happened while save to DB"));
        }

        [HttpGet("user/{id}")]
        public async Task<ActionResult<IReadOnlyList<Comment>>> GetAllCommentsByUserId(int id)
        {
            var spec = new Specification<Comment>(c => c.UserId == id);
            var result = await _commentRepo.GetAllWithSpecAsync(spec);

            if (result.Count > 0)
                return Ok(result);
            return NotFound(new BaseErrorResponse(404, $"No comments found for User with Id {id}."));
            
            
        }

        [HttpGet("content/{id}")]
        public async Task<ActionResult<IReadOnlyList<Comment>>> GetAllCommentsByContentId(int id)
        {
            var spec = new Specification<Comment>(c => c.ContentId == id);
            var result = await _commentRepo.GetAllWithSpecAsync(spec);

            if (result.Count > 0)
                return Ok(result);
            return NotFound(new BaseErrorResponse(404, $"No comments found for User with Id {id}."));
        }

    }
}
