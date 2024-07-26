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
    /// <summary>
    /// Handles the retrieval of articles feed.
    /// </summary>
    public class GetArticlesFeedQueryHandler : IRequestHandler<GetArticlesFeedQuery, ArticlesResponseWrapper>
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;

        /// <summary>
        /// Initializes a new instance of the <see cref="GetArticlesFeedQueryHandler"/> class.
        /// </summary>
        /// <param name="articleRepository">The repository for managing articles.</param>
        /// <param name="mapper">The mapper for converting between entities and DTOs.</param>
        /// <param name="context">The database context.</param>
        /// <param name="currentUserService">The service for retrieving the current user.</param>
        public GetArticlesFeedQueryHandler(IArticleRepository articleRepository, IMapper mapper, ApplicationDbContext context,
            ICurrentUserService currentUserService)
        {
            _articleRepository = articleRepository;
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        /// <summary>
        /// Handles the retrieval of articles feed.
        /// </summary>
        /// <param name="request">The query containing the details for retrieving the articles feed.</param>
        /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
        /// <returns>A wrapper containing the list of articles and the total count.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when a database error occurs, an error occurs while mapping, or an unexpected error occurs.
        /// </exception>
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
                    .Select(a => new Domain.Entities.Article
                    {
                        Slug = a.Slug,
                        Title = a.Title,
                        Description = a.Description,
                        Body = a.Body,
                        Tags = a.Tags,
                        CreatedAt = a.CreatedAt,
                        UpdatedAt = a.UpdatedAt,
                        Favorited = a.Favorited,
                        FavoritesCount = a.ArticleFavorites.Count,
                    })
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
