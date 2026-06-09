using InternshipPractice.Application.Interfaces.HttpClients;
using InternshipPractice.Domain.Dto;
using InternshipPractice.Infrastructure.Options;
using KDS.Primitives.FluentResult;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Text;

namespace InternshipPractice.Infrastructure.HttpClients;

public class AccessHttpClient:IAccessHttpClient
{
    private readonly HttpClient _httpClient;
    private readonly AccessOptions _options;
    private readonly ILogger<AccessHttpClient> _logger;

    public AccessHttpClient(HttpClient httpClient, IOptions<AccessOptions> options, ILogger<AccessHttpClient> logger)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _logger = logger;
    }

    public async Task<Result<LoginResponseDto>> Login(string email, string password)
    {
        try
        {
            var url = $"{_options.BaseUrl}{_options.Login}";
            var json = JsonSerializer.Serialize( new LoginDto
            {  Email = email,
               Password = password
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("Ошибка [{StatusCode}] при запросе на вход.", response.StatusCode);
                return Result.Failure<LoginResponseDto>(new Error("BadRequest", "Не удалось войти."));
            }
            var jsonReturn = await response.Content.ReadAsStringAsync();
            //*
            var result = JsonSerializer.Deserialize<LoginResponseDto>(jsonReturn, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            //*
            return Result.Success(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при входе.");
            return Result.Failure<LoginResponseDto>(new Error("InternalServerError", ex.Message));
        }
    }
}
