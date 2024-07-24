using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.Commands.Favorites;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Favorites
{
    public class UnfavoriteArticleCommandHandler : IRequestHandler<UnfavoriteArticleCommand, ArticleResponseDto>
    {
        private readonly IFavoritesRepository _favoriteRepository;
        private readonly IMapper _mapper;
        public async Task<ArticleResponseDto> Handle(UnfavoriteArticleCommand request, CancellationToken cancellationToken)
        {
            var article = await _favoriteRepository.UnfavoriteArticleAsync(request.Slug);
            var articleResponseDto = _mapper.Map<ArticleResponseDto>(article);
            return articleResponseDto;
        }
    }
}
