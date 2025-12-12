#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】PY系統補充保費計算控制器(Pattern開發模式)
    /// </summary>
    [Route("py/[controller]")]
    public class CalculationController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 補充保費商業邏輯物件屬性
        /// </summary>
        private BlCalculation BlCalculation => mBlCalculation = mBlCalculation ?? new BlCalculation(this.ClientContent);
        private BlCalculation mBlCalculation;

        #endregion

        #region " 補充保費 - 查詢資料 "

        /// <summary>
        /// 取得分頁補充保費資料
        /// </summary>
        /// <param name="companyId">公司別</param>
        /// <param name="payrollDate">發薪日期(YYYYMMDD)</param>
        /// <param name="calculationType">計算類別 (1.超額獎金/2.兼職所得/3.執行業務收入/4.股利所得/5.租金收入/6.利息所得) - 可選</param>
        /// <param name="employeeSocialId">員工身份證字號 - 可選</param>
        /// <param name="pageNo">查詢頁次</param>
        /// <returns>分頁補充保費資料模型物件</returns>
        [HttpPost("supplemental/query/{companyId}/{payrollDate}/pages/{pageNo}")]
        public MdSupplementalInsurance_p GetData(
            string companyId, string payrollDate,
            [FromQuery] string calculationType,
            [FromQuery] string employeeSocialId,
            [DARange(1, int.MaxValue)] int pageNo)
        {
            return BlCalculation.GetData(companyId, payrollDate, calculationType, employeeSocialId, ControlName, pageNo);
        }

        #endregion

        #region " 補充保費 - 執行計算 "

        /// <summary>
        /// 執行補充保費計算 - 回傳計算結果 (不寫入資料庫)
        /// </summary>
        /// <param name="companyId">公司別清單(多個用逗號分隔)</param>
        /// <param name="payrollDate">發薪日期(YYYYMMDD)</param>
        /// <param name="calculationType">計算類別 (1.超額獎金/2.兼職所得/3.執行業務收入/4.股利所得/5.租金收入/6.利息所得) - 可選</param>
        /// <param name="employeeSocialId">員工身份證字號 - 可選</param>
        /// <returns>計算結果包裝物件</returns>
        [HttpPost("supplemental/calculate/{companyId}/{payrollDate}")]
        public MdSupplementalInsurance_p CalculateSupplementalInsurance(
            string companyId, string payrollDate,
            [FromQuery] string calculationType,
            [FromQuery] string employeeSocialId)
        {
            var _results = BlCalculation.CalculateSupplementalInsurance(
                companyId, 
                payrollDate, 
                calculationType, 
                employeeSocialId
            );

            var _resultList = _results?.ToList() ?? new List<MdSupplementalInsurance>();

            return new MdSupplementalInsurance_p
            {
                SupplementalInsurances = _resultList,
                Paging = new MdPaging
                {
                    TotalRows = _resultList.Count,
                    RowsPerPage = _resultList.Count,
                    CurrentPage = 1
                }
            };
        }

        /// <summary>
        /// 儲存補充保費計算結果 - 批次寫入資料庫
        /// </summary>
        /// <param name="request">計算結果請求物件</param>
        /// <returns>系統規範訊息物件</returns>
        [HttpPost("supplemental/save")]
        public MdApiMessage SaveCalculationResults([FromBody] MdSupplementalInsuranceSaveRequest request)
        {
            try
            {
                // 呼叫商業元件執行儲存作業
                int _result = BlCalculation.SaveCalculationResults(
                    request.CalculationResults,
                    request.CompanyId,
                    request.PayrollDate,
                    request.CalculationType,
                    request.EmployeeSocialId
                );

                // 回應前端儲存成功訊息
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                // 回應前端儲存失敗訊息
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        #endregion

        #region " 補充保費 - 查詢參數 "

        /// <summary>
        /// 查詢健保補充保費參數
        /// 比照 VB6: Get_HealthFee
        /// </summary>
        /// <param name="nowDate">計算日期 (YYYYMMDD) 路徑參數</param>
        /// <returns>補充保費參數模型物件</returns>
        [HttpGet("supplemental/parameters/{nowDate}")]
        public MdHealthFeeParametersResult QueryHealthFeeParameters(string nowDate)
        {
            return BlCalculation.QueryHealthFeeParameters(nowDate);
        }

        #endregion

    }
}