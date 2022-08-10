using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Dto.Response;

namespace DatabaseManagement.Models.Repository
{
    public interface IStudentRepository
    {
        ResponseCreateStudent CreateStudent(RequestCreateStudent request);
        List<ResponseCreateStudent> GetAllStudent();
    }
}
