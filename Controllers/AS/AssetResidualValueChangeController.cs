#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Models;
using GUICore.Web.Extensions;
using System;
using GUIStd.BLL.AllNewAS;
using GUIStd;
using GUIStd.DAL.AllNewAS.Models.Private.ResidualValueChange;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 固定資產殘值變更資料控制器（異動別 17）
    /// </summary>
    [Route("as/[controller]")]
    public class AssetResidualValueChangeController : GUIAppAuthController
    {

        #region " 私用屬性 "

        private BlAssetResidualValueChange BlAssetResidualValueChange => new BlAssetResidualValueChange(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢固定資產殘值變更清單
        /// </summary>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數</param>
        /// <returns>查詢清單結果（含分頁資訊）</returns>
        [HttpPost("query/list/pages/{pageNo}")]
        public MdAssetResidualValueChange_p GetDataForList(int pageNo, [FromBody] MdAssetResidualValueChange_q queryParams)
        {
            var _rowsPerPage = queryParams.RowsPerPage;
            return BlAssetResidualValueChange.GetDataForList(queryParams, pageNo, ControlName, ref _rowsPerPage);
        }

        /// <summary>
        /// 取得明細頁面開檔資料（單據 Header + 資產清單）
        /// </summary>
        /// <param name="companyId">公司別 (AC01)</param>
        /// <param name="transNo">單號 (AC04)</param>
        /// <returns>明細開檔資料，或 null（無資料）</returns>
        [HttpGet("query/detail/{companyId}/{transNo}")]
        public MdAssetResidualValueChange_d GetDetailData(string companyId, string transNo)
        {
            return BlAssetResidualValueChange.GetDetailData(companyId, transNo);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 殘值變更單新增作業
        /// </summary>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdAssetResidualValueChange_w obj)
        {
            try
            {
                var _transNo = BlAssetResidualValueChange.Insert(obj);
                return HttpContext.Response.InsertSuccess(
                    affectedRows: 1,
                    responseData: new
                    {
                        transNo = _transNo,
                        lists = BlAssetResidualValueChange.GetDataForList(obj.Header.CompanyId, _transNo)
                    }
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 殘值變更單刪除作業
        /// </summary>
        [HttpDelete("delete/{companyId}/{transNo}")]
        public MdApiMessage Delete(string companyId, string transNo)
        {
            try
            {
                int _result = BlAssetResidualValueChange.Delete(companyId, transNo);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 殘值變更單修改作業
        /// </summary>
        [HttpPut("update")]
        public MdApiMessage Update([FromBody] MdAssetResidualValueChange_w obj)
        {
            try
            {
                BlAssetResidualValueChange.Update(obj);
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: 1,
                    responseData: new
                    {
                        lists = BlAssetResidualValueChange.GetDataForList(obj.Header.CompanyId, obj.Header.TransNo)
                    }
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
