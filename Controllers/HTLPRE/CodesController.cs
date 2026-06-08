#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Models;
using GUIStd.BLL.AllNewHTL;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.CodeTable;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
	/// <summary>
	/// 代碼資料控制器
	/// </summary>
	[Route("htlpre/[controller]")]
	public class CodesController : GUIAppAuthController
	{
		#region " 私用屬性 "

		/// <summary>
		/// 商業邏輯物件屬性
		/// </summary>
		private BlCodes BlCodes => new BlCodes(ClientContent);

		#endregion

		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得代碼資料
		/// </summary>
		/// <param name="typeId">TB01代號</param>
		/// <param name="includeEmptyRow">是否包含空白列</param>
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("{typeId}")]
		public IEnumerable<MdCode> GetHelp(string typeId,
			[FromQuery] bool includeEmptyRow, [FromQuery] bool includeId)
		{
			return BlCodes.GetHelp(typeId, CurrentLang, includeEmptyRow, includeId);
		}

        /// <summary>
        /// 查詢多個代碼
        /// </summary>
        /// <param name="typeIds">代碼類別集合，以逗號分隔</param>
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>代碼資料模型泛型集合物件</returns>
		[HttpGet()]
		public IEnumerable<MdCodeHT> GetData([FromQuery]string typeIds, [FromQuery] bool includeId)
		{
			return BlCodes.GetData(typeIds.Split(','), CurrentLang, includeId);
        }

        /// <summary>
        /// 查詢代碼彙總分業資料，並回傳分頁結果
        /// </summary>
        /// <param name="obj">查詢參數資料物件</param>
        /// <param name="pageNo">頁次</param>
        /// <param name="rowsPerPage">每頁筆數</param>
        /// <returns>代碼彙總分業資料模型物件</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdCodeHT_p GetDataForPaging(MdCodeHT_q obj, int pageNo, [FromQuery] int rowsPerPage)
        {
            return BlCodes.GetDataForPaging(obj, pageNo, rowsPerPage);
        }

        /// <summary>
        /// 取得國家代碼資料
        /// </summary>		
        /// <param name="includeId">是否包含代碼</param>
        /// <returns>程式資料模型泛型集合物件</returns>
        [HttpGet("country")]
		public IEnumerable<MdCode> GetDataForCountry([FromQuery] bool includeId)
		{
			return BlCodes.GetDataForCountry(CurrentLang, includeId);
		}

		/// <summary>
		/// 取得城市代碼資料
		/// </summary>		
		/// <param name="includeId">是否包含代碼</param>
		/// <returns>程式資料模型泛型集合物件</returns>
		[HttpGet("city")]
		public IEnumerable<MdCode> GetDataForCity([FromQuery] bool includeId)
		{
			return BlCodes.GetDataForCity(CurrentLang, includeId);
        }

        [HttpGet("exists/{codeTable}/{id}")]
        public bool IsExists(string codeTable, string id)
        {
            return BlCodes.IsExists(codeTable, id);
        }

        [HttpGet("istransexists/{codeTable}/{id}")]
        public bool IsTranExists(string codeTable, string id)
        {
            return BlCodes.IsTransExists(codeTable, id);
        }

        #endregion

        #region " 共用屬性 - 異動資料"


        /// <summary>
        /// 新增代碼
        /// </summary>
        /// <param name="objs">代碼資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost()]
        public MdApiMessage Insert([FromBody] IEnumerable<MdCodeHT_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlCodes.Insert(objs);

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
        /// 修改代碼
        /// </summary>
        /// <param name="obj">代碼資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("{order}/{id}")]
        public MdApiMessage Update(string order, string id, [FromBody] MdCodeHT_w obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlCodes.Update(obj, order, id);

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
        /// 覆蓋代碼
        /// </summary>
        /// <param name="objs">代碼資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("overwrite")]
        public MdApiMessage Overwrite([FromBody] IEnumerable<MdCodeHT_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlCodes.Overwrite(objs);

                // 回應前端修改成功訊息 
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        [HttpPost("upsert")]
        public MdApiMessage Upsert([FromBody] IEnumerable<MdCodeHT_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlCodes.Upsert(objs);

                // 回應前端修改成功訊息 
                return HttpContext.Response.InsertSuccess(_result, "PgmMsg_SaveSuccess");
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 依序號更改代碼
        /// </summary>
        /// <param name="objs">代碼資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("overwrite/order")]
        public MdApiMessage OverwriteByOrder([FromBody] IEnumerable<MdCodeHT_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlCodes.OverwriteByOrder(objs);

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
        /// 刪除代碼資料
        /// </summary>
        /// <param name="codeTable">代碼類別</param>
        /// <param name="sno">序號</param>
        /// <param name="id">代碼</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{codeTable}/{sno}/{id}")]
        public MdApiMessage Delete(string codeTable, string sno, string id)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlCodes.Delete(codeTable, sno, id);
                // 回應前端修改成功訊息 
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion
    }
}
