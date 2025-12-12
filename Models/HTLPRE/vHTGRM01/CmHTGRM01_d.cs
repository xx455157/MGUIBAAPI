#region " 匯入的名稱空間：Framework "

using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.DAL.AllNewGUI.Models;

#endregion

namespace MGUIBAAPI.Models.HTLPRE
{
	/// <summary>
	/// vHTGRM01_d畫面輔助資料模型類別
	/// </summary>
	public class CmHTGRM01_d 
    {
		#region " 共用屬性 "

		/// <summary>
		/// 性別
		/// </summary>
		public IEnumerable<MdCode> GenderInfo { get; set; }

		/// <summary>
		/// 國籍
		/// </summary>
		public IEnumerable<MdCode> OriginInfo { get; set; }

		/// <summary>
		/// 旅客等級
		/// </summary>
		public IEnumerable<MdCode> ClassInfo { get; set; }

		/// <summary>
		/// 業務碼
		/// </summary>
		public IEnumerable<MdCode> SourceInfo { get; set; }

		/// <summary>
		/// 業務源
		/// </summary>
		public IEnumerable<MdCode> SalesInfo { get; set; }

		/// <summary>
		/// 語音服務
		/// </summary>
		public IEnumerable<MdCode> LanguageInfo { get; set; }

		/// <summary>
		/// 稱謂
		/// </summary>
		public IEnumerable<MdCode> TitleInfo { get; set; }

		/// <summary>
		/// 國家
		/// </summary>
		public IEnumerable<MdCode> CountryInfo { get; set; }

		/// <summary>
		/// 城市
		/// </summary>
		public IEnumerable<MdCode> CityInfo { get; set; }

		#endregion
	}
}
