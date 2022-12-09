using System.Reflection.Metadata;
using EFCoreMultitenantSample.Infrastructure.TenantSupport;

namespace EFCoreMultitenantSample.Web.Middlewares;
//https://github.com/Oriflame/EFCoreMultitenantSample#multiple-databases---complete-data-isolation
public class TenantScopeMiddleware
{
    private readonly RequestDelegate _next;

    public TenantScopeMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext httpContext, ITenantProvider tenantProvider)
    {
        //if(httpContext.Request.Query.TryGetValue("tenant", out var tenantName))
        //{
        //    using var scope = tenantProvider.BeginScope(tenantName);
        //    await _next(httpContext);

        //    return;
        //}
        // Got htis code from  http://blog.gaxion.com/2017/05/how-to-implement-multi-tenancy-with.html
        var GetAddress = httpContext.Request.Headers["Host"];

        var tenant = GetAddress[0];
        tenant = tenant.Replace("www.", "");
        tenant = tenant.Replace("https://", "");


        //tenant = "localhost:7198";
        //tenant = "school1.ahioma.com";
        ////tenant = "school2.exwhyzee.ng";
        //////tenant = "localhost:7198";
        ////if (tenant.ToString().ToLower() == "localhost:7198") // See here for localhost:80 or localhost:9780 ohh also for hamdun soft  execution will enter here . But for less than 2? will hamdunsoft.com enter here?
        ////{
        ////    string tenantname = "Local Host";
        ////    using var scope = tenantProvider.BeginScope(tenant);
        ////    await _next(httpContext);

        ////    return;
        ////}
        //school1.ahioma.com
        //school2.exwhyzee.ng
        //school3.sec44nipss.com
        //school4.exwhyzee.ng
        //school5.dmmmg.org
         if (tenant.ToString().ToLower() == "school1.ahioma.com")
        {
            using var scope = tenantProvider.BeginScope(tenant);
            await _next(httpContext);

            return;

        }
        else if (tenant.ToString().ToLower() == "school2.exwhyzee.ng")
        {
            using var scope = tenantProvider.BeginScope(tenant);
            await _next(httpContext);

            return;

        }
        else if (tenant.ToString().ToLower() == "school3.sec44nipss.com")
        {
            using var scope = tenantProvider.BeginScope(tenant);
            await _next(httpContext);

            return;

        }
        else if (tenant.ToString().ToLower() == "school4.exwhyzee.ng")//;Initial Catalog=;User Id=
        {
            using var scope = tenantProvider.BeginScope(tenant);
            await _next(httpContext);

            return;
        }
        else if (tenant.ToString().ToLower() == "school5.dmmmg.org")
        {
            using var scope = tenantProvider.BeginScope(tenant);
            await _next(httpContext);

            return;

        }
        else if (tenant.ToString().ToLower() == "site6.dmmmg.org")
        {
            using var scope = tenantProvider.BeginScope(tenant);
            await _next(httpContext);

            return;

        }


        await _next(httpContext);
    }
}

/// <summary>
/// Extension method used to add the middleware to the HTTP request pipeline.
/// </summary>
public static class TenantScopeMiddlewareExtensions
{
    public static IApplicationBuilder UseTenantScopeMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<TenantScopeMiddleware>();
    }
}
