using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Bookstore.Domain.Entities.ComicVine;
using Bookstore.Domain.ExternalEntities.ComicEntities;
using Bookstore.Domain.Interfaces;
using Bookstore.Infrastructure.Exceptions;
using Microsoft.Extensions.Logging;

namespace Bookstore.Infrastructure.Services
{
    public class ExternalComicsService : IExternalComicsService
    {
        private readonly HttpClient _client;
        private readonly ILogger<ExternalComicsService> _logger;
        public ExternalComicsService(HttpClient httpClient, ILogger<ExternalComicsService> logger)
        {
            _client = httpClient;
            _logger = logger;
        }

        public async Task<ComicVineResponse> Get(string url, bool totalResults = false)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.UserAgent.ParseAdd("Bookstore");

            HttpResponseMessage response = await _client.SendAsync(request);
            var json = await response.Content.ReadAsStringAsync();

            JsonDocument jsonDocument = JsonDocument.Parse(json);

            if (!response.IsSuccessStatusCode)
                HandleUnsuccessfulRequest(response, jsonDocument);

            int statusCode = jsonDocument.RootElement.GetProperty("status_code").GetInt32();
            if (statusCode != 1)
                HandleUnsuccessfulRequest(response, jsonDocument);

            string resultsJson = jsonDocument.RootElement.GetProperty("results").GetRawText();
            int? total = null;
            if (totalResults && jsonDocument.RootElement.TryGetProperty("number_of_total_results", out var totalProp))
                total = totalProp.GetInt32();

            return new ComicVineResponse
            {
                Results = resultsJson,
                TotalResults = total
            };
        }

        private void HandleUnsuccessfulRequest(HttpResponseMessage response, JsonDocument jsonDocument)
        {
            var errorMessage = "";
            try
            {
                errorMessage = jsonDocument.RootElement.GetProperty("error").GetString();
                _logger.LogError($"Request to API failed: {(int)response.StatusCode} - {response.ReasonPhrase}: {errorMessage}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured with message: {ex.Message}");
            }

            if (response.StatusCode == HttpStatusCode.TooManyRequests)
            {
                throw new RateLimitException();
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedApiAccessException();
            }
            else
            {
                string apiError = string.IsNullOrEmpty(errorMessage) ?
                  "Error occured when sending request to the external API" : errorMessage;
                throw new ApiCommunicationException(apiError);
            }
        }
    }
}
