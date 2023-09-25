using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using McMaster.Extensions.CommandLineUtils;
using PortainerClient.Api;

namespace PortainerClient.Command.Stack
{
    /// <summary>
    /// CMD command for Stack file inspect operation
    /// </summary>
    [Command(Name = "getfile", Description = "Get compose file of stack")]
    public class StackGetFileCmd : BaseApiCommand<StacksApiService>
    {
        /// <summary>
        /// Stack identifier
        /// </summary>
        [Argument(0, "stackId", "Stack instance identifier")]
        [Required]
        public int StackId { get; set; }

        /// <inheritdoc />
        protected override void Do(CommandLineApplication app, IConsole console)
        {
            var data = ApiClient.GetStackFile(StackId);
            Debug.Assert(data != null, nameof(data) + " != null");
            console.Write(data);
        }
    }
}
