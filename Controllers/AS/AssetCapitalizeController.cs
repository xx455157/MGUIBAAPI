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
using GUIStd.DAL.AllNewAS.Models.Private.Capitalize;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 財產資本化資料控制器
    /// </summary>
    [Route("as/[controller]")]
    public class AssetCapitalizeController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlAssetCapitalize BlAssetCapitalize => new BlAssetCapitalize(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢固定資產資本化清單
        /// </summary>
        /// <param name="pageNo">頁碼</param>
        /// <param name="queryParams">查詢參數</param>
        /// <returns>查詢清單結果（含分頁資訊）</returns>
        [HttpPost("query/list/pages/{pageNo}")]
        public MdAssetCapitalize_p GetDataForList(int pageNo, [FromBody] MdAssetCapitalize_q queryParams)
        {
            var _rowsPerPage = queryParams.RowsPerPage;
            return BlAssetCapitalize.GetDataForList(queryParams, pageNo, ControlName, ref _rowsPerPage);
        }

        /// <summary>
        /// 取得明細頁面開檔資料（資本化單 Header + 資產清單）
        /// </summary>
        /// <param name="companyId">公司別 (AC01)</param>
        /// <param name="transNo">資本化單號 (AC04)</param>
        /// <returns>明細開檔資料，或 null（無資料）</returns>
        [HttpGet("query/detail/{companyId}/{transNo}")]
        public MdAssetCapitalize_d GetDetailData(string companyId, string transNo)
        {
            return BlAssetCapitalize.GetDetailData(companyId, transNo);
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
            return BlAssetCapitalize.GetSerialList(companyId, assetNo, purchaseDate);
        }

        /// <summary>
        /// 取得大批資本化候選清單（依篩選條件查詢 AA + AB 保管資料）
        /// </summary>
        /// <param name="query">大批資本化查詢參數（公司別、資產編號起迄、折舊到期日起迄、保管部門）</param>
        /// <returns>符合條件的保管資產清單</returns>
        [HttpPost("query/massSaleList")]
        public IEnumerable<MdAssetMassSale_r> GetMassSaleList([FromBody] MdAssetMassSale_q query)
        {
            return BlAssetCapitalize.GetMassSaleList(query);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 資產資本化單新增作業
        /// </summary>
        /// <param name="obj">資本化單存檔請求模型</param>
        /// <returns>標準 API 回應訊息（附帶新增的資本化單號）</returns>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdAssetCapitalize_w obj)
        {
            try
            {
                var _transNo = BlAssetCapitalize.Insert(obj);
                return HttpContext.Response.InsertSuccess(
                    affectedRows: 1,
                    // 回傳資料
                    responseData: new
                    {
                        transNo = _transNo,
                        lists = BlAssetCapitalize.GetDataForList(obj.Header.CompanyId, _transNo)
                    }
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 資產資本化單刪除作業
        /// </summary>
        /// <param name="companyId">公司別 (AC01)</param>
        /// <param name="transNo">資本化單號 (AC04)</param>
        /// <returns>標準 API 回應訊息</returns>
        [HttpDelete("delete/{companyId}/{transNo}")]
        public MdApiMessage Delete(string companyId, string transNo)
        {
            try
            {
                int _result = BlAssetCapitalize.Delete(companyId, transNo);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 資產資本化單修改作業
        /// </summary>
        /// <param name="obj">資本化單存檔請求模型</param>
        /// <returns>標準 API 回應訊息（附帶查詢結果清單）</returns>
        [HttpPut("update")]
        public MdApiMessage Update([FromBody] MdAssetCapitalize_w obj)
        {
            try
            {
                BlAssetCapitalize.Update(obj);
                return HttpContext.Response.UpdateSuccess(
                    affectedRows: 1,
                    responseData: new
                    {
                        lists = BlAssetCapitalize.GetDataForList(obj.Header.CompanyId, obj.Header.TransNo)
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
