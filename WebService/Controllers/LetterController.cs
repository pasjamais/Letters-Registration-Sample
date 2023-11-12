using DB_Interaction;
using DB_Interaction.Entities;
using DB_Interaction.Views;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.Metrics;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LetterController : ControllerBase
    {
        [HttpGet]
        public List<Letter_Caption> Get()
        {
            return DB_Agent.Get_Letter_Captions();
        }

        [HttpGet("{id}")]
        public dynamic Get(int id)
        {
            try
            {
                Letter letter = DB_Agent.Get_Letter_by_ID(id);
                letter.Sender = null!;
                letter.Addressee = null!;
                return letter;
            }
            catch (Exception e)
            {
                return e.Message;
            }

        }

        [HttpPost]
        public string Post([FromBody] Letter letter)
        {
            string message = $"\nThe letter have been sent to user {letter.Addressee.Name}.";
            letter.Sender = null!;
            letter.Addressee = null!;
            try
            {
                DB_Agent.Add_Letter(letter);
            }
            catch (Exception e)
            {
                return e.Message;
            }
            return message;
        }

    }
}
