using DB_Interaction;
using DB_Interaction.Entities;
using DB_Interaction.Views;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public List<User_Caption> Get()
        {
            return DB_Agent.Get_User_Captions();
        }

        [HttpGet("{id}")]
        public User Get(int id)
        {
            return DB_Agent.Get_User_by_ID(id);
        }

        [HttpPost]
        public string Post([FromBody] User user)
        {
            string message = $"\nThe user {user.Name} with mail {user.Email_Address} has been added.";
            try
            {
                DB_Agent.Add_User(user);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return message;
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] User user)
        {
        }

        [HttpDelete("{id}")]
        public string Delete(int id)
        {
            string message = $"\nUser with id {id} has been deleted.";
            try
            {
                DB_Agent.Delete_User(id);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return message;
        }
    }
}
