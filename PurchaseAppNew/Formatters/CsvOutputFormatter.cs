using DataTransfer.DTO.Categories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PurchaseAppNew.Formatters
{
    public class CsvOutputFormatter : TextOutputFormatter
    {
        public CsvOutputFormatter()
        {
            SupportedMediaTypes.Add(MediaTypeHeaderValue.Parse("text/csv"));
            SupportedEncodings.Add(Encoding.UTF8);
            SupportedEncodings.Add(Encoding.Unicode);
        }

        protected override bool CanWriteType(Type type)
        {
            if (typeof(CategoryDto).IsAssignableFrom(type) || typeof(IEnumerable<CategoryDto>).IsAssignableFrom(type))
            {
                return base.CanWriteType(type);
            }
            return false;
        }

        public override async Task WriteResponseBodyAsync(OutputFormatterWriteContext context, Encoding selectedEncoding)
        {
            var response = context.HttpContext.Response;
            var buffer = new StringBuilder();

            if (context.Object is IEnumerable<CategoryDto>)
            {
                foreach (var category in (IEnumerable<CategoryDto>)context.Object)
                {
                    FormatCsv(buffer, category);
                }
            }
            else
            {
                FormatCsv(buffer, (CategoryDto)context.Object);
            }

            await response.WriteAsync(buffer.ToString());
        }

        private static void FormatCsv(StringBuilder buffer, CategoryDto category)
        {
            buffer.AppendLine($"{category.Id},\" {category.CategoryName}\"");
        }
    }
}
