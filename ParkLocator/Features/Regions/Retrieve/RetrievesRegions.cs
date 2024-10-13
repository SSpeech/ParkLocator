using Carter;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ParkLocator.DataBase;
using ParkLocator.Entities;
using ParkLocator.Features.Regions.Create.Contracts;
using ParkLocator.Features.Regions.Create.Extensions;
using ParkLocator.Shared;
using ParkLocator.Shared.Errors;
using ParkLocator.Shared.Paging;
using ParkLocator.Shared.Results;
using static ParkLocator.Features.Regions.Retrieve.RetrievesRegions;

namespace ParkLocator.Features.Regions.Retrieve;
public class RetrievesRegions
{
        public static void Endpoint(IEndpointRouteBuilder app)
        {
            app.MapGet("api/region", static async (int page, int pageSize, ISender sender, CancellationToken cancellationToken) =>
            {
                Result<PagedList<RegionResponse>> result = await sender.Send(new Query(page, pageSize), cancellationToken);
                return result.IsFailure ? Results.BadRequest(result.Errors.ResponseExtension(result.Error)) : Results.Ok(result.Value);
            });
        }
    
    public class Validator : AbstractValidator<Query>
    {
        public Validator()
        {
            RuleFor(query => query.Page)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .NotNull()
                .NotEmpty();
            RuleFor(query => query.PageSize).
                GreaterThan(0)
                .NotNull()
                .NotEmpty();
        }
    }
    public record Query(int Page, int PageSize) : IRequest<Result<PagedList<RegionResponse>>>;

    internal sealed class Handler : IRequestHandler<Query, Result<PagedList<RegionResponse>>>
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IValidator<Query> _validator;
        public Handler(ApplicationDbContext dbContext, IValidator<Query> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<Result<PagedList<RegionResponse>>> Handle(Query request, CancellationToken cancellationToken)
        {
            ValidationResult validationResult = await _validator.ValidateAsync(request,cancellationToken);
            if (!validationResult.IsValid)
            {
                List<Error>? errors = ValidationsResults<PagedList<RegionResponse>>.ValidationResultErrors(validationResult);
                return ValidationsResults<PagedList<RegionResponse>>.CreateErrorResult(errors,
                    "RegionsRetrieval.Validation",
                    "One or more Validation Errors occurred on during Retrieval");
            }

            IQueryable<Region>? regionsQuery =  _dbContext.Regions.AsNoTracking().AsQueryable();
            IQueryable<RegionResponse>? response = regionsQuery.ToResponse();
            var regions = await PagedList<RegionResponse>.CreateAsync(response, request.Page, request.PageSize, cancellationToken);
            return Result.Success(regions);
        }
    }

}

