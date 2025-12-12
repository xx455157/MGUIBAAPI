#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUICore.Web.Models;
using GUIStd;
using GUIStd.BLL.GUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using MGUIBAAPI.Models.GUI;

#endregion

namespace MGUIBAAPI.Controllers.NetGUI
{
    /// <summary>
    /// 【需經驗證】附件檔的控制器
    /// </summary>
    [Route("netgui/[controller]")]
    public class AttachmentsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA79 BlA79 => new BlA79(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得程式交易資料的附件檔案清單
        /// </summary>
        /// <param name="obj">程式交易資料的 KEY 值模型物件</param>
        /// <returns>附件檔案清單模型物件</returns>
        [HttpPost("getfilelist")]
        public MdAttachments_l GetFileList([FromBody] MdAttachment obj)
        {
            return BlA79.GetRows(obj);
        }

        /// <summary>
        /// 下載檔案
        /// </summary>
        /// <param name="guid">附件檔案的唯一識別碼</param>
        /// <returns>檔案的資料流</returns>
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetFile(Guid guid)
        {
            byte[] _fileContent = null; string _fileName = null;

            try
            {
                await Task.Run(() => BlA79.GetRow(guid, out _fileContent, out _fileName));

                // 找不到附檔時，回傳錯誤訊息
                if (_fileContent == null) return BadRequest(HttpContext.Response.SendFileNotExistFailed());

                // 回傳檔案
                return HttpContext.Response.SendFile(_fileContent, _fileName);
            }
            catch (Exception ex)
            {
                return BadRequest(HttpContext.Response.SendFileFailed(ex));
            }
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增/刪除附件
        /// </summary>
        /// <param name="obj">附件檔的新增模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost()]
        public async Task<MdApiMessage> Insert([ModelBinder(typeof(FromFormModelBinder))] [FromForm] CmAttachments_i obj)
        {
            try
            {
                if ((obj.DelFiles == null || obj.DelFiles.ToList().Count(x => !string.IsNullOrWhiteSpace(x)) == 0) && obj.FormFiles.ToList().Count == 0)
				{
                    //至少須選擇一個檔案或是刪除檔案!
                    throw new Exception(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_MustSelectOneFileOrDelete"));
				} 
                else
				{
                    int _result = 0;

                    // 刪除不需要之檔案
                    if (obj.DelFiles != null && obj.DelFiles.ToList().Count(x => !string.IsNullOrWhiteSpace(x)) > 0)
                    {
                        foreach (string _guid in obj.DelFiles.ToList())
						{
                            // 呼叫商業元件執行刪除作業
                            _result += BlA79.ProcessDelete(Guid.Parse(_guid));
                        }
                    }

                    if (obj.FormFiles.ToList().Count>0)
					{
                        // 建立 BLL、DAL 層級使用的附件檔模型物件
                        var _model = new MdAttachments_i
                        {
                            // 將 FormFile 上傳檔案轉換成 Base64Image 模型清單，並放入附件檔模型物件中的 Files 屬性中
                            Files = await WebFunc.ConvertFormFileToBase64Image(
                                new MdFormData { FormFiles = obj.FormFiles.ToList() })
                        };

                        // 複寫 obj 屬性值給附件檔模型物件，當與附件檔模型物件有相同的屬性時
                        Common.ClonePropertiesValue(obj, _model);

                        // 呼叫商業元件執行新增作業
                        _result += BlA79.ProcessInsert(_model);

                    }


                    // 回應前端新增成功訊息
                    return HttpContext.Response.InsertSuccess(_result, "PgmMsg_UploadSuccess2");
                }

            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex, "PgmMsg_UpLoadFail");
            }
        }

        /// <summary>
        /// 刪除資料 (以程式交易資料的 KEY 值)
        /// </summary>
        /// <param name="obj">程式交易資料的 KEY 值模型物件</param>
        /// <returns>檔案的資料流</returns>
        [HttpDelete]
        public MdApiMessage Delete([FromBody] MdAttachment obj)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlA79.ProcessDelete(obj);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="guid">附件檔案的唯一識別碼</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{guid}")]
        public MdApiMessage Delete(Guid guid)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlA79.ProcessDelete(guid);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}
