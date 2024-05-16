using Newtonsoft.Json;
using PaySpace.Calculator.Web.Services.Exceptions;
using PaySpace.Calculator.Web.Services.Helpers;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PaySpace.Calculator.Web.Services
{
    public class CalculatorHttpService(PaySpaceService paySpaceService) : ICalculatorHttpService
    {
        /// <inheritdoc/>
        public async Task<PagedList<PostalCode>> GetPostalCodesAsync()
        {
            var response = await paySpaceService.client.GetAsync("api/postalcode");
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException($"Cannot fetch postal codes, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<PagedList<PostalCode>>() ?? new PagedList<PostalCode>();
        }

        /// <inheritdoc/>
        public async Task<PagedList<CalculatorHistory>> GetHistoryAsync(QueryRequest request, CancellationToken cancellationToken)
        {
            var response = await paySpaceService.client.GetAsync($"api/Calculator/history?{request}", cancellationToken);
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException($"Cannot fetch history, status code: {response.StatusCode}");
            }

            return await response.Content.ReadFromJsonAsync<PagedList<CalculatorHistory>>(cancellationToken) ?? new PagedList<CalculatorHistory>();
        }

        /// <inheritdoc/>
        public async Task<CalculateResult?> CalculateTaxAsync(CalculateRequest calculationRequest)
        {
            var content = new StringContent(JsonConvert.SerializeObject(calculationRequest), MediaTypeHeaderValue.Parse("application/json"));
            var response = await paySpaceService.client.PostAsync("api/Calculator/calculate-tax", content, CancellationToken.None);
            if (!response.IsSuccessStatusCode)
            {
                throw new NotFoundException($"Cannot calculate tax, status code: {response.StatusCode}");
            }

            return await response!.Content!.ReadFromJsonAsync<CalculateResult>() ?? null;
        }
    }
}