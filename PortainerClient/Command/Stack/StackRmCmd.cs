using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;

namespace PortainerClient.Command.Stack
{
    /// <summary>
    /// CMD command for Stack remove operation
    /// </summary>
    [Command("rm", Description = "Remove stack from portainer and Docker Swarm")]
    public class StackRmCmd : BaseApiCommand<StacksApiService>
    {
        /// <summary>
        /// Stack identifier
        /// </summary>
        [Argument(0, "stackId", "Stack identifier")]
        [Required]
        public int StackId { get; set; }

        /// <inheritdoc />
        protected override void Do(CommandLineApplication app, IConsole console)
        {
            var proceed = Prompt.GetYesNo("Are you sure to do this action?", false);
            if (!proceed)
                return;

            console.Write("Sending remove request to Portainer...");
            ApiClient.RemoveStack(StackId);
            console.WriteLine("Removed!");
        }
    }
}
