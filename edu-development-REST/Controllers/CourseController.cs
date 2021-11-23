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
        /// Fetches all courses.
        /// </summary>
        /// <response code="200">Returns all Courses</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            return Ok(await _unitOfWork.CourseRepository.GetCoursesAsync());
        }

        /// <summary>
        /// Fetches a Course based on the given Id.
        /// </summary>
        /// <param name="id">This is an Id of an existing Course</param>
        /// <response code="200">Returns the Course with the given Id</response>
        /// <response code="204">No Course with the given Id found </response>
        [HttpGet]
        [Route("findnumber/{id:guid}")]
        public async Task<ActionResult<Course>> GetCourseByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
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
        /// <response code="500">Failed to create a new Course</response>
        [HttpPost()]
        public async Task<ActionResult> AddCourse(CourseViewModel model)
        {
            try
            {
                _unitOfWork.CourseRepository.Add(model);
                var result = await _unitOfWork.Complete();
                return StatusCode(201, model);
            }

            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates properties on an existing course.
        /// </summary>
        /// <param name="id">This is an id of an existing Course</param>
        /// <param name="updatedCourse">This is the changes to the Course</param>
        /// <response code="201">Successfully updated the Course</response>
        /// <response code="400">Invalid Course Id</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCourse(Guid id, CourseViewModel updatedCourse)
        {
            try
            {
                _unitOfWork.CourseRepository.Update(updatedCourse, id);
                await _unitOfWork.CourseRepository.SaveAllAsync();
                return StatusCode(201, updatedCourse);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes an existing Course based on Course Code. 
        /// </summary>
        /// <param name="id">This is an id of an existing Course</param>
        /// <response code="200">Successfully deleted an existing Course</response>
        /// <response code="400">Invalid Course Code</response>
        /// <response code="500">Course could not be removed</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourse(Guid id)
        {
            try
            {
                var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(id);
                _unitOfWork.CourseRepository.Delete(course);
                var result = await  _unitOfWork.CourseRepository.SaveAllAsync();
                return StatusCode(200);

            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
