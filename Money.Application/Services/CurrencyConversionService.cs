using AutoMapper;
using Microsoft.Extensions.Logging;
using Money.Application.DTOs;
using Money.Application.Factories;
using Money.Domain.Interfaces;

namespace Money.Application.Services
{
    public class CurrencyConversionService : ICurrencyConversionService
    {
        private readonly ICurrencyConverterFactory _currencyConverterFactory;
        private readonly IMapper _mapper;
        private readonly ILogger<CurrencyConversionService> _logger;
        private readonly ICurrencyConversionRepository _currencyConversionRepository;

        public CurrencyConversionService(
            ICurrencyConverterFactory currencyConverterFactory,
            ICurrencyConversionRepository _currencyConversionRepositor,
            IMapper mapper,
            ILogger<CurrencyConversionService> logger)
        {
            _currencyConverterFactory = currencyConverterFactory;
            _currencyConversionRepository = _currencyConversionRepositor;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CurrencyConversionResponseDTO> ConvertCurrencyAsync(CurrencyConversionRequestDTO request)
        {
            try
            {
                _logger.LogInformation("Processing currency conversion request");

                var conversion = await _currencyConverterFactory.ConvertAsync(

                    request.FromCurrency,
                    request.ToCurrency,
                    request.Amount,
                    request.DepartmentId);

                return _mapper.Map<CurrencyConversionResponseDTO>(conversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing currency conversion request");
                throw;
            }
        }

        public async Task<CurrencyConversionResponseDTO> GetConversionByIdAsync(int id)
        {
            try
            {
                _logger.LogInformation("Getting conversion by ID: {Id}", id);

                var savedConversion = await _currencyConversionRepository.GetByIdAsync(id);

                return _mapper.Map<CurrencyConversionResponseDTO>(savedConversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversion by ID: {Id}", id);
                throw;
            }
        }

        public async Task<IEnumerable<CurrencyConversionResponseDTO>> GetConversionByDepartmentIdAsync(int departmentId)
        {
            try
            {
                _logger.LogInformation("Getting conversion history by department ID: {DepartmentId}", departmentId);

                var savedConversion = await _currencyConversionRepository.GetByDepartmentIdAsync(departmentId);

                return _mapper.Map<IEnumerable<CurrencyConversionResponseDTO>>(savedConversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversion by department ID: {DepartmentId}", departmentId);
                throw;
            }
        }

        public async Task<IEnumerable<CurrencyConversionResponseDTO>> GetConversionByCurrencyCodeAsync(string code)
        {
            try
            {
                _logger.LogInformation("Getting conversion history by currency code: {CurrencyCode}", code);

                var savedConversion = await _currencyConversionRepository.GetByCurrencyCode(code);

                return _mapper.Map<IEnumerable<CurrencyConversionResponseDTO>>(savedConversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting conversion by currency code: {CurrencyCode}", code);
                throw;
            }
        }

        public async Task<IEnumerable<CurrencyConversionResponseDTO>> GetAllAsync()
        {
            try
            {
                _logger.LogInformation("Getting all conversion history");

                var savedConversion = await _currencyConversionRepository.GetAllAsync();

                return _mapper.Map<IEnumerable<CurrencyConversionResponseDTO>>(savedConversion);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all conversion history");
                throw;
            }
        }
    }
}
