namespace Sandbox.WeatherApi.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Dummy filter just for learning.
    /// </summary>
    public class GlobalFilter : IActionFilter
    {
        /// <inheritdoc/>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Before {nameof(GlobalFilter)}");
        }

        /// <inheritdoc/>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"After {nameof(GlobalFilter)}");
        }
    }
}
