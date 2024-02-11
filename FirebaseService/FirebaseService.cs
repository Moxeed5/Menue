using Bham_Events.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

public class FirebaseService
{
    private readonly FirebaseClient _firebaseClient;

    public FirebaseService()
    {
        // Initialize the FirebaseClient here with your Firebase project URL and optionally an Auth Token if needed
        _firebaseClient = new FirebaseClient("https://birmingham-events-default-rtdb.firebaseio.com/");
    }

    //Restuarant Crud 

    //add
    public async Task<Restaurant> AddRestaurantAsync(Restaurant restaurant)
    {
        var newRestaurant = new Restaurant
        {
            Name = restaurant.Name,
            Location = restaurant.Location,
            Menu = new Menu() // Initialize with an empty Menu
        };

        // Ensure the Menu does not contain any items
        newRestaurant.Menu.MenuItems = new Dictionary<string, MenuItem>();

        var result = await _firebaseClient
            .Child("Restaurants")
            .PostAsync(restaurant);

        restaurant.Id = result.Key; // Capture and assign Firebase-generated ID

        return restaurant; // Return the newly created restaurant with its ID
    }


    //Get all
    public async Task<List<Restaurant>> GetAllRestaurantsAsync()
    {
        var restaurants = await _firebaseClient
            .Child("Restaurants")
            .OnceAsync<Restaurant>();

        return restaurants.Select(item => new Restaurant
        {
            Id = item.Key, // The unique key Firebase generated for the item
            // Map other properties as needed
            Name = item.Object.Name,
            Location = item.Object.Location
        }).ToList();
    }

    //get specific
    public async Task<Restaurant> GetRestaurantAsync(string id)
    {
        var restaurant = await _firebaseClient
            .Child("Restaurants")
            .Child(id)
            .OnceSingleAsync<Restaurant>();

        if (restaurant != null)
        {
            restaurant.Id = id; // Set the Id to the key
            return restaurant;
        }
        return null;
    }

    //update a restaurant
    public async Task UpdateRestaurantAsync(string id, Restaurant restaurant)
    {
        await _firebaseClient
            .Child("Restaurants")
            .Child(id)
            .PutAsync(restaurant);
    }

    //delete restaurant 
    public async Task DeleteRestaurantAsync(string id)
    {
        await _firebaseClient
            .Child("Restaurants")
            .Child(id)
            .DeleteAsync();
    }

    //Menu CRUD

    //add menu

    public async Task AddMenuAsync(string restaurantId, Menu menu)
    {
        // No result variable is needed because PutAsync does not return a value.
        await _firebaseClient
            .Child("Restaurants")
            .Child(restaurantId)
            .Child("menu")
            .PutAsync(menu); // This will overwrite the existing menu or create it if it doesn't exist.

        // Since PutAsync doesn't return a value, you don't have a result to check or an ID to set.
        // The menu is directly associated with the restaurantId.
    }

    //get all menus

    public async Task<List<Menu>> GetAllMenusAsync()
    {
        // Fetch all restaurants
        var restaurantSnapshots = await _firebaseClient
            .Child("Restaurants")
            .OnceAsync<Restaurant>();

        // Initialize a list to hold all the menus
        var allMenus = new List<Menu>();

        // Iterate over each restaurant snapshot and extract the menu
        foreach (var snapshot in restaurantSnapshots)
        {
            // Make sure the Menu property exists and is populated
            if (snapshot.Object?.Menu != null)
            {
                // Add the menu to the list
                allMenus.Add(snapshot.Object.Menu);
            }
        }

        // Return the list of all menus
        return allMenus;
    }

    public async Task<List<MenuItem>> GetAllMenuItemsAsync()
    {
        var menuItemsSnapShot = await _firebaseClient
            .Child("Restaurants") // Make sure this is spelled correctly
            .OnceAsync<Restaurant>();

        var allMenuItems = new List<MenuItem>();

        // Iterate over each snapshot and add each menu item individually
        foreach (var snapShot in menuItemsSnapShot)
        {
            if (snapShot.Object?.Menu.MenuItems != null)
            {
                // The MenuItems property is a Dictionary<string, MenuItem>, so iterate over it
                foreach (var menuItemEntry in snapShot.Object.Menu.MenuItems)
                {
                    // Add the MenuItem to the allMenuItems list
                    allMenuItems.Add(menuItemEntry.Value);
                }
            }
        }
        return allMenuItems;
    }


    //Create menuItem

    public async Task<MenuItem> AddMenuItemAsync(string restaurantId, string name, string description, double price)
    {
        MenuItem newItem = new MenuItem
        {
            Name = name,
            Description = description,
            Price = price
        };

        var result = await _firebaseClient
            .Child("Restaurants")
            .Child(restaurantId)
            .Child("menu")
            .Child("menuItems")
            .PostAsync(newItem);

        // Capture and assign the generated key as the MenuItem's ID
        newItem.Id = result.Key;

        return newItem; // Return the updated MenuItem object with an ID
    }


}