using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace RestaurantManager.Interface.Components
{
    using Restaurants;

    public partial class RestaurantMenu : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; } = default!;

        [Parameter]
        public string RestauranteName { get; set; } = string.Empty;

        public RestaurantActiveMenuGetResponse? Restaurant { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Restaurant = await GetRestaurantActiveMenu(CancellationToken.None);

            if (Restaurant?.ActiveMenu != null)
            {
                Restaurant.ActiveMenu.Sections = Restaurant.ActiveMenu.Sections.OrderBy(s => s.Order).ToArray();

                foreach (var section in Restaurant.ActiveMenu.Sections)
                    section.Dishes = section.Dishes.OrderBy(d => d.Order).ToList();
            }
        }

        private Task<RestaurantActiveMenuGetResponse?> GetRestaurantActiveMenu(CancellationToken cancellationToken)
            => Http.GetFromJsonAsync<RestaurantActiveMenuGetResponse>($"restaurants/{RestauranteName}/active-menu", cancellationToken);
    }
}