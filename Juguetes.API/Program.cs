using Juguetes.Persistence.Dapper;
using Juguetes.Persistence.DB_Context;
using Juguetes.Services.Implementations;
using Juguetes.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(option =>
{
    var groupName = "v1";
    option.SwaggerDoc(groupName, new OpenApiInfo
    {
        Title = $"API Juguetes",
        Version = "Version 1.0.0",
        Description = "API para controlar las peticiones a base de datos para el sistema Juguetes"
    });

    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header utiliza esquema Bearer. \r\n\r\n Escribir 'Bearer' [espacio] y enseguida escirbir el token proporcionado.\r\n\r\nEjemplo: \"Bearer 186JGQD89KJSDJNC..\"",
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                   {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:SecretKey"])), //configuration["jwttoken:secretkey"]
            ClockSkew = TimeSpan.Zero
        };
    });

//Inyeccion de dependencias
builder.Services.AddScoped<IDapper_ORM, Dapper_ORM>();
builder.Services.AddTransient<IAuth_JWT, Auth_JWT>();
builder.Services.AddTransient<ILoginService, LoginService>();
builder.Services.AddTransient<IProductosService, ProductosService>();
builder.Services.AddTransient<IFoodTruckService, FoodTruckService>();
builder.Services.AddDbContext<ApiDBContext>(options => options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]));

var app = builder.Build();

app.UseCors(b => b.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "TodoApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
