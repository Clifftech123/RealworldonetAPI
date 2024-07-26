using AutoMapper;
using MediatR;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Queries.Article;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{

    public class GetArticleBySlugQueryHandler : IRequestHandler<GetArticleBySlugQuery, List<ArticleResponseDto>>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;

        public GetArticleBySlugQueryHandler(IArticleRepository articleRepository, IMapper mapper)
        {
            _articleRepository = articleRepository ?? throw new ArgumentNullException(nameof(articleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<List<ArticleResponseDto>> Handle(GetArticleBySlugQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var articles = await _articleRepository.GetBySlugAsync(request.Slug);
                var articleResponseDtos = _mapper.Map<ArticleResponseDto>(articles);
                var totalCount = await _articleRepository.GetTotalCountAsync();
                return new List<ArticleResponseDto>
                {
                    articleResponseDtos
                };
                //return articleResponseDtos;

            }
            catch (Exception ex) when (ex is Microsoft.Data.SqlClient.SqlException || ex is System.Data.Entity.Core.EntityException)
            {
                throw new InvalidOperationException("A database error occurred while retrieving articles feed.", ex);
            }
            catch (AutoMapperMappingException)
            {
                throw new InvalidOperationException("An error occurred while mapping articles for the feed.");
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"An unexpected error occurred: {ex.Message}", ex);
            }
        }
    }

}
