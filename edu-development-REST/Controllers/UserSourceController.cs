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
    /// Controller for working with the UserSources
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class UserSourceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserSourceController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Fetches all UserSources.
        /// </summary>
        /// <response code="200">Returns all UserSources</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserSource>>> GetUserSources()
        {
            return Ok(await _unitOfWork.UserSourceRepository.GetUserSourcesAsync());
        }

        /// <summary>
        /// Fetches a UserSource based on the given id.
        /// </summary>
        /// <param name="id">This is an id of an existing UserSource</param>
        /// <response code="200">Returns the UserSource with the given Id</response>
        /// <response code="404">No UserSource with the given Id found </response>
        [HttpGet("{id}")]
        public async Task<ActionResult<UserSource>> GetUserSourceById(Guid id)
        {
            try
            {
                var result = await _unitOfWork.UserSourceRepository.GetUserSourceByIdAsync(id);
                return StatusCode(200, result);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Creates a new user, using the UserSourceViewModel.
        /// </summary>
        /// <param name="model">This is a new UserSourceViewModel object</param>
        /// <response code="201">Successfully created a new UserSource</response>
        /// <response code="400">Failed to create a new UserSource</response>
        [HttpPost()]
        public async Task<ActionResult> AddUserSource(UserSourceViewModel model)
        {
            try
            {
                _unitOfWork.UserSourceRepository.Add(model);
                await _unitOfWork.Complete();
                return StatusCode(201, model);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Deletes an existing UserSource based on id. 
        /// </summary>
        /// <param name="id">This is an id of an existing UserSource</param>
        /// <response code="200">Successfully deleted an existing UserSource</response>
        /// <response code="400">Invalid UserSource Id</response>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUserSource(Guid id)
        {

            try
            {
                var userSource = await _unitOfWork.UserSourceRepository.GetUserSourceByIdAsync(id);
                _unitOfWork.UserSourceRepository.Delete(userSource);
                var result = await _unitOfWork.UserSourceRepository.SaveAllAsync();
                return StatusCode(200);

            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// Updates properties of an existing UserSource.
        /// </summary>
        /// <param name="id">This is an id of an existing UserSource</param>
        /// <param name="updatedUserSource">This is the changes to the UserSource</param>
        /// <response code="200">Successfully updated the UserSource</response>
        /// <response code="400">Invalid UserSource Id</response>
        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUserSource(Guid id, UserSourceViewModel updatedUserSource)
        {
            try
            {
                _unitOfWork.UserSourceRepository.Update(updatedUserSource, id);
                await _unitOfWork.UserSourceRepository.SaveAllAsync();
                return StatusCode(200, updatedUserSource);
            }
            catch (ValidationException e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
