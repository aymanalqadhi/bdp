using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="SellableReview"/>
/// </summary>
public sealed class SellableReviewsRepository :
    EfRepository<SellableReview, SellableReviewValidator>, ISellableReviewsRepository
{
    public SellableReviewsRepository(DbSet<SellableReview> set) : base(set)
    {
    }
}