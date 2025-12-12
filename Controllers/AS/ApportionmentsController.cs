#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.Models;
using GUIStd.BLL.AllNewAS.Private;
using GUIStd.DAL.AllNewAS.Models.Private.Apportionment;
using GUIStd.DAL.AllNewAS.Models;
using GUICore.Web;
using GUIStd.BLL.PY.Private;
using GUIStd.BLL.GUI;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewAS.Models.Private.vASP07;

#endregion

namespace MGUIBAAPI.Controllers.AS
{
    /// <summary>
    /// 財產費用分攤資料控制器
    /// </summary>
    [Route("as/[controller]")]
    public class ApportionmentsController : GUIAppAuthController
    {

        #region " 私用屬性 "

        ///// <summary>
        ///// 商業邏輯物件屬性
        ///// </summary>
        private BlApportionment BlApportionment => new BlApportionment(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 查詢財產費用分攤比例
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="pageNo">頁次</param>
        /// <param name="assetIdStart">財產編號(起)</param>
        /// <param name="assetIdEnd">財產編號(迄)</param>
        /// <param name="apportionYearFrom">分配年度(起)</param>
        /// <param name="apportionYearTo">分配年度(迄)</param>
        /// <returns>財產費用分攤資料分頁資料</returns>
        [HttpGet("{companyId}/pages/{pageNo}")]
        public MdApportRule_p GetApportRules(string companyId, [DARange(1, int.MaxValue)] int pageNo, 
            [FromQuery] string assetIdStart, [FromQuery] string assetIdEnd, 
            [FromQuery, DAMaxLength(4), DAMinLength(4)] string apportionYearFrom, 
            [FromQuery, DAMaxLength(4), DAMinLength(4)] string apportionYearTo)
        {
            return BlApportionment.GetApportRules(companyId, assetIdStart, assetIdEnd, apportionYearFrom, apportionYearTo, pageNo, ControlName);
        }

        /// <summary>
        /// 年度財產費用分攤規則是否存在
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="assetId">財產編號</param>
        /// <param name="obtainedDate">取得日期</param>
        /// <param name="apportionYear">分配年度</param>
        /// <returns>是否存在</returns>
        [HttpGet("exists/{companyId}/{assetId}/{obtainedDate}/{apportionYear}")]
        public bool IsExists(string companyId, string assetId, [DADate()] string obtainedDate,
            [DAMaxLength(4), DAMinLength(4)] string apportionYear)
        {
            return BlApportionment.IsExistsApportion(companyId, assetId, obtainedDate, apportionYear);
        }

        /// <summary>
        /// 取得財產費用分攤設定
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="obtainedDate">取得日期</param>
        /// <param name="assetId">財產編號</param>
        /// <param name="apportionYear">分配年度</param>
        /// <returns>財產費用分攤明細資料</returns>
        [HttpGet("{companyId}/{assetId}/{obtainedDate}/{apportionYear}")]
        public MdApportDetail GetApportionments(string companyId, string assetId,[DADate()] string obtainedDate,
            [DAMaxLength(4), DAMinLength(4)] string apportionYear)
        {
            return BlApportionment.GetApportionments(companyId, assetId, obtainedDate, apportionYear);
        }

        /// <summary>
        /// 取得財產交易分攤紀錄
        /// </summary>
        /// <param name="queryParams">財產交易分攤查詢參數物件</param>
        /// <returns></returns>
        [HttpPost("trans/query")]
        public IEnumerable<MdApportAsset> GetTrans([FromBody] MdASP07_q queryParams)
        {
            return BlApportionment.GetTrans(queryParams);
        }


        /// <summary>
        /// 取得年月待分攤財產交易
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="apportionYearMonth">分攤年月</param>
        /// <returns></returns>
        [HttpGet("trans/{companyId}/{apportionYearMonth}")]
        public IEnumerable<MdApportAsset> ApportionTrans(string companyId,
            [DAMaxLength(6), DAMinLength(6)] string apportionYearMonth)
        {
            return BlApportionment.ApportionTrans(companyId, apportionYearMonth);
        }

        /// <summary>
        /// 檢查存在未來已分攤，或過去未分攤之交易紀錄
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="apportionYearMonth">分攤年月</param>
        /// <param name="transInFutrue">找未來交易</param>
        /// <returns>是否存在</returns>
        [HttpGet("trans/exists/{companyId}/{apportionYearMonth}")]
        public bool IsExistsTrans(string companyId,
            [DAMaxLength(6), DAMinLength(6)] string apportionYearMonth, [FromQuery] bool transInFutrue)
        {
            return BlApportionment.IsExistsApportionTrans(companyId, apportionYearMonth, transInFutrue);
        }

        #endregion

        #region " 共用屬性 - 異動資料"

        /// <summary>
        /// 鏡像財產費用分攤設定
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="obtainedDate">取得日期</param>
        /// <param name="assetId">財產編號</param>
        /// <param name="apportionYear">分配年度</param>
        /// <param name="objs">財產費用分攤設定資料</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("{companyId}/{assetId}/{obtainedDate}/{apportionYear}")]
        public MdApiMessage UpsertRules(string companyId, string assetId, [DADate()] string obtainedDate,
            [DAMaxLength(4), DAMinLength(4)] string apportionYear,
            [FromBody] IEnumerable<MdApportionment_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlApportionment.UpsertRules(companyId, assetId, obtainedDate, apportionYear, objs,
                    out string addDate, out string addTime, out string addUserId, out string updateDate, out string updateTime, out string updateUserId);
                var _resultObj = HttpContext.Response.UpdateSuccess(
                    _result,
                    responseData: new Hashtable()
                    {
                        { "addDate", addDate },
                        { "addTime", addTime},
                        { "addUserId", addUserId},
                        { "updateDate", updateDate},
                        { "updateTime", updateTime},
                        { "updateUserId", updateUserId}
                    }
                );
                // 回應前端修改成功訊息 
                return _resultObj;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }

        }

        /// <summary>
        /// 刪除財產費用分攤設定
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="obtainedDate">取得日期</param>
        /// <param name="assetId">財產編號</param>
        /// <param name="apportionYear">分配年度</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("{companyId}/{assetId}/{obtainedDate}/{apportionYear}")]
        public MdApiMessage Delete(string companyId, string assetId, [DADate()] string obtainedDate,
            [DAMaxLength(4), DAMinLength(4)] string apportionYear)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlApportionment.Delete(companyId, assetId, obtainedDate, apportionYear);
                var _resultObj = HttpContext.Response.DeleteSuccess(_result);
                // 回應前端修改成功訊息 
                return _resultObj;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 寫入財產交易分攤紀錄
        /// </summary>
        /// <param name="objs">財產交易分攤資料模型泛型集合物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("trans")]
        public MdApiMessage DoApportion(IEnumerable<MdApportTrans_w> objs)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlApportionment.ProcessApportion(objs);
                
                var _resultObj = HttpContext.Response.InsertSuccess(_result);

                // 回應前端修改成功訊息 
                return _resultObj;
            }
            catch (Exception ex)
            {
                // 回應前端修改失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 刪除年月財產交易分攤紀錄
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="apportionYearMonth">分攤年月</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpDelete("trans/{companyId}/{apportionYearMonth}")]
        public MdApiMessage DeleteTrans(string companyId, [DAMaxLength(6), DAMinLength(6)] string apportionYearMonth)
        {
            try
            {
                // 呼叫商業元件執行修改作業
                int _result = BlApportionment.DeleteTrans(companyId, apportionYearMonth);
                var _resultObj = HttpContext.Response.DeleteSuccess(_result);
                // 回應前端修改成功訊息 
                return _resultObj;
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
