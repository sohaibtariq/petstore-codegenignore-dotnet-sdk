
# Custom Header Signature

Documentation for accessing and setting credentials for api_key.

## Auth Credentials

| Name | Type | Description | Getter Name |
|  --- | --- | --- | --- |
| apiKey | string | - | `getApiKey()` |

## Usage Example

### Client Initialization

You must provide credentials in the client as shown in the following code snippet.

```csharp
SwaggerPetstore.Standard.SwaggerPetstoreClient client = new SwaggerPetstore.Standard.SwaggerPetstoreClient.Builder()
    .CustomHeaderAuthenticationCredentials("api_key")
    .Environment(SwaggerPetstore.Standard.Environment.Production)
    .Build();
```

