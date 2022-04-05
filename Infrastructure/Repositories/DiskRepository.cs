namespace Infrastructure.Repositories;

public class DiskRepository : IDiskRepository
{
    private readonly MyMoviesContext _context;

    public DiskRepository(MyMoviesContext context)
    {
        _context = context;
    }
    public IQueryable<Disk> GetAll()
    {
        return _context.Disks;
    }

    public Disk GetById(int id)
    {
        return _context.Disks.SingleOrDefault(x => x.Id == id);
    }

    public Disk Add(Disk disk)
    {
        _context.Disks.Add(disk);
        _context.SaveChanges();
        return disk;
    }
    public void Update(Disk disk)
    {
        _context.Disks.Update(disk);
        _context.SaveChanges();
    }

    public void Delete(Disk disk)
    {
        _context.Disks.Remove(disk);
        _context.SaveChanges();
    }
}
