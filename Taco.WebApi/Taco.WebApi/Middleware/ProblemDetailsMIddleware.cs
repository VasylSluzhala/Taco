using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Serilog;
using System;
using System.Data.SqlClient;
using System.IO;
using Taco.Core.Exceptions;

namespace Taco.WebApi.Middleware
{
    public class ProblemDetailsMIddleware
    {
        internal static void ConfigureProblemDetailsOptions(ProblemDetailsOptions pdOptions, IWebHostEnvironment currentEnvironment)
        {
            pdOptions.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);
            pdOptions.MapToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
            pdOptions.MapToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
            pdOptions.MapToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
            pdOptions.MapToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
            pdOptions.MapToStatusCode<UnauthorizedAccessException>(StatusCodes.Status403Forbidden);

            pdOptions.MapToStatusCode<EntityNotFoundException>(StatusCodes.Status404NotFound);

            pdOptions.MapToStatusCode<InvalidDataException>(StatusCodes.Status409Conflict);

            pdOptions.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);

            pdOptions.MapToStatusCode<SqlException>(StatusCodes.Status503ServiceUnavailable);

            pdOptions.Rethrow<NotSupportedException>();
            pdOptions.IncludeExceptionDetails = (ctx, ex) => currentEnvironment.IsDevelopment() || currentEnvironment.IsStaging();

            pdOptions.OnBeforeWriteDetails = (context, details) =>
            {
                Log.Error(details.Title, details);
            };
        }
    }
}
