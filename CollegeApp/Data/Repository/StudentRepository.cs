using Microsoft.EntityFrameworkCore;


namespace CollegeApp.Data.Repository
{
    public class StudentRepository : CollegeRepository<Student>,IStudentRepository
    {
        private readonly CollegeDbContext _dbContext;
        public StudentRepository(CollegeDbContext dbContext) : base(dbContext) 
        {

            _dbContext=dbContext;
        }
        public Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus)
        {
            ////this is the example of student specific method implementation
            //Write code to return students having fee status pending
            return null;
        }   

    }
}
