Console.WriteLine("Hello, World!");
Console.WriteLine("The current time is " + DateTime.Now);

// 3.
DateTime christmas = new(DateTime.Now.Year, 12, 25);
TimeSpan timeUntilChristmas = christmas - DateTime.Now;

Console.WriteLine("There are " + timeUntilChristmas.Days + " days until Christmas.");