using Application.Dto.Disk;
using Application.Models;

namespace Application.Interfaces;

public interface IDiskService
{
    IEnumerable<DiskDto> GetAllDisks();
    PagedResult<DiskDto> GetDisks(Query query);
    DiskDto GetDiskById(int id);
    DiskDto AddNewDisk(CreateDiskDto newMovie);
    void UpdateDisk(int id, UpdateDiskDto disk);
    void DeleteDisk(int id);
}
