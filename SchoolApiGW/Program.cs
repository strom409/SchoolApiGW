using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolApiGW.Helper;
using SchoolApiGW.Helper.Error;
using SchoolApiGW.Middleware;
using SchoolApiGW.Services.Attendence;
using SchoolApiGW.Services.ClassMaster;
using SchoolApiGW.Services.ClassTest;
using SchoolApiGW.Services.ClassTest.Subject;
using SchoolApiGW.Services.Departments;
using SchoolApiGW.Services.Designation;
using SchoolApiGW.Services.District;
using SchoolApiGW.Services.Employee;
using SchoolApiGW.Services.EmpStatus;
using SchoolApiGW.Services.HT;
using SchoolApiGW.Services.login;
using SchoolApiGW.Services.Marks;
using SchoolApiGW.Services.MaxMarks;
using SchoolApiGW.Services.OptionalMarks;
using SchoolApiGW.Services.OptionalMaxMarks;
using SchoolApiGW.Services.Qualifications;
using SchoolApiGW.Services.Salary;
using SchoolApiGW.Services.Students;
using SchoolApiGW.Services.Subjects;
using SchoolApiGW.Services.TeacherLog;
using SchoolApiGW.Services.Transport;
using SchoolApiGW.Services.Units;
using SchoolApiGW.Services.Users;
using System.Net;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Configuration
builder.Configuration
    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: false, reloadOnChange: true)
    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, $"appsettings.{builder.Environment.EnvironmentName}.json"), optional: true, reloadOnChange: true);

// Bind JwtSettings
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
var jwtSettings = builder.Configuration.GetSection("JwtSettings").Get<JwtSettings>();

ConfigurationManager configuration = builder.Configuration;

// Services
builder.Services.AddOptions();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ClientIdHeaderHandler>();

// Correct HttpClient registration
builder.Services.AddHttpClient("Default", client =>
{
    client.Timeout = TimeSpan.FromMinutes(2);
    client.DefaultRequestVersion = HttpVersion.Version11;
    client.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionOrLower;
})
.ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
{
    ServerCertificateCustomValidationCallback = (message, cert, chain, sslPolicyErrors) => true // Accept any cert
})
.AddHttpMessageHandler<ClientIdHeaderHandler>();

// Dependency Injection
builder.Services.AddSingleton<IConfiguration>(configuration);

builder.Services.AddScoped<ICrescentStudentClient, CrescentStudentClient>();
builder.Services.AddScoped<IStudentClient, StudentClient>();
builder.Services.AddScoped<IDistrictClient, DistrictClient>();
builder.Services.AddScoped<IHTClient, HTClient>();
builder.Services.AddScoped<ILoginClient, LoginClient>();
// builder.Services.AddScoped<ILoginServices, LoginServices>();
builder.Services.AddScoped<IUserClient, UserClient>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ITransportClient, TransportClient>();
builder.Services.AddScoped<IClassMasterClient, ClassMasterClient>();
builder.Services.AddScoped<IAttendenceClient, AttendenceClient>();
builder.Services.AddScoped<IClassTestClient, ClassTestClient>();
builder.Services.AddScoped<ISubjectClient, SubjectClient>();
builder.Services.AddScoped<IEmployeeClient, EmployeeClient>();
builder.Services.AddScoped<ISalaryClient, SalaryClient>();
builder.Services.AddScoped<IDesignationClient, DesignationClient>();
builder.Services.AddScoped<IDepartmentsClient, DepartmentsClient>();
builder.Services.AddScoped<IEmpStatusClient, EmpStatusClient>();
builder.Services.AddScoped<IQualificationsClient, QualificationsClient>();
builder.Services.AddScoped<IEmployeeSubjectsClient, EmployeeSubjectsClient>();
builder.Services.AddScoped<ITeacherLogClient, TeacherLogClient>();
builder.Services.AddScoped<IUnitsClient, UnitsClient>();
builder.Services.AddScoped<IMaxMarksClient, MaxMarksClient>();
builder.Services.AddScoped<IMarksClient, MarksClient>();
builder.Services.AddScoped<IOptionalMaxMarksClient, OptionalMaxMarksClient>();

builder.Services.AddScoped<IOptionalMarksClient, OptionalMarksClient>();
builder.Services.AddScoped<ErrorBLL>();


builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

// ? JWT Authentication
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.Issuer,
        ValidAudience = jwtSettings.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey)),
        ClockSkew = TimeSpan.Zero // Optional: reduces delay for token expiration
    };
});

// Swagger config with JWT
builder.Services.AddSwaggerGen(opt =>
{
    opt.SwaggerDoc("v1", new OpenApiInfo { Title = "NIT Services GW Api", Version = "v1" });

    opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer"
    });

    opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            Array.Empty<string>()
        }
    });
});

// Build App
var app = builder.Build();

ErrorBLL.Configure(app.Services);

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        string swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "NIT Services Bff Api");
    });
}
var clientDataPath = Path.Combine(app.Environment.ContentRootPath, "ClientData");

// Register static file middleware only if folder exists
if (Directory.Exists(clientDataPath))
{
    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = new PhysicalFileProvider(clientDataPath),
        RequestPath = "/ClientData"
    });
}



///app.UseMiddleware<JWTMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowOrigin");
app.UseAuthentication(); // ? Important for JWT
app.UseAuthorization();
app.MapControllers();
app.Run();
