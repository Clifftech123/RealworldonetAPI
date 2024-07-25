using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Application.DTO.comments;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.comments
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentDetailDto>
    {
        private readonly ICommentsRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _context;

        public CreateCommentCommandHandler(ICommentsRepository commentRepository, IMapper mapper, ICurrentUserService currentUserService, ApplicationDbContext context)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<CommentDetailDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.GetUserId();

                var article = await _context.Articles
                    .Where(a => a.AuthorId == userId)
                    .Where(a => a.Slug == request.Slug)
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync();

                if (article == null)
                {
                    throw new InvalidOperationException("Article not found or user not authorized.");
                }

                var comment = _mapper.Map<Domain.Entities.Comment>(request.Comment);
                comment.AuthorId = _currentUserService.GetUserId();

                var createdComment = await _commentRepository.CreateCommentForArticleAsync(request.Slug, comment);
                var commentResponseDto = _mapper.Map<CommentDetailDto>(createdComment);
                return commentResponseDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while creating the comment: {ex.Message}", ex);
            }
        }
    }
}
