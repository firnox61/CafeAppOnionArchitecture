using AutoMapper;
using Cafe.Application.DTOs.Orders;
using Cafe.Application.DTOs.Tables;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Core.Utilities.Results;
using Cafe.Application.Validators.Tables;
using Cafe.Core.Aspects.Validation;

namespace Cafe.Application.Services.Managers
{

    public class TableManager : ITableService
    {
        private readonly ITableDal _tableDal;
        private readonly IMapper _mapper;

        public TableManager(ITableDal tableDal, IMapper mapper)
        {
            _tableDal = tableDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(TableCreateDtoValidator))]
        public async Task<IResult> Add(TableCreateDto tableCreateDto)
        {
            var newTable = _mapper.Map<Table>(tableCreateDto);
            await _tableDal.AddAsync(newTable);
            return new SuccessResult();
        }

        public async Task<IResult> Delete(int id)
        {
            var result = await _tableDal.GetWithOrdersByIdAsync(id); // Include'lu çağır
            if (result == null)
                return new ErrorResult("Masa bulunamadı.");

            // sadece unpaid siparişleri filtrele
            result.Orders = result.Orders
                .Where(o => !o.IsPaid)
                .ToList();

            var dto = _mapper.Map<TableGetDto>(result);

            if (dto.ActiveOrders.Any())
                return new ErrorResult("Masada aktif (ödenmemiş) sipariş var, silinemez.");

            await _tableDal.DeleteAsync(result);
            return new SuccessResult("Masa silindi.");
        }
        // [CacheAspect]
        public async Task<IDataResult<List<TableGetDto>>> GetAllAsync()
        {
            var tables = await _tableDal.GetAllWithOrdersAsync();

            var dtos = tables.Select(table => new TableGetDto
            {
                Id = table.Id,
                Name = table.Name,
                ActiveOrders = table.Orders
                    .Where(o => !o.IsPaid)
                    .Select(o => new OrderSummaryDto
                    {
                        OrderId = o.Id,
                        CreatedAt = o.CreatedAt,
                        TotalAmount = o.OrderItems.Sum(i => i.Quantity * i.UnitPrice)
                    }).ToList()
            }).ToList();

            return new SuccessDataResult<List<TableGetDto>>(dtos);
        }


        public async Task<IDataResult<TableGetDto?>> GetById(int id)
        {
            var table = await _tableDal.GetWithOrdersByIdAsync(id);

            if (table == null)
                return new ErrorDataResult<TableGetDto?>("Masa bulunamadı.");

            // ❗ Sadece ödenmemiş siparişleri ayıkla
            table.Orders = table.Orders
                .Where(o => !o.IsPaid)
                .ToList();

            // ✅ AutoMapper ile eşleştir
            var dto = _mapper.Map<TableGetDto>(table);

            return new SuccessDataResult<TableGetDto?>(dto);
        }
        [ValidationAspect(typeof(TableUpdateDtoValidator))]
        public async Task<IResult> Update(TableUpdateDto tableUpdateDto)
        {
            var existtTable = await _tableDal.GetAsync(t => t.Id == tableUpdateDto.Id);
            if (existtTable == null)
            {
                return new ErrorResult("Masa bulunamadı");
            }

            existtTable.Name = tableUpdateDto.Name;

            await _tableDal.UpdateAsync(existtTable);
            return new SuccessResult();
        }
        public async Task<IDataResult<List<TableGetDto>>> SearchTablesAsync(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return new ErrorDataResult<List<TableGetDto>>("Arama terimi boş olamaz.");

            var tables = await _tableDal.GetAllWithOrdersAsync();
            var filtered = tables
                .Where(t => t.Name.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                .Select(t => _mapper.Map<TableGetDto>(t))
                .ToList();

            return new SuccessDataResult<List<TableGetDto>>(filtered);
        }
        public async Task<IDataResult<List<TableGetDto>>> GetEmptyTablesAsync()
        {
            var tables = await _tableDal.GetAllWithOrdersAsync();
            var emptyTables = tables
                .Where(t => t.Orders.All(o => o.IsPaid))
                .Select(t => _mapper.Map<TableGetDto>(t))
                .ToList();

            return new SuccessDataResult<List<TableGetDto>>(emptyTables);
        }

    }
}
