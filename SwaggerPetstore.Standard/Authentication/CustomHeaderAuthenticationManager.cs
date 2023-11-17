// <copyright file="CustomHeaderAuthenticationManager.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard.Authentication
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using SwaggerPetstore.Standard.Http.Request;
    using APIMatic.Core.Authentication;

    /// <summary>
    /// CustomHeaderAuthenticationManager Class.
    /// </summary>
    internal class CustomHeaderAuthenticationManager : AuthManager, ICustomHeaderAuthenticationCredentials
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomHeaderAuthenticationManager"/> class.
        /// </summary>
        /// <param name="customHeaderAuthentication">CustomHeaderAuthentication.</param>
        public CustomHeaderAuthenticationManager(CustomHeaderAuthenticationModel customHeaderAuthenticationModel)
        {
            ApiKey = customHeaderAuthenticationModel.ApiKey;
            Parameters(paramBuilder => paramBuilder
                .Header(header => header.Setup("api_key", ApiKey).Required())
            );
        }

        /// <summary>
        /// Gets string value for apiKey.
        /// </summary>
        public string ApiKey { get; }

        /// <summary>
        /// Check if credentials match.
        /// </summary>
        /// <param name="apiKey"> The string value for credentials.</param>
        /// <returns> True if credentials matched.</returns>
        public bool Equals(string apiKey)
        {
            return apiKey.Equals(this.ApiKey);
        }
    }

    internal sealed class CustomHeaderAuthenticationModel
    {
        internal string ApiKey { get; set; }
    }
    
}