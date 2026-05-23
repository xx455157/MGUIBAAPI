NetOA欄位說明-OA25（草案已棄用，改由 OA22 統計計算）
※ 棄用說明
本資料表已棄用，請勿建表或使用。
收支款現況不再獨立存表，改由 OA22 的收支明細資料即時統計計算。
廢止原因：
1. 收支款現況本質上是統計值，而非交易明細，不需要獨立存表
2. 由 OA22 即時計算可確保資料一致性，避免存檔快照與實際不符
3. 若有快照需求，可另行規劃，不在此階段處理

※ 收支款現況統計公式（由 OA22 即時計算）
現況類別	計算公式	說明	狀態
receivedAmount（已收款）	SUM(OA2228) WHERE OA2204 IS NOT NULL AND OA2224='Y'	所有已收款的收款金額合計	由OA22計算
arAmount（應收帳款）	SUM(OA2220) WHERE OA2216='Y' MINUS SUM(OA2228) WHERE OA2224='Y'	已開發票但未收款的差額	由OA22計算
blAmount（銀行借款）	待確認來源	目前無對應欄位，需確認業務需求	需確認來源
accrualExpenseAmount（應計支出）	SUM(OA2211) WHERE OA2205='E' AND OA2212=''	支出類型但尚未實際付款的金額	由OA22計算
payableAmount（應付款）	arAmount + blAmount + accrualExpenseAmount	應付款合計（AR + BL + 應計支出）	由OA22計算

※ API 回傳方式
收支款現況透過 GET /oa/revenueDetails/statusSummary/{compId}/{customerId}/{contractId} 即時計算回傳，
不再有獨立的 OA25 資料表與對應 Controller。
