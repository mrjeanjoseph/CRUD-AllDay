using AdventureWorks.ServiceAPI.Models;

namespace AdventureWorks.HumanResources.Services;

public class DepartmentService
{
    private readonly HttpClient _httpClient;
    private readonly ILogger<DepartmentService> _logger;

    public DepartmentService(IHttpClientFactory httpClientFactory, ILogger<DepartmentService> logger)
    {
        _httpClient = httpClientFactory.CreateClient("AdventureWorksAPI");
        _logger = logger;
    }

    public async Task<IEnumerable<DepartmentDTO>> GetAllDepartmentsAsync()
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<DepartmentDTO>>("Department")
                   ?? Enumerable.Empty<DepartmentDTO>();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while fetching all departments.");
            return Enumerable.Empty<DepartmentDTO>();
        }
    }

    public async Task<DepartmentDTO?> GetDepartmentByIdAsync(int id)
    {
        try
        {
            return await _httpClient.GetFromJsonAsync<DepartmentDTO>($"Department/{id}");
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, $"Error occurred while fetching department with ID {id}.");
            return null;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Unexpected error occurred while fetching department with ID {id}.");
            return null;
        }
    }

    public async Task AddDepartmentAsync(DepartmentDTO department)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync("Department", department);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, "Error occurred while adding a new department.");
            throw new InvalidOperationException("Failed to add the department. Please try again later.", ex);
        }
    }

    public async Task UpdateDepartmentAsync(DepartmentDTO department)
    {
        try
        {
            var response = await _httpClient.PutAsJsonAsync($"Department/{department.DepartmentId}", department);
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, $"Error occurred while updating department with ID {department.DepartmentId}.");
            throw new InvalidOperationException("Failed to update the department. Please try again later.", ex);
        }
    }

    public async Task DeleteDepartmentAsync(int id)
    {
        try
        {
            var response = await _httpClient.DeleteAsync($"Department/{id}");
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException ex)
        {
            _logger.LogError(ex, $"Error occurred while deleting department with ID {id}.");
            throw new InvalidOperationException("Failed to delete the department. Please try again later.", ex);
        }
    }
}
