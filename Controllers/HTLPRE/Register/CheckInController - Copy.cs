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
using GUIStd.DAL.AllNewHTL.Models.Private.Register;
using GUIStd.DAL.AllNewHTL.Models;
using GUIStd.Models;
using MGUIBAAPI.Models.HTLPRE;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE.Register
{
    /// <summary>
    /// 入住資料控制器
    /// </summary>
    [Route("htlpre/register/[controller]")]
    public class CheckInController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPreCheckIn BlPreCheckIn => new BlPreCheckIn(ClientContent);

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlArrivalList BlArrivalList => new BlArrivalList(ClientContent);

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlCheckIn BlCheckIn => new BlCheckIn(ClientContent);

        #endregion

        #region " 共用函式 - 取得住客資料 "

        /// <summary>
        /// 取得當日入住清單
        /// </summary>        
        /// <returns>當日入住資料模型物件</returns>
        [HttpGet("arrivallist")]
        public IEnumerable<MdArrivalList> GetData([FromQuery] string checkInDate = "")
        {
            return BlArrivalList.GetArrivalListData(checkInDate);
        }        

        /// <summary>
        /// 取得唯一的住客資料
        /// </summary>
        /// <param name="visitNo">住客號碼路徑參數</param>
        /// <returns>住客資料模型物件</returns>
        [HttpGet("precheck/{visitNo}")]
        public MdPreCheckIn GetRow(string visitNo)
        {
            return BlPreCheckIn.GetPreCheckInData(visitNo);
        }

        /// <summary>
        /// 取得分頁頁次的住客清單
        /// </summary>
        /// <param name="checkInDate">入住日期</param>
        /// <param name="guestName">旅客姓名</param>
        /// <param name="mobileNo">手機號碼</param>
        /// <param name="eMail">電子郵件</param>
        /// <param name="rvNo">訂房號碼</param>
        /// <param name="contractCompany">合約公司</param>
        /// <param name="roomNo">房號</param>
        /// <param name="ciStatus">入住狀態</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <returns>分頁住客資料模型物件</returns>
        [HttpGet("visitdata/{checkInDate}/pages/{pageNo}")]
        public MdVisitData_p GetVisitData([FromRoute] string checkInDate, [DARange(1, int.MaxValue)] int pageNo,
            [FromQuery] string guestName = "", [FromQuery] string mobileNo = "", [FromQuery] string eMail = "", 
            [FromQuery] string rvNo = "", [FromQuery] string contractCompany = "", [FromQuery] string roomNo = "",
            [FromQuery] string ciStatus = "")
        {            
            return BlCheckIn.GetDataByPage(VS03:checkInDate, GR03:guestName,CN09: mobileNo,CN10: eMail, RV01:rvNo, 
                RV10:contractCompany,VS06: roomNo,VS17: ciStatus, currentLang: CurrentLang, funcName: ControlName, pageNo: pageNo);
        }

        /// <summary>
        /// 取得訂房清單及底下的住客資料
        /// </summary>
        /// <param name="reservationNo">訂房號碼路徑參數</param>
        /// <returns>訂房及住客資料模型物件</returns>
        [HttpGet("{reservationno}")]
        public CmHTRGM01_d GetRVData(string reservationNo)
        {            
            MdRVData _rvDetail;
            IEnumerable<MdVisitData> _vsDetails;

            BlCheckIn.GetRVInfo(RV01: reservationNo, currentLang: CurrentLang, rvDetail: out _rvDetail, vsDetails: out _vsDetails);            

            return new CmHTRGM01_d()
            {
                RVDetail = _rvDetail,
                VSDetails = _vsDetails
            };
        }

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 修改資料
		/// </summary>		        
		/// <param name="obj">住客資料模型物件</param>
		/// <returns>系統規範訊息物件</returns>
		[HttpPut("precheck")]
        public MdApiMessage Update([FromBody] MdPreCheckIn obj)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlPreCheckIn.ProcessUpdate(obj, ClientContent);

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
        /// 入住作業
        /// </summary>        
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("")]
        public MdApiMessage UpdateForCheckIn([FromBody] MdVisitor obj)
        {
            try
            {
                IEnumerable<MdCheckIn> _checkinDetails;

                // 呼叫商業元件執行入住作業
                int _result = BlCheckIn.ProcessCheckIn(obj, ClientContent, out _checkinDetails);

                // 回應前端新增成功訊息
                return HttpContext.Response.UpdateSuccess(_result, responseData: _checkinDetails);
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 取消入住檢核作業
        /// </summary>        
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("cancelCheck")]
        public MdApiMessage CancelCheckInCheck([FromBody] MdCheckIn obj)
		{
            try
            {
                // 呼叫商業元件執行入住檢核作業
                BlCheckIn.ProcessCancelCheckInCheckCheck(obj);
                
                // 回應前端修改成功訊息                
                return HttpContext.Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_CheckSuccess"));
            }
            catch (Exception ex)
            {
                // 回應前端新增失敗訊息
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 取消入住作業
        /// </summary>
        /// <param name="cleanRoom">是否變更乾淨房路徑參數</param>
        /// <param name="obj"></param>
        /// <returns></returns>
        [HttpPut("cancel/{cleanRoom}")]
        public MdApiMessage UpdateForCancelCheckIn(string cleanRoom, [FromBody] MdCheckIn obj)
        {
            try
            {
                // 呼叫商業元件執行入住作業
                int _result = BlCheckIn.ProcessCancelCheckIn(cleanRoom, obj);
                
                // 回應前端修改成功訊息                
                return HttpContext.Response.SendSuccess(
                    Localization.GetValue(Enums.ResourceLang.LangHTL, "PgmMsg_CancelCheckInSuccessMsg"));
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
