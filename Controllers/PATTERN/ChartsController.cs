#region " 匯入的名稱空間：Framework "

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

#endregion

#region " 匯入的名稱空間：GoldenUp "

using GUICore.Web.Controllers;

#endregion

namespace MGUIBAAPI.Controllers.Pattern
{
	/// <summary>
	/// 圖表控制器
	/// </summary>
	[Route("pattern/[controller]")]
	public class ChartsController : GUIAppAuthController
	{
		#region " 共用函式 - 查詢資料 "

		/// <summary>
		/// 取得日房型庫存量資料
		/// </summary>
		/// <returns>日房型庫存量集合物件</returns>
		[HttpGet("dailyroomsqty")]
		public IEnumerable<object> GetData()
		{
			return new object[] {
				new {
					date = "20180307",
					roomTypeStock = new {
						STS = 36,
						DXT = 62,
						DXQ = 12,
						DXR = 4,
						EXT = 37,
						EXQ = 31,
						JST = 0,
						JSQ = 1,
						BST = 0,
						BSQ = 1,
						ESQ = 0,
						GSQ = 1
					}
				},
				new {
					date = "20180308",
					roomTypeStock = new {
						STS = 35,
						DXT = 77,
						DXQ = 34,
						DXR = 3,
						EXT = 36,
						EXQ = 26,
						JST = 0,
						JSQ = 1,
						BST = 0,
						BSQ = 1,
						ESQ = 0,
						GSQ = 1
					}
				},
				new {
					date = "20180309",
					roomTypeStock = new {
						STS = 31,
						DXT = 105,
						DXQ = 40,
						DXR = 0,
						EXT = 20,
						EXQ = 22,
						JST = 3,
						JSQ = 1,
						BST = 0,
						BSQ = 1,
						ESQ = 0,
						GSQ = 1
					}
				},
				new {
					date = "20180310",
					roomTypeStock = new {
						STS = 31,
						DXT = 132,
						DXQ = 46,
						DXR = 0,
						EXT = 2,
						EXQ = 13,
						JST = 0,
						JSQ = 0,
						BST = 1,
						BSQ = 1,
						ESQ = 1,
						GSQ = 0
					}
				},
				new {
					date = "20180311",
					roomTypeStock = new {
						STS = 30,
						DXT = 103,
						DXQ = 51,
						DXR = 0,
						EXT = 2,
						EXQ = 7,
						JST = 0,
						JSQ = 0,
						BST = 0,
						BSQ = 0,
						ESQ = 0,
						GSQ = 0
					}
				},
				new {
					date = "20180312",
					roomTypeStock = new {
						STS = 41,
						DXT = 80,
						DXQ = 65,
						DXR = 0,
						EXT = 6,
						EXQ = 13,
						JST = 0,
						JSQ = 0,
						BST = 0,
						BSQ = 0,
						ESQ = 0,
						GSQ = 0
					}
				},
				new {
					date = "20180313",
					roomTypeStock = new {
						STS = 45,
						DXT = 78,
						DXQ = 52,
						DXR = 0,
						EXT = 7,
						EXQ = 13,
						JST = 0,
						JSQ = 0,
						BST = 0,
						BSQ = 1,
						ESQ = 0,
						GSQ = 0
					}
				},
				new {
					date = "20180314",
					roomTypeStock = new {
						STS = 39,
						DXT = 64,
						DXQ = 53,
						DXR = 0,
						EXT = 5,
						EXQ = 15,
						JST = 0,
						JSQ = 0,
						BST = 0,
						BSQ = 1,
						ESQ = 2,
						GSQ = 0
					}
				},
				new {
					date = "20180315",
					roomTypeStock = new {
						STS = 40,
						DXT = 91,
						DXQ = 42,
						DXR = 0,
						EXT = 4,
						EXQ = 8,
						JST = 0,
						JSQ = 1,
						BST = 0,
						BSQ = 1,
						ESQ = 2,
						GSQ = 0
					}
				},
				new {
					date = "20180316",
					roomTypeStock = new {
						STS = 33,
						DXT = 120,
						DXQ = 51,
						DXR = 1,
						EXT = 1,
						EXQ = 6,
						JST = 2,
						JSQ = 0,
						BST = 0,
						BSQ = 1,
						ESQ = 3,
						GSQ = 0
					}
				},
				new {
					date = "20180317",
					roomTypeStock = new {
						STS = 33,
						DXT = 125,
						DXQ = 42,
						DXR = 1,
						EXT = 2,
						EXQ = 3,
						JST = 0,
						JSQ = 0,
						BST = 2,
						BSQ = 0,
						ESQ = 1,
						GSQ = 0
					}
				},
				new {
					date = "20180318",
					roomTypeStock = new {
						STS = 28,
						DXT = 123,
						DXQ = 42,
						DXR = 1,
						EXT = 1,
						EXQ = 9,
						JST = 0,
						JSQ = 0,
						BST = 0,
						BSQ = 0,
						ESQ = 0,
						GSQ = 0
					}
				},
				new {
					date = "20180319",
					roomTypeStock = new {
						STS = 25,
						DXT = 115,
						DXQ = 37,
						DXR = 1,
						EXT = 3,
						EXQ = 12,
						JST = 0,
						JSQ = 0,
						BST = 0,
						BSQ = 0,
						ESQ = 0,
						GSQ = 0
					}
				},
				new {
					date = "20180320",
					roomTypeStock = new {
						STS = 14,
						DXT = 96,
						DXQ = 23,
						DXR = 0,
						EXT = 5,
						EXQ = 9,
						JST = 0,
						JSQ = 0,
						BST = 0,
						BSQ = 0,
						ESQ = 0,
						GSQ = 0
					}
				},
				new {
					date = "20180321",
					roomTypeStock = new {
						STS = 18,
						DXT = 98,
						DXQ = 36,
						DXR = 0,
						EXT = 3,
						EXQ = 6,
						JST = 1,
						JSQ = 0,
						BST = 0,
						BSQ = 0,
						ESQ = 0,
						GSQ = 0
					}
				},
				new {
					date = "20180322",
					roomTypeStock = new {
						STS = 25,
						DXT = 80,
						DXQ = 44,
						DXR = 0,
						EXT = 0,
						EXQ = 5,
						JST = 0,
						JSQ = 0,
						BST = 3,
						BSQ = 0,
						ESQ = 0,
						GSQ = 0
					}
				}
			};
		}

		#endregion
	}
}
