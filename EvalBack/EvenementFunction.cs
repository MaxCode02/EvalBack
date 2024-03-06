using EvalBack.Entity;
using EvalBack.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EvalBack
{
    public class EvenementFunction
    {
        private readonly ILogger<EvenementFunction> _logger;
        private readonly IEventService _eventService;

        public EvenementFunction(ILogger<EvenementFunction> logger, IEventService eventService)
        {
            _logger = logger;
            _eventService = eventService;
        }

        [Function("AddEvent")]
        public async Task<IActionResult> Run(
    [HttpTrigger(AuthorizationLevel.Function, "post", Route = "Add")] HttpRequest req)
        {
            _logger.LogInformation("Received a request to add an event.");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var inputEvent = JsonConvert.DeserializeObject<Event>(requestBody);
                if (inputEvent == null)
                {
                    return new BadRequestObjectResult("Please pass a valid event in the request body.");
                }

                try
                {
                    var createdEvent = await _eventService.CreateEventAsync(inputEvent);
                    return new OkObjectResult(createdEvent);
                }
                catch (Exception ex)
                {

                    _logger.LogError($"Error creating event: {ex.Message}");
                    return new StatusCodeResult(StatusCodes.Status500InternalServerError);
                }
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError($"JSON Error: {jsonEx.Message}");
                return new BadRequestObjectResult("The provided event data is not in a valid format.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unhandled exception: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

    }
}