using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TenkiApp {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private async void GetWeather_Click(object sender, RoutedEventArgs e) {
            string city = CityBox.Text;

            if (string.IsNullOrWhiteSpace(city)) {
                ResultBox.Text = "都市名を入力してください。";
                return;
            }

            using var http = new HttpClient();

            try {
                // -------------------------
                // ① 都市名 → 緯度経度検索
                // -------------------------
                string geoUrl =
                    $"https://geocoding-api.open-meteo.com/v1/search?name={city}&language=ja&count=1";

                var geoResponse = await http.GetFromJsonAsync<GeoResponse>(geoUrl);

                if (geoResponse?.results == null || geoResponse.results.Length == 0) {
                    ResultBox.Text = "都市が見つかりませんでした。";
                    return;
                }

                var location = geoResponse.results[0];

                double lat = location.latitude;
                double lon = location.longitude;

                // -------------------------
                // ② 緯度経度 → 天気データ取得
                // -------------------------
                string weatherUrl =
                    $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current=temperature_2m,wind_speed_10m,relative_humidity_2m";

                var weather = await http.GetFromJsonAsync<WeatherResponse>(weatherUrl);

                if (weather?.current == null) {
                    ResultBox.Text = "天気データの取得に失敗しました。";
                    return;
                }

                // 出力
                ResultBox.Text =
                    $"都市名：{location.name}（{location.country}）\n" +
                    $"緯度：{lat}\n" +
                    $"経度：{lon}\n" +
                    $"-----------------------\n" +
                    $"取得時刻：{weather.current.time}\n" +
                    $"気温：{weather.current.temperature_2m} ℃\n" +
                    $"風速：{weather.current.wind_speed_10m} m/s\n" +
                    $"湿度：{weather.current.relative_humidity_2m} %\n";
            }
            catch (Exception ex) {
                ResultBox.Text = $"エラー: {ex.Message}";
            }
        }
    }

    // ==============================
    //  ▼ JSON データ受け取りクラス
    // ==============================

    public class GeoResponse {
        public GeoLocation[]? results { get; set; }
    }

    public class GeoLocation {
        public string? name { get; set; }
        public string? country { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
    }

    public class WeatherResponse {
        public CurrentWeather? current { get; set; }
    }

    public class CurrentWeather {
        public string? time { get; set; }
        public float temperature_2m { get; set; }
        public float wind_speed_10m { get; set; }
        public float relative_humidity_2m { get; set; }
    }
}