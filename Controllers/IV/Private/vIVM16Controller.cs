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
using GUIStd.DAL.AllNewIV.Models.Private.vIVM16;
using GUIStd.DAL.AllNewIV.Models;
using GUIStd.DAL.AllNewIV.DAO.Private;

#endregion

namespace MGUIBAAPI.Controllers.IV
{
    /// <summary>
    /// vMCF01 公司基本資料管理 程式資料控制器
    /// </summary>
    [Route("iv/private/[controller]")]
	public class vIVM16Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlIVM16 BlIVM16 => mBlIVM16 = mBlIVM16 ?? new BlIVM16(ClientContent);
        private BlIVM16 mBlIVM16;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        ///  取得畫面選項資料
        /// </summary>
        /// <returns></returns>
        [HttpGet("query/uidata")]
        public MdIVM16UIData GetUIData()
        {
            return BlIVM16.GetUIData();
        }

        /// <summary>
        ///  取得 明細 大部門 選項資料
        /// </summary>
        /// <param name="company">公司別</param>
        /// <returns></returns>
        [HttpGet("query/detail/majordepts")]
        public IEnumerable<MdCode> GetDetailMajorDepts([FromQuery] string company="")
        {
            return BlIVM16.GetDetailMajorDepts(company);
        }

        /// <summary>
        /// 取得 明細 配號部門 選項資料
        /// </summary>
        /// <param name="company">公司別</param>
        /// <param name="majordept">大部門</param>
        /// <returns></returns>
        [HttpGet("query/detail/assignnumberdepts")]
        public IEnumerable<MdCode> GetDetailAssignNumberDepts([FromQuery] string company="", [FromQuery] string majordept="")
        {
            return BlIVM16.GetDetailAssignNumberDepts(company, majordept);
        }

        /// <summary>
        /// 取得分頁頁次的發票設定列表資料
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryParams">查詢參數</param>
        /// <returns>分頁發票設定資料模型物件</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdIVM16QueryList_p GetQueryData([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdIVM16Query queryParams)
        {
            return BlIVM16.GetQueryData(queryParams, ControlName, pageNo);
        }

        /// <summary>
        /// 取得查詢資料
        /// </summary>
        /// <param name="obj">查詢參數</param>
        /// <returns></returns>
        [HttpPost("query/getviewrow")]
        public MdIVM16QueryList GetViewRow(MdInvoiceTrackPkey obj)
        {
            return BlIVM16.GetViewRow(obj);
        }

        /// <summary>
        /// 檢查發票設定是否存在
        /// </summary>
        /// <param name="obj">查詢參數</param>
        /// <returns>是否存在</returns>
        [HttpPost("query/isexist")]
        public bool IsExist([FromBody] MdInvoiceTrackPkey obj)
        {
            return BlIVM16.IsExist(obj);
        }

        /// <summary>
        /// 檢查字軌設定是否存在
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPost("query/isexist/ivprefix")]
        public bool IsExistIVPrifix([FromBody] MdCheckIVPrifix obj)
        {
            return BlIVM16.IsExistIVPrifix(obj);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增字軌資料
        /// </summary>
        /// <param name="obj">字軌異動資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdInvoiceTrack obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlIVM16.ProcessInsert(obj);
                // 回應前端新增成功訊息
                //return HttpContext.Response.InsertSuccess(_result);
                return HttpContext.Response.InsertSuccess(
                    affectedRows: _result,
                    responseData: BlIVM16.GetViewRow(obj)
                );
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
        public MdApiMessage Update([FromBody] MdInvoiceTrack obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlIVM16.ProcessUpdate(obj);
                // 回應前端修改成功訊息
                //return HttpContext.Response.UpdateSuccess(_result);
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: _result,
                    responseData: BlIVM16.GetViewRow(obj)
                );
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
        public MdApiMessage Delete([FromBody] MdInvoiceTrackPkey obj)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlIVM16.ProcessDelete(obj);

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
