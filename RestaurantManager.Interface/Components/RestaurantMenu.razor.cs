using System.Net.Http.Json;
using Microsoft.AspNetCore.Components;
using RestaurantManager.Interface.DTOs.Response;

namespace RestaurantManager.Interface.Components
{
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

            if (Restaurant != null)
                Restaurant.ActiveMenu.Sections = Restaurant.ActiveMenu.Sections.OrderBy(s => s.Order).ToList();
        }

        private Task<RestaurantResponse?> GetRestaurant(CancellationToken cancellationToken)
            => Http.GetFromJsonAsync<RestaurantResponse>($"restaurants/{RestauranteName}", cancellationToken);
    }
}