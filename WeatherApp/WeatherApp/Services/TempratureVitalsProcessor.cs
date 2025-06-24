namespace WeatherApp.Services
{
    public class TempratureVitalsProcessor : ITempratureVitalsProcessor
    {
        public double CalculateHeatIndex(double temprature, double relative_humidity)
        {
            return temprature + 61.0 + ((temprature - 68.0) * 1.2) + (0.094 * relative_humidity);
        }
    }
}
