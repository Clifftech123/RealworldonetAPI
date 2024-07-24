using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using RealworldonetAPI.Application.Commands.Favorites;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Favorites
{
    public class FavoriteArticleCommandHandler : IRequestHandler<FavoriteArticleCommand, ArticleResponseDto>
    {
        private readonly IFavoritesRepository _favoriteRepository;
        private readonly IMapper _mapper;

        public FavoriteArticleCommandHandler(IFavoritesRepository favoriteRepository, IMapper mapper)
        {
            _favoriteRepository = favoriteRepository ?? throw new ArgumentNullException(nameof(favoriteRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ArticleResponseDto> Handle(FavoriteArticleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var article = await _favoriteRepository.FavoriteArticleAsync(request.Slug);
                var articleResponseDto = _mapper.Map<ArticleResponseDto>(article);
                return articleResponseDto;
            }
            catch (Exception ex) when (ex is SqlException || ex is System.Data.Entity.Core.EntityException)
            {
                // Handle database-related exceptions
                throw new InvalidOperationException("A database error occurred while favoriting the article.", ex);
            }
            catch (AutoMapperMappingException)
            {
                // Handle mapping exceptions
                throw new InvalidOperationException("An error occurred while mapping the article for the favorite operation.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                throw new InvalidOperationException($"An unexpected error occurred: {ex.Message}", ex);
            }
        }
    }
}
