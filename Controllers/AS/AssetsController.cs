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

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 排班資料控制器
    /// </summary>
    [Route("as/[controller]")]
    public class AssetsController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlAsset BlAsset => new BlAsset(ClientContent);

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
        /// 資產單號
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("number/help/{queryText}/pages/{pageNo}")]
        public MdCode_p GetAssetNoHelp(string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] companies, [FromQuery] bool FuzzySearch = true)
        {
            return BlAsset.GetAssetNoHelp(SearchKey: queryText, companies: companies, funcName: ControlName, pageNo: pageNo);
        }
        #endregion
    }
}
