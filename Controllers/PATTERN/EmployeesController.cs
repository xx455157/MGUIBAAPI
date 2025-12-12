#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.GUI;
using GUIStd.DAL.GUI.Models;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.Pattern
{
    /// <summary>
    /// 【需經驗證】PTN員工基本資料控制器
    /// </summary>
    [Route("pattern/[controller]")]
    public class EmployeesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlT02 BlT02 => new BlT02(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次的員工基本資料
        /// </summary>
        /// <param name="employeeIds">員工編號(起)</param>
        /// <param name="employeeIde">員工編號(迄)</param>
        /// <param name="employeeName">員工姓名</param>
        /// <param name="onBroadDates">到職日期(起)</param>
        /// <param name="onBroadDatee">到職日期(迄)</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="departments">部門字串陣列</param>
        [HttpPost("query/{employeeIds}/{employeeIde}/pages/{pageNo}")]
        public MdPTNEmployees_p GetData(string employeeIds, string employeeIde, string employeeName,
            string onBroadDates, string onBroadDatee,
            [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] departments)
        {
            return BlT02.GetData(employeeIds, employeeIde, employeeName, onBroadDates, onBroadDatee,
                 departments, "PTNEmployees", pageNo);
        }

        /// <summary>
        /// 取得唯一的員工資料
        /// </summary>
        /// <param name="employeeId">員工編號路徑參數</param>
        /// <returns>員工資料模型物件</returns>
        [HttpGet("{employeeId}")]
        public MdPTNEmployee GetRow(string employeeId)
        {
            return BlT02.GetRow(employeeId);
        }

        /// <summary>
        /// 判斷員工編號是否已存在
        /// </summary>
        /// <param name="employeeId">員工編號路徑參數</param>
        /// <returns></returns>
        [HttpGet("exists/{employeeId}")]
        public bool IsExist(string employeeId)
        {
            return BlT02.IsExist(employeeId);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">員工資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdPTNEmployee obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlT02.ProcessInsert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改資料
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <param name="obj">員工資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{employeeId}")]
        public MdApiMessage Update(string employeeId, [FromBody] MdPTNEmployee obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlT02.ProcessUpdate(employeeId, obj);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="employeeId">員工編號</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{employeeId}")]
        public MdApiMessage Delete(string employeeId)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlT02.ProcessDelete(employeeId);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}
