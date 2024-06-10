using AdoptPets.Application.Features.Announcements.Commands.CreateAnnouncement;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace AdoptPets.API.Utility
{
    public class AddFileUploadParams : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var formFileParams = context.MethodInfo.GetParameters()
                 .Where(p => p.ParameterType == typeof(IFormFile))
                 .ToList();

             if (formFileParams.Any())
             {
                 operation.Parameters.Clear();

                 // Add parameters for form file upload
                 foreach (var param in formFileParams)
                 {
                     operation.Parameters.Add(new OpenApiParameter
                     {
                         Name = param.Name,
                         In = ParameterLocation.Query,
                         Description = "Upload File",
                         Required = true,
                         Schema = new OpenApiSchema
                         {
                             Type = "string",
                             Format = "binary"
                         }
                     });
                 }

                 // Add parameters for JSON data
                 operation.RequestBody = new OpenApiRequestBody
                 {
                     Content = new Dictionary<string, OpenApiMediaType>
                     {
                         ["application/json"] = new OpenApiMediaType // Use "application/json" for JSON data
                         {
                             Schema = context.SchemaGenerator.GenerateSchema(typeof(AnnouncementData), context.SchemaRepository) // Generate schema for AnnouncementData
                         }
                     }
                 };
             }
         
        }
    }
}
