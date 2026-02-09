#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.Attributes;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class CustomersController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA16 BlA16 => new BlA16(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得體系店別資料
        /// </summary>
        /// <param name="dataType">資料類別</param>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("help")]
        public IEnumerable<MdCode> GetHelp([FromQuery] string dataType,
            [FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
        {
            return BlA16.GetHelp(dataType, CurrentLang, includeEmptyRow, includeId);
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">編號或名稱必需包含傳入的參數值</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="fullName">是否全名</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/{queryText}/pages/{pageNo}")]
        public MdCode_p GetSHelp(string queryText, [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] bool fullName, [FromQuery] bool sortByName)
        {
            return BlA16.GetSHelp(queryText, ControlName, pageNo, fullName, sortByName);
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料(搜尋字眼非必要)
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryText">編號或名稱的參數值</param>
        /// <param name="fullName">是否全名</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/pages/{pageNo}")]
        public MdCode_p GetSHelp2(string queryText, [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] bool fullName, [FromQuery] bool sortByName)
        {
            return BlA16.GetSHelp(queryText, ControlName, pageNo, fullName, sortByName, true);
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料(搜尋字眼非必要)
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="queryText">編號或名稱的參數值</param>
        /// <param name="dataType">(1:客戶 2:廠商3:銀行):1,2 or 2,3 or 3</param>
        /// <param name="fullName">是否全名</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/datatype/pages/{pageNo}")]
        public MdCode_p GetSHelpDataType([DARange(1, int.MaxValue)] int pageNo, [FromQuery] string queryText, [FromQuery] string dataType,
            [FromQuery] bool fullName = false, [FromQuery] bool sortByName = false)
        {
            return BlA16.GetSHelp(queryText, ControlName, pageNo, fullName, sortByName, true, dataType);
        }

        /// <summary>
        /// 取得客戶資料
        /// </summary>
        /// <param name="customerId">資料類別</param>
        /// <returns>客戶資料模型泛型集合物件</returns>
        [HttpGet("{customerId}")]
        public IEnumerable<MdCustomer> GetData(string customerId)
        {
            return BlA16.GetData(customerId);
        }

        #endregion
    }
}
