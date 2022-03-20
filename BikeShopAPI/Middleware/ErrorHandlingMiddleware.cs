using BikeShopAPI.Exceptions;

namespace BikeShopAPI.Middleware
{
    public class ErrorHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (NotFoundException notFoundException)
            {
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notFoundException.Message);
            }
            catch (OutOfStockException outOfStockException)
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(outOfStockException.Message);
            }
            catch (NullSpecificationException nullException)
            {
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(nullException.Message);
            }
            catch (BadRequestException badRequest)
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync(badRequest.Message);
            }
            catch (ForbidException forbidException)
            {
                context.Response.StatusCode = 403;
            }
            catch (Exception e)
            {
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync(e.Message);
            }
        }
    }
}
