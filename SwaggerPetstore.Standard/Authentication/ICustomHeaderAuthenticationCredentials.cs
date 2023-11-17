// <copyright file="ICustomHeaderAuthenticationCredentials.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard.Authentication
{
    using System;

    public interface ICustomHeaderAuthenticationCredentials
    {
        /// <summary>
        /// Gets string value for apiKey.
        /// </summary>
        string ApiKey { get; }

        /// <summary>
        ///  Returns true if credentials matched.
        /// </summary>
        /// <param name="apiKey"> The string value for credentials.</param>
        /// <returns>True if credentials matched.</returns>
        bool Equals(string apiKey);
    }
}