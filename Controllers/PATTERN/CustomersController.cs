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
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.Pattern
{
    /// <summary>
    /// 【需經驗證】PTN客戶基本資料控制器
    /// </summary>
    [Route("pattern/[controller]")]
    public class CustomersController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlT01 BlT01 => new BlT01(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得分頁頁次的客戶基本資料
        /// </summary>
        /// <param name="customerIds">客戶編號(起)</param>
        /// <param name="customerIde">客戶編號(迄)</param>
        /// <param name="setupDates">成立日期(起)</param>
        /// <param name="setupDatee">成立日期(迄)</param>
        /// <param name="capitalAmounts">資本額(起)</param>
        /// <param name="capitalAmounte">資本額(迄)</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <param name="countries">國家字串陣列</param>
        /// <returns>分頁客戶資料模型物件</returns>
        [HttpPost("query/{customerIds}/{customerIde}/pages/{pageNo}")]
        public MdPTNCustomers_p GetData(string customerIds, string customerIde,
            string setupDates, string setupDatee, decimal? capitalAmounts, decimal? capitalAmounte,
            [DARange(1, int.MaxValue)] int pageNo, [FromBody] string[] countries, int rowsPerPage = 0)
        {
            return BlT01.GetData(customerIds, customerIde, setupDates, setupDatee,
                capitalAmounts ?? 0, capitalAmounte ?? 0, countries, ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 取得唯一的客戶資料
        /// </summary>
        /// <param name="customerId">客戶編號路徑參數</param>
        /// <returns>客戶資料模型物件</returns>
        [HttpGet("{customerId}")]
        public MdPTNCustomer GetRow(string customerId)
        {
            return BlT01.GetRow(customerId);
        }

        /// <summary>
        /// 判斷客戶編號是否已存在
        /// </summary>
        /// <param name="customerId">客戶編號路徑參數</param>
        /// <returns></returns>
        [HttpGet("exists/{customerId}")]
        public bool IsExist(string customerId)
        {
            return BlT01.IsExist(customerId);
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增資料
        /// </summary>
        /// <param name="obj">客戶資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdPTNCustomer obj)
        {
            try
            {
                // 呼叫商業元件執行新增作業
                int _result = BlT01.ProcessInsert(obj);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result);

                //// Sample：回應前端新增成功訊息及前端所需的文字型態資料
                //return HttpContext.Response.InsertSuccess(_result, responseData: "C230830001");

                //// Sample：回應前端新增成功訊息及前端所需的 JSON 模型物件型態資料
                //return HttpContext.Response.InsertSuccess(
                //    _result,
                //    responseData: new MdApiMessage
                //    {
                //        Result = true,
                //        Message = "資料新增成功.",
                //        ResponseData = "C230830001"
                //    }
                //);
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
        /// <param name="customerId">客戶編號</param>
        /// <param name="obj">客戶資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{customerId}")]
        public MdApiMessage Update(string customerId, [FromBody] MdPTNCustomer obj)
        {
            // 檢查鍵值路徑參數與本文中的鍵值是否相同
            if (!customerId.EqualsIgnoreCase(obj.T0101))
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();
            }

            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlT01.ProcessUpdate(customerId, obj);

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
        /// <param name="customerId">客戶編號</param>
		/// <returns>系統規範訊息物件</returns>
        [HttpDelete("{customerId}")]
        public MdApiMessage Delete(string customerId)
        {
            try
            {
                // 呼叫商業元件執行刪除作業
                int _result = BlT01.ProcessDelete(customerId);

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
