using AutoMapper;
using ShopeeApi.Dtos;
using ShopeeApi.Models;
using ShopeeApi.Repository;

namespace ShopeeApi.Service
{
    public class CategoryFoodService : ICategoryFoodService
    {
        private readonly ICategoryFoodRepository _repository;
        private readonly IMapper _mapper;

        public CategoryFoodService(ICategoryFoodRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<string>> ConnectFoodTag(int resId, RequestCategoryFood request)
        {
            var response = new ServiceResponse<string>();

            if (await _repository.ExistsRelationFoodTag(resId, _mapper.Map<RelationCategoryFood>(request)))
            {
                response.Success = false;
                response.Message = "Can't Create New Relationship";
            }
            
            else
            {
                await _repository.ConnectFoodTag(_mapper.Map<RelationCategoryFood>(request));
                response.Data = "Create New Relationship";
            }

            return response;
        }

        public async Task<ServiceResponse<string>> DeleteRelationFoodTag(int resId, RequestCategoryFood request)
        {
            var response  = new ServiceResponse<string> ();

            if (await _repository.ExistsRelationFoodTag(resId, _mapper.Map<RelationCategoryFood>(request)))
            {
                var deleteRelation = await _repository.DeleteRelationFoodTag(_mapper.Map<RelationCategoryFood>(request));

                if (deleteRelation == false)
                {
                    response.Success = false;
                    response.Message = "Can't Delete Relationship";
                }

                response.Data = "Delete Succesfully";
            }

            else
            {
                response.Success = false;
                response.Message = "Can't Find Value Use For Delete";
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ResponseGetCategoryCombineFood>>> GetAllCategorywithRestaurantIdCombineFood(int resId)
        {
            var response = new ServiceResponse<IEnumerable<ResponseGetCategoryCombineFood>>();

            var getCategoryCombineFood = await _repository.GetAllCategorywithRestaurantIdCombineFood(resId);

            if (getCategoryCombineFood.ToList().Count <= 0)
            {
                response.Success = false;
                response.Message = "Not Found Any Value";
            } 

            else
            {
                response.Data = getCategoryCombineFood.Select(x => 
                _mapper.Map<ResponseGetCategoryCombineFood>(x));
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ResponseGetFoodCombineCategory>>> GetAllFoodwithRestaurantIdCombineCategory(int resId)
        {
            var response = new ServiceResponse<IEnumerable<ResponseGetFoodCombineCategory>>();

            var getFoodCombineCategory = await _repository.GetAllFoodwithRestaurantIdCombineCategory(resId);

            if (getFoodCombineCategory.ToList().Count <= 0)
            {
                response.Success = false;
                response.Message = "Not Found Any Value";
            }

            else
            {
                response.Data = getFoodCombineCategory.Select(x =>
                _mapper.Map<ResponseGetFoodCombineCategory>(x));
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ResponseGetFoodTag>>> GetAllRelation()
        {
            var response = new ServiceResponse<IEnumerable<ResponseGetFoodTag>>();

            var getAllRelation = await _repository.GetAllRelation();

            if (getAllRelation.ToList().Count <= 0)
            {
                response.Message = "Can't Find Any Value";
                response.Success = false;
            } 

            else
            {
                response.Data = getAllRelation.Select(x => _mapper.Map<ResponseGetFoodTag>(x));
            }

            return response;
        }

        public async Task<ServiceResponse<ResponseGetCategoryCombineFood>> GetCategorywithRestaurantIdCombineFood(RequestCategoryCombineFood request)
        {
            var response = new ServiceResponse<ResponseGetCategoryCombineFood>();

            var getFoodCombineCategory = await _repository.GetCategorywithRestaurantIdCombineFood(request.RestaurantId, request.CategoryId);

            if (getFoodCombineCategory == null)
            {
                response.Success = false;
                response.Message = "Not Found Value";
            }

            else
            {
                response.Data = _mapper.Map<ResponseGetCategoryCombineFood>(getFoodCombineCategory);
            }

            return response;
        }

        public async Task<ServiceResponse<ResponseGetFoodCombineCategory>> GetFoodwithRestaurantIdCombineCategory(RequestFoodCombineCategory request)
        {
            var response = new ServiceResponse<ResponseGetFoodCombineCategory>();

            var getFoodCombineCategory = await _repository.GetFoodwithRestaurantIdCombineCategory(request.RestaurantId, request.FoodId);

            if (getFoodCombineCategory == null)
            {
                response.Success = false;
                response.Message = "Not Found Value";
            }

            else
            {
                response.Data = _mapper.Map<ResponseGetFoodCombineCategory>(getFoodCombineCategory);
            }

            return response;
        }
    }
}
