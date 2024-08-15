using AutoMapper;
using Microsoft.Extensions.Logging;
using Restaurants.Application.Restaurants.Dtos;
using Restaurants.Domain.Entities;
using Restaurants.Domain.Repositories;

namespace Restaurants.Application.Restaurants;

internal class RestaurantsService(
    IRestaurantsRepository restaurantsRepository, 
    ILogger<RestaurantsService> logger,
    IMapper mapper) : IRestaurantsService
{
    public async Task<int> Create(CreateRestaurantDto createRestaurantDto)
    {
        logger.LogInformation("Creating a restaurant");
        var restaurant = mapper.Map<Restaurant>(createRestaurantDto);
        
        int id = await restaurantsRepository.Create(restaurant);
        return id;
    }
    
    public async Task<IEnumerable<RestaurantDto>> GetAllRestaurants()
    {
        logger.LogInformation("Getting all restaurants");
        var restaurants = await restaurantsRepository.GetAllAsync();

        // var restaurantsDtos = restaurants.Select(RestaurantDto.FromEntity);
        var restaurantsDtos = mapper.Map<IEnumerable<RestaurantDto>>(restaurants);
        
        return restaurantsDtos!;
    }

    public async Task<RestaurantDto?> GetById(int id)
    {
        logger.LogInformation($"Getting restaurant by Id: {id}");
        var restaurant = await restaurantsRepository.GetByIdAsync(id);
        //var restaurantDto = RestaurantDto.FromEntity(restaurant);
        var restaurantDto = mapper.Map<RestaurantDto?>(restaurant);
        return restaurantDto;
    }

    
    
}