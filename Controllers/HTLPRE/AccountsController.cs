#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 出納科目資料控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class AccountsController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlHTCA BlHTCA => new BlHTCA(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得會計科目代碼資料
		/// </summary>
		/// <param name="posId">廳別代碼</param> 		
		/// <param name="includeEmptyRow">是否包含空白列</param>
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>代碼資料模型泛型集合物件</returns>
		[HttpGet("{posId}")]
		public IEnumerable<MdCode> GetHelp(string posId,
			[FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
		{
			return BlHTCA.GetHelp(posId, CurrentLang, includeEmptyRow, includeId);
		}

        /// <summary>
        /// 檢查出納科目是否存在
        /// </summary>
        /// <param name="posId">廳別代碼</param>
        /// <param name="acctCode">出納科目</param>
        /// <returns>是否存在</returns>
        [HttpGet("isexists/{posId}/{acctCode}")]
        public bool IsExists(string posId, string acctCode)
        {
            return BlHTCA.IsExists(posId, acctCode);
        }

        /// <summary>
        /// 檢查廳別是否存在出納科目
        /// </summary>
        /// <param name="posId">廳別代碼</param>
        /// <returns>該廳別資料筆數</returns>
        [HttpGet("isposexists/{posId}")]
        public int IsExists(string posId)
        {
            return BlHTCA.IsExists(posId);
        }


        /// <summary>
        /// 查詢是否存在交易資料
        /// </summary>
        /// <param name="posId">廳別代碼</param>
        /// <param name="acctCode">出納科目</param>
        /// <returns>是否存在</returns>
        [HttpGet("istransexists/{posId}/{acctCode}")]
        public bool IsTransExists(string posId, string acctCode)
        {
            return BlHTCA.IsTransExists(posId, acctCode);
        }

        /// <summary>
        /// 查詢出納科目分業資料，並回傳分頁結果
        /// </summary>
        /// <param name="obj">查詢參數資料物件</param>
        /// <param name="pageNo">頁次</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>出納科目分業資料模型物件</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdAccount_p GetDataForPaging(MdAccount_q obj, int pageNo, [FromQuery] int rowsPerPage)
		{
			return BlHTCA.GetDataForPaging(obj, pageNo, rowsPerPage);
        }

        #endregion

        #region " 共用屬性 - 異動資料"

        /// <summary>
        /// 新增出納科目
        /// </summary>
        /// <param name="objs">出納科目資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost()]
        public MdApiMessage Insert([FromBody] IEnumerable<MdAccount_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlHTCA.Insert(objs);

                // 回應前端修改成功訊息 
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改出納科目
        /// </summary>
        /// <param name="objs">出納科目資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut()]
        public MdApiMessage Update([FromBody] IEnumerable<MdAccount_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlHTCA.Update(objs);

                // 回應前端修改成功訊息 
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 刪除出納科目
        /// </summary>
        /// <param name="posId">廳別代碼</param>
        /// <param name="acctCode">出納科目代碼</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{posId}/{acctCode}")]
        public MdApiMessage Delete(string posId, string acctCode)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlHTCA.Delete(posId, acctCode);
                // 回應前端修改成功訊息 
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 自來源廳別複製出納科目
        /// </summary>
        /// <param name="posId">廳別代碼</param>
        /// <param name="newPosId">目標廳別</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("copy/{posId}/{newPosId}")]
        public MdApiMessage Copy(string posId, string newPosId)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlHTCA.Copy(posId, newPosId);
                // 回應前端修改成功訊息 
                return HttpContext.Response.SendSuccess(
                    String.Format(Localization.GetValue(Enums.ResourceLang.Lang, "PgmMsg_SaveSuccess"), _result)    
                );
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.SendFailed(ex.Message);
            }
        }

        /// <summary>
        /// Excel大批匯入
        /// </summary>
        /// <param name="objs"></param>
        /// <returns></returns>
        [HttpPost("import")]
        public MdApiMessage Import([FromBody] IEnumerable<MdAccount_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlHTCA.Upsert(objs);

                // 回應前端修改成功訊息 
                return HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }

        }

        #endregion
    }
}
