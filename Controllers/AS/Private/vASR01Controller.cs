#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Attributes;
using GUIStd.DAL.AllNewAS.Models.Private.vASR01;


#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// vASR02固定資產清單統程式資料控制器
    /// </summary>
    [Route("as/private/[controller]")]
	public class vASR01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        private BlDepreciation BlDepreciation => new BlDepreciation(ClientContent);
        private BlAsset BlAsset => new BlAsset(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "


        /// <summary>
        /// 查詢頁面預設資料
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("page")]
        public MdASR01_h GetUIData()
        {
            return BlDepreciation.GetUIData();
        }

        /// <summary>
        /// 資產單號
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("AssetNumber/{queryText}/pages/{pageNo}")]
        public MdCode_p GetASR01_GetAssetNoHelp(string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] companies)
        {
            return BlDepreciation.GetAssetNoHelp(SearchKey: queryText, companies: companies, funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        /// 資產/折舊科目
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("AssetAccounts/{queryText?}/pages/{pageNo}")]
        [HttpGet("AssetAccounts/pages/{pageNo}")]
        public MdCode_p GetASR01_GetSubjectData([FromRoute] string queryText, [DARange(1, int.MaxValue)] int pageNo, [FromQuery] string companies)
        {
            string[] companiesArray = !string.IsNullOrWhiteSpace(companies)
                ? companies.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(t => t.Trim()).ToArray()
                : new string[0];
            return BlDepreciation.GetSubjectData(SearchKey: queryText, companies: companiesArray, funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        /// 資產屬性
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpGet("AssetAttribute")]
        public IEnumerable<MdCode> GetASR01_ASSINIGetHelp()
        {
            string section = "ASSet_State";
            return BlDepreciation.ASSINIGetHelp(section: section, topic: new string[0]);
        }

        /// <summary>
        /// 折舊報表畫面
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("Q_GetList")]
        public IEnumerable<MdASR01> Q_GetList([FromBody] MdASR01_q queryParams)
        {
            return BlDepreciation.Q_GetList(queryParams);
        }

        /// <summary>
        /// 折舊報表列印輸出 (尚未開發)
        /// </summary>
        /// <returns>系統參數代碼模型集合物件</returns>
        [HttpPost("Q_GetList/export")]
        public IEnumerable<MdASR01> Q_GetListExport([FromBody] MdASR01_q queryParams)
        {
            return BlDepreciation.Q_GetListExport(queryParams);
        }

        #endregion

    }
}
