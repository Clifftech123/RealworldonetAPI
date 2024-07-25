using AutoMapper;
using MediatR;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RealworldonetAPI.Application.DTO.article;
using RealworldonetAPI.Application.Interface;
using RealworldonetAPI.Application.Queries.Article;
using RealworldonetAPI.Infrastructure.Context;
using RealworldonetAPI.Infrastructure.Interfaces;

namespace RealworldonetAPI.Application.Handlers.Article
{
    public class GetArticlesFeedQueryHandler : IRequestHandler<GetArticlesFeedQuery, ArticlesResponseWrapper>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        public GetArticlesFeedQueryHandler(IArticleRepository articleRepository, IMapper mapper, ApplicationDbContext context,
            ICurrentUserService currentUserService)
        {

            _articleRepository = articleRepository;
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<ArticlesResponseWrapper> Handle(GetArticlesFeedQuery request, CancellationToken cancellationToken)
        {
            try
            {

                var articles = await _context.Articles
                    .Where(a => a.AuthorId == _currentUserService.GetUserId())
                    .Include(a => a.Author)
                    .OrderByDescending(a => a.CreatedAt)
                    .Skip(request.Offset)
                    .Take(request.Limit)
                    .ToListAsync();
                var articleResponseDtos = _mapper.Map<List<ArticleResponseDto>>(articles);
                var totalCount = await _articleRepository.GetTotalCountAsync();

                return new ArticlesResponseWrapper
                {
                    Articles = articleResponseDtos,
                    ArticlesCount = articles.Count()
                };
            }
            catch (Exception ex) when (ex is SqlException || ex is System.Data.Entity.Core.EntityException)
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
