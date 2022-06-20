﻿using BDP.Domain.Entities;
using BDP.Domain.Repositories;

namespace BDP.Domain.Services;

public interface ISellablesService
{
    /// <summary>
    /// Gets a sellable item by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<Sellable> GetByIdAsync(Guid id);

    /// <summary>
    /// Asynchronosly gets sellables for a user, limited with pagination
    /// </summary>
    /// <param name="userId">The id of the user which to get the products for</param>
    /// <returns></returns>
    IQueryBuilder<Sellable> GetForAsync(Guid userId);

    /// <summary>
    /// Asynchronosly searches sellables for a user, limited with paging
    /// </summary>
    /// <param name="userId">The id of the user which to get the products for</param>
    /// <param name="query">The to look for</param>
    /// <returns></returns>
    IQueryBuilder<Sellable> SearchForAsync(Guid userId, string query);

    /// <summary>
    /// Asynchronosly searches sellables gloablly, limited with paging
    /// </summary>
    /// <param name="query">The to look for</param>
    /// <returns></returns>
    IQueryBuilder<Sellable> SearchAsync(string query);
}