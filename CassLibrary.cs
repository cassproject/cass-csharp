using System;
using System.IO;
using System.Threading.Tasks;
using Jering.Javascript.NodeJS;
using Microsoft.Extensions.DependencyInjection;

public class CassLibrary
{
    public void ConfigureServices(IServiceCollection services)
    {
        // Add node js
        services.AddNodeJS();
    }

    private readonly INodeJSService _nodeServices;

    private readonly string _scriptFolder;

    public CassLibrary(System.Action<NodeJSProcessOptions> options, string scriptFolder)
    {
        _scriptFolder = scriptFolder;

        var services = new ServiceCollection();
        services.AddNodeJS();

        // Options for the NodeJSProcess, here we enable debugging
        services.Configure<NodeJSProcessOptions>(options);

        // Options for the service that manages the process, here we make its timeout infinite
        services.Configure<OutOfProcessNodeJSServiceOptions>(opt => opt.TimeoutMS = -1);

        ServiceProvider serviceProvider = services.BuildServiceProvider();
        _nodeServices = serviceProvider.GetRequiredService<INodeJSService>();
    }
    public async Task<string> Func(string filename, string func, params object[] args)
    {
        var path = Path.Combine(_scriptFolder, filename);
        var result = await _nodeServices.InvokeFromFileAsync<String>(path, func, args: args);
        return result;
    }
}