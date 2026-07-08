using HelpDeskAPI.Models;
using HelpDeskAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ✅ إعداد الاتصال بقاعدة البيانات SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// ✅ إعداد نظام الهوية (Identity) مع خيارات كلمة المرور
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;              // لازم يحتوي رقم
    options.Password.RequireUppercase = true;          // لازم يحتوي حرف كبير
    options.Password.RequireNonAlphanumeric = true;    // لازم يحتوي رمز خاص
    options.Password.RequiredLength = 6;               // الطول الأدنى 6
})
.AddEntityFrameworkStores<AppDbContext>()
.AddDefaultTokenProviders();

// ✅ تفعيل MVC مع الـ Views
builder.Services.AddControllersWithViews();

// ✅ تفعيل Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// ✅ إعدادات التشغيل
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// ✅ لازم نضيف Authentication قبل Authorization
app.UseAuthentication();
app.UseAuthorization();

// ✅ تفعيل المسارات
app.MapControllers();
app.MapDefaultControllerRoute();

app.Run();
