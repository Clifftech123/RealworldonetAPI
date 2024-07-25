using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Application.Commands.Comments;
using RealworldonetAPI.Application.DTO.comments;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.comments
{
    public class GetCommentCommandHandler : IRequestHandler<GetCommentCommand, List<CommentDetailDto>>
    {
        private readonly ICommentsRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;


        /// <summary>
        /// Initializes a new instance of the <see cref="GetCommentCommandHandler"/> class.
        /// </summary>
        /// <param name="commentRepository">The repository for managing comments.</param>
        /// <param name="mapper">The mapper for converting between entities and DTOs.</param>
        /// <param name="currentUserService">The service for retrieving the current user.</param>
        /// <param name="context">The database context.</param>
        public GetCommentCommandHandler(ICommentsRepository commentRepository, IMapper mapper, ICurrentUserService currentUserService, ApplicationDbContext context)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _context = context;
        }

        /// <summary>
        /// Handles the retrieval of comments for a specific article.
        /// </summary>
        /// <param name="request">The command containing the details of the article to retrieve comments for.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A list of comment details.</returns>
        /// <exception cref="InvalidOperationException">Thrown when the article is not found or the user does not have permission to view it.</exception>
        public async Task<List<CommentDetailDto>> Handle(GetCommentCommand request, CancellationToken cancellationToken)
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
                    throw new InvalidOperationException("Article not found or you do not have permission to view it.");
                }

                var comments = await _commentRepository.GetCommentsForArticleAsync(request.Slug);
                var commentResponseDto = _mapper.Map<List<CommentDetailDto>>(comments);
                return commentResponseDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while retrieving the comments: {ex.Message}", ex);
            }
        }
    }
}
