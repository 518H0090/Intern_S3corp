using AutoMapper;
using ShopeeApi.Dtos;
using ShopeeApi.Models;
using ShopeeApi.Repository;

namespace ShopeeApi.Service
{
    public class FoodService : IFoodService
    {
        private readonly IMapper _mapper;
        private readonly IFoodRepository _repository;

        public FoodService(IMapper mapper, IFoodRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ServiceResponse<ResponseGetFood>> CreateFood(RequestAddFood request)
        {
            var serviceResponse = new ServiceResponse<ResponseGetFood>();

            if ((await _repository.ExistsRestaurant(_mapper.Map<Food>(request))) == false )
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Exist Restaurant";
            }
            
            else
            {
                try
                {
                    var newFood = await _repository.CreateFood(_mapper.Map<Food>(request));

                    serviceResponse.Data = _mapper.Map<ResponseGetFood>(newFood);
                } 
                
                catch (Exception ex)
                {
                    serviceResponse.Success = false;
                    serviceResponse.Message = $"Exists Food In this Restaurant + {ex.Message}";
                }
                
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> DeleteFood(RequestFoodContainRestaurant request)
        {
            var response = new ServiceResponse<string>();
           
            var deleteFood = await _repository.DeleteFood(_mapper.Map<Food>(request));

            if (!deleteFood)
            {
                response.Success = false;
                response.Message = "Can't Delete Food";
            } 
            
            else
            {
                response.Data = "Delete Success";
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ResponseGetFood>>> GetAllFood()
        {
            var response = new ServiceResponse<IEnumerable<ResponseGetFood>>();

            var getAllWithoutRestaurant = await _repository.GetAllFood();

            if (getAllWithoutRestaurant.ToList().Count <= 0)
            {
                response.Success = false;
                response.Message = "Not Found Any Value";
            }

            else
            {
                response.Data = getAllWithoutRestaurant.Select(x => _mapper.Map<ResponseGetFood>(x));
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ResponseGetFood>>> GetAllFoodInRestaurant(int resId)
        {
            var response = new ServiceResponse<IEnumerable<ResponseGetFood>>();

            var queryForFood = new Food
            {
                RestaurantId = resId
            };

            var getAllWithRestaurant = await _repository.GetAllFoodInRestaurant(queryForFood);

            if (getAllWithRestaurant.ToList().Count <= 0)
            {
                response.Success = false;
                response.Message = "Not Found Any Value";
            }

            else
            {
                response.Data = getAllWithRestaurant.Select(x => _mapper.Map<ResponseGetFood>(x));
            }

            return response;
        }

        public async Task<ServiceResponse<ResponseGetFood>> GetFoodById(int foodId)
        {
            var response = new ServiceResponse<ResponseGetFood>();

            Food queryFood = new Food
            {
                FoodId = foodId
            };

            var getFoodById = await _repository.GetFoodById(queryFood);

            if (getFoodById == null)
            {
                response.Success = false;
                response.Message = "Not Found Value";
            } 

            else
            {
                response.Data = _mapper.Map<ResponseGetFood>(getFoodById);
            }

            return response;
        }

        public async Task<ServiceResponse<ResponseGetFood>> GetFoodInRestaurantById(RequestFoodContainRestaurant request)
        {
            var response = new ServiceResponse<ResponseGetFood>();

            Food queryFood = new Food
            {
                FoodId = request.FoodId,
                RestaurantId = request.RestaurantId
            };

            var getFoodById = await _repository.GetFoodInRestaurantById(queryFood);

            if (getFoodById == null)
            {
                response.Success = false;
                response.Message = "Not Found Value";
            }

            else
            {
                response.Data = _mapper.Map<ResponseGetFood>(getFoodById);
            }

            return response;
        }

        public async Task<ServiceResponse<ResponseGetFood>> UpdateFood(RequestUpdateFood request)
        {
            var response = new ServiceResponse<ResponseGetFood>();

            var updateFood = await _repository.UpdateFood(_mapper.Map<Food>(request));

            if (updateFood == null)
            {
                response.Success = false;
                response.Message = "Can't Update Value";
            } 
            
            else
            {
                response.Data = _mapper.Map<ResponseGetFood>(updateFood);
            }

            return response;
        }
    }
}
