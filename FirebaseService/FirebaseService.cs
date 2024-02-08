﻿using Bham_Events.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class FirebaseService
{
    private readonly FirebaseClient _firebaseClient;

    public FirebaseService()
    {
        // Initialize the FirebaseClient here with your Firebase project URL and optionally an Auth Token if needed
        _firebaseClient = new FirebaseClient(
            "https://your-firebase-project.firebaseio.com/",
            new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult("your_auth_token") // Only if you need Auth, else remove this
            });
    }

    public async Task<Restaurant> AddRestaurantAsync(Restaurant restaurant)
    {
        var result = await _firebaseClient
            .Child("Restaurants")
            .PostAsync(restaurant);

        if (result.Object != null)
        {
            // Assuming your Restaurant class has an Id property
            restaurant.Id = result.Key; // Firebase generates a unique key for each item
            return restaurant;
        }
        return null;
    }

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

    public async Task UpdateRestaurantAsync(string id, Restaurant restaurant)
    {
        await _firebaseClient
            .Child("Restaurants")
            .Child(id)
            .PutAsync(restaurant);
    }

    public async Task DeleteRestaurantAsync(string id)
    {
        await _firebaseClient
            .Child("Restaurants")
            .Child(id)
            .DeleteAsync();
    }
}