using BDP.Domain.Entities;
using BDP.Domain.Entities.Validators;
using BDP.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace BDP.Infrastructure.Repositories.EntityFramework;

/// <summary>
/// An implementation of <see cref="EfRepository{T}"/> for <see cref="Attachment"/>
/// </summary>
public sealed class AttachmentsRepository :
    EfRepository<Attachment, Validator<Attachment>>, IAttachmentsRepository
{
    public AttachmentsRepository(DbSet<Attachment> set) : base(set)
    {
    }
}
