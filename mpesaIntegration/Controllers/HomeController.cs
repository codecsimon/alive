using System.Diagnostics;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using mpesaIntegration.Models;

namespace mpesaIntegration.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    private readonly  IHttpClientFactory _clientFactory;
    public HomeController(ILogger<HomeController> logger ,IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _clientFactory = clientFactory;
    }

    public IActionResult Index()
    {
        return View();
    }

    public async Task<string> GetToken()
    {
        //we create a variable client that is going to create 
        //client called mpesa using our our clientFactory

    var client = _clientFactory.CreateClient("mpesa");
    //This string variable contains both consumer key and consumer secret

    var authString ="6pR9SyhAQLowqAi5kI8z5soCcmsfdw2G:2bMs0Dr3RYrMzeeX";        
    //Convert the authstring to base 64DOTNET BUILD
    var encodedString =Convert.ToBase64String(System.Text.ASCIIEncoding.ASCII.GetBytes(authString));
    //url variable that contains the remaining url
    
    var _url="/oauth/v1/generate?grant_type=client_credentials";

    //we are going to create a request header

    var request = new HttpRequestMessage(HttpMethod.Get ,_url);

    request.Headers.Add("Authorization",$"Basic {encodedString}");

    var response = await client.SendAsync(request);

    var mpesaResponse = await response.Content.ReadAsStringAsync();

    return mpesaResponse;

    }
    public IActionResult Privacy()
    {
        return View();
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
