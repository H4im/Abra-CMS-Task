using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Core.Models.PublishedContent;

namespace AbraCmsTask.Models;

public class HomePageViewModel : ContentModel
{
    public HomePageViewModel(IPublishedContent? content) : base(content) { }

    public IEnumerable<IPublishedContent> SearchResults { get; set; } = Enumerable.Empty<IPublishedContent>();
    public string? Query { get; set; }
    public string? SelectedCategory { get; set; }
}