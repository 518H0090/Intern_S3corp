using RefreshToken0011.Models.DatabaseContext;

namespace RefreshToken0011.Models.Interface
{
    public interface IJobRepository
    {
        Job CreateJob(Job job);
        List<Job> GetAllJob();
        Job GetJobById(int jobId);
        Job EditJob(Job job);
        Job DeleteJob(int jobId);
        List<Job> DeleteAllJob();
    }
}
