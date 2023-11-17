
# Getting Started with Swagger Petstore

## Introduction

This is a sample server Petstore server.  You can find out more about Swagger at [http://swagger.io](http://swagger.io) or on [irc.freenode.net, #swagger](http://swagger.io/irc/).  For this sample, you can use the api key `special-key` to test the authorization filters.

Find out more about Swagger: [http://swagger.io](http://swagger.io)

## Building

The generated code uses the Newtonsoft Json.NET NuGet Package. If the automatic NuGet package restore is enabled, these dependencies will be installed automatically. Therefore, you will need internet access for build.

* Open the solution (SwaggerPetstore.sln) file.

Invoke the build process using Ctrl + Shift + B shortcut key or using the Build menu as shown below.

The build process generates a portable class library, which can be used like a normal class library. More information on how to use can be found at the MSDN Portable Class Libraries documentation.

The supported version is **.NET Standard 2.0**. For checking compatibility of your .NET implementation with the generated library, [click here](https://dotnet.microsoft.com/en-us/platform/dotnet-standard#versions).

## Installation

The following section explains how to use the SwaggerPetstore.Standard library in a new project.

### 1. Starting a new project

For starting a new project, right click on the current solution from the solution explorer and choose `Add -> New Project`.

![Add a new project in Visual Studio](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=addProject)

Next, choose `Console Application`, provide `TestConsoleProject` as the project name and click OK.

![Create a new Console Application in Visual Studio](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=createProject)

### 2. Set as startup project

The new console project is the entry point for the eventual execution. This requires us to set the `TestConsoleProject` as the start-up project. To do this, right-click on the `TestConsoleProject` and choose `Set as StartUp Project` form the context menu.

![Adding a project reference](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=setStartup)

### 3. Add reference of the library project

In order to use the `SwaggerPetstore.Standard` library in the new project, first we must add a project reference to the `TestConsoleProject`. First, right click on the `References` node in the solution explorer and click `Add Reference...`

![Adding a project reference](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=addReference)

Next, a window will be displayed where we must set the `checkbox` on `SwaggerPetstore.Standard` and click `OK`. By doing this, we have added a reference of the `SwaggerPetstore.Standard` project into the new `TestConsoleProject`.

![Creating a project reference](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=createReference)

### 4. Write sample code

Once the `TestConsoleProject` is created, a file named `Program.cs` will be visible in the solution explorer with an empty `Main` method. This is the entry point for the execution of the entire solution. Here, you can add code to initialize the client library and acquire the instance of a Controller class. Sample code to initialize the client library and using Controller methods is given in the subsequent sections.

![Adding a project reference](https://apidocs.io/illustration/cs?workspaceFolder=Swagger%20Petstore-CSharp&workspaceName=SwaggerPetstore&projectName=SwaggerPetstore.Standard&rootNamespace=SwaggerPetstore.Standard&step=addCode)

## Test the SDK

The generated SDK also contain one or more Tests, which are contained in the Tests project. In order to invoke these test cases, you will need `NUnit 3.0 Test Adapter Extension` for Visual Studio. Once the SDK is complied, the test cases should appear in the Test Explorer window. Here, you can click `Run All` to execute these test cases.

## Initialize the API Client

**_Note:_** Documentation for the client can be found [here.](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/client.md)

The following parameters are configurable for the API Client:

| Parameter | Type | Description |
|  --- | --- | --- |
| `Environment` | Environment | The API environment. <br> **Default: `Environment.Production`** |
| `Timeout` | `TimeSpan` | Http client timeout.<br>*Default*: `TimeSpan.FromSeconds(100)` |
| `CustomHeaderAuthenticationCredentials` | [`CustomHeaderAuthenticationCredentials`](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/auth/custom-header-signature.md) | The Credentials Setter for Custom Header Signature |
| `ImplicitAuth` | [`ImplicitAuth`](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/auth/oauth-2-implicit-grant.md) | The Credentials Setter for OAuth 2 Implicit Grant |

The API client can be initialized as follows:

```csharp
SwaggerPetstore.Standard.SwaggerPetstoreClient client = new SwaggerPetstore.Standard.SwaggerPetstoreClient.Builder()
    .CustomHeaderAuthenticationCredentials("api_key")
    .ImplicitAuth("OAuthClientId", "OAuthRedirectUri", new List<OAuthScopeEnum>
    {
        OAuthScopeEnum.Readpets,
        OAuthScopeEnum.Writepets,
    })
    .Environment(SwaggerPetstore.Standard.Environment.Production)
    .Build();
```

## Environments

The SDK can be configured to use a different environment for making API calls. Available environments are:

### Fields

| Name | Description |
|  --- | --- |
| production | **Default** |
| environment2 | - |
| environment3 | - |

## Authorization

This API uses the following authentication schemes.

* [`Custom Header Signature`](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/auth/custom-header-signature.md)
* [`OAuth 2 Implicit Grant`](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/auth/oauth-2-implicit-grant.md)

## List of APIs

* [Pet](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/controllers/pet.md)
* [Store](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/controllers/store.md)
* [User](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/controllers/user.md)

## Classes Documentation

* [Utility Classes](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/utility-classes.md)
* [HttpRequest](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/http-request.md)
* [HttpResponse](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/http-response.md)
* [HttpStringResponse](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/http-string-response.md)
* [HttpContext](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/http-context.md)
* [HttpClientConfiguration](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/http-client-configuration.md)
* [HttpClientConfiguration Builder](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/http-client-configuration-builder.md)
* [IAuthManager](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/i-auth-manager.md)
* [ApiException](https://www.github.com/sohaibtariq/petstore-codegenignore-dotnet-sdk/tree/1.0.0/doc/api-exception.md)

