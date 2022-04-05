using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using Application.Dto.Disk;
using Application.Models;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DiskController : ControllerBase
{
    private readonly IDiskService _diskService;

    public DiskController(IDiskService diskService)
    {
        _diskService = diskService;
    }


    [SwaggerOperation(Summary = "Retrieves all disks")]
    [HttpGet("all")]
    public IActionResult Get()
    {
        var disks = _diskService.GetAllDisks();
        return Ok(disks);
    }

    [SwaggerOperation(Summary = "Retrieves filtered disks")]
    [HttpGet]
    public IActionResult Get([FromQuery] Query diskQuery)
    {
        var disks = _diskService.GetDisks(diskQuery);
        return Ok(disks);
    }

    [SwaggerOperation(Summary = "Retrieves a specific disk by unique id")]
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        var disk = _diskService.GetDiskById(id);
        if (disk == null)
        {
            return NotFound();
        }
        return Ok(disk);
    }

    [SwaggerOperation(Summary = "Create a new disk")]
    [HttpPost]
    public IActionResult Create(CreateDiskDto newDisk)
    {
        var disk = _diskService.AddNewDisk(newDisk);
        return Created($"api/disks/{disk.Id}", disk);
    }

    [SwaggerOperation(Summary = "Update an existing disk")]
    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateDiskDto updateDisk)
    {
        _diskService.UpdateDisk(id, updateDisk);
        return NoContent();
    }

    [SwaggerOperation(Summary = "Delete an existing disk")]
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _diskService.DeleteDisk(id);
        return NoContent();
    }
}
