using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using RealworldonetAPI.Application.Commands.Comments;
using RealworldonetAPI.Application.DTO.comments;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Domain.Entities;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.comments
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentResponseDto>
    {
        private readonly ICommentsRepository _commentRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateCommentCommandHandler(ICommentsRepository commentRepository, IMapper mapper, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
        {
            _commentRepository = commentRepository ?? throw new ArgumentNullException(nameof(commentRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _currentUserService = currentUserService ?? throw new ArgumentNullException(nameof(currentUserService));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<CommentResponseDto> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.GetUserId();
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new InvalidOperationException("User not found.");
                }

                var comment = _mapper.Map<Domain.Entities.Comment>(request.CreateComment);
                // Set the Username using the ApplicationUser's username
                comment.Username = user.UserName;

                var createdComment = await _commentRepository.CreateCommentForArticleAsync(request.Slug, comment);
                var commentResponseDto = _mapper.Map<CommentResponseDto>(createdComment);
                return commentResponseDto;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An error occurred while creating the comment: {ex.Message}", ex);
            }
        }
    }
}
