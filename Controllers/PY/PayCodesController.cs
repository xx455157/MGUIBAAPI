#region " 匯入的名稱空間：Framework "

using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using GUIStd.Attributes;
using GUIStd.BLL.AllNewPY;
using GUIStd.DAL.AllNewPY.Models;
using GUIStd.DAL.AllNewPY.Models.Private;
using GUIStd.Extensions;
using GUIStd.Models;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】ARTHPY.PC 支薪代碼維護及 ARTHPY.PCA 會計科目設定（vPYTBM02、vPYTBM20），路由 py/paycodes
    /// </summary>
    [Route("py/[controller]")]
    public class PayCodesController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPC BlPC => mBlPC = mBlPC ?? new BlPC(ClientContent);
        private BlPC mBlPC;

        private BlPF BlPF => mBlPF = mBlPF ?? new BlPF(ClientContent);
        private BlPF mBlPF;

        private BlPCA BlPCA => mBlPCA = mBlPCA ?? new BlPCA(ClientContent);
        private BlPCA mBlPCA;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得支薪代碼輔助清單（ARTHPY.PC：支薪代碼 PC01、支薪名稱 PC02 等；供 vPYTBM02／vPYTBM20 下拉篩選）
        /// </summary>
        /// <param name="includeEmptyRow">是否包含空白列</param>
        /// <param name="includeId">是否一併傳回代碼欄位</param>
        /// <param name="includeIdIncomeTax">預設 false 僅篩 PC04 列入所得為 1 或 2；true 時不篩 PC04（全檔支薪代碼）</param>
        /// <returns>支薪代碼輔助清單集合</returns>
        [HttpGet("help")]
        public IEnumerable<MdPayCode> GetHelp(
            [FromQuery] bool includeEmptyRow = false,
            [FromQuery] bool includeId = false,
            [FromQuery] bool includeIdIncomeTax = false)
        {
            return BlPC.GetHelp(includeEmptyRow, includeId, includeIdIncomeTax);
        }

        /// <summary>
        /// 依參數 id 取得支薪相關付款資料（舊介面；非 vPYTBM02 主維護 API，其他程式沿用）
        /// </summary>
        /// <param name="id">查詢識別字串</param>
        /// <returns>付款相關資料列集合</returns>
        [HttpGet("query/{id}")]
        public IEnumerable<MdPayment> GetData(string id)
        {
            return BlPF.GetData(id);
        }

        /// <summary>
        /// 取得分頁支薪代碼維護資料（ARTHPY.PC：支薪代碼 PC01、支薪名稱 PC02、加減項 PC03、列入所得 PC04、固定／變動所得 PC06、所得類別 PC07 等；含 SINI／PD 對應欄；vPYTBM02）
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數，0 時由後端依 SINI 取得</param>
        /// <param name="payCodeStart">支薪代碼(起)，選填</param>
        /// <param name="payCodeEnd">支薪代碼(迄)，選填</param>
        /// <param name="payNameKeyword">支薪名稱關鍵字，選填</param>
        /// <param name="incomeCategoryUi">所得類別條件（Query 字串），選填</param>
        /// <param name="addSubItems">加減項（PC03）單一選值代碼，選填</param>
        /// <param name="includeIncomeCode">列入所得條件（Query 字串），選填</param>
        /// <param name="fixedVariableItems">固定／變動所得（PC06）單一選值代碼，選填</param>
        /// <param name="filterInsuranceBase">是否篩選「列入投保級距」</param>
        /// <param name="filterLaborRetire">是否篩選「列入舊制勞退」</param>
        /// <param name="filterSupplementPremium">是否篩選「列入獎金補充保費」</param>
        /// <returns>分頁支薪代碼維護查詢結果（items、paging）</returns>
        [HttpPost("query/pages/{pageNo}")]
        public MdPayCodeMaint_p GetMaintData(
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0,
            string payCodeStart = null,
            string payCodeEnd = null,
            string payNameKeyword = null,
            string incomeCategoryUi = null,
            string addSubItems = null,
            string includeIncomeCode = null,
            string fixedVariableItems = null,
            bool filterInsuranceBase = false,
            bool filterLaborRetire = false,
            bool filterSupplementPremium = false)
        {
            return BlPC.GetPayCodeMaintData(
                ControlName,
                pageNo,
                ref rowsPerPage,
                payCodeStart,
                payCodeEnd,
                payNameKeyword,
                incomeCategoryUi,
                addSubItems,
                includeIncomeCode,
                fixedVariableItems,
                filterInsuranceBase,
                filterLaborRetire,
                filterSupplementPremium);
        }

        /// <summary>
        /// 判斷支薪代碼是否已存在（ARTHPY.PC01；供 vPYTBM02 新增檢核；須在單筆 payCode 路由之前註冊）
        /// </summary>
        /// <param name="payCode">支薪代碼路徑參數（PC01）</param>
        /// <returns>已存在為 true，否則為 false</returns>
        [HttpGet("exists/{payCode}")]
        public bool IsExist(string payCode)
        {
            return BlPC.IsExist(payCode);
        }

        /// <summary>
        /// 判斷支薪名稱是否與他筆重複（ARTHPY.PC02；供 vPYTBM02 異動檢核）
        /// </summary>
        /// <param name="payName">支薪名稱（PC02）</param>
        /// <param name="excludePayCode">排除之支薪代碼（編輯時傳入，與目前筆 PC01 相同則不視為重複）</param>
        /// <returns>與他筆重複為 true，否則為 false</returns>
        [HttpGet("existsname")]
        public bool IsExistPayName([FromQuery] string payName, [FromQuery] string excludePayCode = null)
        {
            return BlPC.IsExistPayName(payName, excludePayCode);
        }

        /// <summary>
        /// 判斷支薪英文名稱是否與他筆重複（ARTHPY.PC09；供 vPYTBM02 異動檢核）
        /// </summary>
        /// <param name="payNameE">支薪英文名稱（PC09）</param>
        /// <param name="excludePayCode">排除之支薪代碼（編輯時傳入）</param>
        /// <returns>與他筆重複為 true，否則為 false</returns>
        [HttpGet("existsnamee")]
        public bool IsExistPayNameEn([FromQuery] string payNameE, [FromQuery] string excludePayCode = null)
        {
            return BlPC.IsExistPayNameEn(payNameE, excludePayCode);
        }

        /// <summary>
        /// 取得單筆支薪代碼維護明細（ARTHPY.PC 及關聯 SINI；vPYTBM02）
        /// </summary>
        /// <param name="payCode">支薪代碼路徑參數（PC01）</param>
        /// <returns>單筆支薪代碼維護 DTO</returns>
        [HttpGet("{payCode}")]
        public MdPayCodeMaint GetMaintRow(string payCode)
        {
            return BlPC.GetMaintRow(payCode);
        }

        /// <summary>
        /// 取得分頁 PCA 支薪代碼會計科目綁定（ARTHPY.PCA，PCA03＝1：公司別 PCA01、部門 PCA02、支薪代碼 PCA04、主科目 PCA05、子科目 PCA06；併 PC 支薪名稱 PC02；vPYTBM20；條件與 vPYTBM02 對齊並加公司／科目）
        /// </summary>
        /// <param name="pageNo">查詢頁次</param>
        /// <param name="rowsPerPage">一頁筆數，0 時由後端依 SINI 取得</param>
        /// <param name="PCA04S">支薪代碼起（PCA04），選填</param>
        /// <param name="PCA04E">支薪代碼迄（PCA04），選填</param>
        /// <param name="PCA01N">支薪名稱關鍵字（篩 PC.PC02），選填</param>
        /// <param name="PCA01">公司別，選填</param>
        /// <param name="PCA02">部門別，選填</param>
        /// <param name="PCA05">主科目代碼，選填</param>
        /// <param name="PCA06">子科目代碼，選填</param>
        /// <param name="PC07eq">所得類別（PC07 等於），選填</param>
        /// <param name="PC03eq">加減項（PC03 等於），選填</param>
        /// <param name="PC04eq">列入所得（PC04 等於），選填</param>
        /// <param name="PC06eq">固定／變動所得（PC06 等於），選填</param>
        /// <param name="PCINSBASE">是否篩選「列入投保級距」</param>
        /// <param name="PCLABRET">是否篩選「列入舊制勞退」</param>
        /// <param name="PC08SUP">是否篩選「列入獎金補充保費」</param>
        /// <returns>分頁 PCA 綁定查詢結果（items、paging）</returns>
        [HttpPost("account/query/pages/{pageNo}")]
        public MdPayCodeAcct_p GetPCAData(
            [DARange(1, int.MaxValue)] int pageNo,
            int rowsPerPage = 0,
            string PCA04S = null,
            string PCA04E = null,
            string PCA01N = null,
            string PCA01 = null,
            string PCA02 = null,
            string PCA05 = null,
            string PCA06 = null,
            string PC07eq = null,
            string PC03eq = null,
            string PC04eq = null,
            string PC06eq = null,
            bool PCINSBASE = false,
            bool PCLABRET = false,
            bool PC08SUP = false)
        {
            return BlPCA.GetPCAData(
                ControlName,
                pageNo,
                ref rowsPerPage,
                PCA04S,
                PCA04E,
                PCA01N,
                PCA01,
                PCA02,
                PCA05,
                PCA06,
                PC07eq,
                PC03eq,
                PC04eq,
                PC06eq,
                PCINSBASE,
                PCLABRET,
                PC08SUP);
        }

        /// <summary>
        /// 取得單筆 PCA 綁定明細（ARTHPY.PCA；vPYTBM20；同公司與支薪代碼若有多筆須傳部門以唯一定位）
        /// </summary>
        /// <param name="companyId">公司別路徑參數（PCA01）</param>
        /// <param name="payCode">支薪代碼路徑參數（PCA04）</param>
        /// <param name="departmentId">部門別（PCA02），多筆時必填（Query）</param>
        /// <returns>單筆 PCA 綁定列 DTO</returns>
        [HttpGet("account/{companyId}/{payCode}")]
        public MdPayCodeAcct_row GetPCARow(
            string companyId,
            string payCode,
            [FromQuery] string departmentId = null) =>
            BlPCA.GetPCARow(companyId, payCode, departmentId);

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 新增支薪代碼（ARTHPY.PC／SINI；vPYTBM02；與 py/bank、py/incomecategory 異動路由風格一致，使用 insert 路徑）
        /// </summary>
        /// <param name="obj">支薪代碼維護本文（MdPayCodeMaint）</param>
        /// <returns>異動結果訊息封裝</returns>
        [HttpPost("insert")]
        public MdApiMessage Insert([FromBody] MdPayCodeMaint obj)
        {
            try
            {
                int _result = BlPC.Insert(obj);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (System.Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 修改支薪代碼（ARTHPY.PC／SINI；vPYTBM02；路徑代碼須與 Body PC01 一致）
        /// </summary>
        /// <param name="payCode">原支薪代碼路徑參數（須與 Body PC01 一致）</param>
        /// <param name="obj">支薪代碼維護本文（MdPayCodeMaint）</param>
        /// <returns>異動結果訊息封裝</returns>
        [HttpPut("{payCode}")]
        public MdApiMessage Update(string payCode, [FromBody] MdPayCodeMaint obj)
        {
            if (!payCode.EqualsIgnoreCase(obj.PC01))
                return HttpContext.Response.UpdateFailedWhenKeyNotSame();

            try
            {
                int _result = BlPC.Update(payCode, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (System.Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除支薪代碼（ARTHPY.PC；vPYTBM02）
        /// </summary>
        /// <param name="payCode">支薪代碼路徑參數（PC01）</param>
        /// <returns>異動結果訊息封裝</returns>
        [HttpDelete("{payCode}")]
        public MdApiMessage Delete(string payCode)
        {
            try
            {
                int _result = BlPC.Delete(payCode);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (System.Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 修改 PCA 支薪代碼會計科目綁定（ARTHPY.PCA；vPYTBM20；路徑為修改前公司／支薪代碼；部門以 Query 傳入）
        /// </summary>
        /// <param name="companyId">原公司別路徑參數（PCA01）</param>
        /// <param name="payCode">原支薪代碼路徑參數（PCA04）</param>
        /// <param name="departmentId">原部門別（PCA02）</param>
        /// <param name="obj">PCA 維護本文（MdPayCodeAcct_maint）</param>
        /// <returns>異動結果訊息封裝</returns>
        [HttpPut("account/{companyId}/{payCode}")]
        public MdApiMessage PCAUpdate(
            string companyId,
            string payCode,
            [FromQuery] string departmentId,
            [FromBody] MdPayCodeAcct_maint obj)
        {
            try
            {
                int _result = BlPCA.Update(companyId, payCode, departmentId ?? string.Empty, obj);
                return HttpContext.Response.UpdateSuccess(_result);
            }
            catch (System.Exception ex)
            {
                return HttpContext.Response.UpdateFailed(ex);
            }
        }

        /// <summary>
        /// 刪除 PCA 支薪代碼會計科目綁定（ARTHPY.PCA；vPYTBM20）
        /// </summary>
        /// <param name="companyId">公司別路徑參數（PCA01）</param>
        /// <param name="payCode">支薪代碼路徑參數（PCA04）</param>
        /// <param name="departmentId">部門別（PCA02）</param>
        /// <returns>異動結果訊息封裝</returns>
        [HttpDelete("account/{companyId}/{payCode}")]
        public MdApiMessage PCADelete(
            string companyId,
            string payCode,
            [FromQuery] string departmentId)
        {
            try
            {
                int _result = BlPCA.Delete(companyId, payCode, departmentId ?? string.Empty);
                return HttpContext.Response.DeleteSuccess(_result);
            }
            catch (System.Exception ex)
            {
                return HttpContext.Response.DeleteFailed(ex);
            }
        }

        /// <summary>
        /// 批次新增或更新 PCA 支薪代碼會計科目綁定（ARTHPY.PCA；vPYTBM20；同一公司、同一部門、同一會計科目批量寫入）
        /// </summary>
        /// <param name="body">批次請求本文（MdPayCodeAcct_bulk）</param>
        /// <returns>異動結果訊息封裝</returns>
        [HttpPost("account/bulkupsert")]
        public MdApiMessage PCABulkUpsert([FromBody] MdPayCodeAcct_bulk body)
        {
            try
            {
                int _result = BlPCA.BulkUpsert(body);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (System.Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        /// <summary>
        /// 批次新增前：同一公司、部門下，勾選支薪代碼任一筆已存在 PCA 綁定則回傳 true（vPYTBM20 覆蓋確認用）
        /// </summary>
        /// <param name="body">批次請求本文（MdPayCodeAcct_bulk；僅使用 companyId、departmentId、payCodes）</param>
        /// <returns>任一筆已存在為 true，否則為 false</returns>
        [HttpPost("account/exists")]
        public bool IsExistPayCodesAcct([FromBody] MdPayCodeAcct_bulk body)
        {
            return BlPCA.IsExistPayCodesAcct(body);
        }

        /// <summary>
        /// 複製公司 PCA 支薪代碼會計科目設定（ARTHPY.PCA；vPYTBM20；來源公司複製至目標公司；固定先刪除目標公司既有列再複製）
        /// </summary>
        /// <param name="sourceCompanyCode">來源公司別路徑參數（PCA01）</param>
        /// <param name="targetCompanyCode">目標公司別路徑參數（PCA01）</param>
        /// <returns>異動結果訊息封裝</returns>
        [HttpPost("account/copy/{sourceCompanyCode}/{targetCompanyCode}")]
        public MdApiMessage PCACopy(string sourceCompanyCode, string targetCompanyCode)
        {
            var src = sourceCompanyCode?.Trim() ?? string.Empty;
            var tgt = targetCompanyCode?.Trim() ?? string.Empty;
            if (string.IsNullOrWhiteSpace(src) || string.IsNullOrWhiteSpace(tgt))
                return HttpContext.Response.InsertFailed(
                    new ArgumentException("sourceCompanyCode and targetCompanyCode required"));
            if (string.Equals(src, tgt, StringComparison.OrdinalIgnoreCase))
                return HttpContext.Response.InsertFailed(
                    new ArgumentException("source and target company must differ"));
            try
            {
                int _result = BlPCA.Copy(src, tgt);
                return HttpContext.Response.InsertSuccess(_result);
            }
            catch (System.Exception ex)
            {
                return HttpContext.Response.InsertFailed(ex);
            }
        }

        #endregion
    }
}

