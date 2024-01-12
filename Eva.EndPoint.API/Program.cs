using Eva.EndPoint.API.Extensions;
var builder = WebApplication.CreateBuilder(args);
builder.AddEva(out WebApplication app);
app.Run();