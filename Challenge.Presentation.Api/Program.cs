using Challenge.Infrastructure.Configuration;
using Challenge.Presentation.Api;
using Framework.Application;
using Framework.Application.Api;
using Framework.Application.Api.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;


    var builder = WebApplication.CreateBuilder(args);
    IWebHostEnvironment env = builder.Environment;

    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

    #region Cors
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("SpaApp",
                          policy =>
                          {
                              policy.WithOrigins("*");
                              policy.WithHeaders("*");
                              policy.WithMethods("*");
                          });
    });
    #endregion


    IConfiguration configuration = builder.Configuration;
    IServiceCollection services = builder.Services;
    ChallengeBootstrapper.Configure(services, configuration);

    services.AddScoped<IFileUploader, FileUploader>();
    services.AddScoped<IPasswordHasher, PasswordHasher>();
    services.AddScoped<IJwToken, JwToken>();
    services.Configure<JwtModel>(configuration.GetSection("Jwt"));
    services.AddSwagger();
    services.AddJWT();

    services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    services.AddEndpointsApiExplorer();



builder.Services.AddCors(option =>

        option.AddPolicy("spa", builder => builder.AllowAnyHeader()
        .AllowAnyMethod().WithOrigins("http:/localhost:4200")
        )
    );


services.AddApiVersioning(opt =>
    {
        opt.DefaultApiVersion = new ApiVersion(1, 0);
        opt.AssumeDefaultVersionWhenUnspecified = true;
        opt.ReportApiVersions = true;
        opt.ApiVersionReader = ApiVersionReader.Combine(new UrlSegmentApiVersionReader(),
                                                        new HeaderApiVersionReader("x-api-version"),
                                                        new MediaTypeApiVersionReader("x-api-version"));
    });
    // Add ApiExplorer to discover versions
    services.AddVersionedApiExplorer(setup =>
    {
        setup.GroupNameFormat = "'v'VVV";
        setup.SubstituteApiVersionInUrl = true;
    });

    services.AddEndpointsApiExplorer();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();


    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions.Reverse())
            {
                options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                    description.GroupName.ToUpperInvariant());
            }
        });
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(env.WebRootPath, "Images")),
        RequestPath = new PathString("/Images")
    });
    app.UseRouting();
//app.UseMiddleware<AuthenticationValidationMiddleware>();
app.UseCors(c => c.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    app.Run();

