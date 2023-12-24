using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NLayer.Core;
using NLayer.Core.DTOs;
using NLayer.Core.Repositories;
using NLayer.Core.UnitOfWorks;

namespace NLayer.Service.Services;

public class ServiceWithDto<Entity,Dto> : IServiceWithDto<Entity,Dto> where Entity : BaseEntity where Dto:class
{
    private readonly IGenericRepository<Entity> _genericRepository;
    protected readonly IUnitOfWork _unitOfWork;
    protected readonly IMapper _mapper;

    public ServiceWithDto(IGenericRepository<Entity> genericRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _genericRepository = genericRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<CustomResponseDto<Dto>> GetByIdAsync(int id)
    {
        var entity = await _genericRepository.GetByIdAsync(id);
        var dto = _mapper.Map<Dto>(entity);
        return CustomResponseDto<Dto>.Success(200,dto);
    }

    public async Task<CustomResponseDto<IEnumerable<Dto>>> GetAllAsync()
    {
        var entities = _genericRepository.GetAll().ToListAsync();
        var dtos = _mapper.Map<IEnumerable<Dto>>(entities);
        return CustomResponseDto<IEnumerable<Dto>>.Success(200,dtos);
    }

    public async Task<CustomResponseDto<IEnumerable<Dto>>> Where(Expression<Func<Entity, bool>> expression)
    {
        var entites = await _genericRepository.Where(expression).ToListAsync();
        var dtos = _mapper.Map<IEnumerable<Dto>>(entites);
        return CustomResponseDto<IEnumerable<Dto>>.Success(200,dtos);
    }

    public async Task<CustomResponseDto<bool>> AnyAsync(Expression<Func<Entity, bool>> expression)
    {
        var hasResult = await _genericRepository.AnyAsync(expression);
        return CustomResponseDto<bool>.Success(200,hasResult);
    }

    public async Task<CustomResponseDto<Dto>> AddAsync(Dto dto)
    {
        var newEntity = _mapper.Map<Entity>(dto);
        await _genericRepository.AddAsync(newEntity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<Dto>.Success(200,dto);
    }

    public async Task<CustomResponseDto<IEnumerable<Dto>>> AddRangeAsync(IEnumerable<Dto> dtos)
    {
        var newEntites = _mapper.Map<IEnumerable<Entity>>(dtos);
        await _genericRepository.AddRangeAsync(newEntites);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<IEnumerable<Dto>>.Success(200,dtos);
    }

    public async Task<CustomResponseDto<NoContentDto>> UpdateAsync(Dto dto)
    {
        var entity = _mapper.Map<Entity>(dto);
        _genericRepository.Update(entity);
        _unitOfWork.CommitAsync();
        return CustomResponseDto<NoContentDto>.Success(200,new NoContentDto());
    }

    public async Task<CustomResponseDto<NoContentDto>> RemoveAsync(int id)
    {
        var entity = await _genericRepository.GetByIdAsync(id);
        _genericRepository.Remove(entity);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<NoContentDto>.Success(200,new NoContentDto());
    }

    public async Task<CustomResponseDto<NoContentDto>> RemoveRangeAsync(IEnumerable<int> ids)
    {
        var entites = _genericRepository.Where(x => ids.Contains(x.Id)).ToList();
        _genericRepository.RemoveRange(entites);
        await _unitOfWork.CommitAsync();
        return CustomResponseDto<NoContentDto>.Success(200,new NoContentDto());
    }
}