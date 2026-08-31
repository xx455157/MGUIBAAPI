#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI.Private;
using GUIStd.DAL.AllNewGUI.Models.Private.GUI.CustomerSupplier;
using GUIStd.Models;
using GUICore.Web.Attributes;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// CustomerSupplier 客戶/廠商共用 REST API 程式資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class CustomerSupplierController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>BlCustomerSupplier 商業邏輯物件屬性</summary>
        private BlCustomerSupplier BlCustomerSupplier =>
            mBlCustomerSupplier = mBlCustomerSupplier ?? new BlCustomerSupplier(ClientContent);
        private BlCustomerSupplier mBlCustomerSupplier;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得查詢資料（分頁）
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryParams">查詢條件模型物件</param>
        /// <param name="actType">作業類型（Customer：客戶；Supplier：廠商）</param>
        /// <param name="rowsPerPage">每頁筆數（0 表示使用 SINI 設定值）</param>
        /// <returns>分頁查詢結果模型物件</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdCustomerSupplier_l_p GetQueryList([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdCustomerSupplier_q queryParams, [RequiredFromQuery] string actType, int rowsPerPage = 0)
        {
            return BlCustomerSupplier.GetQueryList(queryParams, actType, pageNo, rowsPerPage);
        }

        /// <summary>
        /// 取得單筆明細資料
        /// </summary>
        /// <param name="customerId">客戶/廠商代碼</param>
        /// <param name="actType">作業類型（Customer：客戶；Supplier：廠商）</param>
        /// <returns>明細資料模型物件</returns>
        [HttpGet("query/detail/{customerId}")]
        public MdCustomerSupplier_d GetDetailData(string customerId, [RequiredFromQuery]  string actType)
        {
            return BlCustomerSupplier.GetDetailData(customerId, actType);
        }

        /// <summary>
        /// 取得明細頁 UI 選項資料
        /// </summary>
        /// <param name="actType">作業類型（Customer：客戶；Supplier：廠商）</param>
        /// <returns>明細頁輔助資料模型物件</returns>
        [HttpGet("query/uidata/d")]
        public MdCustomerSupplier_h_d GetUIData([RequiredFromQuery] string actType)
        {
            return BlCustomerSupplier.GetUIData(actType);
        }

        /// <summary>
        /// 同類型主鍵存在檢查
        /// </summary>
        /// <param name="customerId">客戶/廠商代碼</param>
        /// <param name="actType">作業類型（Customer：客戶；Supplier：廠商）</param>
        /// <returns>主鍵是否已存在</returns>
        [HttpGet("exists/{customerId}")]
        public bool IsExist(string customerId, [RequiredFromQuery] string actType)
        {
            return BlCustomerSupplier.IsExist(customerId);
        }

        ///// <summary>
        ///// 跨類型主鍵衝突檢查
        ///// </summary>
        ///// <param name="customerId">客戶/廠商代碼</param>
        ///// <param name="actType">作業類型（Customer：客戶；Supplier：廠商）</param>
        ///// <returns>是否與另一類型（客戶/廠商）主鍵衝突</returns>
        //[HttpGet("exists/{customerId}/crosstype")]
        //public bool IsExistCrossType(string customerId, [RequiredFromQuery] string actType)
        //{
        //    return BlCustomerSupplier.IsExistCrossType(customerId, actType);
        //}

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增客戶/廠商資料
        /// </summary>
        /// <param name="obj">寫入資料模型物件</param>
        /// <param name="actType">作業類型（Customer：客戶；Supplier：廠商）</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdCustomerSupplier_w obj, [RequiredFromQuery] string actType)
        {
            try
            {
                int _result = BlCustomerSupplier.ProcessInsert(obj, actType);
                return HttpContext.Response.InsertSuccess(
                    affectedRows: _result,
                    responseData: BlCustomerSupplier.GetQueryViewRow(obj.A1601, actType)
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改客戶/廠商資料
        /// </summary>
        /// <param name="obj">寫入資料模型物件</param>
        /// <param name="actType">作業類型（Customer：客戶；Supplier：廠商）</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut]
        public MdApiMessage Update([FromBody] MdCustomerSupplier_w obj, [RequiredFromQuery] string actType)
        {
            try
            {
                int _result = BlCustomerSupplier.ProcessUpdate(obj, actType);
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: _result,
                    responseData: BlCustomerSupplier.GetQueryViewRow(obj.A1601, actType)
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除客戶/廠商資料
        /// </summary>
        /// <param name="customerId">客戶/廠商代碼</param>
        /// <param name="actType">作業類型（Customer：客戶；Supplier：廠商）</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{customerId}")]
        public MdApiMessage Delete(string customerId, [RequiredFromQuery] string actType)
        {
            try
            {
                int _result = BlCustomerSupplier.ProcessDelete(customerId, actType);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}
