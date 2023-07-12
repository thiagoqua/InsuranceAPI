using Microsoft.EntityFrameworkCore;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using InsuranceAPI.Repositories;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http.Features;
using InsuranceAPI.Exceptions;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config => {
    string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.SwaggerDoc("v1", new OpenApiInfo {
            Title = "Insurance API",
            Description = "An API to perform and manage insurance company's clients",
            Contact = new OpenApiContact {
                Name = "Thiago Quaglia",
                Email = "thiagoqua16@gmail.com",
                Url = new Uri("http://thiagoqua.ar")
            }
        }
    );
    config.IncludeXmlComments(xmlPath);
    config.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    config.AddSecurityRequirement(new OpenApiSecurityRequirement {
        { 
            new OpenApiSecurityScheme { Reference = new OpenApiReference {
                Type = ReferenceType.SecurityScheme,
                Id = "Bearer"
            }}, new string[]{ } }
    });
});
builder.Services.AddDbContext<DbInsuranceContext>(opt => {
    //DB in DESKTOP
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("desktopDB"));
    //DB in DOCKER
    //opt.UseSqlServer(builder.Configuration.GetConnectionString("dockerDB"));
    //DB and API in DOCKER
    opt.UseSqlServer(builder.Configuration.GetConnectionString("docker-netDB"));
}
);

//adding DI instances
builder.Services.AddScoped<IInsuredService, InsuredService>();
builder.Services.AddScoped<IInsuredRepository, InsuredRepository>();
builder.Services.AddScoped<IAddressRepository,AddressRepository>();
builder.Services.AddScoped<IPhoneRepository, PhoneRepository>();
builder.Services.AddScoped<IProducerRepository, ProducerRepository>();
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IFileService, FileService>();

//cors
builder.Services.AddCors(opt => {
    opt.AddPolicy("everything", policy => 
        policy.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .WithExposedHeaders("content-disposition"));
});

//adding JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                builder.Configuration["JWT:key"]
            )),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

//configuring to enable uploading files
builder.Services.Configure<FormOptions>(opt =>{
    opt.ValueLengthLimit = int.MaxValue;
    opt.MultipartBodyLengthLimit = int.MaxValue;
    opt.MemoryBufferThreshold = int.MaxValue;
});

// Configuring writing bytes to the response body
builder.Services.Configure<IISServerOptions>(options => {
    options.AllowSynchronousIO = true;
});

builder.Services.AddControllers(opt =>
    opt.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()){
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("everything");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
