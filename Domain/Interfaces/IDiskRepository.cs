using Domain.Entities;

namespace Domain.Interfaces;

public interface IDiskRepository
{
    IQueryable<Disk> GetAll();
    Disk GetById(int id);
    Disk Add(Disk disk);
    void Update(Disk disk);
    void Delete(Disk disk);
}
