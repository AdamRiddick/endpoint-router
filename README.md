# Endpoint Router

Endpoint Router is a simple endpoint router design to capture incoming requests to registered paths and redirect them to an endpoint handler, which response with an endpoint result.

The aim is to allow packages to utilise endpoints without controller implementations.

## Easy To Use

**Currently not available through nuget**

1. Add Services.
  ```services.AddEndpointRouter();```
2. Add Endpoint Router to the application. 
    ```app.UseEndpointRouter();```
3. Create an IEndpointHandler which returns an IEndpointResult, and register it against a path;
    ```services.AddEndpointHandler<DemoEndpointHandler>("/demo");```
4. Any requests to the registered path (/demo) will now be handled by your endpoint handler.

## Todo

 - Add to nuget
 - Write some tests

##License

MIT

**Free Software, Hell Yeah!**