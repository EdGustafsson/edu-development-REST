using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using edu_development_REST.ViewModels;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace edu_development_REST.Controllers
{
    /// <summary>
    /// Controller for working with the Courses
    /// </summary>
    [ApiController]
    [Route("api/courses")]
    public class CourseController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all posts.
        /// </summary>
        /// <response code="200">Returns all Courses</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return Ok(await _unitOfWork.CourseRepository.GetCoursesAsync());
        }

        /// <summary>
        /// Fetches a Post based on the given Course Code.
        /// </summary>
        /// <param name="courseCode">This is an Course Code of an existing Course</param>
        /// <response code="200">Returns the Post with the given Id</response>
        /// <response code="404">No Post with the given Id found </response>
        [HttpGet]
        [Route("findnumber/{courseCode:guid}")]
        public async Task<ActionResult<Course>> GetCourseByCourseCodeAsync(Guid courseCode)
        {


            try
            {

                var result = await _unitOfWork.CourseRepository.GetCourseByCourseCodeAsync(courseCode);
                return StatusCode(200, result);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates a new Course. Using the ViewModel.
        /// </summary>
        /// <param name="model">A new CourseViewModel object</param>
        /// <response code="201">Successfully created a new Course</response>
        /// <response code="500">Failed to create a new Coourse</response>
        [HttpPost()]
        public async Task<ActionResult> AddCourse(CourseViewModel model)
        {
            _unitOfWork.CourseRepository.Add(model);
            if (await _unitOfWork.Complete())
            {
                return StatusCode(201);
            }

            return StatusCode(500, "Det gick inte att spara ner ny kurs");
        }

        /// <summary>
        /// Updates properties on an existing post.
        /// </summary>
        /// <param name="id">This is an id of an existing Course</param>
        /// <param name="updatedCourse">This is the changes to the Course</param>
        /// <response code="200">Successfully updated the Post</response>
        /// <response code="400">Invalid Course Id</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCourse(Guid id, CourseViewModel updatedCourse)
        {

            _unitOfWork.CourseRepository.Update(updatedCourse, id);
            if (await _unitOfWork.CourseRepository.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Det gick inte att uppdatera kursen");
        }

        /// <summary>
        /// Deletes an existing Course based on Course Code. 
        /// </summary>
        /// <param name="id">This is an id of an existing Course</param>
        /// <response code="200">Successfully deleted an existing Course</response>
        /// <response code="400">Invalid Course Code</response>
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
