#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using GUIStd.BLL.AllNewGUI;
using GUIStd.DAL.AllNewPY.Models.Private.PYP53;
using GUIStd.BLL.AllNewPY.Private;

#endregion

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// 【需經驗證】PYP53 補充保費程式預設資料控制器(Pattern開發模式)
    /// </summary>
    [Route("py/private/[controller]")]
    public class vPYP53Controller : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlPYP53 BlPYP53 => mBlPYP53 = mBlPYP53 ?? new BlPYP53(this.ClientContent);
        private BlPYP53 mBlPYP53;

        /// <summary>
        /// 公司別商業邏輯物件屬性
        /// </summary>
        private BlA01 BlA01 => new BlA01(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得 PYP53 程式預設資料
        /// </summary>
        /// <returns>PYP53 預設資料模型物件</returns>
        [HttpGet("help")]
        public MdPYP53Defaults GetDefaults()
        {
            // 1. 公司別 - Controller 呼叫 BlA01
            var _companyOptions = BlA01.GetHelp(false, false, false, "PY", true);
            
            // 2. SINI 資料 - Controller 呼叫 BlPYP53
            var _siniData = BlPYP53.GetSINIData();

            return new MdPYP53Defaults()
            {
                CompanyOptions = _companyOptions,
                CalculationTypes = _siniData.CalculationTypes,
                PaymentCodes = _siniData.PaymentCodes,
                CalculationTypeMapping = _siniData.CalculationTypeMapping
            };
        }

        #endregion
    }
}
