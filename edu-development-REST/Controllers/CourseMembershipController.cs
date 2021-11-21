using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace edu_development_REST.Controllers
{
    [Route("api/coursemembership")]
    [ApiController]
    public class CourseMembershipController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CourseMembershipController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CourseMembership>>> GetCourseMemberships()
        {
            return Ok(await _unitOfWork.CourseMembershipRepository.GetCourseMembershipsAsync());
        }

        [HttpPost()]
        public async Task<ActionResult> AddCourseMembership(CourseMembership courseMembership)
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

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateCourseMembership(Guid id, CourseMembership updatedCourse)
        {
            _unitOfWork.CourseMembershipRepository.Update(updatedCourse, id);
            if (await _unitOfWork.CourseRepository.SaveAllAsync()) return NoContent();

            return StatusCode(500, "Det gick inte att uppdatera kursen");
        }
    }
}
