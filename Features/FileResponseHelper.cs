using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;
using GUICore.Web.Extensions;

namespace MGUIBAAPI.Features
{
    internal static class FileResponseHelper
    {
        public static IActionResult SendFileOrUtf8Text(this ControllerBase controller, byte[] fileContent, string fileName)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));
            if (fileContent == null) throw new ArgumentNullException(nameof(fileContent));

            if (!IsMarkdownFile(fileName))
            {
                return controller.HttpContext.Response.SendFile(fileContent, fileName);
            }

            var text = Encoding.UTF8.GetString(RemoveUtf8Bom(fileContent));
            return controller.Content(text, "text/markdown; charset=utf-8", Encoding.UTF8);
        }

        public static IActionResult SendFileOrUtf8Text(this ControllerBase controller, Stream fileStream, string fileName)
        {
            if (controller == null) throw new ArgumentNullException(nameof(controller));
            if (fileStream == null) throw new ArgumentNullException(nameof(fileStream));

            if (!IsMarkdownFile(fileName))
            {
                var downloadStream = fileStream as MemoryStream;
                if (downloadStream == null)
                {
                    downloadStream = new MemoryStream();
                    if (fileStream.CanSeek)
                    {
                        fileStream.Position = 0;
                    }

                    fileStream.CopyTo(downloadStream);
                }

                if (downloadStream.CanSeek)
                {
                    downloadStream.Position = 0;
                }

                return controller.HttpContext.Response.SendFile(downloadStream, fileName);
            }

            if (fileStream.CanSeek)
            {
                fileStream.Position = 0;
            }

            using (var memoryStream = new MemoryStream())
            {
                fileStream.CopyTo(memoryStream);
                return controller.SendFileOrUtf8Text(memoryStream.ToArray(), fileName);
            }
        }

        private static bool IsMarkdownFile(string fileName)
        {
            var extension = Path.GetExtension(fileName ?? string.Empty);
            return extension.Equals(".md", StringComparison.OrdinalIgnoreCase)
                || extension.Equals(".markdown", StringComparison.OrdinalIgnoreCase);
        }

        private static byte[] RemoveUtf8Bom(byte[] fileContent)
        {
            if (fileContent.Length >= 3
                && fileContent[0] == 0xEF
                && fileContent[1] == 0xBB
                && fileContent[2] == 0xBF)
            {
                var contentWithoutBom = new byte[fileContent.Length - 3];
                Buffer.BlockCopy(fileContent, 3, contentWithoutBom, 0, contentWithoutBom.Length);
                return contentWithoutBom;
            }

            return fileContent;
        }
    }
}
