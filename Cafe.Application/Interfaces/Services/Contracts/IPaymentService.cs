using Cafe.Application.DTOs.Payments;
using Cafe.Domain.Entities;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IPaymentService
    {
        Task<IDataResult<List<PaymentGetDto>>> GetAllAsync();
        Task<IDataResult<PaymentGetDto?>> GetById(int id);
        Task<IResult> Add(PaymentCreateDto paymentCreateDto);
        Task<IResult> Update(PaymentUpdateDto paymentUpdateDto);
        Task<IResult> Delete(int id);
    }
}
