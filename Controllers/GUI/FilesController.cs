#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc; 
using System.IO;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GUI "

using MGUIBAAPI.Features;
using GUICore.Web;
using GUICore.Web.Models;
using GUICore.Web.Filters;
using GUICore.Web.Attributes;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Models;
using GUIStd.BLL.AllNewGUI.Private;
using GUICore.Web.Controllers;
using Microsoft.SqlServer.Server;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 檔案上傳控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class FilesController : GUIAppAuthController
    {
        #region " 私用函式 "


        #endregion

        #region " 公用函式 "

        /// <summary>
        /// 條列主機上傳目錄底下的圖檔
        /// </summary>
        /// <param name="photoPath">要條列圖片的根目錄路徑</param>
        /// <param name="minDate">目錄底下檔案最小修改日期</param>
        /// <param name="maxDate">目錄底下檔案最大修改日期</param>
        /// <returns></returns>
        [HttpGet("photosInfo")]
        public async Task<MdDirInfoForPhoto> GetPhotoInfo([RequiredFromQuery] string photoPath,
            [FromQuery] string minDate = null, [FromQuery] string maxDate = null)
        {
            return await new BlFormData().GetDirectory<MdDirInfoForPhoto, MdFileInfoForPhoto>(
                Path.Combine(Path.GetDirectoryName(Utils.AppSettings.ImageUploadPath), photoPath ?? ""),
                minEditDate: minDate, maxEditDate: maxDate);
        }

        /// <summary>
        /// 條列主機上傳目錄底下的資料夾內容
        /// </summary>
        /// <param name="lookupPath">要條列的根目錄路徑</param>
        /// <param name="minDate">目錄底下檔案最小修改日期</param>
        /// <param name="maxDate">目錄底下檔案最大修改日期</param>
        /// <returns></returns>
        [HttpGet("directoriesInfo")]
        public async Task<MdDirInfo> GetDirectoryInfo([RequiredFromQuery] string lookupPath, 
            [FromQuery] string minDate = null, [FromQuery] string maxDate = null)
        {
            return await new BlFormData().GetDirectory<MdDirInfo, MdFileInfo>(
                Path.Combine(Path.GetDirectoryName(Utils.AppSettings.ImageUploadPath), lookupPath ?? ""),
                minEditDate: minDate, maxEditDate: maxDate);
        }

        /// <summary>
        /// 使用表單資料模型傳遞參數，提供表單資料上傳、檔案上傳儲存的 API
        /// </summary>
        /// <param name="file">檔案清單模型物件</param>
        /// <param name="savePath">表單檔案上傳主機目錄的路徑</param>
        /// <param name="writeTime">強制設定檔案建檔、修改的時間</param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<MdApiMessage> UploadFiles([FromForm] MdFile file, 
            [RequiredFromQuery] string savePath, [FromQuery] string writeTime = null, [FromQuery] string delFileStr = null)
        {
            //計算檔案總數
            long _count = 0, _del = 0;

            //先刪除檔案
            if (string.IsNullOrWhiteSpace(delFileStr) == false)
            {
                var _delFiles = delFileStr.Split(';');
                await Common.RemoveFile(Path.Combine(Path.GetDirectoryName(Utils.AppSettings.ImageUploadPath), savePath ?? ""), _delFiles);
                _del += _delFiles.Length;
            }

            //寫入檔案
            var _savepath = Path.Combine(Path.GetDirectoryName(Utils.AppSettings.ImageUploadPath), savePath ?? "");
            if (file.FormFiles != null)
                foreach(var _file in file.FormFiles)
                {
                    //將FormFile轉換為Base64Image格式進行寫入
                    var _img = await WebFunc.IFormFile2Base64Image(_file);
                    await new BlFormData().FileSave(_img, _savepath, writeTime:writeTime);
                    _count++;
                }
                
            
            // 回傳上傳檔案總數及總長度
            return Response.SendSuccess($@"upload count: {_count}{(_del>0 ? $", delele count: {_del}" : "")}");
        }

        /// <summary>
        /// 刪除主機上傳目錄底下的檔案
        /// </summary>
        /// <param name="allNames">多個檔案名稱，以分號(;)區分</param>
        /// <param name="deletePath">要刪除檔案位於主機上船目錄下的路徑，例如:MenuOrder\GUI</param>
        /// <returns></returns>
        [HttpDelete()]
        public async Task<MdApiMessage> DeleteByName([RequiredFromQuery] string allNames, [RequiredFromQuery] string deletePath)
        {
            await Common.RemoveFile(
                Path.Combine(Path.GetDirectoryName(Utils.AppSettings.ImageUploadPath), deletePath ?? ""), allNames.Split(";"));
            return Response.SendSuccess($@"remove ok");
        }

        #endregion
    }

}

