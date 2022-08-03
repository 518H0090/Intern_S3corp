using RefreshToken0011.Models.DatabaseContext;
using RefreshToken0011.Models.Interface;
using RefreshToken0011.Models.ModelDatabase;

namespace RefreshToken0011.Models.Repository
{
    public class JobRepository : IJobRepository
    {
        private readonly DataContext _context;

        public JobRepository(DataContext context)
        {
            _context = context;
        }

        public Job CreateJob(Job job)
        {
            var jobCreated = _context.Jobs.Add(job);
            _context.SaveChanges();
            return jobCreated.Entity;
        }

        public List<Job> DeleteAllJob()
        {
            List<Job> listJob = GetAllJob();

            if (listJob.Count <= 0)
            {
                return null;
            }

            _context.Jobs.RemoveRange(listJob);
            _context.SaveChanges();

            return listJob;
        }

        public Job DeleteJob(int jobId)
        {
            var findJob = GetJobById(jobId);

            if (findJob == null)
            {
                return null;
            }

            var jobDeleted = _context.Jobs.Remove(findJob);
            _context.SaveChanges();

            return jobDeleted.Entity;
        }

        public Job EditJob(Job job)
        {
            var findJob = GetJobById(job.JobId);

            if (findJob == null)
            {
                return null;
            }

            findJob.Name = job.Name;
            findJob.Description = job.Description;

            var jobChanged = _context.Jobs.Update(findJob);

            _context.SaveChanges();

            return jobChanged.Entity;
        }

        public List<Job> GetAllJob()
        {
            return _context.Jobs.ToList();
        }

        public Job GetJobById(int jobId)
        {
            return _context.Jobs.Find(jobId);
        }
    }
}
