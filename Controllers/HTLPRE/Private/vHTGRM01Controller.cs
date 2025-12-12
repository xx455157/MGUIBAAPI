#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using MGUIBAAPI.Models.HTLPRE;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// vHTGRM01旅客維護系統程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTGRM01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlGuest BlGuest=> new BlGuest(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public CmHTGRM01 GetUIData()
        {
            return null;
        }

        [HttpGet("paged")]
        public CmHTGRM01_d GetUIData_d()
        {
            IEnumerable<MdCode> _genderInfo;
            IEnumerable<MdCode> _originInfo;
            IEnumerable<MdCode> _classInfo;
            IEnumerable<MdCode> _sourceInfo;
            IEnumerable<MdCode> _salesInfo;
            IEnumerable<MdCode> _languageInfo;
            IEnumerable<MdCode> _titleInfo;            
            IEnumerable<MdCode> _countryInfo;
            IEnumerable<MdCode> _cityInfo;            

            BlGuest.GetHTGRM01_dUIData(CurrentLang, out _genderInfo, out _originInfo, out _classInfo, out _sourceInfo,
                out _salesInfo, out _languageInfo, out _titleInfo, out _countryInfo, out _cityInfo);

            return new CmHTGRM01_d()
            {
                GenderInfo = _genderInfo,
                OriginInfo = _originInfo,
                ClassInfo = _classInfo,
                SourceInfo = _sourceInfo,
                SalesInfo = _salesInfo,
                LanguageInfo = _languageInfo,
                TitleInfo = _titleInfo,
                CountryInfo = _countryInfo,
                CityInfo = _cityInfo
            };
        }        

        #endregion

    }
}
