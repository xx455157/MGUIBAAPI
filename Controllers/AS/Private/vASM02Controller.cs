#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.BLL.AllNewAS;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models;
using GUIStd.DAL.AllNewAS.DAO;
using GUIStd.DAL.AllNewAS.Models.Private.vASM02;
using GUIStd.Models;
using System;
using GUIStd;


#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASM02 固定資產購入 控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASM02Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlASM02 BlASM02 => new BlASM02(ClientContent);
        private BlAA BlAA => new BlAA(ClientContent);
        private BlAC BlAC => new BlAC(ClientContent);
        private BlAG BlAG => new BlAG(ClientContent);
        private BlSINI BlSINI=> new BlSINI(ClientContent);


        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("paged/{companyId}")]
        public MdASM02_dh GetDPageHelp(string companyId)
        {
            return BlASM02.GetDPageHelp(companyId);
        }

        /// <summary>
        /// 查詢頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("help/companies")]
        public MdASM02_h GetCompanies()
        {
            return BlASM02.GetCompanies();
        }

        /// <summary>
        /// 查詢固定資產購入清單
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>查詢結果（含分頁資訊）</returns>
        [HttpPost("qGetList/{companyId}/pages/{pageNo}")]
        public MdASM02_QueryResult Q_GetList(string companyId, int pageNo, [FromBody] MdASM02_QueryParams queryParams, int rowsPerPage = 0)
        {
            // 建立查詢條件
            var _query = new MdASM02_q
            {
                AC01_Value = companyId,
                AC04_S = queryParams.PurchaseNoStart ?? "",
                AC04_E = queryParams.PurchaseNoEnd ?? "",
                AC07_S = queryParams.AssetNoStart ?? "",
                AC07_E = queryParams.AssetNoEnd ?? "",
                AC08_S = queryParams.PurchaseDateStart ?? "",
                AC08_E = queryParams.PurchaseDateEnd ?? "",
                AA04_Text = queryParams.AssetName ?? "",
                AC16_Value = queryParams.SupplierId ?? "",
                AA26_Value = queryParams.AssetCategory ?? "",
                AA19_Value = queryParams.AssetAccount ?? "",
                AA21_Value = queryParams.DepreciationExpenseAccount ?? "",
                AC19_Text = queryParams.PurchaseReason ?? "",
                DataType = queryParams.DataType ?? ""
            };

            // 調用 BLL 層查詢
            return BlASM02.Q_GetList(_query, pageNo, ControlName, ref rowsPerPage);
        }

        /// <summary>
        /// 查詢財產編號是否存在其他異動資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="assetNo">財產編號</param>
        /// <returns>是否被使用</returns>
        [HttpGet("isExisTXRecord/{companyId}/{assetNo}")]
        public bool IsExisTXRecord(string companyId, string assetNo)
        {
            return BlASM02.IsExisTXRecord(companyId, assetNo);
        }

        /// <summary>
        /// 取得購入單的所有明細
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="docType">單據別</param>
        /// <param name="txType">異動別</param>
        /// <param name="purchaseNo">購入單號</param>
        /// <returns>固定資產購入明細資料模型</returns>
        [HttpGet("ac/details/{companyId}/{docType}/{txType}/{purchaseNo}")]
        public MdApiMessage GetACDetails(string companyId, string docType, string txType, string purchaseNo)
        {
            try
            {
                var _data = BlASM02.D_GetDetail(companyId, docType, txType, purchaseNo);
                
                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetDetailsSuccess"),
                    _data
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_GetDetailsFailed"),
                    ex
                );
            }
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增購入單明細
        /// </summary>
        /// <param name="obj">新增購入單明細請求模型</param>
        /// <returns>操作結果</returns>
        [HttpPost("processInsert")]
        public MdApiMessage ProcessInsert([FromBody] MdASM02_i obj)
        {
            try
            {
                // 新增資料（同時處理 AC 表和 AA 表）
                // 返回結果包含自動生成的 AC05 項次和可能的錯誤信息
                var _result = BlASM02.Insert(obj);

                // 檢查是否成功
                if (!_result.IsSuccess)
                {
                    // 返回業務邏輯錯誤（AA已存在或AC已存在）
                    return HttpContext.Response.SendFailed(_result.ErrorMessage);
                }

                // 將自動生成的 AC05 項次回傳到前端
                return HttpContext.Response.InsertSuccess(
                    affectedRows: _result.RowsAffected,
                    //回傳資料
                    responseData: new 
                    { 
                        serial = _result.AC05,        //  AC05 項次（前端用 serial）
                        purchaseNo = _result.AC04,    //  購入單號（前端用 purchaseNo）
                        assetNo = _result.AC07        //  資產編號（前端用 assetNo）
                    }
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 更新購入單明細
        /// </summary>
        /// <param name="obj">新增購入單明細請求模型</param>
        /// <returns>操作結果</returns>
        [HttpPut("processUpdate")]
        public MdApiMessage ProcessUpdate([FromBody] MdASM02_i obj)
        {
            try
            {
                // 更新資料（傳遞原始 AC37 和 AC09 給 BLL）
                int _result = BlASM02.Update(obj, obj.OriginalAC37, obj.OriginalAC09);

                //  只返回影響筆數（前端已有所有數據）
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除購入單的所有財產資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="noteType">單據別</param>
        /// <param name="txType">異動別</param>
        /// <param name="purchaseNo">購入單號</param>
        /// <returns>操作結果</returns>
        [HttpDelete("ac/delete/{companyId}/{noteType}/{txType}/{purchaseNo}")]
        public MdApiMessage ProcessDelete(string companyId,string noteType, string txType, string purchaseNo)
        {
            try
            {
                int _result = BlASM02.ProcessDelete(companyId, noteType, txType, purchaseNo);

                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 刪除單筆購入單的財產資料
        /// </summary>
        /// <param name="obj">刪除購入記錄請求模型</param>
        /// <returns>操作結果</returns>
        [HttpPost("ac/deleteBySingle")]
        public MdApiMessage ProcessDeletebySingle([FromBody] MdASM02_del obj)
        {
            try
            {
                // 調用 BlASM02.DeleteSingle 進行單筆刪除
                // 自動處理：AC、AA、AD、AB + 更新涉及的資產類別 AE04
                int _result = BlASM02.DeleteSingle(obj);

                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 分佈資料功能 "

        /// <summary>
        /// 取得分佈明細列表（分佈資料對話框使用）
        /// </summary>
        /// <param name="query">查詢分佈明細的請求參數模型</param>
        /// <returns>分佈明細列表</returns>
        [HttpPost("distribution/details")]
        public MdApiMessage GetDistributionDetails([FromBody] MdASM02_DistributionQuery query)
        {
            try
            {
                var _data = BlASM02.Distribution_GetDetail(
                    companyId: query.CompanyId,
                    docType: query.DocType,
                    txType: query.TxType,
                    purchaseNo: query.PurchaseNo,
                    serial: query.Serial,
                    assetNo: query.AssetNo,
                    acquireDate: query.AcquireDate
                );

                return Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Success"),
                    _data
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_Fail"),
                    ex
                );
            }
        }

        /// <summary>
        /// 批量更新分佈明細
        /// </summary>
        /// <param name="obj">批量更新請求模型</param>
        /// <returns>操作結果</returns>
        /// <remarks>
        /// </remarks>
        [HttpPut("distribution/update")]
        public MdApiMessage UpdateDistribution([FromBody] MdASM02_DistributionItems obj)
        {
            try
            {
                if (obj == null || obj.Count == 0)
                {
                    return Response.SendFailed("無更新資料");
                }

                int _result = BlASM02.Distribution_Update(obj);

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
