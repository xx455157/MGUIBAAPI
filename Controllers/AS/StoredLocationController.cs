#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.vASM26;
using GUIStd.Attributes;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using GUICore.Web.Extensions;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 存放位置設定控制器
    /// </summary>
    [Route("as/[controller]")]
    public class StoredLocationController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlStoredLocation BlStoredLocation => new BlStoredLocation(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得資產存放位置清單（可選 Like 條件＋分頁；空白條件＝全檔）
        /// </summary>
        /// <param name="queryParams">查詢條件（storedLocation）</param>
        /// <param name="pageNo">頁次</param>
        /// <param name="rowsPerPage">每頁筆數（0 時由 SINI 取得）</param>
        /// <returns>存放位置分頁資料</returns>
        [HttpPost("getdata/{pageNo}")]
        public MdCode_p GetData(
            [FromBody] MdASM26_q queryParams,
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0)
        {
            return BlStoredLocation.GetData(
                queryParams,
                funcName: ControlName,
                pageNo: pageNo,
                rowsPerPage: rowsPerPage);
        }

        /// <summary>
        /// 判斷資產存放位置是否存在
        /// </summary>
        /// <param name="AQ01">資產存放位置</param>
        /// <returns></returns>
        [HttpGet("isexits/{AQ01}")]
        public bool isExists(string AQ01)
        {
            bool _result = BlStoredLocation.IsExists(AQ01);

            return _result;
        }

        /// <summary>
        /// 判斷資產存放位置是否存在AB
        /// </summary>
        /// <param name="AQ01">資產存放位置</param>
        /// <returns></returns>
        [HttpGet("isexitsByAB/{AQ01}")]
        public bool isExistsByAB(string AQ01)
        {
            bool _result = BlStoredLocation.IsExistsByAB(AQ01);

            return _result;
        }

        /// <summary>
        /// 取得存放位置輔助分頁資料（用於遠端搜尋）
        /// </summary>
        /// <param name="queryText">查詢文字</param>
        /// <param name="pageNo">頁次</param>
        /// <returns>存放位置分頁資料</returns>
        [HttpGet("help/{queryText}/pages/{pageNo}")]
        public MdCode_p GetHelpPaging(string queryText, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlStoredLocation.GetHelpPaging(queryText, ControlName, pageNo);
        }

        #endregion

        #region " 異動資料 "

        /// <summary>
        /// 刪除資產存放位置
        /// </summary>
        /// <param name="AQ01">資產存放位置</param>
        /// <returns></returns>
        [HttpDelete("delete/{AQ01}")]
        public MdApiMessage Delete(string AQ01)
        {
            try
            {
                int _result = BlStoredLocation.Delete(AQ01);
                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 新增資產存放位置
        /// </summary>
        /// <returns></returns>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdASM26_q obj)
        {
            try
            {
                int _result = BlStoredLocation.Insert(obj);

                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 更新資產存放位置
        /// </summary>
        /// <returns></returns>
        [HttpPost("update")]
        public MdApiMessage Update([FromBody] MdASM26_q obj)
        {
            try
            {
                int _result = BlStoredLocation.Update(obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
