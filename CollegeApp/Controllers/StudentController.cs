using AutoMapper;
using CollegeApp.Data;
using CollegeApp.Data.Repository;
using CollegeApp.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;


namespace CollegeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        //Builtin Loggers
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        public StudentController(ILogger<StudentController> logger, IMapper mapper, IStudentRepository studentRepository)
        {
            _logger = logger;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("all",Name ="getallstudent")]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsAsync()
        {
            _logger.LogInformation("Get student method started");
            var students = await _studentRepository.GetAllAsync();

            //auto mapping

            var studentDtodata = _mapper.Map<List<StudentDto>>(students);




            //linq
            //manual mapping
            //var students = await _dbContext.Students.Select(s => new StudentDto()
            //{
            //    Id = s.Id,
            //    StudentName = s.StudentName,
            //    Address = s.Address,
            //    Email = s.Email,
            //    Dob=s.Dob
            //}).ToListAsync();

            return Ok(studentDtodata);
        }

        [HttpGet]
        [Route("{id:int}", Name = "getstudentbyid") ]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> GetStudentByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning("bad request");
                return BadRequest();
            }
            var student= await _studentRepository.GetAsync(student=> student.Id == id);
            if (student == null)
            {
                _logger.LogError("student not found with given id");
                return NotFound($"student with id {id} not found");
            }

            var StudentDto = _mapper.Map<StudentDto>(student);
            //var StudentDto = new StudentDto
            //{
            //    Id = student.Id,
            //    StudentName = student.StudentName,
            //    Address = student.Address,
            //    Email = student.Email,
            //    Dob = student.Dob
            //};
            return Ok(StudentDto);
        }


        [HttpGet]
        [Route("{name:alpha}", Name = "getstudentbyname")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> GetStudentByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student =await _studentRepository.GetAsync(student=> student.StudentName.ToLower().Contains(name));
            if (student == null)
                return NotFound($"student with name {name} not found");

            var StudentDto = _mapper.Map<StudentDto>(student);

            //var StudentDto = new StudentDto
            //{
            //    Id = student.Id,
            //    StudentName = student.StudentName,
            //    Address = student.Address,
            //    Email = student.Email,
            //    Dob=student.Dob
            //};
            return Ok(StudentDto);
        }

        [HttpPost]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StudentDto>> createStudentAsync([FromBody] StudentDto Dto)

        
        {
            if(Dto == null) return BadRequest();

            Student student = _mapper.Map<Student>(Dto);

            //Student student = new Student
            //{

            //    StudentName = Dto.StudentName,
            //    Address = Dto.Address,
            //    Email = Dto.Email,
            //    Dob= Dto.Dob

            //};
            var studentaftercreation = await _studentRepository.CreateAsync(student);
            Dto.Id = studentaftercreation.Id;

            return CreatedAtRoute("GetStudentById", new { id = Dto.Id }, Dto);
            


        } 

        [HttpPut]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateStudentAsync([FromBody] StudentDto Dto)
        {
            if (Dto == null || Dto.Id<0) return BadRequest();

            var existingstudent = await _studentRepository.GetAsync(student=> student.Id == Dto.Id, true);

            if(existingstudent == null) return NotFound();

            var newRecord = _mapper.Map<Student>(Dto);

            await _studentRepository.UpdateAsync(newRecord);


            return NoContent();

            //var newRecord = new Student()
            //{
            //    StudentName = Dto.StudentName,
            //    Address = Dto.Address,
            //    Email = Dto.Email,
            //    Dob = Dto.Dob
            //};


            //existingstudent.StudentName=model.StudentName; 
            //existingstudent.Address=model.Address;
            //existingstudent.Email=model.Email;
            //existingstudent.Dob = model.Dob;


        }
        
        [HttpPatch]
        [Route("{id:int}/updatepartial")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<ActionResult> UpdateStudentPartialAsync(int id ,[FromBody] JsonPatchDocument<StudentDto> patchdocument)
        {
            if (patchdocument == null || id < 0) return BadRequest();

            var existingstudent = await _studentRepository.GetAsync(student => student.Id == id, true);

            if (existingstudent == null) return NotFound();

            var studentDto = _mapper.Map<StudentDto>(existingstudent);

            //var studentDto = new StudentDto
            //{
            //    Id = existingstudent.Id,
            //    StudentName=existingstudent.StudentName,
            //    Email=existingstudent.Email,
            //    Address=existingstudent.Address,

            //};

            patchdocument.ApplyTo(studentDto,ModelState);

            if(!ModelState.IsValid) return BadRequest(ModelState);

            existingstudent = _mapper.Map<Student>(studentDto);

            await _studentRepository.UpdateAsync(existingstudent);  

            //existingstudent.StudentName = studentDto.StudentName;
            //existingstudent.Address = studentDto.Address;
            //existingstudent.Email = studentDto.Email;

           

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}", Name = "deletestudentbyid")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<bool>> DeleteStudentByIdAsync(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }
            var student =await _studentRepository.GetAsync(student => student.Id == id);
            if (student == null)
                return NotFound($"student with id {id} not found");

            await _studentRepository.DeleteAsync(student);
            return Ok(true);
        }
         
    }
}
