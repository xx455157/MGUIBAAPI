#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewAS.Models.Private.vASR02;
using GUIStd.Models;
using GUICore.Web.Extensions;
using System;
using GUIStd.BLL.AllNewAS;
using GUIStd;
using BLL_GUI = GUIStd.BLL.GUI;
using DAL_BASE_MODEL = GUIStd.DAL.Base.Models;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUIStd.DAL.AllNewAS.Models.Private.AssetSale;
using GUIStd.DAL.AllNewAS.Models.Private.Maintain;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 財產資料控制器
    /// </summary>
    [Route("as/[controller]")]
    public class AssetMaintainController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlAssetMaintain BlAssetMaintain => new BlAssetMaintain(ClientContent);

        private BlSINI BlSINI => new BlSINI(ClientContent);

        private BlAA BlAA => new BlAA(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢固定資產維修清單
        /// </summary>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數</param>
        /// <returns>查詢清單結果（含分頁資訊）</returns>
        [HttpPost("query/list/pages/{pageNo}")]
        public MdAssetMaintain_p GetDataForList(int pageNo, [FromBody] MdAssetSale_q queryParams)
        {
            var _rowsPerPage = queryParams.RowsPerPage;
            return BlAssetMaintain.GetDataForList(queryParams, pageNo, ControlName, ref _rowsPerPage);
        }

        /// <summary>
        /// 取得明細頁面開檔資料（維修單 Header + 資產清單）
        /// </summary>
        /// <param name="companyId">公司別 (AC01)</param>
        /// <param name="saleNo">維修單號 (AC04)</param>
        /// <returns>明細開檔資料，或 null（無資料）</returns>
        [HttpGet("query/detail/{companyId}/{saleNo}")]
        public MdAssetMaintain_d GetDetailData(string companyId, string saleNo)
        {
            return BlAssetMaintain.GetDetailData(companyId, saleNo);
        }

        /// <summary>
        /// 取得財產序號清單（序號選擇對話框用）
        /// </summary>
        /// <param name="companyId">公司別 (AA01)</param>
        /// <param name="assetNo">財產編號 (AA02)</param>
        /// <param name="purchaseDate">購入日期 (AA03)</param>
        /// <returns>序號明細清單</returns>
        [HttpGet("query/serialList/{companyId}/{assetNo}/{purchaseDate}")]
        public IEnumerable<MdAssetSale_si> GetSerialList(string companyId, string assetNo, string purchaseDate)
        {
            return BlAssetMaintain.GetSerialList(companyId, assetNo, purchaseDate);
        }

        /// <summary>
        /// 取得大批維修候選清單（依篩選條件查詢 AA + AB 保管資料）
        /// </summary>
        /// <param name="query">大批維修查詢參數（公司別、資產編號起迄、折舊到期日起迄、保管部門）</param>
        /// <returns>符合條件的保管資產清單</returns>
        [HttpPost("query/massSaleList")]
        public IEnumerable<MdAssetMassSale_r> GetMassSaleList([FromBody] MdAssetMassSale_q query)
        {
            return BlAssetMaintain.GetMassSaleList(query);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 資產維修單新增作業
        /// </summary>
        /// <param name="obj">維修單存檔請求模型</param>
        /// <returns>標準 API 回應訊息（附帶新增的維修單號）</returns>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdAssetSale_w obj)
        {
            try
            {
                var _saleNo = BlAssetMaintain.Insert(obj);
                return HttpContext.Response.InsertSuccess(
                    affectedRows: 1,
                    // 回傳資料
                    responseData: new
                    {
                        // 銷售單號
                        saleNo = _saleNo,
                        // 查詢存檔後的結果清單
                        lists = BlAssetMaintain.GetDataForList(obj.Header.CompanyId, _saleNo)
                    }
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 資產維修單刪除作業
        /// </summary>
        /// <param name="companyId">公司別 (AC01)</param>
        /// <param name="saleNo">維修單號 (AC04)</param>
        /// <returns>標準 API 回應訊息</returns>
        [HttpDelete("delete/{companyId}/{saleNo}")]
        public MdApiMessage Delete(string companyId, string saleNo)
        {
            try
            {
                int _result = BlAssetMaintain.Delete(companyId, saleNo);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 資產維修單新增作業
        /// </summary>
        /// <param name="obj">維修單存檔請求模型</param>
        /// <returns>標準 API 回應訊息（附帶新增的維修單號）</returns>
        [HttpPut("update")]
        public MdApiMessage Update([FromBody] MdAssetSale_w obj)
        {
            try
            {
                var _saleNo = BlAssetMaintain.Update(obj);
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: 1,
                    // 回傳資料
                    responseData: new
                    {
                        // 查詢存檔後的結果清單
                        lists = BlAssetMaintain.GetDataForList(obj.Header.CompanyId, obj.Header.SaleNo)
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
