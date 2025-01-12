using Microsoft.Maui.Controls;

namespace time
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();  // автоматический вызов

            
            ClockGraphicsView.Drawable = new ClockDrawable();

            // оббновляем часы каждую секунду
            Dispatcher.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                ClockGraphicsView.Invalidate(); // перерисовка
                return true;
            });
        }
    }
}
