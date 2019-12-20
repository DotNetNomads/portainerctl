using System.ComponentModel.DataAnnotations;
using IO.Swagger.Api;
using McMaster.Extensions.CommandLineUtils;

namespace PortainerClient.Command.Stack
{
    [Command("rm", Description = "Remove stack from portainer and Docker Swarm")]
    public class StackRmCmd : BaseApiCommand<StacksApi>
    {
        [Argument(0, "stackId", "Stack identifier")]
        [Required]
        public int StackId { get; set; }

        public override void Do(CommandLineApplication app, IConsole console)
        {
            var proceed = Prompt.GetYesNo("Are you sure to do this action?", false);

            if (!proceed)
            {
                return;
            }

            console.Write("Sending remove request to Portainer...");
            ApiClient.StackDelete(StackId, null, null);
            console.WriteLine("Removed!");
        }
    }
}