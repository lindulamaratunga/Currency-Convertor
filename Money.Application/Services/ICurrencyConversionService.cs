using Money.Application.DTOs;

namespace Money.Application.Services
{
    public interface ICurrencyConversionService
    {
        Task<CurrencyConversionResponseDTO> ConvertCurrencyAsync(CurrencyConversionRequestDTO request);
        Task<CurrencyConversionResponseDTO> GetConversionByIdAsync(int id);
        Task<IEnumerable<CurrencyConversionResponseDTO>> GetConversionByDepartmentIdAsync(int departmentId);
        Task<IEnumerable<CurrencyConversionResponseDTO>> GetConversionByCurrencyCodeAsync(string code);
        Task<IEnumerable<CurrencyConversionResponseDTO>> GetAllAsync();
    }
}
