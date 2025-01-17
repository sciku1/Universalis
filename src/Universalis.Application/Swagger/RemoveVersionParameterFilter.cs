﻿using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace Universalis.Application.Swagger;

public class RemoveVersionParameterFilter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var versionParameter = operation.Parameters.SingleOrDefault(p => p.Name == "version");
        if (versionParameter == null) return;
        operation.Parameters.Remove(versionParameter);
    }
}