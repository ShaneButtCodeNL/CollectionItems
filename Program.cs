using CollectionItems.Models;
using CollectionItems.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Text;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
//var symmetric = Aes.Create();
//symmetric.Key =  System.Text.Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JWT").GetValue<string>("key"));

// Add services to the container.
//Added to get database values from appsettings.json
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettings"));
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("DatabaseSettingsLogin"));


//Added items services
builder.Services.AddSingleton<ItemsService>();
builder.Services.AddSingleton<LoginService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options=>{
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters{
            ValidateIssuer=true,
            ValidateAudience=true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey=true,
            ValidIssuer = builder.Configuration.GetSection("JWT").GetValue<string>("Issuer"),
            ValidAudience = builder.Configuration.GetSection("JWT").GetValue<string>("Audience"),
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT").GetValue<string>("key")))
        };
    }
);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
        Name = "Authorization",
            Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
