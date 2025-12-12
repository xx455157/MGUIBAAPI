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

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 【需經授權】表單資料上傳控制器
    /// </summary>
    [Route("gui/[controller]")]
    [WSAuthActionFilter]
    [ApiController]
    public class FormDataController : ControllerBase
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
        /// <param name="formData">表單資料模型物件</param>
        /// <param name="savePath">表單檔案上傳主機目錄的路徑</param>
        /// <param name="writeTime">強制設定檔案建檔、修改的時間</param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<MdApiMessage> UploadByModel([FromForm] MdFormData formData, 
            [RequiredFromQuery] string savePath, [FromQuery] string writeTime = null)
        {
            //透過底層函式將FormData轉換成Base64Image模型清單
            var _imgList = await WebFunc.ConvertFormFileToBase64Image(formData);

            //計算檔案總數
            long _count = 0;
            var _savepath = Path.Combine(Path.GetDirectoryName(Utils.AppSettings.ImageUploadPath), savePath ?? "");
            foreach(var img in _imgList)
            {
                await new BlFormData().FileSave(img, _savepath, writeTime:writeTime);
                _count++;
            }
            
            // 回傳上傳檔案總數及總長度
            return Response.SendSuccess($@"upload count: {_count}");
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

