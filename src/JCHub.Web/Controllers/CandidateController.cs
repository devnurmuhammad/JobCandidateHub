using JCHub.Application.DTOs;
using JCHub.Application.IServices;
using Microsoft.AspNetCore.Mvc;

namespace JCHub.Web.Controllers;

[Route("[controller]/[action]")]
[ApiController]
public class CandidateController : ControllerBase
{
    private readonly ICandidateService _candidateService;

    public CandidateController(ICandidateService candidateService)
    {
        _candidateService = candidateService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CandidateDto candidateDto)
    {
        await _candidateService.CreateOrUpdate(candidateDto);
        return Ok(new { message = "Candidate created/updated successfully" });
    }
    
    // commented, because according to task guide, there is should be only one endpoint (CreateOrUpdate)
    /*
    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_candidateService.GetAll());
    }

    [HttpGet("{id:int}")]
    public IActionResult Get(int id)
    {
        var candidate = _candidateService.GetById(id);
        if (candidate == null)
            return NotFound($"Candidate not found, id: {id}");
        
        return Ok(candidate);
    }
    */
}