#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private.PYTBM04;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】vPYTBM04 級距表公開 API（路由 py/bracket）。
    /// ARTHPY 級距：labor（PI）、health（PQ）、laborpension（P1）、incometax（PH）。
    /// 投保類別（SINI）、職災費率、勞退提撥（A01）：inscategory、occaccident、pensioncontrib。
    /// 對應前端 APIConst.py.bracket.*；頁籤 UIData（及日後 GetHelp）由 vPYTBM04Controller（py/private/vpytbm04）負責。
    /// </summary>
    [Route("py/bracket")]
    public class BracketController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 勞保費級距（ARTHPY.PI）商業邏輯物件屬性
        /// </summary>
        private BlPI BlPI => mBlPI = mBlPI ?? new BlPI(ClientContent);
        private BlPI mBlPI;

        /// <summary>
        /// 健保費級距（ARTHPY.PQ）商業邏輯物件屬性
        /// </summary>
        private BlPQ BlPQ => mBlPQ = mBlPQ ?? new BlPQ(ClientContent);
        private BlPQ mBlPQ;

        /// <summary>
        /// 勞退級距（ARTHPY.P1）商業邏輯物件屬性
        /// </summary>
        private BlP1 BlP1 => mBlP1 = mBlP1 ?? new BlP1(ClientContent);
        private BlP1 mBlP1;

        /// <summary>
        /// 所得稅級距（ARTHPY.PH）商業邏輯物件屬性
        /// </summary>
        private BlPH BlPH => mBlPH = mBlPH ?? new BlPH(ClientContent);
        private BlPH mBlPH;

        /// <summary>
        /// vPYTBM04 投保類別／職災／提撥（SINI、A01）商業邏輯物件屬性
        /// </summary>
        private BlPYTBM04 BlvPYTBM04 => mBlvPYTBM04 = mBlvPYTBM04 ?? new BlPYTBM04(ClientContent);
        private BlPYTBM04 mBlvPYTBM04;

        #endregion

        #region " 共用函式 - 查詢資料（勞保 labor） "

        /// <summary>
        /// 取得分頁勞保費級距資料（依 InsAmount 排序）
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數；0 表示由商業層決定</param>
        /// <returns>勞保費級距分頁資料模型</returns>
        [HttpPost("query/labor/pages/{pageNo}")]
        public MdLaborInsuranceBracket_p GetData(
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0)
        {
            return BlPI.GetData(ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 判斷勞保投保金額是否已存在
        /// </summary>
        /// <param name="insAmount">投保金額 PI01</param>
        /// <returns>是否已存在</returns>
        [HttpGet("exists/labor/{insAmount}")]
        public bool IsExist(decimal insAmount)
        {
            return BlPI.IsExist(insAmount);
        }

        /// <summary>
        /// 取得單筆勞保費級距
        /// </summary>
        /// <param name="insAmount">投保金額 PI01</param>
        /// <returns>勞保費級距資料模型</returns>
        [HttpGet("labor/{insAmount}")]
        public MdLaborInsuranceBracket GetRow(decimal insAmount)
        {
            return BlPI.GetRow(insAmount);
        }

        #endregion

        #region " 共用函式 - 異動資料（勞保 labor） "

        /// <summary>
        /// 新增勞保費級距
        /// </summary>
        /// <param name="obj">勞保費級距資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPost("labor")]
        public MdApiMessage Insert([FromBody] MdLaborInsuranceBracket obj)
        {
            try
            {
                int _result = BlPI.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改勞保費級距（路徑為原 InsAmount）
        /// </summary>
        /// <param name="insAmount">原投保金額 PI01</param>
        /// <param name="obj">勞保費級距資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPut("labor/{insAmount}")]
        public MdApiMessage Update(decimal insAmount, [FromBody] MdLaborInsuranceBracket obj)
        {
            try
            {
                int _result = BlPI.ProcessUpdate(insAmount, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除勞保費級距
        /// </summary>
        /// <param name="insAmount">投保金額 PI01</param>
        /// <returns>API 訊息物件</returns>
        [HttpDelete("labor/{insAmount}")]
        public MdApiMessage Delete(decimal insAmount)
        {
            try
            {
                int _result = BlPI.ProcessDelete(insAmount);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 查詢資料（健保 health） "

        /// <summary>
        /// 取得分頁健保費級距資料（依 InsAmount 排序）
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數；0 表示由商業層決定</param>
        /// <returns>健保費級距分頁資料模型</returns>
        [HttpPost("query/health/pages/{pageNo}")]
        public MdHealthInsuranceBracket_p GetHealthData(
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0)
        {
            return BlPQ.GetData(ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 判斷健保投保金額是否已存在
        /// </summary>
        /// <param name="insAmount">投保金額 PQ01</param>
        /// <returns>是否已存在</returns>
        [HttpGet("exists/health/{insAmount}")]
        public bool IsHealthExist(decimal insAmount)
        {
            return BlPQ.IsExist(insAmount);
        }

        /// <summary>
        /// 取得單筆健保費級距
        /// </summary>
        /// <param name="insAmount">投保金額 PQ01</param>
        /// <returns>健保費級距資料模型</returns>
        [HttpGet("health/{insAmount}")]
        public MdHealthInsuranceBracket GetHealthRow(decimal insAmount)
        {
            return BlPQ.GetRow(insAmount);
        }

        #endregion

        #region " 共用函式 - 異動資料（健保 health） "

        /// <summary>
        /// 新增健保費級距
        /// </summary>
        /// <param name="obj">健保費級距資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPost("health")]
        public MdApiMessage InsertHealth([FromBody] MdHealthInsuranceBracket obj)
        {
            try
            {
                int _result = BlPQ.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改健保費級距（路徑為原 InsAmount）
        /// </summary>
        /// <param name="insAmount">原投保金額 PQ01</param>
        /// <param name="obj">健保費級距資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPut("health/{insAmount}")]
        public MdApiMessage UpdateHealth(decimal insAmount, [FromBody] MdHealthInsuranceBracket obj)
        {
            try
            {
                int _result = BlPQ.ProcessUpdate(insAmount, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除健保費級距
        /// </summary>
        /// <param name="insAmount">投保金額 PQ01</param>
        /// <returns>API 訊息物件</returns>
        [HttpDelete("health/{insAmount}")]
        public MdApiMessage DeleteHealth(decimal insAmount)
        {
            try
            {
                int _result = BlPQ.ProcessDelete(insAmount);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 查詢資料（勞退 laborpension） "

        /// <summary>
        /// 取得分頁勞退級距資料（依 WageFrom 排序）
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數；0 表示由商業層決定</param>
        /// <returns>勞退級距分頁資料模型</returns>
        [HttpPost("query/laborpension/pages/{pageNo}")]
        public MdLaborPensionBracket_p GetLaborPensionData(
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0)
        {
            return BlP1.GetData(ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 判斷實際工資起值是否已存在
        /// </summary>
        /// <param name="wageFrom">實際工資起值 P101</param>
        /// <returns>是否已存在</returns>
        [HttpGet("exists/laborpension/{wageFrom}")]
        public bool IsLaborPensionExist(decimal wageFrom)
        {
            return BlP1.IsExist(wageFrom);
        }

        /// <summary>
        /// 取得單筆勞退級距
        /// </summary>
        /// <param name="wageFrom">實際工資起值 P101</param>
        /// <returns>勞退級距資料模型</returns>
        [HttpGet("laborpension/{wageFrom}")]
        public MdLaborPensionBracket GetLaborPensionRow(decimal wageFrom)
        {
            return BlP1.GetRow(wageFrom);
        }

        #endregion

        #region " 共用函式 - 異動資料（勞退 laborpension） "

        /// <summary>
        /// 新增勞退級距
        /// </summary>
        /// <param name="obj">勞退級距資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPost("laborpension")]
        public MdApiMessage InsertLaborPension([FromBody] MdLaborPensionBracket obj)
        {
            try
            {
                int _result = BlP1.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改勞退級距（路徑為原 WageFrom）
        /// </summary>
        /// <param name="wageFrom">原實際工資起值 P101</param>
        /// <param name="obj">勞退級距資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPut("laborpension/{wageFrom}")]
        public MdApiMessage UpdateLaborPension(decimal wageFrom, [FromBody] MdLaborPensionBracket obj)
        {
            try
            {
                int _result = BlP1.ProcessUpdate(wageFrom, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除勞退級距
        /// </summary>
        /// <param name="wageFrom">實際工資起值 P101</param>
        /// <returns>API 訊息物件</returns>
        [HttpDelete("laborpension/{wageFrom}")]
        public MdApiMessage DeleteLaborPension(decimal wageFrom)
        {
            try
            {
                int _result = BlP1.ProcessDelete(wageFrom);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 查詢資料（所得稅 incometax） "

        /// <summary>
        /// 取得分頁所得稅級距資料（依 PH01、PH02 排序）
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數；0 表示由商業層決定</param>
        /// <returns>所得稅級距分頁資料模型</returns>
        [HttpPost("query/incometax/pages/{pageNo}")]
        public MdIncomeTaxBracket_p GetIncomeTaxData(
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0)
        {
            return BlPH.GetData(ControlName, pageNo, ref rowsPerPage);
        }

        /// <summary>
        /// 判斷所得金額起迄組合是否已存在
        /// </summary>
        /// <param name="incomeFrom">所得金額起 PH01</param>
        /// <param name="incomeTo">所得金額迄 PH02</param>
        /// <returns>是否已存在</returns>
        [HttpGet("exists/incometax/{incomeFrom}/{incomeTo}")]
        public bool IsIncomeTaxExist(decimal incomeFrom, decimal incomeTo)
        {
            return BlPH.IsExist(incomeFrom, incomeTo);
        }

        /// <summary>
        /// 取得單筆所得稅級距
        /// </summary>
        /// <param name="incomeFrom">所得金額起 PH01</param>
        /// <param name="incomeTo">所得金額迄 PH02</param>
        /// <returns>所得稅級距資料模型</returns>
        [HttpGet("incometax/{incomeFrom}/{incomeTo}")]
        public MdIncomeTaxBracket GetIncomeTaxRow(decimal incomeFrom, decimal incomeTo)
        {
            return BlPH.GetRow(incomeFrom, incomeTo);
        }

        #endregion

        #region " 共用函式 - 異動資料（所得稅 incometax） "

        /// <summary>
        /// 新增所得稅級距
        /// </summary>
        /// <param name="obj">所得稅級距資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPost("incometax")]
        public MdApiMessage InsertIncomeTax([FromBody] MdIncomeTaxBracket obj)
        {
            try
            {
                int _result = BlPH.ProcessInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改所得稅級距（路徑為原 incomeFrom、incomeTo）
        /// </summary>
        /// <param name="incomeFrom">原所得金額起 PH01</param>
        /// <param name="incomeTo">原所得金額迄 PH02</param>
        /// <param name="obj">所得稅級距資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPut("incometax/{incomeFrom}/{incomeTo}")]
        public MdApiMessage UpdateIncomeTax(
            decimal incomeFrom,
            decimal incomeTo,
            [FromBody] MdIncomeTaxBracket obj)
        {
            try
            {
                int _result = BlPH.ProcessUpdate(incomeFrom, incomeTo, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除所得稅級距
        /// </summary>
        /// <param name="incomeFrom">所得金額起 PH01</param>
        /// <param name="incomeTo">所得金額迄 PH02</param>
        /// <returns>API 訊息物件</returns>
        [HttpDelete("incometax/{incomeFrom}/{incomeTo}")]
        public MdApiMessage DeleteIncomeTax(decimal incomeFrom, decimal incomeTo)
        {
            try
            {
                int _result = BlPH.ProcessDelete(incomeFrom, incomeTo);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 勞健保投保類別（inscategory） "

        /// <summary>
        /// 取得勞健保投保類別列表（SINI 三 section 單一 SQL）
        /// </summary>
        /// <returns>投保類別資料模型集合</returns>
        [HttpGet("query/inscategory/list")]
        public IList<MdPYTBM04InsCategory> GetInsCategoryList() => BlvPYTBM04.GetInsCategoryList();

        /// <summary>
        /// 判斷投保別代碼是否已存在
        /// </summary>
        /// <param name="topic">投保別代碼</param>
        /// <returns>是否已存在</returns>
        [HttpGet("exists/inscategory/{topic}")]
        public bool IsInsCategoryExist(string topic) => BlvPYTBM04.IsInsCategoryExist(topic);

        /// <summary>
        /// 新增勞健保投保類別
        /// </summary>
        /// <param name="obj">投保類別資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPost("inscategory")]
        public MdApiMessage InsertInsCategory([FromBody] MdPYTBM04InsCategory obj)
        {
            try
            {
                int _result = BlvPYTBM04.ProcessInsCategoryInsert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改勞健保投保類別（路徑為原投保別代碼）
        /// </summary>
        /// <param name="originalTopic">原投保別代碼</param>
        /// <param name="obj">投保類別資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPut("inscategory/{originalTopic}")]
        public MdApiMessage UpdateInsCategory(string originalTopic, [FromBody] MdPYTBM04InsCategory obj)
        {
            try
            {
                int _result = BlvPYTBM04.ProcessInsCategoryUpdate(originalTopic, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除勞健保投保類別
        /// </summary>
        /// <param name="topic">投保別代碼</param>
        /// <returns>API 訊息物件</returns>
        [HttpDelete("inscategory/{topic}")]
        public MdApiMessage DeleteInsCategory(string topic)
        {
            try
            {
                int _result = BlvPYTBM04.ProcessInsCategoryDelete(topic);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 職災保險費率（occaccident） "

        /// <summary>
        /// 取得職災保險費率列表（A01 + Occupation_Risk）
        /// </summary>
        /// <returns>職災保險費率資料模型集合</returns>
        [HttpGet("query/occaccident/list")]
        public IList<MdPYTBM04OccAccident> GetOccAccidentList() => BlvPYTBM04.GetOccAccidentList();

        /// <summary>
        /// 修改職災保險費率
        /// </summary>
        /// <param name="companyCode">公司代碼</param>
        /// <param name="obj">職災保險費率資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPut("occaccident/{companyCode}")]
        public MdApiMessage UpdateOccAccident(string companyCode, [FromBody] MdPYTBM04OccAccident obj)
        {
            try
            {
                int _result = BlvPYTBM04.ProcessOccAccidentUpdate(companyCode, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion

        #region " 共用函式 - 勞退金提撥比例（pensioncontrib） "

        /// <summary>
        /// 取得勞退金提撥比例列表（A01 + NewRetirementRate1 / NewRetirementRate2）
        /// </summary>
        /// <returns>勞退金提撥比例資料模型集合</returns>
        [HttpGet("query/pensioncontrib/list")]
        public IList<MdPYTBM04PensionContrib> GetPensionContribList() => BlvPYTBM04.GetPensionContribList();

        /// <summary>
        /// 修改勞退金提撥比例
        /// </summary>
        /// <param name="companyCode">公司代碼</param>
        /// <param name="obj">勞退金提撥比例資料模型</param>
        /// <returns>API 訊息物件</returns>
        [HttpPut("pensioncontrib/{companyCode}")]
        public MdApiMessage UpdatePensionContrib(string companyCode, [FromBody] MdPYTBM04PensionContrib obj)
        {
            try
            {
                int _result = BlvPYTBM04.ProcessPensionContribUpdate(companyCode, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        #endregion
    }
}
