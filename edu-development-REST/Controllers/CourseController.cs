using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace edu_development_REST.Controllers
{
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return Ok(await _unitOfWork.CourseRepository.GetCoursesAsync());
        }


        [HttpGet("findnumber/{coursenumber}")]
        public async Task<ActionResult<Course>> GetCourseByCourseCodeAsync(Guid courseCode)
        {


            try
            {

                var result = await _unitOfWork.CourseRepository.GetCourseByCourseCodeAsync(courseCode);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpPost()]
        public async Task<ActionResult> AddCourse(Course course)
        {
            _unitOfWork.CourseRepository.Add(course);
            if (await _unitOfWork.Complete())
            {
                return StatusCode(201);
            }

            return StatusCode(500, "Det gick inte att spara ner ny kurs");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCourse(Guid id, Course updatedCourse)
        {

            _unitOfWork.CourseRepository.Update(updatedCourse, id);
            if (await _unitOfWork.CourseRepository.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Det gick inte att uppdatera kursen");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(Guid id)
        {
            var course = await _unitOfWork.CourseRepository.GetCourseByCourseCodeAsync(id);
            if (course == null) return NotFound($"Hittade ingen kurs med id: {id}");

            _unitOfWork.CourseRepository.Delete(course);
            if (await _unitOfWork.CourseRepository.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Det gick inte att ta bort kursen");
        }
    }
}
