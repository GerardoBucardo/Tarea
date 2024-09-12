var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var products = new List<Product>();

app.MapGet("/products", () =>
{
    return products;
});

app.MapGet("/products/{id}", (int id) =>
{
    var product = products.FirstOrDefault(c => c.Id == id);
    return product;
});

app.MapPost("/products", (Product product) =>
{
    products.Add(product);
    return Results.Ok();
});

app.MapPost("/products/{id}", (int id, Product product) =>
{
    var existingClient = products.FirstOrDefault(c => c.Id == id);
    if(existingClient != null)
    {
        existingClient.ProductName = product.ProductName;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("/products/{id}", (int id) =>
{
    var existingClient = products.FirstOrDefault(c => c.Id == id);
    if(existingClient != null)
    {
        products.Remove(existingClient);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.Run();

internal class Product
{
    public int Id { get; set; }
    public string ProductName { get; set; }
}