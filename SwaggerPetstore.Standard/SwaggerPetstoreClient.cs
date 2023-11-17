// <copyright file="SwaggerPetstoreClient.cs" company="APIMatic">
// Copyright (c) APIMatic. All rights reserved.
// </copyright>
namespace SwaggerPetstore.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using APIMatic.Core;
    using APIMatic.Core.Authentication;
    using APIMatic.Core.Types;
    using SwaggerPetstore.Standard.Authentication;
    using SwaggerPetstore.Standard.Controllers;
    using SwaggerPetstore.Standard.Http.Client;
    using SwaggerPetstore.Standard.Utilities;

    /// <summary>
    /// The gateway for the SDK. This class acts as a factory for Controller and
    /// holds the configuration of the SDK.
    /// </summary>
    public sealed class SwaggerPetstoreClient : IConfiguration
    {
        // A map of environments and their corresponding servers/baseurls
        private static readonly Dictionary<Environment, Dictionary<Enum, string>> EnvironmentsMap =
            new Dictionary<Environment, Dictionary<Enum, string>>
        {
            {
                Environment.Production, new Dictionary<Enum, string>
                {
                    { Server.Default, "https://petstore.swagger.io/v2" },
                }
            },
            {
                Environment.Environment2, new Dictionary<Enum, string>
                {
                    { Server.Default, "http://petstore.swagger.io/v2" },
                }
            },
            {
                Environment.Environment3, new Dictionary<Enum, string>
                {
                    { Server.Default, "https://petstore.swagger.io/oauth" },
                }
            },
        };

        private readonly GlobalConfiguration globalConfiguration;
        private const string userAgent = "APIMATIC 3.0";
        private readonly HttpCallBack httpCallBack;
        private readonly Lazy<PetController> pet;
        private readonly Lazy<StoreController> store;
        private readonly Lazy<UserController> user;

        private SwaggerPetstoreClient(
            Environment environment,
            CustomHeaderAuthenticationManager customHeaderAuthenticationManager,
            ImplicitAuthManager implicitAuthManager,
            HttpCallBack httpCallBack,
            IHttpClientConfiguration httpClientConfiguration)
        {
            this.Environment = environment;
            this.httpCallBack = httpCallBack;
            this.HttpClientConfiguration = httpClientConfiguration;
            implicitAuthManager.ApplyGlobalConfiguration(globalConfiguration);
            globalConfiguration = new GlobalConfiguration.Builder()
                .AuthManagers(new Dictionary<string, AuthManager> {
                    {"api_key", customHeaderAuthenticationManager},
                    {"petstore_auth", implicitAuthManager},
                })
                .ApiCallback(httpCallBack)
                .HttpConfiguration(httpClientConfiguration)
                .ServerUrls(EnvironmentsMap[environment], Server.Default)
                .UserAgent(userAgent)
                .Build();

            CustomHeaderAuthenticationCredentials = customHeaderAuthenticationManager;
            ImplicitAuth = implicitAuthManager;

            this.pet = new Lazy<PetController>(
                () => new PetController(globalConfiguration));
            this.store = new Lazy<StoreController>(
                () => new StoreController(globalConfiguration));
            this.user = new Lazy<UserController>(
                () => new UserController(globalConfiguration));
        }

        /// <summary>
        /// Gets PetController controller.
        /// </summary>
        public PetController PetController => this.pet.Value;

        /// <summary>
        /// Gets StoreController controller.
        /// </summary>
        public StoreController StoreController => this.store.Value;

        /// <summary>
        /// Gets UserController controller.
        /// </summary>
        public UserController UserController => this.user.Value;

        /// <summary>
        /// Gets the configuration of the Http Client associated with this client.
        /// </summary>
        public IHttpClientConfiguration HttpClientConfiguration { get; }

        /// <summary>
        /// Gets Environment.
        /// Current API environment.
        /// </summary>
        public Environment Environment { get; }

        /// <summary>
        /// Gets http callback.
        /// </summary>
        internal HttpCallBack HttpCallBack => this.httpCallBack;

        /// <summary>
        /// Gets the credentials to use with CustomHeaderAuthentication.
        /// </summary>
        public ICustomHeaderAuthenticationCredentials CustomHeaderAuthenticationCredentials { get; private set; }

        /// <summary>
        /// Gets the credentials to use with ImplicitAuth.
        /// </summary>
        public IImplicitAuth ImplicitAuth { get; private set; }

        /// <summary>
        /// Gets the URL for a particular alias in the current environment and appends
        /// it with template parameters.
        /// </summary>
        /// <param name="alias">Default value:DEFAULT.</param>
        /// <returns>Returns the baseurl.</returns>
        public string GetBaseUri(Server alias = Server.Default)
        {
            return globalConfiguration.ServerUrl(alias);
        }

        /// <summary>
        /// Creates an object of the SwaggerPetstoreClient using the values provided for the builder.
        /// </summary>
        /// <returns>Builder.</returns>
        public Builder ToBuilder()
        {
            Builder builder = new Builder()
                .Environment(this.Environment)
                .OAuthToken(ImplicitAuth.OAuthToken)
                .HttpCallBack(httpCallBack);

            if (CustomHeaderAuthenticationCredentials.ApiKey != null)
            {
                builder.CustomHeaderAuthenticationCredentials(CustomHeaderAuthenticationCredentials.ApiKey);
            }

            if (ImplicitAuth.OAuthClientId != null && ImplicitAuth.OAuthRedirectUri != null)
            {
                builder.ImplicitAuth(ImplicitAuth.OAuthClientId, ImplicitAuth.OAuthRedirectUri, ImplicitAuth.OAuthScopes);
            }

            return builder;
        }

        /// <inheritdoc/>
        public override string ToString()
        {
            return
                $"Environment = {this.Environment}, " +
                $"HttpClientConfiguration = {this.HttpClientConfiguration}, ";
        }

        /// <summary>
        /// Creates the client using builder.
        /// </summary>
        /// <returns> SwaggerPetstoreClient.</returns>
        internal static SwaggerPetstoreClient CreateFromEnvironment()
        {
            var builder = new Builder();

            string environment = System.Environment.GetEnvironmentVariable("SWAGGER_PETSTORE_STANDARD_ENVIRONMENT");
            string apiKey = System.Environment.GetEnvironmentVariable("SWAGGER_PETSTORE_STANDARD_API_KEY");
            string oAuthClientId = System.Environment.GetEnvironmentVariable("SWAGGER_PETSTORE_STANDARD_O_AUTH_CLIENT_ID");
            string oAuthRedirectUri = System.Environment.GetEnvironmentVariable("SWAGGER_PETSTORE_STANDARD_O_AUTH_REDIRECT_URI");

            if (environment != null)
            {
                builder.Environment(ApiHelper.JsonDeserialize<Environment>($"\"{environment}\""));
            }

            if (apiKey != null)
            {
                builder.CustomHeaderAuthenticationCredentials(apiKey);
            }

            if (oAuthClientId != null && oAuthRedirectUri != null)
            {
                builder.ImplicitAuth(oAuthClientId, oAuthRedirectUri);
            }

            return builder.Build();
        }

        /// <summary>
        /// Builder class.
        /// </summary>
        public class Builder
        {
            private Environment environment = SwaggerPetstore.Standard.Environment.Production;
            private readonly CustomHeaderAuthenticationModel customHeaderAuthentication = new CustomHeaderAuthenticationModel();
            private readonly ImplicitAuthModel implicitAuth = new ImplicitAuthModel();
            private HttpClientConfiguration.Builder httpClientConfig = new HttpClientConfiguration.Builder();
            private HttpCallBack httpCallBack;

            /// <summary>
            /// Sets credentials for CustomHeaderAuthentication.
            /// </summary>
            /// <param name="apiKey">ApiKey.</param>
            /// <returns>Builder.</returns>
            public Builder CustomHeaderAuthenticationCredentials(string apiKey)
            {
                customHeaderAuthentication.ApiKey = apiKey ?? throw new ArgumentNullException(nameof(apiKey));
                return this;
            }

            /// <summary>
            /// Sets credentials for ImplicitAuth.
            /// </summary>
            /// <param name="oAuthClientId">OAuthClientId.</param>
            /// <param name="oAuthRedirectUri">OAuthRedirectUri.</param>
            /// <param name="oAuthScopes">OAuthScopes.</param>
            /// <returns>Builder.</returns>
            public Builder ImplicitAuth(string oAuthClientId, string oAuthRedirectUri, List<Models.OAuthScopeEnum> oAuthScopes = null)
            {
                implicitAuth.OAuthClientId = oAuthClientId ?? throw new ArgumentNullException(nameof(oAuthClientId));
                implicitAuth.OAuthRedirectUri = oAuthRedirectUri ?? throw new ArgumentNullException(nameof(oAuthRedirectUri));
                implicitAuth.OAuthScopes = oAuthScopes;
                return this;
            }

            /// <summary>
            /// Sets OAuthToken.
            /// </summary>
            /// <param name="oAuthToken">OAuthToken.</param>
            /// <returns>Builder.</returns>
            public Builder OAuthToken(Models.OAuthToken oAuthToken)
            {
                implicitAuth.OAuthToken = oAuthToken;
                return this;
            }

            /// <summary>
            /// Sets Environment.
            /// </summary>
            /// <param name="environment"> Environment. </param>
            /// <returns> Builder. </returns>
            public Builder Environment(Environment environment)
            {
                this.environment = environment;
                return this;
            }

            /// <summary>
            /// Sets HttpClientConfig.
            /// </summary>
            /// <param name="action"> Action. </param>
            /// <returns>Builder.</returns>
            public Builder HttpClientConfig(Action<HttpClientConfiguration.Builder> action)
            {
                if (action is null)
                {
                    throw new ArgumentNullException(nameof(action));
                }

                action(this.httpClientConfig);
                return this;
            }

           

            /// <summary>
            /// Sets the HttpCallBack for the Builder.
            /// </summary>
            /// <param name="httpCallBack"> http callback. </param>
            /// <returns>Builder.</returns>
            internal Builder HttpCallBack(HttpCallBack httpCallBack)
            {
                this.httpCallBack = httpCallBack;
                return this;
            }

            /// <summary>
            /// Creates an object of the SwaggerPetstoreClient using the values provided for the builder.
            /// </summary>
            /// <returns>SwaggerPetstoreClient.</returns>
            public SwaggerPetstoreClient Build()
            {

                return new SwaggerPetstoreClient(
                    environment,
                    new CustomHeaderAuthenticationManager(customHeaderAuthentication),
                    new ImplicitAuthManager(implicitAuth),
                    httpCallBack,
                    httpClientConfig.Build());
            }
        }
    }
}
