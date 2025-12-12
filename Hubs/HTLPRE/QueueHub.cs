#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUIStd.BLL.AllNewHTL;
using GUIStd.BLL.AllNewHTL.Private;
using GUIStd.Models;
using GUICore.Web.Controllers;

#endregion


namespace MGUIBAAPI.Hubs.HTLPRE
{
	/// <summary>
	/// 候位資料查詢、叫號推送最新候位資料
	/// </summary>
	public class QueueHub : GUIAppSIGRHub
	{
		#region " 建構子：欲自行指定使用者帳號時才需加入 "

		/// <summary>
		/// 建構子
		/// </summary>
		public QueueHub()
		{
			// 改變執行服務的使用者帳號
			this.HubUser = "QueueHub";
		}

		#endregion

		#region " 私用屬性 "

		/// <summary>
		/// HTPF 商業邏輯物件屬性
		/// </summary>
		private BlHTPF BlHTPF => new BlHTPF(ClientContent);

		/// <summary>
		/// HTPR 商業邏輯物件屬性
		/// </summary>
		private BlHTPR BlHTPR => new BlHTPR(ClientContent);

		/// <summary>
		/// Orders 商業邏輯物件屬性
		/// </summary>
		private BlOrders BlOrders => new BlOrders(ClientContent);

		#endregion

		#region " 共用函式 -  查詢資料 "

		/// <summary>
		/// 已叫號資料
		/// </summary>
		/// <param name="receiver">回應客戶端</param>
		/// <param name="posId">廳別</param>
		/// <param name="date">日期</param>   
		/// <returns>候位資料物件</returns>
		public async Task GetCalledData(string receiver, string posId, string date)
		{
			var _result = BlHTPR.GetWaitingData(posId, date, "CALL,NOTE");

			switch (receiver.ToLower())
			{
				case "all":
					//傳送最新候位資料給連線內所有人
					await Clients.All.SendAsync("CalledMessage", _result);
					break;
				case "caller":
					//傳送最新候位資料給呼叫功能的使用者
					await Clients.Caller.SendAsync("CalledMessage", _result);
					break;
				case "others":
					//傳送最新候位資料給呼叫功能的使用者以外的所有人
					await Clients.Others.SendAsync("CalledMessage", _result);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 候位中號碼資料
		/// </summary>
		/// <param name="receiver">回應客戶端</param>
		/// <param name="posId">廳別</param>
		/// <param name="date">日期</param>
		/// <returns>候位資料物件</returns>
		public async Task GetWaitingData(string receiver, string posId, string date)
		{
			var _result = BlHTPR.GetWaitingData(posId, date, "ACTV,CALL,NOTE");

			switch (receiver.ToLower())
			{
				case "all":
					await Clients.All.SendAsync("WaitingMessage", _result);
					break;
				case "caller":
					await Clients.Caller.SendAsync("WaitingMessage", _result);
					break;
				case "others":
					await Clients.Others.SendAsync("WaitingMessage", _result);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// 取餐狀態資料查詢
		/// </summary>
		/// <param name="receiver">回應客戶端</param>
		/// <param name="posId">廳別</param>
		/// <param name="posDate">日期</param>
		/// <returns>取餐狀態資料</returns>
		public async Task GetMealStatusData(string receiver, string posId, string posDate = "")
		{
			var _result = BlOrders.GetMealStatusData(posId, posDate);

			switch (receiver.ToLower())
			{
				case "all":
					await Clients.All.SendAsync("MealStatusMessage", _result);
					break;
				case "caller":
					await Clients.Caller.SendAsync("MealStatusMessage", _result);
					break;
				case "others":
					await Clients.Others.SendAsync("MealStatusMessage", _result);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// PQTEST
		/// </summary>
		/// <param name="receiver">回應客戶端</param>
		/// <param name="pqArr"></param>
		/// <returns>PQ01 arr</returns>
		public async Task PrintQueue(string receiver, int[] pqArr)
		{
			switch (receiver.ToLower())
			{
				case "all":
					await Clients.All.SendAsync("PQMessage", pqArr);
					break;
				case "caller":
					await Clients.Caller.SendAsync("PQMessage", pqArr);
					break;
				case "others":
					await Clients.Others.SendAsync("PQMessage", pqArr);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// CustomerDisplay
		/// </summary>
		/// <param name="receiver">回應客戶端</param>
		/// <param name="display">要顯示的內容</param>
		public async Task CustomerDisplay(string receiver, string[] display)
		{
			switch (receiver.ToLower())
			{
				case "all":
					await Clients.All.SendAsync("CustomerDisplayMessage", display);
					break;
				case "caller":
					await Clients.Caller.SendAsync("CustomerDisplayMessage", display);
					break;
				case "others":
					await Clients.Others.SendAsync("CustomerDisplayMessage", display);
					break;
				default:
					break;
			}
		}

		/// <summary>
		/// QRCode送單後(上記號)告知其他連線刷新平面圖 
		/// or
		/// 服務員點餐進入帳單後(取消記號),告知其他其他連線刷新平面圖
		/// </summary>
		/// <param name="receiver">回應客戶端</param>
		/// <param name="posId">廳別</param>
		public async Task NotifyPosMap(string receiver, string posId)
		{
			switch (receiver.ToLower())
			{
				case "all":
					await Clients.All.SendAsync("RefreshPosMap", posId);
					break;
				case "caller":
					await Clients.Caller.SendAsync("RefreshPosMap", posId);
					break;
				case "others":
					await Clients.Others.SendAsync("RefreshPosMap", posId);
					break;
				default:
					break;
			}
		}

		#endregion

		#region " 共用函式 - 異動資料 "

		/// <summary>
		/// 更新叫號狀態
		/// </summary>
		/// <param name="posId">廳別</param>
		/// <param name="date">日期</param>
		/// <param name="id">更新ID(PR01 key)</param>
		/// <param name="status">更新狀態</param>
		/// <returns>候位資料物件</returns>
		public async Task CallNumber(string posId, string date, string id, string status)
		{
			try
			{
				//更新狀態
				var _result = BlHTPR.UpdateNumberStatus(id, status);
				if (_result > 0)
				{
					//成功，將最新叫號資料發送給所有人
					await GetCalledData("all", posId, date);
					await GetWaitingData("all", posId, date);
				}
				else
				{
					//失敗，傳送錯誤訊息給使用者
					await Clients.Caller.SendAsync("CallNumberMessage", new MdApiMessage
					{
						Result = false,
					});
				}
			}
			catch (Exception ex)
			{
				//失敗，傳送錯誤訊息給使用者
				await Clients.Caller.SendAsync("CallNumberMessage", new MdApiMessage
				{
					Result = false,
					Message = ex.Message,
				});
			}
		}

		/// <summary>
		/// 更新取餐狀態
		/// </summary>
		/// <param name="posId">廳別</param>
		/// <param name="posDate">日期</param>
		/// <param name="tableNo">桌號</param>
		/// <param name="folioNo">帳單號碼</param>
		/// <param name="status">更新狀態</param>
		/// <returns>取餐資料物件</returns>
		public async Task UpdateMealStatus(string posId, string posDate,
			string tableNo, string folioNo, string status)
		{
			try
			{
				//更新狀態
				var _result = BlHTPF.UpdateMealStatus(posId, posDate, tableNo, folioNo, status);
				if (_result > 0)
				{
					//成功，將最新取餐資料發送給所有人
					await GetMealStatusData("all", posId, posDate);
				}
				else
				{
					//失敗，傳送錯誤訊息給使用者
					await Clients.Caller.SendAsync("UpdateStatusMessage", new MdApiMessage
					{
						Result = false,
					});
				}
			}
			catch (Exception ex)
			{
				//失敗，傳送錯誤訊息給使用者
				await Clients.Caller.SendAsync("UpdateStatusMessage", new MdApiMessage
				{
					Result = false,
					Message = ex.Message,
				});
			}
		}

		#endregion
	}
}
