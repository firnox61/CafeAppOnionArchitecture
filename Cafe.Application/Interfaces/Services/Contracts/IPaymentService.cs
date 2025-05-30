using Cafe.Application.DTOs.Payments;
using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IPaymentService
    {
        Task<IDataResult<List<PaymentGetDto>>> GetAllAsync();
        Task<IDataResult<PaymentGetDto?>> GetById(int id);
        Task<IResult> Add(PaymentCreateDto paymentCreateDto);
        Task<IResult> Update(Payment payment);
        Task<IResult> Delete(Payment payment);
    }
}
