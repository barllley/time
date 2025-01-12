using Microsoft.Maui.Graphics;
using System;

namespace time
{
    public class ClockDrawable : IDrawable
    {
        public void Draw(ICanvas canvas, RectF dirtyRect)
        {
        
            canvas.FillColor = Colors.White;
            canvas.FillRectangle(dirtyRect);

            
            var currentTime = DateTime.Now;    // получаем текущее время


            string timeString = currentTime.ToString("HH:mm:ss");

            
            float segmentWidth = dirtyRect.Width / 100;
            float segmentLength = dirtyRect.Width / 12;
            float spacing = segmentWidth * 2; // отступы между символами

            
            float x = dirtyRect.Left + segmentWidth;   // начальная позиция для рисования цифр
            float y = dirtyRect.Top + (dirtyRect.Height / 2 - segmentLength); // центрируем по вертикали

            
            foreach (char c in timeString)  // отрисовка каждого символа времени
            {
                if (c == ':')
                {
                    DrawColon(canvas, x, y);
                    x += spacing;
                }
                else
                {
                    DrawDigit(canvas, c - '0', x, y, segmentWidth, segmentLength);
                    x += segmentLength + spacing;
                }
            }
        }

        
        private void DrawDigit(ICanvas canvas, int digit, float x, float y, float segmentWidth, float segmentLength)  // отрисовка цифр в семисегментном формате
        {
            
            bool[] segments = GetSegmentsForDigit(digit);   // получаем активные сегменты для цифры

            //  параметры для рисования сегментов
            canvas.StrokeColor = Colors.Black;
            canvas.StrokeSize = segmentWidth;

            // рисуем сегменты 
            if (segments[0]) canvas.DrawLine(x, y, x + segmentLength, y);                     // верх
            if (segments[1]) canvas.DrawLine(x + segmentLength, y, x + segmentLength, y + segmentLength); // право верх
            if (segments[2]) canvas.DrawLine(x + segmentLength, y + segmentLength, x + segmentLength, y + 2 * segmentLength); // право низ
            if (segments[3]) canvas.DrawLine(x, y + 2 * segmentLength, x + segmentLength, y + 2 * segmentLength);             // низ
            if (segments[4]) canvas.DrawLine(x, y + segmentLength, x, y + 2 * segmentLength);             // лево низ
            if (segments[5]) canvas.DrawLine(x, y, x, y + segmentLength);                 // лево верх
            if (segments[6]) canvas.DrawLine(x, y + segmentLength, x + segmentLength, y + segmentLength);   // средний
        }

        
        private bool[] GetSegmentsForDigit(int digit)  // активные сегменты для каждой цифры
        {
            switch (digit)
            {
                case 0: return new bool[] { true, true, true, true, true, true, false };
                case 1: return new bool[] { false, true, true, false, false, false, false };
                case 2: return new bool[] { true, true, false, true, true, false, true };
                case 3: return new bool[] { true, true, true, true, false, false, true };
                case 4: return new bool[] { false, true, true, false, false, true, true };
                case 5: return new bool[] { true, false, true, true, false, true, true };
                case 6: return new bool[] { true, false, true, true, true, true, true };
                case 7: return new bool[] { true, true, true, false, false, false, false };
                case 8: return new bool[] { true, true, true, true, true, true, true };
                case 9: return new bool[] { true, true, true, true, false, true, true };
                default: return new bool[] { false, false, false, false, false, false, false };
            }
        }

        // отрисовка двоеточия (:) между часами, минутами и секундами
        private void DrawColon(ICanvas canvas, float x, float y)
        {
            canvas.FillColor = Colors.Black;
            float radius = 3;
            canvas.FillCircle(x, y + 10, radius); // верхняя точка
            canvas.FillCircle(x, y + 30, radius); // нижняя точка
        }
    }
}
