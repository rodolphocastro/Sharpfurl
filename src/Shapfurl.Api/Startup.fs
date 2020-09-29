namespace Shapfurl.Api

open System
open System.Collections.Generic
open System.Linq
open System.Threading.Tasks
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.HttpsPolicy;
open Microsoft.AspNetCore.Mvc
open Microsoft.Extensions.Configuration
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Microsoft.OpenApi.Models

type Startup private () =
    new (configuration: IConfiguration) as this =
        Startup() then
        this.Configuration <- configuration

    member __.SetupOpenApi(openApi: OpenApiInfo) =
        let result = openApi
        result.Title <- "Sharpfurl"
        result.Version <- "v1"
        result

    // This method gets called by the runtime. Use this method to add services to the container.
    member this.ConfigureServices(services: IServiceCollection) =                
        // Add framework services.
        services.AddControllers().AddNewtonsoftJson() |> ignore
        services
            .AddSwaggerGen(fun cfg ->
                cfg.SwaggerDoc ("v1", this.SetupOpenApi <| OpenApiInfo())
                )
            .AddSwaggerGenNewtonsoftSupport() |> ignore        

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    member this.Configure(app: IApplicationBuilder, env: IWebHostEnvironment) =
        if (env.IsDevelopment()) then
            app.UseDeveloperExceptionPage() |> ignore

        app.UseHttpsRedirection() |> ignore
        app.UseRouting() |> ignore
        
        app.UseAuthorization() |> ignore
        app
            .UseSwagger()
            .UseSwaggerUI(fun stp ->
                stp.RoutePrefix <- ""
                stp.SwaggerEndpoint ("/swagger/v1/swagger.json", "My API V1")
            ) |> ignore
        app.UseEndpoints(fun endpoints ->
            endpoints.MapControllers() |> ignore
            ) |> ignore

    member val Configuration : IConfiguration = null with get, set
