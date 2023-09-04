using MediatR;
using UnitOfWork.Application;
using UnitOfWork.Application.Commands.Product;
using UnitOfWork.Application.Queries.Product;
using UnitOfWork.Domain.Dtos.Product;
using UnitOfWork.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

{
    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services
        .AddApplication()
        .AddInfrastructure();
}

var app = builder.Build();

{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();
}

try
{
    await UnitOfWork.Infrastructure.Repositories.UnitOfWork.MigrateAsync(app.Configuration);
}
catch (Exception ex)
{
    throw new Exception($"Could not create database. Reason: {ex.Message}");
}

app.MapGet("/api/products", async (ISender mediator) =>
{
    var products = await mediator.Send(new GetAllProductsQuery());

    return Results.Ok(products);
});

app.MapGet("/api/products/{id:guid}", async (Guid id, ISender mediator) =>
{
    var product = await mediator.Send(new GetProductByIdQuery(id));

    return product is null 
    ? Results.NotFound() 
    : Results.Ok(product);
});

app.MapPost("/api/products", async (CreateProductDto dto, ISender mediator) =>
{
    var product = await mediator.Send(new CreateProductCommand(dto.Name, dto.Description));

    return Results.Created("/api/products", product);
});

app.MapPatch("/api/products/{id:guid}", async (Guid id, UpdateProductDto dto, ISender mediator) =>
{
    var product = await mediator.Send(new UpdateProductCommand(id, dto));

    return Results.Ok(product);
});

app.MapDelete("/api/products/{id:guid}", async (Guid id, ISender mediator) =>
{
    await mediator.Send(new DeleteProductCommand(id));

    return Results.NoContent();
});

app.Run();
