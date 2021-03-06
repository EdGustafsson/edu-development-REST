using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using edu_development_REST.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace edu_development_REST.Controllers
{

    /// <summary>
    /// Controller for working with the Course Memberships
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CourseMembershipController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseMembershipController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Fetches all Course Memberships.
        /// </summary>
        /// <response code="200">Returns all Course Memberships</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseMembership>>> GetCourseMemberships()
        {
            return Ok(await _unitOfWork.CourseMembershipRepository.GetCourseMembershipsAsync());
        }

        /// <summary>
        /// Fetches a CourseMembership based on the given Id.
        /// </summary>
        /// <param name="id">This is an Id of an existing CourseMembership</param>
        /// <response code="200">Returns the CourseMembership with the given Id</response>
        /// <response code="204">No CourseMembership with the given Id found </response>
        [HttpGet]
        [Route("findnumber/{id:guid}")]
        public async Task<ActionResult<CourseMembership>> GetCourseMembershipByIdAsync(Guid id)
        {
            try
            {
                var result = await _unitOfWork.CourseMembershipRepository.GetCourseMembershipByIdAsync(id);
                return StatusCode(200, result);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates a new CourseMembership, using the CourseMembershipViewModel.
        /// </summary>
        /// <param name="courseMembership">This is a new CourseMembershipViewModel object</param>
        /// <response code="201">Successfully created a new Course Membership</response>
        /// <response code="400">Failed to create a new Course Membership</response>
        [HttpPost()]
        public async Task<ActionResult> AddCourseMembership(CourseMembershipViewModel courseMembership)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(courseMembership.UserId);
                if (user == null) return BadRequest($"User Id could not be found");

                var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(courseMembership.CourseId);
                if (course == null) return BadRequest($"Course Id could not be found");

                _unitOfWork.CourseMembershipRepository.Add(courseMembership);
                await _unitOfWork.Complete();

                return StatusCode(201, courseMembership);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }


        }

        /// <summary>
        /// Updates properties on an existing Course Membership.
        /// </summary>
        /// <param name="id">This is an id of an existing Course Membership</param>
        /// <param name="updatedCourseMembership">This is the changes to the Course Membership</param>
        /// <response code="200">Successfully updated the Course Membership</response>
        /// <response code="400">Invalid Course Membership Id</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCourseMembership(Guid id, CourseMembershipViewModel updatedCourseMembership)
        {
            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(updatedCourseMembership.UserId);
                if (user == null) return BadRequest($"User Id could not be found");

                var course = await _unitOfWork.CourseRepository.GetCourseByIdAsync(updatedCourseMembership.CourseId);
                if (course == null) return BadRequest($"Course Id could not be found");

                _unitOfWork.CourseMembershipRepository.Update(updatedCourseMembership, id);
                await _unitOfWork.CourseRepository.SaveAllAsync();
                return StatusCode(201, updatedCourseMembership);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
