#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Models;
using GUIStd.BLL.AllNewAS;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewAS.Models.Private.vASR02;
using BLL_GUI = GUIStd.BLL.GUI;
using DAL_BASE_MODEL = GUIStd.DAL.Base.Models;
using GUIStd.DAL.AllNewAS.Models.Private.Assets;
using GUICore.Web.Attributes;
using Newtonsoft.Json;
using System.Threading.Tasks;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 財產資料控制器
    /// </summary>
    [Route("as/[controller]")]
    public class AssetsController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlAsset BlAsset => new BlAsset(ClientContent);

        private BlSINI BlSINI => new BlSINI(ClientContent);

        private BlAA BlAA => new BlAA(ClientContent);

        private BlAB BlAB => new BlAB(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("{compId}")]
        public IEnumerable<MdAsset>GetAssets(string compId,[FromQuery] string purchaseDate,[FromQuery] string searchKey)
        {
            return BlAsset.GetAssets(compId, purchaseDate,searchKey);
        }

        /// <summary>
        /// 取得財產分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">財產編號或名稱必需包含傳入的參數值</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <param name="singleLocation">是否指定單一分佈的資產</param>
        /// <returns>財產分頁輔助資料模型物件</returns>
        [HttpGet("help/{compId}/{queryText}/pages/{pageNo}")]
        public MdCode_p GetSHelp(string compId, string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromQuery] bool sortByName, [FromQuery] bool singleLocation)
        {
            return BlAsset.GetSHelp(compId, queryText, ControlName, pageNo, sortByName, singleLocation);
        }

        /// <summary>
        /// 使用公司別清單/大部門清單尋找資產科目
        /// </summary>
        /// <param name="queryParams">資料查詢物件參數</param>
        /// <returns></returns>
        [HttpPost("accts")]
        public IEnumerable<MdCode> GetAssetAccts([FromBody] MdASR02_q queryParams)
        {
            return BlAsset.GetAssetAccts(queryParams);
        }


        /// <summary>
        /// 查詢固定資產目錄
        /// </summary>
        /// <param name="queryParams">資料查詢物件參數</param>
        /// <param name="pageNo">頁次</param>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("pages/{pageNo}")]
        public MdASR02_p GetData([FromBody] MdASR02_q queryParams, [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlAsset.GetData(queryParams, ControlName, pageNo);
        }

        /// <summary>
        /// 資產/折舊科目
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("accounts/help/{queryText}/pages/{pageNo}")]
        public MdCode_p GetSubjectData(string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] companies)
        {
            return BlAsset.GetAssetAcct(SearchKey: queryText, companies: companies, funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        [HttpPost("accounts/help/{queryText}/pages/{pageNo}/fldName/{fldName}")]
        public MdCode_p GetAssetAccts(string queryText, [DARange(1, int.MaxValue)] int pageNo,string fldName, [FromBody] string[] companies)
        {
            return BlAsset.GetAccts(SearchKey: queryText,fldName: fldName, companies: companies, funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        /// 資產單號
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("number/help/{queryText}/pages/{pageNo}")]
        public MdCode_p GetAssetNoHelp(string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] companies, [FromQuery] bool FuzzySearch = true)
        {
            return BlAsset.GetAssetNoHelp(SearchKey: queryText, companies: companies, funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        /// 資產單號基本資料 輔助查詢 多資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("number/help2/{queryText}/pages/{pageNo}")]
        public MdAssetHelp_p GetAssetNoHelp2(string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] companies, [FromQuery] bool FuzzySearch = true)
        {
            return BlAsset.GetAssetNoHelp2(SearchKey: queryText, companies: companies, funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        /// 取得資產編號（自動取號）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="assetCategory">資產類別</param>
        /// <param name="purchaseDate">購入日期 (YYYYMMDD)</param>
        /// <returns>自動產生的資產編號</returns>
        [HttpGet("assetNo/{companyId}/{assetCategory}/{purchaseDate}")]
        public MdApiMessage GetAssetNo(string companyId, string assetCategory, string purchaseDate)
        {
            try
            {
                // 呼叫自動取號
                bool _success = BlSINI.GetAssetNo(
                    AA01: companyId,
                    AA26: assetCategory,
                    AA03: purchaseDate,
                    assetNo: out string _assetNo,
                    errMsg: out string _errMsg
                );

                if (!_success)
                {
                    // 不自動取號或發生錯誤
                    if (string.IsNullOrEmpty(_errMsg))
                    {
                        // 人工輸入模式
                        return HttpContext.Response.SendSuccess(
                            "人工輸入模式",
                            responseData: new
                            {
                                assetNo = "",
                                isAutoGenerate = false
                            }
                        );
                    }
                    else
                    {
                        // 發生錯誤
                        throw new Exception(_errMsg);
                    }
                }

                // 返回成功訊息
                return Response.SendSuccess(
                    "資產編號自動取號成功",
                    new
                    {
                        assetNo = _assetNo,
                        isAutoGenerate = true
                    }
                );
            }
            catch (Exception ex)
            {
                return HttpContext.Response.SendFailed(
                    $"取得資產編號失敗：{ex.Message}",
                    ex
                );
            }
        }

        /// <summary>
        /// 檢查財產編號是否重複（檢查 AA 表）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="assetNo">財產編號</param>
        /// <returns>是否重複（true=重複, false=不重複）</returns>
        [HttpGet("checkAssetNoExists/{companyId}/{assetNo}")]
        public MdApiMessage CheckAssetNoDuplicate(string companyId, string assetNo)
        {
            try
            {
                // 檢查 AA 表中是否存在該財產編號
                bool _isDuplicate = BlAA.IsExist2(companyId, assetNo);

                string _message = !_isDuplicate
                    ? Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_NotExistForAPI")
                    : Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_RecordExist");

                return Response.SendSuccess(
                     _message = Localization.GetValue(Enums.ResourceLang.LangAS, "PanelDescpt_AssetNo") + _message,
                    new
                    {
                        isDuplicate = _isDuplicate,
                        assetNo = assetNo
                    }
                );
            }
            catch (Exception ex)
            {
                return Response.SendFailed(
                    Localization.GetValue(Enums.ResourceLang.LangAS, "PgmMsg_CheckAssetNoFailed"),
                    ex
                );
            }
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/pages/{pageNo}")]
        public MdBasic_p GetSHelpv2([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText,
            [FromQuery] bool sortByName, [FromQuery] string companyId = "")
        {
            var _para = new DAL_BASE_MODEL.MdHelpPaging
            {
                Language = ClientContent.Language,
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo,
            };
            return BlAA.GetSHelpv2(_para, companyId);
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/forhasqty/pages/{pageNo}")]
        public MdAssetKeyCols_p GetSHelpv2ForHasQty([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText,
            [FromQuery] bool sortByName, [FromQuery] string companyId = "")
        {
            var _para = new DAL_BASE_MODEL.MdHelpPaging
            {
                Language = ClientContent.Language,
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo,
            };
            return BlAA.GetSHelpv2ForHasQty(_para, companyId);
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料 AC03='05' 檢核
        /// </summary>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/forDepreciation/pages/{pageNo}")]
        public MdAssetCapitalizeCols_p GetSHelpv2ForDepreciation([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText,
            [FromQuery] bool sortByName, [FromQuery] string companyId = "", [FromQuery] string transDate = "")
        {
            var _para = new DAL_BASE_MODEL.MdHelpPaging
            {
                Language = ClientContent.Language,
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo,
            };
            return BlAA.GetSHelpv2ForDepreciation(_para, companyId, transDate);
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料 AC03='05' 檢核
        /// </summary>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/forResidualValueChange/pages/{pageNo}")]
        public MdAssetResidualValueChangeCols_p GetSHelpv2ForResidualValueChange([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText,
            [FromQuery] bool sortByName, [FromQuery] string companyId = "", [FromQuery] string transDate = "")
        {
            var _para = new DAL_BASE_MODEL.MdHelpPaging
            {
                Language = ClientContent.Language,
                QueryText = queryText,
                FuncName = this.ControlName,
                SortByName = sortByName,
                PageNo = pageNo,
            };
            return BlAA.GetSHelpv2ForResidualValueChange(_para, companyId, transDate);
        }

        /// <summary>
        /// 取得固定資產明細（分佈清單）
        /// </summary>
        /// <param name="query">查詢參數（公司別、財產編號、購入日期）</param>
        /// <returns>固定資產明細合併模型</returns>
        [HttpPost("query/basic")]
        public MdBasic GetBasicData([FromBody] MdBasic_q query)
        {
            return BlAsset.GetBasicData(query.AA01, query.AA02, query.AA03);
        }


        /// <summary>
        /// 取得固定資產明細（分佈清單）
        /// </summary>
        /// <param name="query">查詢參數（公司別、財產編號、購入日期）</param>
        /// <returns>固定資產明細合併模型</returns>
        [HttpPost("query/distribution")]
        public IEnumerable<MdDistribution> GetDistribution([FromBody] MdBasic_q query)
        {
            return BlAsset.GetDistribution(query.AA01, query.AA02, query.AA03);
        }

        /// <summary>
        /// 取得固定資產明細（基本資料 + 分佈清單）
        /// </summary>
        /// <param name="query">查詢參數（公司別、財產編號、購入日期）</param>
        /// <returns>固定資產明細合併模型</returns>
        [HttpPost("query/basicDistribution")]
        public MdBasicInfo GetBasicDistrInfo([FromBody] MdBasic_q query)
        {
            return BlAsset.GetBasicDistrInfo(query.AA01, query.AA02, query.AA03);
        }

        /// <summary>
        /// 取得帳卡分頁查詢資料
        /// </summary>
        /// <param name="pageNo">頁碼（最小值 1）</param>
        /// <param name="query">查詢參數</param>
        /// <param name="rowsPerPage">一頁筆數（0 表示使用系統預設值）</param>
        /// <returns>帳卡分頁查詢結果</returns>
        [HttpPost("query/acctcard/pages/{pageNo}")]
        public MdAcctCard_p GetAcctCardData([DARange(1, int.MaxValue)] int pageNo, [FromBody] MdAcctCard_q query, int rowsPerPage = 0)
        {
            return BlAsset.GetAcctCardData(query, ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 取得 資產科目清單
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("accounts/help/{companyId}")]
        public IEnumerable<MdCode> GetAccountsHelp(string companyId)
        {
            return BlAsset.GetAccountsHelp(companyId);
        }

        /// <summary>
        /// 取得 會計科目 分頁的輔助資料
        /// A15InAA 檢核 1:固定資產科目/AA19、2:備抵折舊科目/AA20、3:折舊費用科目/AA21
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <param name="companyId">公司別</param>
        /// <param name="accountType">科目類型</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/assetAccount/pages/{pageNo}")]
        public MdCode_p GetSHelpv2AssetAccount([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText,
            [FromQuery] bool sortByName, [FromQuery] string companyId = "", [FromQuery] string accountType = "")
        {
            var _para = new DAL_BASE_MODEL.MdHelpPaging
            {
                Language = ClientContent.Language,
                FuncName = this.ControlName,
                QueryText = queryText,
                SortByName = sortByName,
                PageNo = pageNo,
            };
            return BlAA.GetSHelpv2AssetAccount(_para, companyId, accountType);
        }


        /// <summary>
        /// 取得 保管人 分頁的輔助資料
        /// usp_SelectA0801InAB07ForHelpPaging
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <param name="companyId">公司別</param>
        /// <param name="accountType">科目類型</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/assetKeeper/pages/{pageNo}")]
        public MdCode_p GetSHelpv2AssetKeeper([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText,
            [FromQuery] bool sortByName, [FromQuery] string companyId = "", [FromQuery] string accountType = "")
        {
            var _para = new DAL_BASE_MODEL.MdHelpPaging
            {
                Language = ClientContent.Language,
                FuncName = this.ControlName,
                QueryText = queryText,
                SortByName = sortByName,
                PageNo = pageNo,
            };
            return BlAB.GetSHelpv2AssetKeeper(_para, companyId);
        }


        /// <summary>
        /// 取得 保管部門 分頁的輔助資料
        /// usp_SelectA0201InAB06ForHelpPaging
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryText">搜尋資料的關鍵字，允許空白</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <param name="companyId">公司別</param>
        /// <param name="accountType">科目類型</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("helpv2/assetKeepDept/pages/{pageNo}")]
        public MdCode_p GetSHelpv2AssetKeepDept([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText,
            [FromQuery] bool sortByName, [FromQuery] string companyId = "", [FromQuery] string accountType = "")
        {
            var _para = new DAL_BASE_MODEL.MdHelpPaging
            {
                Language = ClientContent.Language,
                FuncName = this.ControlName,
                QueryText = queryText,
                SortByName = sortByName,
                PageNo = pageNo,
            };
            return BlAB.GetSHelpv2AssetKeepDept(_para, companyId);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 批次更新勾選財產之會計科目（AA19／AA20／AA21 擇一）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="accountType">科目類型</param>
        /// <param name="newAccountSubject">新會計科目</param>
        [HttpPost("updateForAccountChange")]
        public async Task<MdApiMessage> UpdateForAccountChange([RequiredFromQuery] string companyId, [RequiredFromQuery] string accountType, [RequiredFromQuery] string newAccountSubject)
        {
            try
            {
                var _rows = BlAsset.UpdateForAccountChange(companyId, accountType, newAccountSubject, await Request.GetRawBodyStringAsync());
                return HttpContext.Response.UpdateSuccess(_rows);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 批次更新勾選財產之目前狀態（AB09）
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="newStatus">新狀態</param>
        [HttpPost("updateForStatusChange")]
        public async Task<MdApiMessage> UpdateForStatusChange([RequiredFromQuery] string companyId, [RequiredFromQuery] string newStatus)
        {
            try
            {
                var _rows = BlAsset.UpdateForStatusChange(companyId, newStatus, await Request.GetRawBodyStringAsync());
                return HttpContext.Response.UpdateSuccess(_rows);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion

    }
}
