using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="PhoneNumber"/>
/// </summary>
public sealed class PhoneNumbersRepository :
    EfRepository<PhoneNumber, Validator<PhoneNumber>>, IPhoneNumbersRepository
{
    public PhoneNumbersRepository(
        DbSet<PhoneNumber> set) : base(set)
    {
    }
}
