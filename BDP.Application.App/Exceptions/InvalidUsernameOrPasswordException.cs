﻿namespace BDP.Application.App.Exceptions;

public sealed class InvalidUsernameOrPasswordException : Exception
{
    /// <summary>
    /// Default constructor
    /// </summary>
    public InvalidUsernameOrPasswordException()
        : base($"invalid username or password")
    {
    }
}