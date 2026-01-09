#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewGUI.Models.Private.IV.Departments;
using GUIStd.DAL.AllNewHTL.Models.Private.HTL.Departments;

#endregion

namespace MGUIBAAPI.Controllers.GUI
{
    /// <summary>
    /// 程式資料控制器
    /// </summary>
    [Route("gui/[controller]")]
    public class DepartmentsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlA02 BlA02 => new BlA02(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得體系店別資料
        /// </summary>
        /// <param name="posType">體系代碼</param>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("typecodes/{posType}")]
        public IEnumerable<MdCode> GetData(string posType,
            [FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
        {
            return BlA02.GetHelpByPosId(posType, CurrentLang, includeEmptyRow, includeId);
        }

        /// <summary>
        /// 取得體系店別資料
        /// </summary>
        /// <param name="compId">體系代碼</param>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <param name="excludeCancelDate">是否包含取消日期有值</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("typecodes")]
        public IEnumerable<MdDepartment> GetData02([FromQuery] string compId,
            [FromQuery] bool includeEmptyRow, [FromQuery] bool includeId, [FromQuery] bool excludeCancelDate = false)
        {
            return BlA02.GetHelpByCompId(compId, CurrentLang, includeEmptyRow, includeId, excludeCancelDate);
        }

        /// <summary>
        /// 取得各體系店別明細資料
        /// </summary>
        /// <param name="posType">體系代碼</param>
        /// <param name="pageNo">頁碼</param>
        /// <param name="detail">是否顯示詳細資料</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("typecodes/{posType}/{pageNo}")]
        public MdPoses_p GetDataByPage(string posType, int pageNo, [FromQuery] bool detail)
        {
            if (!detail) return null;
            return BlA02.GetDataForPosByPage(
                posType, CurrentLang, pageNo, "mHTSHM10", ImageReadPath);
        }

        /// <summary>
        /// 取得分頁頁次的輔助資料
        /// </summary>
        /// <param name="queryText">編號或名稱必需包含傳入的參數值</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="fullName">是否全名</param>
        /// <param name="sortByName">是否依名稱排序</param>
        /// <param name="companyId">只呈現指定公司別或公司別空白的部門</param>
        /// <returns>分頁輔助資料模型物件</returns>
        [HttpGet("help/{queryText}/pages/{pageNo}")]
        public MdCode_p GetSHelp(string queryText, [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] bool fullName, [FromQuery] bool sortByName, 
            [FromQuery] string companyId = "")
        {
            return BlA02.GetSHelp(queryText, ControlName, pageNo, fullName, sortByName, companyId);
        }


        /// <summary>
        /// 取得授權部門的輔助資料
        /// S140313030
        /// </summary>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <param name="fullName">是否全名</param>
        /// <param name="empId">員工編號</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("authdepts")]
        public IEnumerable<MdCode> GetAuthDetps([FromQuery] bool includeEmptyRow, [FromQuery] bool includeId, [FromQuery] bool fullName, [FromQuery] string empId)
        {
            if (string.IsNullOrWhiteSpace(empId))
            {
                empId = ClientContent.SystemUserId;
            }
            return BlA02.GetAuthDetps(empId, fullName, includeEmptyRow, includeId);
        }

        #endregion
    }
}
