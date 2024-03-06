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

        [Function("GetAllEvents")]
        public async Task<IActionResult> GetAllEvents(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "GetAllEvents")] HttpRequest req)
        {
            _logger.LogInformation("Received a request to get all events.");

            try
            {
                var events = await _eventService.GetAllEventsAsync();
                return new OkObjectResult(events);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving events: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Function("UpdateEvent")]
        public async Task<IActionResult> UpdateEvent(
            [HttpTrigger(AuthorizationLevel.Function, "put", Route = "Events/{id}")] HttpRequest req,
            int id)
        {
            _logger.LogInformation($"Received a request to update event with ID: {id}");

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var updatedEventInfo = JsonConvert.DeserializeObject<Event>(requestBody);
            if (updatedEventInfo == null)
            {
                return new BadRequestObjectResult("Please pass valid event data in the request body.");
            }

            try
            {
                var updatedEvent = await _eventService.UpdateEventAsync(id, updatedEventInfo);
                if (updatedEvent == null)
                {
                    return new NotFoundResult();
                }
                return new OkObjectResult(updatedEvent);
            }
            catch (JsonException jsonEx)
            {
                _logger.LogError($"JSON Error: {jsonEx.Message}");
                return new BadRequestObjectResult("The provided event data is not in a valid format.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating event: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }

        [Function("DeleteEvent")]
        public async Task<IActionResult> DeleteEvent(
         [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "DeleteEvents/{id}")] HttpRequest req,
         int id)
        {
            _logger.LogInformation($"Received a request to delete event with ID: {id}");

            try
            {
                bool result = await _eventService.DeleteEventAsync(id);
                if (!result)
                {
                    return new NotFoundResult();
                }
                return new OkResult();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting event: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}

    
