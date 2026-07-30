using System.Text.Json;

namespace TaskManagement.API.DTOModels.Common
{
    /// <summary>
    /// Represents standard error response details returned by API endpoints during exceptions or failure states.
    /// </summary>
    public class ErrorDetails
    {
        /// <summary>
        /// Gets or sets the HTTP status code associated with the error.
        /// </summary>
        public int StatusCode { get; set; }

        /// <summary>
        /// Gets or sets a brief summary message describing the error.
        /// </summary>
        public string Message { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets additional detailed diagnostic or stack trace information when available.
        /// </summary>
        public string? Detailed { get; set; }

        /// <summary>
        /// Serializes the current <see cref="ErrorDetails"/> instance into a JSON string representation.
        /// </summary>
        /// <returns>A JSON-formatted string representing the error details.</returns>
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}