using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace ASP_Library.route_constraits
{
    public class IdRangeConstraint : IRouteConstraint
    {
        private readonly long _minValue;
        private readonly long _maxValue;

        public IdRangeConstraint(long minValue, long maxValue)
        {
            this._minValue = minValue;
            this._maxValue = maxValue;
        }

        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values.TryGetValue(routeKey, out var routeValue))
            {
                string value = routeValue.ToString();

                if (!string.IsNullOrEmpty(value) && long.TryParse(value, out long id)) 
                {
                    return id >= _minValue && id <= _maxValue;
                }
            }

            return false;
        }
    }
}
