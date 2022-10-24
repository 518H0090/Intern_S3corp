using AutoMapper;
using ShopeeApi.Dtos;
using ShopeeApi.Models;
using ShopeeApi.Repository;

namespace ShopeeApi.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ResponseGetCategory>> CreateCategory(RequestAddCategory request)
        {
            var serviceResponse = new ServiceResponse<ResponseGetCategory>();

            var checkExistTagInRestaurant = await _repository.ExistsCategoryTag(request.RestaurantId, request.CategoryTag);
            var checkExistRestaurant = await _repository.ExistsRestaurantOrNot(request.RestaurantId);

            if (!checkExistRestaurant)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Exist Restaurant";
            }
            else if (checkExistTagInRestaurant)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Exists Category Tag In This Restaurant";
            }
            else
            {
                var newCategoryInRes = await _repository.CreateCategory(_mapper.Map<Category>(request));
                serviceResponse.Data = _mapper.Map<ResponseGetCategory>(newCategoryInRes);
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<string>> DeleteCategory(RequestDeleteCategory request)
        {
            var response = new ServiceResponse<string>();

            var deleteCategory = await _repository.DeleteCategory(request.RestaurantId, request.CategoryId);

            if (!deleteCategory)
            {
                response.Success = false;
                response.Message = "Can't Delete Category";
            }
            else
            {
                response.Data = "Delete Success";
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ResponseGetCategory>>> GetAllCategories(int ResId)
        {
            var serviceResponse = new ServiceResponse<IEnumerable<ResponseGetCategory>>();

            var getAllCategory = await _repository.GetAllCategories(ResId);

            if (getAllCategory.ToList().Count <= 0)
            {
                serviceResponse.Success = false;
                serviceResponse.Message = "Not Found Any Category In This Restaurant";
            }
            else
            {
                serviceResponse.Data = getAllCategory.Select(c => _mapper.Map<ResponseGetCategory>(c)).ToList();
            }

            return serviceResponse;
        }

        public async Task<ServiceResponse<ResponseGetCategory>> GetCategoryById(RequestCategoryById request)
        {
            var response = new ServiceResponse<ResponseGetCategory>();

            var CategoryWithId = await _repository.GetCategoryById(request.RestaurantId, request.CategoryId);

            if (CategoryWithId == null)
            {
                response.Success = false;
                response.Message = "Not Found Any Category In This Restaurant";
            }
            else
            {
                response.Data = _mapper.Map<ResponseGetCategory>(CategoryWithId);
            }

            return response;
        }

        public async Task<ServiceResponse<ResponseGetCategory>> GetCategoryByTag(RequestCategoryByTag request)
        {
            var response = new ServiceResponse<ResponseGetCategory>();

            var CategoryWithTag = await _repository.GetCategoryByTag(request.RestaurantId, request.CategoryTag);

            if (CategoryWithTag == null)
            {
                response.Success = false;
                response.Message = "Not Found Any Category In This Restaurant";
            }
            else
            {
                response.Data = _mapper.Map<ResponseGetCategory>(CategoryWithTag);
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ResponseGetCategory>>> GetFullCategories()
        {
            var response = new ServiceResponse<IEnumerable<ResponseGetCategory>>();

            var getFullCatgories = await _repository.GetFullCategories();

            if (getFullCatgories.ToList().Count <= 0)
            {
                response.Success = false;
                response.Message = "Not Found Any Category";
            }
            else
            {
                response.Data = getFullCatgories.Select(x => _mapper.Map<ResponseGetCategory>(x));
            }

            return response;
        }

        public async Task<ServiceResponse<ResponseGetCategory>> UpdateCategory(RequestEditCategory request)
        {
            var response = new ServiceResponse<ResponseGetCategory>();

            var updateCategory = await _repository.UpdateCategory(_mapper.Map<Category>(request));

            if (updateCategory == null)
            {
                response.Success = false;
                response.Message = "Can't Update Category";
            }
            else
            {
                response.Data = _mapper.Map<ResponseGetCategory>(updateCategory);
            }

            return response;
        }
    }
}