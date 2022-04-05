using Application.Dto.Disk;
using Application.Interfaces;
using Application.Models;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using System.Linq.Expressions;

namespace Application.Services;

public class DiskService : IDiskService
{
    private readonly IMapper _mapper;
    private readonly IDiskRepository _diskRepository;

    public DiskService(IDiskRepository diskRepository, IMapper mapper)
    {
        _diskRepository = diskRepository;
        _mapper = mapper;
    }

    public IEnumerable<DiskDto> GetAllDisks()
    {
        var disks = _diskRepository.GetAll();
        return _mapper.Map<IEnumerable<DiskDto>>(disks);
    }

    public PagedResult<DiskDto> GetDisks(Query query)
    {
        var baseQuery = _diskRepository.GetAll()
            .Where(m => query.SearchPhrase == null || m.Name.ToLower().Contains(query.SearchPhrase.ToLower()));

        if (!string.IsNullOrEmpty(query.SortBy))
        {
            var columnsSelector = new Dictionary<string, Expression<Func<Disk, object>>>
            {
                {nameof(Disk.Name), r => r.Name }
            };

            var selectedColumn = columnsSelector[query.SortBy];

            baseQuery = query.SortDirection == SortDirection.ASC
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }

        var disks = baseQuery
            .Skip(query.PageSize * (query.PageNumber - 1))
            .Take(query.PageSize);

        var totalItemsCount = baseQuery.Count();

        var disksDtos = _mapper.Map<List<DiskDto>>(disks);

        var result = new PagedResult<DiskDto>(disksDtos, totalItemsCount, query.PageSize, query.PageNumber);

        return result;
    }

    public IEnumerable<DiskDto> SearchByKeyword(string keyword)
    {
        var movies = _diskRepository.GetAll()
            .Where(x => x.Name.Contains(keyword));
        return _mapper.Map<IEnumerable<DiskDto>>(movies);
    }

    public DiskDto GetDiskById(int id)
    {
        var disk = _diskRepository.GetById(id);
        return _mapper.Map<DiskDto>(disk);
    }

    public DiskDto AddNewDisk(CreateDiskDto newDisk)
    {
        if (string.IsNullOrEmpty(newDisk.Name))
        {
            throw new Exception("Movie can not have an empty code");
        }

        var diskWithSameName = _diskRepository.GetAll().SingleOrDefault(x => x.Name == newDisk.Name);
        if(diskWithSameName != null)
        {
            throw new Exception("Disk with the same name already exist.");
        }

        var disk = _mapper.Map<Disk>(newDisk);
        _diskRepository.Add(disk);
        return _mapper.Map<DiskDto>(disk);
    }

    public void UpdateDisk(int id, UpdateDiskDto disk)
    {
        if (string.IsNullOrEmpty(disk.Name))
        {
            throw new Exception("Movie can not have an empty name");
        }

        var diskWithSameName = _diskRepository.GetAll().SingleOrDefault(x => x.Name == disk.Name);
        if (diskWithSameName != null && diskWithSameName.Id != id)
        {
            throw new Exception("Disk with the same name already exist.");
        }

        var existingDisk = _diskRepository.GetById(id);
        var updatedDisk = _mapper.Map(disk, existingDisk);

        _diskRepository.Update(updatedDisk);
    }

    public void DeleteDisk(int id)
    {
        var disk = _diskRepository.GetById(id);
        _diskRepository.Delete(disk);
    }
}
