using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Recaffeinate.ApiSecurity
{
    /// <summary>
    /// An authorization filter that closes connections if they are not secure (HTTPS).
    /// Be aware that sensitive information sent by the client WILL be visible!
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, Inherited = true, AllowMultiple = false)]
    public class RequireHttpsOrCloseAttribute : RequireHttpsAttribute
    {
        protected int StatusCode { get; }

        /// <summary>
        /// Return a status result with the given status code when the request does not use HTTPS.
        /// </summary>
        /// <param name="statusCode"></param>
        public RequireHttpsOrCloseAttribute(int statusCode)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Return a 400 Bad Request status code result when the request does not use HTTPS.
        /// </summary>
        public RequireHttpsOrCloseAttribute()
         : this(400)
        {
        }

        /// <summary>
        /// Sets the status result to the appropriate StatusCodeResult specified in the constructor.
        /// The default is 400 Bad Request.
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleNonHttpsRequest(AuthorizationFilterContext filterContext)
        {
            filterContext.Result = new StatusCodeResult(StatusCode);
        }
    }
}
