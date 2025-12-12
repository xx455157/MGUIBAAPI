using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using GUICore.Web.Controllers;
using GUICore.Web.Extensions;
using MGUIBAAPI.Models.PY;
using GUIStd.Models;
using GUIStd.BLL.AllNewPY.Private;
using GUIStd.DAL.AllNewPY.Models.Private.PersonalAttend;

namespace MGUIBAAPI.Controllers.PY
{
    /// <summary>
    /// PY系統設定控制器
    /// </summary>
    [ApiController]
    [Route("py/private/[controller]")]
    public class vPYConfigsController : GUIAppAuthController
    {
        #region " 私用屬性 "

        /// <summary>
        /// 商業邏輯物件屬性
        /// </summary>
        private BlAnnualAttAppr BlAnnualAttAppr => mBlAnnualAttAppr = mBlAnnualAttAppr ?? new BlAnnualAttAppr(this.ClientContent);
        private BlAnnualAttAppr mBlAnnualAttAppr;

        #endregion

        #region " 共用函式 - 查詢資料 "

        /// <summary>
        /// 取得共用下拉資料
        /// </summary>
        /// <returns>包含考勤選項、公司選項的綜合下拉資料</returns>
        [HttpGet("help")]
        public CmPYConfigs GetUIData()
        {
            var _attendanceOptions = BlAnnualAttAppr.GetAttendanceOptions();
            var _companyOptions = BlAnnualAttAppr.GetCompanyOptions();

            return new CmPYConfigs()
            {
                AttendanceOptions = _attendanceOptions,
                CompanyOptions = _companyOptions
            };
        }

        /// <summary>
        /// 
        /// 取得指定類型的配置資料
        /// </summary>
        /// <param name="configType">配置類型</param>
        /// <returns>指定類型的配置資料</returns>
        [HttpGet("config/{configType}")]
        public object GetConfig(string configType)
        {
            switch (configType.ToLower())
            {
                case "annualattappr":
                    return BlAnnualAttAppr.GetAnnualAttApprConfig();

                case "annualleave":
                    return BlAnnualAttAppr.GetAnnualLeaveConfig();

                case "overtime":
                    return BlAnnualAttAppr.GetOvertimeConfig();

                default:
                    return null;
            }
        }

        #endregion

        #region " 共用函式 - 異動資料 "

        /// <summary>
        /// 更新指定類型的配置資料
        /// </summary>
        /// <param name="configType">配置類型</param>
        /// <param name="data">配置資料</param>
        /// <returns>更新結果</returns>
        [HttpPut("config/{configType}")]
        public MdApiMessage UpdateConfig(string configType, [FromBody] JObject data)
        {
            int result;
            switch (configType.ToLower())
            {
                case "annualattappr":
                    var _annualattappr = data.ToObject<MdAnnualAttAppr>();
                    result = BlAnnualAttAppr.UpdateAnnualAttApprConfig(_annualattappr);
                    break;

                case "annualleave":
                    var _annualleave = data.ToObject<MdAnnualLeaveConfig>();
                    result = BlAnnualAttAppr.UpdateAnnualLeaveConfig(_annualleave);
                    break;

                case "overtime":
                    var _overtime = data.ToObject<MdOvertimeConfig>();
                    result = BlAnnualAttAppr.UpdateOvertimeConfig(_overtime);
                    break;

                default:
                    return null;
            }

            return HttpContext.Response.UpdateSuccess(result);
        }

        #endregion
    }
} 