
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE GraphData
AS
BEGIN
SELECT TDate,SUM(Collections) Collections,SUM(Expenses) Expenses,SUM(Balance) Balance
FROM (
(select CollectionDate as TDate,sum(Amount) as Collections,0 Expenses, 0 Balance from Collections
group by CollectionDate)
UNION
(select ExpenseDate as TDate,0 as Collections, sum(ExpenseAmount) as Expenses, 0 Balance from Expenses
group by ExpenseDate)
) AS TBL
GROUP BY TDate
END
GO
