using BDP.Domain.Entities;

namespace BDP.Domain.Repositories;

/// <summary>
/// An interface to represent the functionality of a repository for the
/// <see cref="PhoneNumber"/> entity
/// </summary>
public interface IPhoneNumbersRepository : IRepository<PhoneNumber>
{
}