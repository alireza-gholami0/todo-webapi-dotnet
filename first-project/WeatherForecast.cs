namespace first_project
{
    public class WeatherForecast
    {
        public DateOnly Date { get; set; } = new DateOnly(2002,2,15);

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
