using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace PortainerClient.Api.Base;

/// <inheritdoc />
public class TraceRequest : RestRequest
{
    #region Properties

    private readonly bool _debug;

    #endregion

    #region Constructor

    /// <inheritdoc />
    public TraceRequest(string pResource, bool debug = false)
        : base(pResource)
    {
        _debug = debug;
        InitializeLogs();
    }

    /// <inheritdoc />
    public TraceRequest(string pResource, Method method, bool debug = false) : base(pResource, method)
    {
        _debug = debug;
        InitializeLogs();
    }

    #endregion

    #region Methods

    private void InitializeLogs()
    {
        if (!_debug)
            return;
        OnBeforeRequest = OnBeforeRequestMethod;
        OnAfterRequest = OnAfterRequestMethod;
    }

    private ValueTask OnBeforeRequestMethod(HttpRequestMessage pMessage)
    {
        var builder = new StringBuilder();

        builder.AppendLine("------------------------------");
        builder.AppendLine($"REQUEST [{pMessage.Method}] {pMessage.RequestUri}");

        foreach (var header in pMessage.Headers)
        {
            builder.AppendLine($"  {header.Key}: {string.Join(';', header.Value)}");
        }

        var stream = pMessage.Content?.ReadAsStream();

        ReadStream(stream, builder);


        builder.AppendLine("------------------------------");

        var content = builder.ToString();

        Console.WriteLine(content);

        return ValueTask.CompletedTask;
    }

    private void ReadContent(HttpContent? pContent, StringBuilder pBuilder)
    {
        if (pContent == null)
        {
            return;
        }

        foreach (var header in pContent.Headers)
        {
            pBuilder.AppendLine($"  {header.Key}: {string.Join(';', header.Value)}");
        }

        ReadContent(pContent as StreamContent, pBuilder);
        ReadContent(pContent as StringContent, pBuilder);
        ReadContent(pContent as MultipartFormDataContent, pBuilder);

        Console.WriteLine();
    }

    private void ReadContent(MultipartFormDataContent? pContent, StringBuilder pBuilder)
    {
        if (pContent == null) return;
        foreach (var content in pContent)
        {
            pBuilder.AppendLine();
            ReadContent(content, pBuilder);
        }
    }

    private static void ReadContent(StreamContent? pContent, StringBuilder pBuilder)
    {
        if (pContent == null) return;
        var stream = pContent.ReadAsStream();
        pBuilder.AppendLine($" contains {stream.Length} bytes");
    }

    private void ReadContent(StringContent? pContent, StringBuilder pBuilder)
    {
        if (pContent == null) return;
        var stream = pContent.ReadAsStream();
        pBuilder.Append("  ");
        ReadStream(stream, pBuilder);
    }

    private static void ReadStream(Stream? pStream, StringBuilder pBuilder)
    {
        if (pStream == null)
        {
            return;
        }

        var index = 0L;
        var length = pStream.Length;
        var buffer = new byte[1024];

        while (index < length - 1)
        {
            var read = pStream.Read(buffer, 0, 1024);
            var result = Encoding.UTF8.GetString(buffer, 0, read);

            pBuilder.Append(result);

            index += read;
        }

        pBuilder.AppendLine();

        pStream.Seek(0L, SeekOrigin.Begin);
    }

    private ValueTask OnAfterRequestMethod(HttpResponseMessage pMessage)
    {
        var builder = new StringBuilder();

        builder.AppendLine("------------------------------");
        builder.AppendLine(
            $"RESPONSE {pMessage.RequestMessage.Method} [{pMessage.RequestMessage.RequestUri}] {pMessage.StatusCode}");

        foreach (var header in pMessage.Headers)
        {
            builder.AppendLine($"  {header.Key}: {string.Join(';', header.Value)}");
        }

        var stream = pMessage.Content.ReadAsStream();

        ReadStream(stream, builder);

        builder.AppendLine("------------------------------");

        var content = builder.ToString();

        Console.WriteLine(content);

        return ValueTask.CompletedTask;
    }

    #endregion
}
