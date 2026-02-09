#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;

using GUIStd.DAL.AllNewIV.Models.Private.vIVM92;
using GUIStd.BLL.AllNewIV.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Attributes;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// vMCF01 公司基本資料管理 程式資料控制器
    /// </summary>
    [Route("gui/private/[controller]")]
	public class vIVM92Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// BlMCF01 商業邏輯物件屬性
        /// </summary>
        private BlIVM92 BlIVM92 => mBlIVM92 = mBlIVM92 ?? new BlIVM92(ClientContent);
        private BlIVM92 mBlIVM92;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次的字軌基本資料
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryParams">查詢參數</param>
        /// <returns>分頁公司資料模型物件</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdIVM92QueryList_p GetData([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdIVM92Query queryParams)
        {
            return BlIVM92.GetData(queryParams,ControlName,pageNo);
        }

        /// <summary>
        /// 檢查字軌資料是否存在
        /// </summary>
        /// <param name="queryParams">查詢參數 (Year, InvoiceType, Period)</param>
        /// <returns>是否存在</returns>
        [HttpPost("isexist")]
        public bool IsExist([FromBody] MdIVM92Details queryParams)
        {
            return BlIVM92.IsExist(queryParams);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增字軌資料
        /// </summary>
        /// <param name="obj">字軌異動資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdIVM92Details obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlIVM92.ProcessInsert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改字軌資料
        /// </summary>
        /// <param name="obj">字軌異動資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut]
        public MdApiMessage Update([FromBody] MdIVM92Details obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlIVM92.ProcessUpdate(obj);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除字軌資料
        /// </summary>
        /// <param name="obj">字軌異動資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete]
        public MdApiMessage Delete([FromBody] MdIVM92Details obj)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlIVM92.ProcessDelete(obj);

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
