namespace Sandbox.WeatherApi.Filters
{
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// Another testing filter attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ScopedFilterAttribute : Attribute, IActionFilter
    {
        private readonly string injectedValue;

        /// <summary>
        /// Initializes a new instance of the <see cref="ScopedFilterAttribute"/> class.
        /// </summary>
        /// <param name="injectedValue">The dummy injected value.</param>
        public ScopedFilterAttribute(string injectedValue)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(injectedValue);
            this.injectedValue = injectedValue;
        }

        /// <inheritdoc/>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            Console.WriteLine($"Before: {this.injectedValue}");
        }

        /// <inheritdoc/>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"After: {this.injectedValue}");
        }
    }
}
