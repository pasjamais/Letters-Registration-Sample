using DB_Interaction.Views;
using DB_Interaction;
using Microsoft.AspNetCore.Mvc;
using DB_Interaction.Entities;

namespace WebService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemController : ControllerBase
    {

        [HttpGet("{keyword}")]
        public string Get(string keyword)
        {
            string message;
            if (keyword == "refill")
            {
                message = $"\nThe database is initialized with sample values.";
                try
                {
                    DB_Agent.Initialize_BD_with_Default_Values();
                }
                catch (Exception e)
                {
                    return e.Message;
                }
                return message;
            }
            else if (keyword == "competely_new")
            {
                message = $"\nThe database has been recreated.";
                try
                {
                    DB_Agent.RecreateDB();
                }
                catch (Exception e)
                {
                    return e.Message;
                }
                return message;
            }
            else
                return "\nWrong request";
        }

    }
}
