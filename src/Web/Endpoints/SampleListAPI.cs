using SampleProject.Application.SampleListAPI.Commands.CreateSampleList;
using SampleProject.Application.TodoItems.Commands.CreateTodoItem;
using SampleProject.Application.WeatherForecasts.Queries.GetWeatherForecasts;

namespace SampleProject.Web.Endpoints;

public class SampleListAPI : EndpointGroupBase
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .MapPost(CreateSampleApi);
    }

    public Task<int> CreateSampleApi(ISender sender, CreateSampleList command)
    {
        return sender.Send(command);
    }
}
