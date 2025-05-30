using Test.Interfaces.Repository;

namespace Test.Middlewares
{
    public class ConnectionManagerMiddleware
    {
        private readonly RequestDelegate _next;

        public ConnectionManagerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext context, IUnitOfWork uow)
        {
            var ct = context.RequestAborted;

            await uow.InitializeAsync(context.RequestAborted);

            try
            {
                await _next(context);
            }
            catch
            {

                uow.RollbackTransaction();
                throw;
            }
            finally
            {

                await uow.DisposeAsync();
            }
        }
    }
}
