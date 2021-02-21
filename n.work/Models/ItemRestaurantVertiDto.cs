using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class ItemRestaurantVertiDto
  {
    public int RestaurantId { get; set; }

    public string RestaurantName { get; set; }

    public string RestaurantImage { get; set; }

    public string RestaurantType { get; set; }

    public double RestaurantRate { get; set; }

    public double DeliveryTime { get; set; }

    public double Distance { get; set; }
  }
}
