using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using OdeToFood.Core;
using OdeToFood.Data;
using System.Collections.Generic;

namespace OdeToFood.Pages.Restaurants
{
    public class EditModel : PageModel
    {
        private readonly IRestaurantData _restaurantData;
        private readonly IHtmlHelper _htmlHelper;
        [BindProperty]
        public Restaurant Restaurant { get; set; }
        public IEnumerable<SelectListItem> Cuisines { get; set; }

        public EditModel(IRestaurantData restaurantData,IHtmlHelper htmlHelper)
        {
            this._restaurantData = restaurantData;
            this._htmlHelper = htmlHelper;
        }

        public IActionResult OnGet(int restaurantId)
        {
            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            Restaurant = _restaurantData.GetById(restaurantId);

            if (Restaurant == null )
            {
                return RedirectToPage("./NotFound");
            }
            return Page();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                _restaurantData.Update(Restaurant);
                _restaurantData.Commit();
                return RedirectToPage("./Detail", new { restaurantId = Restaurant.Id});
            }

            Cuisines = _htmlHelper.GetEnumSelectList<CuisineType>();
            return Page();
        }
    }
}
