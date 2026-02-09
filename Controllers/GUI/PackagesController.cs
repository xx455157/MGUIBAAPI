#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewGUI.Models.Share.A28;
using GUIStd.Models;
using GUICore.Web.Extensions;
using System;


#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 專案資料資控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class PackagesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA28 BlA28 => new BlA28(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得查詢資料
        /// </summary>
        /// <param name="request">請求參數</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <returns></returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdPackages_p GetData([FromBody] MdPackages_q request, int pageNo, int rowsPerPage = 0)
        {
            return BlA28.GetData(request, pageNo, ControlName, ref rowsPerPage);
        }

        /// <summary>
        /// 取得單筆資料
        /// </summary>
        /// <param name="packageId">專案編號</param>
        /// <returns></returns>
        [HttpGet("data/{packageId}")]
        public MdPackages GetRow(string packageId)
        {
            return BlA28.GetRow(packageId);
        }

        /// <summary>
        /// 新增單筆資料
        /// </summary>
        /// <param name="request">請求參數</param>
        /// <returns></returns>
        [HttpPost()]
        public MdApiMessage Insert([FromBody] MdPackages_w request)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlA28.Insert(request, out string packageId);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result, responseData: packageId);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改單筆資料
        /// </summary>
        /// <param name="request">請求參數</param>
        /// <returns></returns>
        [HttpPut()]
        public MdApiMessage Update([FromBody] MdPackages_w request)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlA28.Update(request);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除單筆資料
        /// </summary>
        /// <param name="packageId">專案編號</param>
        /// <returns></returns>
        [HttpDelete("{packageId}")]
        public MdApiMessage Delete(string packageId)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlA28.Delete(packageId);

                // 回應前端新增成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}
