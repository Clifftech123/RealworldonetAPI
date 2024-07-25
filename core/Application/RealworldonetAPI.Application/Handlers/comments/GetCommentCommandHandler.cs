using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.Commands.Comments;
using RealworldonetAPI.Application.DTO.comments;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.comments
{
    public class GetCommentCommandHandler : IRequestHandler<GetCommentCommand, List<CommentDetailDto>>
    {
        private readonly ICommentsRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetCommentCommandHandler(ICommentsRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<CommentDetailDto>> Handle(GetCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var comment = await _commentRepository.GetCommentsForArticleAsync(request.Slug);
                var commentResponseDto = _mapper.Map<List<CommentDetailDto>>(comment);
                return commentResponseDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving the comment: {ex.Message}", ex);
            }
        }
    }
}
