using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;

namespace RestaurantManager.Interface.Components
{
    using Restaurant;

    public partial class RestaurantMenu : ComponentBase
    {
        [Inject]
        private HttpClient Http { get; set; } = default!;

        [Parameter]
        public string RestauranteName { get; set; } = string.Empty;

        public RestaurantResponse? Restaurant { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Restaurant = await GetRestaurant(CancellationToken.None);

            if (Restaurant?.ActiveMenu != null)
            {
                Restaurant.ActiveMenu.Sections = Restaurant.ActiveMenu.Sections.OrderBy(s => s.Order).ToList();

                foreach (var section in Restaurant.ActiveMenu.Sections)
                    section.Dishes = section.Dishes.OrderBy(d => d.Order).ToList();
            }
        }

        private Task<RestaurantResponse?> GetRestaurant(CancellationToken cancellationToken)
            => Http.GetFromJsonAsync<RestaurantResponse>($"restaurants/{RestauranteName}", cancellationToken);
    }
}