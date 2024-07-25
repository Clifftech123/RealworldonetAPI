using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Application.Commands.Favorites;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Favorites
{
    public class UnfavoriteArticleCommandHandler : IRequestHandler<UnfavoriteArticleCommand, ArticleResponseDto>
    {
        private readonly IFavoritesRepository _favoriteRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly ApplicationDbContext _context;

        public UnfavoriteArticleCommandHandler(IFavoritesRepository favoriteRepository, IMapper mapper, ICurrentUserService currentUserService, ApplicationDbContext context)
        {
            _favoriteRepository = favoriteRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _context = context;
        }

        public async Task<ArticleResponseDto> Handle(UnfavoriteArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = _currentUserService.GetUserId();
                var article = await _context.Articles
                    .Where(a => a.Slug == request.Slug)
                    .Include(a => a.Author)
                    .FirstOrDefaultAsync();

                if (article == null)
                {
                    throw new InvalidOperationException("Article not found or you do not have permission to view it.");
                }

                var unfavoritedArticle = await _favoriteRepository.UnfavoriteArticleAsync(request.Slug, userId);
                var articleResponseDto = _mapper.Map<ArticleResponseDto>(unfavoritedArticle);
                return articleResponseDto;
            }
            catch (Exception ex) when (ex is SqlException || ex is System.Data.Entity.Core.EntityException)
            {
                // Handle database-related exceptions
                throw new InvalidOperationException("A database error occurred while unfavoriting the article.", ex);
            }
            catch (AutoMapperMappingException)
            {
                // Handle mapping exceptions
                throw new InvalidOperationException("An error occurred while mapping the article for the unfavorite operation.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new InvalidOperationException($"An unexpected error occurred: {ex.Message}", ex);
            }
        }
    }
}
