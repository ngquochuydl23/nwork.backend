using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Models
{
  public class ItemFoodDto
  {
    public int FoodId { get; set; }

    public string FoodName { get; set; }


    public string FoodImage { get; set; }

    public double FoodCost { get; set; }

    public int RestaurantId { get; set; }
    public string RestaurantName { get; set; }
  }
}
