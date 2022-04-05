using Application.Models;
using Domain.Entities;
using FluentValidation;

namespace Application.Validators;

public class MovieQueryValidator : AbstractValidator<Query>
{
    private int[] allowedPageSizes = new[] { 5, 10, 15 };
    private string[] allowedSortByColumnNames = { nameof(Movie.Title), nameof(Movie.Detail.LastModified), nameof(Movie.Disk.Name) };
    public MovieQueryValidator()
    {
        RuleFor(r => r.PageNumber).GreaterThanOrEqualTo(1);
        RuleFor(r => r.PageSize).Custom((value, context) =>
        {
            if (!allowedPageSizes.Contains(value))
            {
                context.AddFailure("PagedSize", $"PageSize must be in [{string.Join(",", allowedPageSizes)}]");
            }
        });
        RuleFor(r => r.SortBy).Must(value => string.IsNullOrEmpty(value) || allowedSortByColumnNames.Contains(value))
            .WithMessage($"Sort by is optional, or must be in [{string.Join(",", allowedSortByColumnNames)}]");
    }
}
