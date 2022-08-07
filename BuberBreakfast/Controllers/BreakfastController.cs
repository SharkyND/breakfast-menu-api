using BuberBreakfast.Contracts.Breakfast;
using BuberBreakfast.Models;
using BuberBreakfast.Services.Breakfasts;
using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;
[ApiController]
[Route("[controller]")]
public class BreakfastController : ControllerBase
{
  // Injection
  private readonly IBreakfastService _breakfastService;
  private readonly ILogger<BreakfastController> _logger;
  public BreakfastController(IBreakfastService breakfastService, ILogger<BreakfastController> logger)
  {
    _breakfastService = breakfastService;
    _logger = logger;
  }

  [HttpPost]
  public IActionResult CreateBreakfast(CreateBreakfastRequest request)
  {
    var breakfast = new Breakfast
    (
      id: Guid.NewGuid(),
      name: request.Name,
      description: request.Description,
      startDateTime: request.StartDateTime,
      endDateTime: request.EndDateTime,
      lastModifiedDateTime: DateTime.UtcNow,
      savory: request.Savory,
      sweet: request.Sweet
    );

    _breakfastService.CreateBreakfast(breakfast);

    var response = new BreakfastResponse
    (
      breakfast.Id,
      breakfast.Name,
      breakfast.Description,
      breakfast.StartDateTime,
      breakfast.EndDateTime,
      breakfast.LastModifiedDateTime,
      breakfast.Savory,
      breakfast.Sweet
    );
    return CreatedAtAction(
      nameof(GetBreakfasts),
      new { id = breakfast.Id },
      response
    );
  }

  [HttpPut("{id:guid}")]
  public IActionResult UpsertBreakfast(UpsertBreakfastResponse request, Guid id)
  {
    var breakfast = new Breakfast
    (
      id: request.Id,
      name: request.Name,
      description: request.Description,
      startDateTime: request.StartDateTime,
      endDateTime: request.EndDateTime,
      lastModifiedDateTime: DateTime.UtcNow,
      savory: request.Savory,
      sweet: request.Sweet
    );

    _breakfastService.UpsertBreakfast(breakfast);

    return NoContent();
  }

  [HttpGet("{id:guid}")]
  public IActionResult GetBreakfasts(Guid id)
  {
    _logger.LogInformation("Getting breakfast information");

    Breakfast breakfast = _breakfastService.GetBreakfast(id);
    var response = new BreakfastResponse(
      breakfast.Id,
      breakfast.Name,
      breakfast.Description,
      breakfast.StartDateTime,
      breakfast.EndDateTime,
      breakfast.LastModifiedDateTime,
      breakfast.Savory,
      breakfast.Sweet);

    return Ok(response);
  }
  [HttpDelete("{id:guid}")]
  public IActionResult DeleteBreakfasts(Guid id)
  {
    _breakfastService.DeleteBreakfast(id);
    return Ok(id);
  }
}