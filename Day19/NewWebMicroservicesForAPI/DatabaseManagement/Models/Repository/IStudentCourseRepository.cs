using DatabaseManagement.Models.DatabaseModels;
using DatabaseManagement.Models.Dto.Request;
using DatabaseManagement.Models.Dto.Response;

namespace DatabaseManagement.Models.Repository
{
    public interface IStudentCourseRepository
    {
        ResponseJoinStudentCourse JoinStudentCourse(RequestJoinStudentName request);
        List<ResponseJoinStudentCourse> GetAllRecord();
    }
}
