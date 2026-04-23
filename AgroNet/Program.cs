using AgroNet.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using AgroNet.Mapping;
using AgroNet.Services;
using AgroNet.Interfaces.Usuario;
using AgroNet.Interfaces.Finca;
using System.Text.Json.Serialization;
using AgroNet.Interfaces.Cosecha;
using AgroNet.Interfaces.Pedido;
using AgroNet.Interfaces.Trazabilidad;

var builder = WebApplication.CreateBuilder(args);



// -------------------------------
// 1.1 Configurar conexión a MySQL
// -------------------------------

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//configuracion para que detecte la version de mysql
builder.Services.AddDbContext<AppDbContext>(options =>
options.UseMySql(
    connectionString,
    ServerVersion.AutoDetect(connectionString)
    )
);


// -------------------------------
// 2.  Configurar JWT
// -------------------------------

var jwtSettings = builder.Configuration.GetSection("Jwt");
var key = Encoding.UTF8.GetBytes(jwtSettings["Key"]);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});



// --------------------------------
// 3. Registro de servicios personalizados
// --------------------------------

builder.Services.AddScoped<IAuthService,AuthService>();
builder.Services.AddScoped<IPerfilService,PerfilService>();
builder.Services.AddScoped<IFincaService, FincaService>();
builder.Services.AddScoped<ICosechaService, CosechaService>();
builder.Services.AddScoped<IPedidoService, PedidoService>();
builder.Services.AddScoped<ITrazabilidadService, TrazabilidadService>();

// --------------------------------
// 4. Registro de AutoMapper
// --------------------------------

builder.Services.AddAutoMapper(typeof(MappingProfile));

// --------------------------------
// 5. Controladores para la API
// --------------------------------


// --------------------------------
// 6. Swagger + Configuración JWT
// --------------------------------
// Swagger permite probar la API desde el navegador

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "AgroNet API", Version = "v1" });

    // Añadir el botón de "Authorize" en la UI de Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. Ejemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

// Add services to the container.

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

/*esta pequeña configuracion ayudara a que el frontend no sufra al momento de hacer el menu desplegable con 
  las palabras clave del estado de cosecha */
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Esto le dice a mi API: "Si te envian un Enum como texto, entiéndelo y conviértelo"
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();



// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())       COMENTO ESTE IF PARA HABILITAR SWAGGER EN PRODUCCION 
//{
    //app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
