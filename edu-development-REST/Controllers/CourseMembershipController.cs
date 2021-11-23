using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using edu_development_REST.ViewModels;

namespace edu_development_REST.Controllers
{

    /// <summary>
    /// Controller for working with the Course Memberships
    /// </summary>
    [Route("api/coursemembership")]
    [ApiController]
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
        /// Creates a new CourseMembership, using the CourseMembershipViewModel.
        /// </summary>
        /// <param name="courseMembership">This is a new CourseMembershipViewModel object</param>
        /// <response code="201">Successfully created a new Course Membership</response>
        /// <response code="400">Failed to create a new Course Membership</response>
        [HttpPost()]
        public async Task<ActionResult> AddCourseMembership(CourseMembershipViewModel courseMembership)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(courseMembership.UserId);
            if (user == null) return BadRequest($"Användaren kunde inte hittas");

            var course = await _unitOfWork.CourseRepository.GetCourseByCourseCodeAsync(courseMembership.CourseId);
            if (course == null) return BadRequest($"Kursen kunde inte hittas");

            _unitOfWork.CourseMembershipRepository.Add(courseMembership);

            if (await _unitOfWork.Complete())
            {
                return StatusCode(201);
            }

            return StatusCode(500, "Det gick inte att spara använvaden");
        }

        /// <summary>
        /// Updates properties on an existing post.
        /// </summary>
        /// <param name="id">This is an id of an existing Course Membership</param>
        /// <param name="updatedCourse">This is the changes to the Course Membership</param>
        /// <response code="200">Successfully updated the Course Membership</response>
        /// <response code="400">Invalid Course Membership Id</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCourseMembership(Guid id, CourseMembershipViewModel updatedCourse)
        {
            _unitOfWork.CourseMembershipRepository.Update(updatedCourse, id);
            if (await _unitOfWork.CourseRepository.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Det gick inte att uppdatera kursen");
        }
    }
}
