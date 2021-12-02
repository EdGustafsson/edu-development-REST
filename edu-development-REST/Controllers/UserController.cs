using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using edu_development_REST.ViewModels;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace edu_development_REST.Controllers
{
    /// <summary>
    /// Controller for working with the Users
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
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
            try
            {
                var result = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
                return StatusCode(200, result);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
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
            try
            {
                _unitOfWork.UserRepository.Add(model);
                await _unitOfWork.Complete();
                return StatusCode(201, model);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
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

            try
            {
                var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);
                _unitOfWork.UserRepository.Delete(user);
                var result = await _unitOfWork.UserRepository.SaveAllAsync();
                return StatusCode(200);

            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
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
            try
            {
                _unitOfWork.UserRepository.Update(updatedUser, id);
                await _unitOfWork.UserRepository.SaveAllAsync();
                return StatusCode(200, updatedUser);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
