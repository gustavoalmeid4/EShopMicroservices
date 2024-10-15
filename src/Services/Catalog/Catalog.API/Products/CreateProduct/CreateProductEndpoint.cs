namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductRequest(Guid Id, string Name, string Description, string ImageFile, decimal Price, List<string> Category);
    
    public record CreateProductResponse(Guid id);
    public class CreateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products",
                async (CreateProductResponse request, ISender sender) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.id}", response);

            })
                .WithName("Create product")
                .Produces<CreateProductResponse>(StatusCodes.Status201Created)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .WithSummary("CreateProduct")
                .WithDescription("CreateProduct");

        }
    }
}
