using APIEducation.Common;
using APIEducation.Model;
using APIEducation.Models;
using APIEducation.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIEducation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Produces("application/json")]
    public class StudentController : ControllerBase
    {
        private readonly ILoggerService _logger;
        private readonly EducationContext _context;
        public StudentController(EducationContext context, ILoggerService logger)
        {
            this._context = context;
            _logger = logger;
        }        
    
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
         
            MessageResult msResult = new MessageResult();
            try
            {
                var st = await _context.Students.ToListAsync();

                if (st.Count == 0)
                {
                    msResult.ERRORCODE = GlobalCode.NOT_FOUND;
                    msResult.ERRORDESC = "Data not found";
                    msResult.DATA = new object();
                    return NotFound(msResult);
                }
                else
                {
                    msResult.ERRORCODE = GlobalCode.OK;
                    msResult.ERRORDESC = "Successfully";
                    msResult.DATA = st;
                }
            }
            catch(Exception e)
            {

            }
            return Ok(msResult);
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<MessageResult>> GetStudentByID(int id)
        {
            MessageResult msResult = new MessageResult();
            try
            {
                var st = await _context.Students.Where(s => s.Studentid == id).FirstOrDefaultAsync();
                if (st == null)
                {
                    msResult.ERRORCODE = GlobalCode.NOT_EXIST;
                    msResult.ERRORDESC = "Student doesn't exist.";
                    return NotFound(msResult);
                }
                msResult.ERRORCODE = GlobalCode.OK;
                msResult.ERRORDESC = "Succesfullt";
                msResult.DATA = st;
                _logger.logInfo("the name of student is: " + st.Name);
            }
            catch(Exception ex)
            {
                msResult.ERRORCODE = GlobalCode.NOT_EXIST;
                msResult.ERRORDESC = ex.ToString();
                _logger.logInfo("Error student: " + ex.ToString());
                return NotFound(msResult);
            }
           
            return Ok(msResult);
        }
        
       [HttpGet("{name}")]
       public async Task<ActionResult<Student>> GetStudentByName(String name)
       {
           //var st = from c in _context.Students where EF.Functions.Like(c.Name, "%" + name + "%") select c;
           var st = await _context.Students.Where(c => EF.Functions.Like(c.Name, "%" + name + "%")).ToListAsync();
           MessageResult msResult = new MessageResult();

           if (st.Count==0)
           {
               msResult.ERRORCODE = GlobalCode.NOT_FOUND;
               msResult.ERRORDESC = "Data not found";
               msResult.DATA = new object();
               return NotFound(msResult);
           }
           else
           {
               msResult.ERRORCODE = GlobalCode.OK;
               msResult.ERRORDESC = "Successfully";
               msResult.DATA = st;
             
           }
           return Ok(msResult);
       }
       [HttpPost]
       public async Task<ActionResult<MessageResult>> CreateStudent(Student st)
       {
           MessageResult msResut = new MessageResult();
           try
           {

               _context.Students.Add(st);
               await _context.SaveChangesAsync();
               msResut.ERRORCODE = GlobalCode.OK;
               msResut.ERRORDESC = "Successfully";
               msResut.DATA = st;
           }
           catch(Exception ex)
           {
               msResut.ERRORCODE = GlobalCode.OK;
               msResut.ERRORDESC = ex.ToString();
           }
           if (msResut.ERRORCODE==0)
           {
               return BadRequest(msResut);
           }
           else
               return CreatedAtAction("",msResut);
       }
    }
}
