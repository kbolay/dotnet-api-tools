# dotnet-api-tools
A repository of tools and examples for dotnet APIs.


# src/FromCookie
Provides tools to use [FromCookie] in the same way as [FromHeader] can be used to bind an api method parameter value.

Run the examples/FromCookie.Api project and use the examples/FromCookie.Api/FromCookie.Api.http file to run a request. Change the Cookie my-cookie value to get a different response.


# src/TrackingCookie
Uses FromCookie and adds Middleware to create a tracking cookie if one doesn't exist, using Set-Cookie. 
The domain of the cookie will be set to a subdomain of the referer domain.
The name of the cookie, the subdomain, and expiration of the cookie are currently configurable, more configuration could be added.
Expiration of the cookie is bumped with every request.

Run the examples/TrackingCookie.Api project and hit the /v1/test endpoint. View the response Set-Cookie header, the body of the response is set with the value of the cookie.

# Testing
maybe soon

# Packaging
Need to build up Github Actions to push to Nuget after testing.