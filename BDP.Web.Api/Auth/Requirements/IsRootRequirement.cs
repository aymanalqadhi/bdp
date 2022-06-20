﻿using BDP.Domain.Entities;

namespace BDP.Web.Api.Auth.Requirements;

public class IsRootRequirement : HasAllRolesRequirement
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public IsRootRequirement() : base(UserRole.Root)
    {
    }
}