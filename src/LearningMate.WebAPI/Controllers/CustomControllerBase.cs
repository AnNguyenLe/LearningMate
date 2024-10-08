using Microsoft.AspNetCore.Mvc;

namespace LearningMate.WebAPI.Controllers;

[Route("api/v{version:apiVersion}/[controller]")]
[ApiController]
public class CustomControllerBase : ControllerBase { }
