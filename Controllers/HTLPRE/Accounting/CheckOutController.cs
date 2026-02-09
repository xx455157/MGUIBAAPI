#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models.Private.Accounting;
using GUIStd.DAL.AllNewHTL.Models.Private.CheckOut;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.Models;
using MGUIBAAPI.Models.HTLPRE;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Accounting
{
    /// <summary>
    /// 退房資料控制器
    /// </summary>
    [Route("htlpre/accounting/[controller]")]
    public class CheckOutController : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlCheckOut BlCheckOut => new BlCheckOut(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 退房查詢(客帳)
        /// </summary>
        /// <param name="roomNo">房號</param>        
        /// <returns>住客及帳單(帳夾)資料模型物件</returns>
        [HttpGet("query/{roomNo}")]
        public CmHTFCM06 GetData(string roomNo)
        {
            MdFolioVisitData _folioVisit;
            IEnumerable<MdHouseFolio> _folios;

            BlCheckOut.GetFolioData(VS06: roomNo, currentLang: CurrentLang, folioVisit: out _folioVisit, folios: out _folios);

            return new CmHTFCM06()
            {
                FolioVSData = _folioVisit,
                Folios = _folios
            };
        }

        /// <summary>
        /// 取得帳單號碼底下的帳務資料
        /// </summary>
        /// <param name="folioNo">帳單號碼路徑參數</param>
        /// <returns>明細畫面帳單(帳夾)及帳務資料模型物件</returns>
        [HttpGet("{folioNo}")]
        public MdFolioData_d GetRow(string folioNo)
        {
            MdHouseFolio _folio;
            IEnumerable<MdFocTransaction> _focTransactions;

            BlCheckOut.GetRow(folioNo, currentLang: CurrentLang, folio: out _folio, focTransactions: out _focTransactions);

            return new MdFolioData_d()
            {
                Folio = _folio,
                FXs = _focTransactions
            };            
        }

        /// <summary>
        /// 依房號取得空帳單(帳夾)
        /// </summary>
        /// <param name="roomNo">房號</param>        
        /// <returns>空帳單(帳夾)資料模型物件</returns>
        [HttpGet("queryemptyfolio/{roomNo}")]
        public IEnumerable<MdEmptyHouseFolio> GetEmptyFolio(string roomNo)
		{
            return BlCheckOut.GetEmptyFolio(VS06: roomNo);
        }

        /// <summary>
        /// 取得分頁頁次的退房清單
        /// </summary>
        /// <param name="roomNo">房號</param>
        /// <param name="groupName">團名</param>
        /// <param name="rvNo">訂房號碼</param>
        /// <param name="checkOutDate">退房日期</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數</param>
        /// <returns>分頁退房資料模型物件</returns>
        [HttpGet("checkoutdata/pages/{pageNo}")]
        public MdCheckOutData_p GetCheckOutData([DARange(1, int.MaxValue)] int pageNo, int rowsPerPage = 0,
            [FromQuery] string roomNo = "", [FromQuery] string groupName = "", [FromQuery] string rvNo = "",
            [FromQuery] string checkOutDate = "")
		{
            return BlCheckOut.GetDataByPage(VS06: roomNo, RV36: groupName, RV01: rvNo, 
                RV04: checkOutDate, currentLang: CurrentLang, funcName: ControlName, 
                pageNo: pageNo, rowsPerPage: ref rowsPerPage);
        }

        /// <summary>
        /// 帳務查詢(轉帳)
        /// </summary>
        /// <param name="roomNo">房號</param>        
        /// <returns>已入住房號清單、帳單(帳夾)資料模型物件</returns>
        [HttpGet("querytransferfolio/{roomNo}")]
        //public CmHTFCM09_d GetTransferFolio(string roomNo)
        //{
        //    IEnumerable<MdCode> _checkinRoomNos;
        //    IEnumerable<MdHouseFolio> _folios;

        //    BlCheckOut.GetTransferFolio(VS06: roomNo, checkinRoomNos: out _checkinRoomNos, folios: out _folios);
            
        //    return new CmHTFCM09_d()
        //    {
        //        CheckInRoomNos = _checkinRoomNos,
        //        Folios = _folios
        //    };
        //}

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增帳務資料
        /// </summary>
        /// <param name="obj">帳務資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost]
        public MdApiMessage Insert([FromBody] MdFolioTransaction obj)
        {
            try
            {
                IEnumerable<MdHouseFolio> _folios= null;
                MdFolioData_d _folioData_d = null;

                // 呼叫商業元件執行新增作業                
                int _result = BlCheckOut.ProcessInsert(CurrentLang, obj, ClientContent, out _folios, out _folioData_d);

                // 回應前端新增成功訊息                
                return HttpContext.Response.InsertSuccess(_result, 
                    responseData: new
                    {
                        Folios = _folios,
                        FolioData_d = _folioData_d
                    });
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
		/// 修改帳務資料
		/// </summary>		
		/// <param name="obj">帳務資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut]
        public MdApiMessage Update([FromBody] MdFolioTransaction obj)
        {            
            try
            {
                IEnumerable<MdHouseFolio> _folios = null;
                MdFolioData_d _folioData_d = null;

                // 呼叫商業元件執行修改作業
                int _result = BlCheckOut.ProcessUpdate(CurrentLang, obj, ClientContent, out _folios, out _folioData_d);

                // 回應前端修改成功訊息
                return HttpContext.Response.UpdateSuccess(_result,
                    responseData: new
                    {
                        Folios = _folios,
                        FolioData_d = _folioData_d
                    });
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除帳務資料
        /// </summary>
        /// <param name="folioNo">帳單號碼</param>
        /// <param name="createDate">建檔日期</param>
        /// <param name="createTime">建檔時間</param>
        /// <param name="roomNo">房號</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{folioNo}/{createDate}/{createTime}/{roomNo}")]
        public MdApiMessage Delete(string folioNo, string createDate, string createTime, string roomNo)
        {
            try
            {
                IEnumerable<MdHouseFolio> _folios = null;
                MdFolioData_d _folioData_d = null;

                // 呼叫商業元件執行刪除作業
                int _result = BlCheckOut.ProcessDelete(folioNo, createDate, createTime, roomNo, CurrentLang,
                    out _folios, out _folioData_d);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result,
                    responseData: new
                    {
                        Folios = _folios,
                        FolioData_d = _folioData_d
                    });
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 新增帳單(帳夾)
        /// </summary>
        /// <param name="roomNo">房號</param>
        /// <param name="obj">帳單(帳夾)資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("housefolio/{roomNo}")]
        public MdApiMessage Insert(string roomNo, [FromBody] MdHouseFolio_w obj)
        {
            try
            {
                IEnumerable<MdHouseFolio> _folios = null;

                // 呼叫商業元件執行新增作業                
                int _result = BlCheckOut.ProcessInsertFolio(roomNo, CurrentLang, obj, ClientContent, out _folios);

                // 回應前端新增成功訊息
                return HttpContext.Response.InsertSuccess(_result, responseData: _folios);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 刪除帳單(帳夾)
        /// </summary>        
        /// <param name="roomNo">房號</param>
        /// <param name="obj">帳單(帳夾)資料模型物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("deletehousefolio/{roomNo}")]
        public MdApiMessage Delete(string roomNo, [FromBody] IEnumerable<MdEmptyHouseFolio> obj)
		{
            try
			{
                IEnumerable<MdHouseFolio> _folios = null;

                // 呼叫商業元件執行刪除作業
                int _result = BlCheckOut.ProcessDeleteFolio(roomNo, obj, out _folios);

                // 回應前端刪除成功訊息
                return HttpContext.Response.DeleteSuccess(_result, responseData: _folios);                
            }
            catch (Exception ex)
            {
                // 回應前端刪除失敗訊息
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 開關帳單(帳夾)
        /// </summary>
        /// <param name="folioNo">帳單號碼</param>
        /// <param name="closed">登錄管制</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("lock/{folioNo}/{closed}")]
        public MdApiMessage UpdateForFolioClosed(string folioNo, string closed)
		{
            try
            {
                // 呼叫商業元件執行新增作業
                var _result = BlCheckOut.ProcessFolioClosed(folioNo, closed);

                // 回應前端修改成功訊息
                if (closed != "*")                     
                    return HttpContext.Response.SendSuccess(
                        string.Format(Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_UnLockFolioSuccess"), folioNo));
                else
                    return HttpContext.Response.SendSuccess(
                        string.Format(Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_LockFolioSuccess"), folioNo));

            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 退房作業
        /// </summary>
        /// <param name="roomNo">房號</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("checkout/{roomNo}")]
		public MdApiMessage UpdateForCheckOut(string roomNo)
		{
			try
			{
				// 呼叫商業元件執行新增作業
				var _result = BlCheckOut.ProcessCheckOut(roomNo);

                // 回應前端修改成功訊息
                return HttpContext.Response.SendSuccess(
                    string.Format(Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_CheckOutSuccessMsg"), roomNo));
            }
			catch (Exception ex)
			{
				// 回應前端新增失敗訊息
				return HttpContext.Response.UpdateFailed(ex);
			}
		}

        /// <summary>
        /// 轉帳作業(可以用帳夾或是逐筆帳務傳入)
        /// </summary>
        /// <param name="transferInRoomNo">轉入房號</param>
        /// <param name="transferInFolioNo">轉入帳夾</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("transfer/{transferInRoomNo}/{transferInFolioNo}")]
        public MdApiMessage UpdateForTransfer(string transferInRoomNo, string transferInFolioNo, [FromBody] MdTransfer obj)
        {
            try
            {                
                // 呼叫商業元件執行修改作業
                int _result = BlCheckOut.ProcessTransfer(transferInRoomNo, transferInFolioNo, obj, ClientContent);

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
        /// 大批退房作業
        /// </summary>
        /// <param name="roomNos">房號集合，以,區隔</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPut("bulkcheckout/{roomNos}")]
        public MdApiMessage UpdateForBulkCheckOut(string roomNos)
        {
            try
			{
                // 呼叫商業元件執行新增作業
                var _result = BlCheckOut.ProcessBulkCheckOut(roomNos);

                // 回應前端修改成功訊息
                return HttpContext.Response.SendSuccess(
                    string.Format(Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_CheckOutSuccessMsg"), roomNos));
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
