using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class HomeContent
  {
    public List<ItemBannerDto> ListBanner { get; set; }

    public List<ItemCategoryDto> ListCategory { get; set; }

    public List<ItemListHomeDto> ListHome { get; set; }

    public List<ItemRestaurantVertiDto> MoreRestaurant { get; set; }
  }
}
