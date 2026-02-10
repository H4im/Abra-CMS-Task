using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Web.Common.Controllers;
using AbraCmsTask.Models;

namespace AbraCmsTask.Controllers;

public class HomeController : RenderController
{
    public HomeController(
        ILogger<HomeController> logger, 
        ICompositeViewEngine compositeViewEngine, 
        IUmbracoContextAccessor umbracoContextAccessor)
        : base(logger, compositeViewEngine, umbracoContextAccessor)
    {
    }

public override IActionResult Index()
{
    var model = new HomePageViewModel(CurrentPage);
    model.Query = Request.Query["query"];
    model.SelectedCategory = Request.Query["category"];

    var articles = CurrentPage.Children().Where(x => x.ContentType.Alias == "article");

    if (!string.IsNullOrEmpty(model.SelectedCategory))
    {
        articles = articles.Where(x => 
            string.Equals(x.Value<string>("category"), model.SelectedCategory, StringComparison.OrdinalIgnoreCase));
    }

    if (!string.IsNullOrEmpty(model.Query))
    {
        articles = articles.Where(x => x.Name.Contains(model.Query, StringComparison.OrdinalIgnoreCase));
    }

    if (!string.IsNullOrEmpty(model.SelectedCategory))
    {
        articles = articles.Where(x => x.Value<string>("category") == model.SelectedCategory);
    }

    model.SearchResults = articles.ToList();
    return CurrentTemplate(model);
}
}