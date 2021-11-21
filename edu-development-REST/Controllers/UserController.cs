using edu_development_REST.Entities;
using edu_development_REST.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace edu_development_REST.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            return Ok(await _unitOfWork.UserRepository.GetUsersAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(Guid id)
        {
            return Ok(await _unitOfWork.UserRepository.GetUserByIdAsync(id));
        }

        [HttpPost()]
        public async Task<ActionResult> AddUser(User user)
        {
            _unitOfWork.UserRepository.Add(user);
            if (await _unitOfWork.Complete())
            {
                return StatusCode(201);
            }
            return StatusCode(500, "Det gick inte att spara ner ny användare");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(Guid id)
        {
            var user = await _unitOfWork.UserRepository.GetUserByIdAsync(id);

            if (user == null) return NotFound($"Hittade ingen användare med id: {id}");

            _unitOfWork.UserRepository.Delete(user);

            if (await _unitOfWork.UserRepository.SaveAllAsync()) return NoContent();
            return StatusCode(500, "Det gick inte att ta bort användaren");
        }

        [HttpPatch("{id}")]
        public async Task<ActionResult> UpdateUser(Guid id, User updatedUser)
        {

            _unitOfWork.UserRepository.Update(updatedUser, id);

            if (await _unitOfWork.UserRepository.SaveAllAsync()) return NoContent();
            return StatusCode(500, "Det gick inte att uppdatera användaren");
        }

    }

}
