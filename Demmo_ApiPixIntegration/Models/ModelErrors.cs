using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace Demmo_ApiPixIntegration.Models
{
    /// <summary>
    /// Usado para padronizar o retorno de erros de validação.
    /// </summary>
    public class ModelErrors
    {
        /// <summary>
        /// Cria uma nova instância de ModelErrors.
        /// Usado para padronizar o retorno de erros de validação.
        /// </summary>
        /// <param name="modelState">ModelState contendo erros de validação.</param>
        public ModelErrors(ModelStateDictionary modelState)
        {
            Status = 400;
            Title = "One or more validation errors occurred.";
            Errors = modelState.ToDictionary(k => CamelCase(k.Key), v => v.Value.Errors.Select(e => e.ErrorMessage));
        }

        /// <summary>
        /// Status code.
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// Error title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// List of errors.
        /// </summary>
        public IDictionary<string, IEnumerable<string>> Errors { get; set; }

        private static string CamelCase(string str)
        {
            var pattern = new Regex(@"[A-Z]{2,}(?=[A-Z][a-z]+[0-9]*|\b)|[A-Z]?[a-z]+[0-9]*|[A-Z]|[0-9]+");
            return new string(
              new CultureInfo("en-US", false)
                .TextInfo
                .ToTitleCase(
                  string.Join(" ", pattern.Matches(str)).ToLower()
                )
                .Replace(@" ", "")
                .Select((x, i) => i == 0 ? char.ToLower(x) : x)
                .ToArray()
            );
        }
    }
}
