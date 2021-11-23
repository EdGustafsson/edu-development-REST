using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using edu_development_REST.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace edu_development_REST.Controllers
{
    /// <summary>
    /// Controller for working with the Users
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all Users.
        /// </summary>
        /// <response code="200">Returns all Users</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _unitOfWork.UserRepository.GetUsersAsync());
        }

        /// <summary>
        /// Fetches a User based on the given id.
        /// </summary>
        /// <param name="id">This is an id of an existing User</param>
        /// <response code="200">Returns the User with the given Id</response>
        /// <response code="404">No User with the given Id found </response>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            return Ok(await _unitOfWork.UserRepository.GetUserByIdAsync(id));
        }

        /// <summary>
        /// Creates a new user, using the UserViewModel.
        /// </summary>
        /// <param name="model">This is a new UserViewModel object</param>
        /// <response code="201">Successfully created a new User</response>
        /// <response code="400">Failed to create a new User</response>
        [HttpPost()]
        public async Task<ActionResult> AddUser(UserViewModel model)
        {
            _unitOfWork.UserRepository.Add(model);
            if (await _unitOfWork.Complete())
            {
                return StatusCode(201);
            }
            return StatusCode(500, "Det gick inte att spara ner ny användare");
        }

        /// <summary>
        /// Deletes an existing User based on id. 
        /// </summary>
        /// <param name="id">This is an id of an existing User</param>
        /// <response code="200">Successfully deleted an existing User</response>
        /// <response code="400">Invalid User Id</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            if (user == null) return NotFound($"Hittade ingen användare med id: {id}");

            _unitOfWork.UserRepository.Delete(user);

            if (await _unitOfWork.UserRepository.SaveAllAsync()) return NoContent();
            return StatusCode(500, "Det gick inte att ta bort användaren");
        }

        /// <summary>
        /// Updates properties of an existing User.
        /// </summary>
        /// <param name="id">This is an id of an existing User</param>
        /// <param name="updatedUser">This is the changes to the User</param>
        /// <response code="200">Successfully updated the User</response>
        /// <response code="400">Invalid User Id</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, UserViewModel updatedUser)
        {

            _unitOfWork.UserRepository.Update(updatedUser, id);

            if (await _unitOfWork.UserRepository.SaveAllAsync()) return NoContent();
            return StatusCode(500, "Det gick inte att uppdatera användaren");
        }

    }

}
