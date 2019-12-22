using System.ComponentModel.DataAnnotations;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;
using YamlDotNet.Serialization;

namespace PortainerClient.Command.Stack
{
    /// <summary>
    /// CMD operation for Stack inspect operation
    /// </summary>
    [Command("inspect", Description = "Show information about stack")]
    public class StackInspectCmd : BaseApiCommand<StacksApiService>
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
            var stackInfo = ApiClient.GetStackInfo(StackId);
            console.WriteLine(new Serializer().Serialize(stackInfo));
        }
    }
}
