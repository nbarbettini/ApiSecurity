using System;
using System.Collections.Generic;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;

using Recaffeinate.ApiSecurity;

using Xunit;

namespace ApiSecurity.Tests
{
    public class RequireHttpsOrCloseAttributeTests
    {
        [Fact]
        public void When_no_status_code_is_provided_the_result_matches_bad_request()
        {
            var attribute = new RequireHttpsOrCloseAttribute();

            var filterContext = new AuthorizationFilterContext(new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()), new List<IFilterMetadata>());

            attribute.OnAuthorization(filterContext);

            var result = filterContext.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [InlineData(426), InlineData(505)]
        public void When_a_status_code_is_provided_the_result_matches(int statusCode)
        {
            var attribute = new RequireHttpsOrCloseAttribute(statusCode);

            var filterContext = new AuthorizationFilterContext(new ActionContext(new DefaultHttpContext(), new RouteData(), new ActionDescriptor()), new List<IFilterMetadata>());

            attribute.OnAuthorization(filterContext);

            var result = filterContext.Result as StatusCodeResult;

            Assert.NotNull(result);
            Assert.Equal(statusCode, result.StatusCode);
        }
    }
}
