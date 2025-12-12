#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;

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
