using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace TenkiApp {
    public partial class MainWindow : Window {
        private readonly DispatcherTimer timer = new DispatcherTimer(); // null 非許容で即初期化

        public MainWindow() {
            InitializeComponent();

            SetBackgroundByTime();   // 起動時に背景色と文字色を設定
            StartTimer();            // 1分ごとに更新
            SetCurrentCityAsync();   // 現在地の都市を取得
        }

        // タイマー開始
        private void StartTimer() {
            timer.Interval = TimeSpan.FromMinutes(1);
            timer.Tick += (s, e) => SetBackgroundByTime();
            timer.Start();
        }

        // 時間帯に応じて背景色と文字色を設定
        private void SetBackgroundByTime() {
            int hour = DateTime.Now.Hour;

            GradientStopCollection stops = new GradientStopCollection();
            SolidColorBrush textBrush;

            if (hour >= 5 && hour < 8) // 朝
            {
                stops.Add(new GradientStop(Color.FromRgb(255, 200, 150), 0));
                stops.Add(new GradientStop(Color.FromRgb(255, 223, 186), 1));
                textBrush = new SolidColorBrush(Color.FromRgb(50, 30, 20));
            } else if (hour >= 8 && hour < 17) // 昼
              {
                stops.Add(new GradientStop(Color.FromRgb(135, 206, 250), 0));
                stops.Add(new GradientStop(Color.FromRgb(135, 206, 235), 1));
                textBrush = new SolidColorBrush(Colors.Black);
            } else if (hour >= 17 && hour < 19) // 夕方
              {
                stops.Add(new GradientStop(Color.FromRgb(250, 128, 114), 0));
                stops.Add(new GradientStop(Color.FromRgb(255, 182, 193), 1));
                textBrush = new SolidColorBrush(Color.FromRgb(50, 0, 0));
            } else // 夜
              {
                stops.Add(new GradientStop(Color.FromRgb(25, 25, 112), 0));
                stops.Add(new GradientStop(Color.FromRgb(0, 0, 64), 1));
                textBrush = new SolidColorBrush(Colors.White);
            }

            LinearGradientBrush brush = new LinearGradientBrush(stops, new Point(0.5, 1), new Point(0.5, 0));
            this.Background = brush;

            // 天気表示の文字色を更新
            ResultBox.Foreground = textBrush;
        }

        private async void SetCurrentCityAsync() {
            try {
                using var http = new HttpClient();
                string json = await http.GetStringAsync("https://ipapi.co/json/");
                var data = JsonSerializer.Deserialize<IpInfo>(json);

                CityBox.Text = !string.IsNullOrWhiteSpace(data?.city) ? data.city : "東京";
                await GetWeatherAsync();
            }
            catch {
                CityBox.Text = "東京";
                await GetWeatherAsync();
            }
        }

        private async System.Threading.Tasks.Task GetWeatherAsync() {
            string city = CityBox.Text.Trim();
            if (string.IsNullOrEmpty(city)) {
                ResultBox.Text = "都市名を入力してください。";
                WeatherIcon.Source = null;
                return;
            }

            ResultBox.Text = "天気を取得中…";
            WeatherIcon.Source = null;

            try {
                using var http = new HttpClient();
                string geoUrl = $"https://geocoding-api.open-meteo.com/v1/search?name={city}&language=ja&count=1";
                var geoResponse = await http.GetFromJsonAsync<GeoResponse>(geoUrl);

                if (geoResponse?.results == null || geoResponse.results.Length == 0) {
                    ResultBox.Text = "都市が見つかりませんでした。";
                    return;
                }

                var location = geoResponse.results[0];
                double lat = location.latitude;
                double lon = location.longitude;

                string weatherUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&current_weather=true";
                var weather = await http.GetFromJsonAsync<WeatherResponse>(weatherUrl);

                if (weather?.current_weather == null) {
                    ResultBox.Text = "天気データの取得に失敗しました。";
                    return;
                }

                var current = weather.current_weather;

                ResultBox.Text =
                    $"都市名：{location.name}（{location.country}）\n" +
                    $"緯度：{lat}\n" +
                    $"経度：{lon}\n" +
                    $"取得時刻：{current.time}\n" +
                    $"気温：{current.temperature} ℃\n" +
                    $"風速：{current.windspeed} m/s\n" +
                    $"風向：{current.winddirection}°\n";

                // 天気コードに応じたアイコン
                int code = current.weathercode;
                string iconPath = code switch {
                    0 => "Icons/sunny.png",
                    1 or 2 or 3 => "Icons/cloudy.png",
                    61 or 63 or 65 => "Icons/rain.png",
                    71 or 73 or 75 => "Icons/snow.png",
                    95 or 96 or 99 => "Icons/thunder.png",
                    _ => "Icons/unknown.png"
                };

                WeatherIcon.Source = new BitmapImage(new Uri($"pack://application:,,,/{iconPath}"));
            }
            catch (Exception ex) {
                ResultBox.Text = $"エラー: {ex.Message}";
                WeatherIcon.Source = null;
            }
        }

        private async void GetWeather_Click(object sender, RoutedEventArgs e) {
            await GetWeatherAsync();
        }

        private async void CityButton_Click(object sender, RoutedEventArgs e) {
            if (sender is Button btn) {
                CityBox.Text = btn.Content.ToString();
                await GetWeatherAsync();
            }
        }

        // JSON 用クラス
        public class IpInfo { public string? city { get; set; } }
        public class GeoResponse { public GeoLocation[]? results { get; set; } }
        public class GeoLocation { public string? name { get; set; } public string? country { get; set; } public double latitude { get; set; } public double longitude { get; set; } }
        public class WeatherResponse { public CurrentWeather? current_weather { get; set; } }
        public class CurrentWeather { public string? time { get; set; } public float temperature { get; set; } public float windspeed { get; set; } public float winddirection { get; set; } public int weathercode { get; set; } }
    }
}
