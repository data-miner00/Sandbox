namespace Sandbox.WeatherApi.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Example async attribute. For order, the lower the number the more front it is in the pipeline.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ScopedAsyncFilterAttribute : Attribute, IAsyncActionFilter, IOrderedFilter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedAsyncFilterAttribute"/> class.
        /// </summary>
        /// <param name="order">The order of execution.</param>
        public ScopedAsyncFilterAttribute(int order = 0)
        {
            this.Order = order;
        }

        public int Order { get; private set; }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await Console.Out.WriteLineAsync($"Before {nameof(ScopedAsyncFilterAttribute)}");
            await next();
            await Console.Out.WriteLineAsync($"After {nameof(ScopedAsyncFilterAttribute)}");
        }
    }
}
