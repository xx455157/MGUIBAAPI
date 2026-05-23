#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;
using MGUIBAAPI.Models.HTLPRE;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.DAL.AllNewGUI.Models;
using GUIStd.DAL.AllNewHTL.Models;

#endregion

namespace MGUIBAAPI.Controllers.HTLPRE
{
    /// <summary>
    /// vHTRVM01訂房系統程式資料控制器
    /// </summary>
    [Route("htlpre/private/[controller]")]
	public class vHTRVM01Controller : GUIAppAuthController
    {
        #region " 私用屬性 "
        
        private BlReservation BlReservation => new BlReservation(ClientContent);

        #endregion

        #region " 共用函式 - 查詢資料 "

        [HttpGet("page")]
        public CmHTRVM01 GetUIData()
        {
            string _bkDate;
            IEnumerable<MdCode> _selectConditions;
            IEnumerable<MdCompany_r> _companies;

            BlReservation.GetHTRVM01UIData(out _bkDate, out _selectConditions, out _companies);
            
            return new CmHTRVM01()
            {
                BKDate = _bkDate,
                SelectConditions = _selectConditions,
                Companies = _companies,
            };
        }

        [HttpGet("paged")]
        public CmHTRVM01_d GetUIData_d()
        {
			string _bkDate;			
            IEnumerable<MdCompany_r> _companies;
            IEnumerable<MdCode> _roomTypes;
            IEnumerable<MdService> _services;
            IEnumerable<MdCode> _rvTypes;
            IEnumerable<MdCode> _salesInfo;
            IEnumerable<MdCode> _originInfo;
            IEnumerable<MdCode> _sourceInfo;

            BlReservation.GetHTRVM01_dUIData(out _bkDate, out _companies, out _roomTypes, 
                out _services, out _rvTypes, out _salesInfo, out _originInfo, out _sourceInfo);

            return new CmHTRVM01_d()
            {
                BKDate = _bkDate,
                Companies = _companies,
                RoomTypes = _roomTypes,
                Services = _services,
                RVTypes = _rvTypes,
                SalesInfo = _salesInfo,
                OriginInfo = _originInfo,
                SourceInfo = _sourceInfo
			};			
        }

        #endregion

    }
}
