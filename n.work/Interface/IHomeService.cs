using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.WebEncoders.Testing;
using n.work.DataContext;
using n.work.Entity;
using n.work.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace n.work.Interface
{
  public interface IHomeService
  {
    GetStarted GetStarted(string authorization);

    HomeContent GetHomeContent(string authorization);
  }

  public class HomeService : BaseService, IHomeService
  {
    private readonly DatabaseContext context;

    private readonly AppSettings _appSettings;

    public HomeService(DatabaseContext context, IOptions<AppSettings> appSettings) : base(appSettings)
    {
      _appSettings = appSettings.Value;
      this.context = context;
    }

    public HomeContent GetHomeContent(string authorization)
    {
      var itemFood = new ItemFoodDto()
      {
        FoodId = 1,
        RestaurantId = 1,
        FoodCost = 34000,
        FoodName = "Homestyle Crispy Chicken",
        RestaurantName = "McDonald's",
        FoodImage = "https://cdn.shopify.com/s/files/1/0269/5967/5490/products/6.2.jpg"
      };

      var itemRestaurant = new ItemRestaurantHoriDto()
      {
        RestaurantId = 1,
        RestaurantName = "McDonald's® (Adams & Wells)",
        RestaurantImage = "http://52.187.117.17/nwork-api/image/0b0b6588-52d3-42d1-a4e8-6b949561957f.jfif"
      };

      var itemRestaurant1 = new ItemRestaurantHoriDto()
      {
        RestaurantId = 1,
        RestaurantName = "McDonald's® (Adams & Wells)",
        RestaurantImage = "https://mcdonalds.vn/uploads/2018/gacay_squared-rev.jpg"
      };

      var itemCollection1 = new ItemCollectionDto()
      {
        CollectionId = 1,
        CollectionTitle = "Let's Eat Pizza With Emily",
        CollectionSubtitle = "Delicious and nutritious",
        CollectionImage = "https://scontent.fdad3-2.fna.fbcdn.net/v/t1.15752-9/s2048x2048/149581589_748881315744891_4814137521680383224_n.jpg?_nc_cat=107&ccb=3&_nc_sid=ae9488&_nc_ohc=K_qXkTXFt2MAX8ua8Or&_nc_ht=scontent.fdad3-2.fna&tp=7&oh=e99582684eb28ce343bd3268883d2a2e&oe=605267CC"
      };

      var itemHomeCollection1 = new ItemListHomeDto()
      {
        Id = 1,
        Title = "Delicious food at Chicago",
        Subtitle = "Order now to get your meal served in minutes",
        Type = "collection",
        ListCollection = new List<ItemCollectionDto>() { itemCollection1, itemCollection1, itemCollection1, itemCollection1, itemCollection1, itemCollection1 }
      };

      var itemHomeRestaurant = new ItemListHomeDto()
      {
        Id = 2,
        Title = "Tasty snack near you",
        Subtitle = "Too good to be missed get the deals now!",
        Type = "restaurant",
        ListRestaurant = new List<ItemRestaurantHoriDto>() { itemRestaurant, itemRestaurant1, itemRestaurant, itemRestaurant1, itemRestaurant, itemRestaurant1 }
      };

      var itemHomeFood= new ItemListHomeDto()
      {
        Id = 3,
        Title = "Hot Item Today",
        Subtitle = "Order now to get your meal served in minutes",
        Type = "food",
        ListFood = new List<ItemFoodDto>() { itemFood, itemFood, itemFood, itemFood, itemFood, itemFood,itemFood,itemFood }
      };

      var itemBanner = new ItemBannerDto()
      {
        Id = 1,
        Image = "http://channel.mediacdn.vn/2019/1/20/image005-15479948829561514367436.jpg"
      };

      var itemCategory = new ItemCategoryDto()
      {
        CategoryId = 1,
        Title = "Bread",
        Image = "http://52.187.117.17/nwork-api/image/6bd41860-95f3-4b20-a42c-2a6aceadf54e.png"
      };

      var itemCategory2 = new ItemCategoryDto()
      {
        CategoryId = 1,
        Title = "Hamburger",
        Image = "http://52.187.117.17/nwork-api/image/610431f9-32e7-4aad-8b24-88d89668209c.jpg"
      };

      var itemCategory3 = new ItemCategoryDto()
      {
        CategoryId = 1,
        Title = "Cake",
        Image = "http://52.187.117.17/nwork-api/image/9d63bd85-a3cc-4e48-8039-bd2c45bcbdc8.png"
      };

      var itemRestaurantVerti = new ItemRestaurantVertiDto()
      {
        RestaurantId = 2,
        RestaurantName = "KFC Camberwell - Church Street",
        RestaurantType = "Fried Chicken - American - Fast Food",
        RestaurantImage = "https://d1ralsognjng37.cloudfront.net/a7002177-10a0-4cee-98e3-007e5805fc7e.jpeg",
        RestaurantRate = 4.5,
        DeliveryTime = 23,
        Distance = 5.0
      };

      var contentHome = new HomeContent()
      {
        ListBanner = new List<ItemBannerDto>() { itemBanner , itemBanner , itemBanner , itemBanner },
        ListCategory = new List<ItemCategoryDto>() { itemCategory2, itemCategory2, itemCategory2, itemCategory2, itemCategory2, itemCategory2 },
        ListHome = new List<ItemListHomeDto>() { itemHomeCollection1, itemHomeRestaurant, itemHomeRestaurant },
        MoreRestaurant = new List<ItemRestaurantVertiDto>() { itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti ,
        itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti ,
        itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti , itemRestaurantVerti }

      };
      return contentHome;
    }

    public GetStarted GetStarted(string authorization)
    {
      var authorizationString = getTokenFromAuthorization(authorization);
      var tokenContent = readTokenAuthenticate(authorizationString);
      var userID = tokenContent.userId;
      var profile = GetUser(userID);

      var responseHome = new GetStarted()
      {
        helloUser = "Hello " + profile.Fullname + " !",
        wishUser = SessionsOfTheDay() + ", welcome back"
      };
      return responseHome;
    }

    public Profile GetUser(int userId)
    {
      var account = context.Profile.FirstOrDefault(profile => profile.AccountId == userId);
      return account;
    }

    public string SessionsOfTheDay()
    {
      var time = DateTime.UtcNow.AddHours(5.5).Hour;
      
      if (time < 12)
        return "Good Morning";
      else if (time < 17)
        return "Good Afternoon";
      else if (time < 20)
        return "Good Evening";
      return "Good Night";
    }

  }
}
