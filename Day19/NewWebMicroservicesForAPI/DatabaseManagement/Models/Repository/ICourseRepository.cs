using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Dto.Response;

namespace DatabaseManagement.Models.Repository
{
    public interface ICourseRepository
    {
        ResponseCreateCourse CreateCourse(RequestCreateCourse request);
        List<ResponseCreateCourse> GetAllCourse();
    }
}
