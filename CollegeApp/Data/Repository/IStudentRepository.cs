namespace CollegeApp.Data.Repository
{
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        //this is the example of student specific method

        Task<List<Student>> GetStudentsByFeeStatusAsync(int feeStatus);
    }
}
