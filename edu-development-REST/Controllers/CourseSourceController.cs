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
    [Route("api/[controller]")]
    public class CourseSourceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseSourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all CourseSource.
        /// </summary>
        /// <response code="200">Returns all Courses</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseSource>>> GetCourseSources()
        {
            return Ok(await _unitOfWork.CourseSourceRepository.GetCourseSourcesAsync());
        }

        /// <summary>
        /// Fetches a CourseSource based on the given Id.
        /// </summary>
        /// <param name="id">This is an Id of an existing CourseSource</param>
        /// <response code="200">Returns the CCourseSource with the given Id</response>
        /// <response code="204">No CourseSource with the given Id found </response>
        [HttpGet]
        [Route("findnumber/{id:guid}")]
        public async Task<ActionResult<Course>> GetCourseSourceByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.CourseSourceRepository.GetCourseSourceByIdAsync(id);
                return StatusCode(200, result);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates a new CourseSource. Using the ViewModel.
        /// </summary>
        /// <param name="model">A new CourseSourceViewModel object</param>
        /// <response code="201">Successfully created a new CourseSource</response>
        /// <response code="500">Failed to create a new CourseSource</response>
        [HttpPost()]
        public async Task<ActionResult> AddCourse(CourseSourceViewModel model)
        {
            try
            {
                _unitOfWork.CourseSourceRepository.Add(model);
                var result = await _unitOfWork.Complete();
                return StatusCode(201, model);
            }

            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates properties on an existing CourseSource.
        /// </summary>
        /// <param name="id">This is an id of an existing CourseSource</param>
        /// <param name="updatedCourseSource">This is the changes to the CourseSource</param>
        /// <response code="201">Successfully updated the CourseSource</response>
        /// <response code="400">Invalid CourseSource Id</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCourse(Guid id, CourseSourceViewModel updatedCourseSource)
        {
            try
            {
                _unitOfWork.CourseSourceRepository.Update(updatedCourseSource, id);
                await _unitOfWork.CourseSourceRepository.SaveAllAsync();
                return StatusCode(201, updatedCourseSource);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes an existing Course based on CourseSource Code. 
        /// </summary>
        /// <param name="id">This is an id of an existing CourseSource</param>
        /// <response code="200">Successfully deleted an existing CourseSource</response>
        /// <response code="400">Invalid CourseSource Id</response>
        /// <response code="500">CourseSource could not be removed</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCourseSource(Guid id)
        {
            try
            {
                var course = await _unitOfWork.CourseSourceRepository.GetCourseSourceByIdAsync(id);
                _unitOfWork.CourseSourceRepository.Delete(course);
                var result = await _unitOfWork.CourseSourceRepository.SaveAllAsync();
                return StatusCode(200);

            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
