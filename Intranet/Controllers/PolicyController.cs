namespace Intranet;

[Route("/api/policy")]
public class PolicyController : BaseController
{
    public IMapper _mapper;
    public PolicyController(IMapper mapper)
    {
        _mapper = mapper;
    }

    [HttpGet]
    [AllowAnonymous]
    public ContentResult Index()
    {
        var html = System.IO.File.ReadAllText(@"./Assets/policy.html");
        return base.Content(html, "text/html");
    }
}
