CREATE VIEW [dbo].[vwBINonsense]
	AS 
	select id, praiser, successdate,
datepart(year, successdate) as year,
datepart(quarter, successdate) as quarter,
datepart(month, successdate) as month,
datepart(dayofyear, successdate) as dayofyear,
datepart(day, successdate) as day,
datepart(week, successdate) as week,
datepart(weekday, successdate) as weekday,
datepart(hour, successdate) as hour,
datepart(minute, successdate) as minute,
datepart(second, successdate) as second,
datepart(millisecond, successdate) as millisecond,
datepart(microsecond, successdate) as microsecond,
datepart(nanosecond, successdate) as nanosecond,
Message
from slacksuccess
