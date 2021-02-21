using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class ItemListHomeDto
  {
    public int Id { get; set; }

    public string Title { get; set; }

    public string Subtitle { get; set; }

    public string Type { get; set; }

    public List<ItemFoodDto> ListFood { get; set; }

    public List<ItemCollectionDto> ListCollection { get; set; }

    public List<ItemRestaurantHoriDto> ListRestaurant { get; set; }
  }
}

