namespace WeatherApp.Services
{
    public interface ITempratureVitalsProcessor
    {
        double CalculateHeatIndex(double temprature, double relative_humidity);
    }
}