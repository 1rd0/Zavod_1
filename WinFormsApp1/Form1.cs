using AForge;
using AForge.Imaging;
using AForge.Imaging.Filters;
using AForge.Math.Geometry;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing;
using System.Linq;
using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
  


namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        int greenLineY;
        int blueLineY;
        int greenLineY2;
        int blueLineY2;
        public Form1()
        {
            InitializeComponent();
            CenterToParent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


        }

        private void button2_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.gif;*.bmp|Все файлы|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Загрузка выбранного изображения в PictureBox
                    string selectedImagePath = openFileDialog.FileName;
                    ProcessImage(selectedImagePath);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.gif;*.bmp|Все файлы|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Загрузка выбранного изображения в PictureBox
                    string selectedImagePath = openFileDialog.FileName;
                    DrawRedLines(selectedImagePath);
                }
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.gif;*.bmp|Все файлы|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Загрузка выбранного изображения в PictureBox
                    string selectedImagePath = openFileDialog.FileName;
                    ProcessImage_withContours(selectedImagePath);
                }
            }
        }

       

        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.gif;*.bmp|Все файлы|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Загрузка выбранного изображения в PictureBox
                    string selectedImagePath = openFileDialog.FileName;
                    grenndetec(selectedImagePath);
                }
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox5.Text = string.Empty;
            textBox6.Text = string.Empty;
            textBox7.Text = string.Empty;
            textBox8.Text = string.Empty;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения|*.png;*.jpg;*.jpeg;*.gif;*.bmp|Все файлы|*.*";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Загрузка выбранного изображения в PictureBox
                    string selectedImagePath = openFileDialog.FileName;
                    starttobegin(selectedImagePath);
                }
            }

        }


        private void ProcessImage(string imagePath)
        {
            // Загрузка изображения
            Bitmap originalImage = new Bitmap(imagePath);

            // Преобразование в черно-белое изображение
            Grayscale filter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap grayImage = filter.Apply(originalImage);

            // Применение фильтра Canny для обнаружения границ
            CannyEdgeDetector cannyFilter = new CannyEdgeDetector();
            Bitmap edgesImage = cannyFilter.Apply(grayImage);

            // Применение преобразования Хафа для поиска линий
            HoughLineTransformation lineTransform = new HoughLineTransformation();
            lineTransform.ProcessImage(edgesImage);


            // Отображение результатов



            // Отображение результатов
            pictureBox1.Image = originalImage;
            pictureBox2.Image = edgesImage;
        }

        private void ProcessImage_to_linedrawing(string imagePath)
        {
            Bitmap originalImage = new Bitmap(imagePath);

            // Отображение оригинального изображения
            pictureBox1.Image = originalImage;

            // Преобразование изображения в черно-белое
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap processedImage = grayscaleFilter.Apply(originalImage);

            GaussianBlur blurFilter = new GaussianBlur(4, 11);
            processedImage = blurFilter.Apply(processedImage);

            BradleyLocalThresholding thresholdFilter = new BradleyLocalThresholding();
            processedImage = thresholdFilter.Apply(processedImage);



            // Отображение измененного изображения
            pictureBox2.Image = processedImage;
        }
        private void DrawRedLines(string imagePath)
        {
            Bitmap originalImage = new Bitmap(imagePath);

            // Отображение оригинального изображения
            pictureBox1.Image = originalImage;

            // Преобразование изображения в черно-белое
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap processedImage = grayscaleFilter.Apply(originalImage);

            BradleyLocalThresholding thresholdFilter = new BradleyLocalThresholding();
            processedImage = thresholdFilter.Apply(processedImage);

            // Обнаружение линий с помощью преобразования Хафа
            HoughLineTransformation lineTransform = new HoughLineTransformation();
            lineTransform.ProcessImage(processedImage);

            HoughLine[] lines = lineTransform.GetLinesByRelativeIntensity(0.5);

            // Рисование красных линий поверх обнаруженных линий
            using (Graphics g = Graphics.FromImage(processedImage))
            {
                foreach (HoughLine line in lines)
                {
                    int radius = line.Radius;
                    double theta = line.Theta;

                    // Конвертация полярных координат в декартовы
                    double x0 = radius * Math.Cos(theta);
                    double y0 = radius * Math.Sin(theta);

                    System.Drawing.Point p1 = new System.Drawing.Point((int)(x0 - 1000 * (-Math.Sin(theta))), (int)(y0 - 1000 * Math.Cos(theta)));
                    System.Drawing.Point p2 = new System.Drawing.Point((int)(x0 + 1000 * (-Math.Sin(theta))), (int)(y0 + 1000 * Math.Cos(theta)));

                    // Рисование линии
                    g.DrawLine(new Pen(Color.Red), p1, p2);
                }
            }

            // Отображение измененного изображения
            pictureBox2.Image = processedImage;
        }

        private void ProcessImage_withContours(string imagePath)
        {
            Bitmap originalImage = new Bitmap(imagePath);

            // Отображение оригинального изображения
            pictureBox1.Image = originalImage;

            // Преобразование изображения в черно-белое
            // Преобразование изображения в черно-белое
            Grayscale grayscaleFilter = new Grayscale(0.2125, 0.7154, 0.0721);
            Bitmap processedImage = grayscaleFilter.Apply(originalImage);

            // Применение Canny Edge Detection
            CannyEdgeDetector cannyFilter = new CannyEdgeDetector();
            Bitmap edgesImage = cannyFilter.Apply(processedImage);



            // Усиление контуров
            ContrastStretch contrastFilter = new ContrastStretch();
            contrastFilter.ApplyInPlace(edgesImage);




            // Create a new bitmap with the same size as edgesImage and a non-indexed format
            Bitmap newEdgesImage = new Bitmap(edgesImage.Width, edgesImage.Height, PixelFormat.Format32bppArgb);

            bool pixelFound_top = false;
            bool pixelFound_down = false;


            // Draw the processed image onto the new bitmap
            using (Graphics g = Graphics.FromImage(newEdgesImage))
            {
                g.DrawImage(edgesImage, new Rectangle(0, 0, edgesImage.Width, edgesImage.Height));
            }

            using (Graphics g = Graphics.FromImage(newEdgesImage))
            {
                for (int y = 0; y < newEdgesImage.Height; y++)
                {
                    for (int x = 0; x < newEdgesImage.Width; x++)
                    {
                        Color pixelColor = newEdgesImage.GetPixel(x, y);
                        int red = pixelColor.R;
                        int green = pixelColor.G;
                        int blue = pixelColor.B;

                        if (red != 0 || green != 0 || blue != 0)
                        {
                            greenLineY = y;
                            int lineWidth = 5; // Ширина линии
                            g.DrawLine(new Pen(Color.Green, lineWidth), new System.Drawing.Point(0, y), new System.Drawing.Point(newEdgesImage.Width, y));
                            pixelFound_top = true;

                            // Начинаем поиск снизу после того, как найден пиксель сверху
                            for (int y_down = newEdgesImage.Height - 1; y_down >= 0; y_down--)
                            {
                                for (int x_down = 0; x_down < newEdgesImage.Width; x_down++)
                                {
                                    Color pixelColor_down = newEdgesImage.GetPixel(x_down, y_down);
                                    int red_down = pixelColor_down.R;
                                    int green_down = pixelColor_down.G;
                                    int blue_down = pixelColor_down.B;

                                    if (red_down != 0 || green_down != 0 || blue_down != 0)
                                    {
                                        blueLineY = y_down;
                                        g.DrawLine(new Pen(Color.Blue, lineWidth), new System.Drawing.Point(0, y_down), new System.Drawing.Point(newEdgesImage.Width, y_down));
                                        pixelFound_down = true;

                                        break;
                                    }
                                }

                                if (pixelFound_down)
                                    break;
                            }

                            break;
                        }
                    }

                    if (pixelFound_top)
                        break;
                }
            }


            Bitmap croppedImage = CropImage(newEdgesImage, greenLineY + 2, blueLineY - 2);

            Invert invertFilter = new Invert();
            invertFilter.ApplyInPlace(processedImage);
            Bitmap croppedImage2 = CropImage(processedImage, greenLineY + 2, blueLineY - 2);

            // Create a new Bitmap with the same dimensions and a different pixel format
            Bitmap newImage = new Bitmap(croppedImage2.Width, croppedImage2.Height, PixelFormat.Format32bppArgb);

            for (int x = 0; x < croppedImage2.Width; x++)
            {
                for (int y = 0; y < croppedImage2.Height; y++)
                {
                    Color pixelColor = croppedImage2.GetPixel(x, y);

                    // Check if the pixel is black or gray
                    if (pixelColor.GetBrightness() <= 0.5) // Check brightness instead of individual RGB components
                    {
                        // Replace the pixel with red color
                        newImage.SetPixel(x, y, Color.Red);
                    }
                    else
                    {
                        // Copy non-black or gray pixels
                        newImage.SetPixel(x, y, pixelColor);
                    }
                }
            }

            int greenLineYPosition = newImage.Height / 13;

            // Check if the y-coordinate is within the image bounds
            if (greenLineYPosition >= 0 && greenLineYPosition < newImage.Height)
            {
                for (int x = 0; x < newImage.Width; x++)
                {
                    Color pixelColor = newImage.GetPixel(x, greenLineYPosition);

                    // Check if the pixel is red
                    if (pixelColor == Color.Red)
                    {
                        // Replace the pixel with green color
                        newImage.SetPixel(x, greenLineYPosition, Color.Green);
                    }
                }
            }


            //double FindAngle(System.Drawing.Point point1, System.Drawing.Point point2)
            //{
            //    // Вычисляем изменение координат по осям x и y
            //    double deltaX = point2.X - point1.X;
            //    double deltaY = point2.Y - point1.Y;

            //    // Вычисляем арктангенс отношения deltaY к deltaX и конвертируем результат в градусы
            //    double angle = Math.Atan2(deltaY, deltaX) * (180 / Math.PI);

            //    return angle;
            //}

            //System.Drawing.Point point1;
            //System.Drawing.Point point2;
            //for (int x = 0; x < newImage.Width; x++)
            //{

            //    int y = newImage.Height / 16;

            //    Color pixelColor = newImage.GetPixel(x, y);

            //    if (pixelColor == Color.Red)
            //    {
            //        point1 = new System.Drawing.Point(x, y);

            //        for (int X = 0; X < croppedImage2.Width; X++)
            //        {
            //            int Y = newImage.Height - (newImage.Height / 16);

            //            Color pixelColor2 = newImage.GetPixel(x, y);

            //            if (pixelColor2 == Color.Red)
            //            {
            //                point2 = new System.Drawing.Point(X, Y);
            //                FindAngle(point1, point2);
            //                textBox5.Text += FindAngle(point1, point2).ToString() + " ,";
            //                x += croppedImage2.Width / 15;
            //                X += croppedImage2.Width / 15;
            //            }



            //        }

            //    }






            pictureBox2.Image = newImage;
        }

        private void grenndetec(string imagePath)
        {
            Bitmap originalImage = new Bitmap(imagePath);
            Bitmap processedImage = new Bitmap(imagePath);






            // Create a new bitmap with the same size as edgesImage and a non-indexed format
            Bitmap newEdgesImage = new Bitmap(processedImage.Width, processedImage.Height, PixelFormat.Format32bppArgb);

            bool pixelFound_top = false;
            bool pixelFound_down = false;


            // Draw the processed image onto the new bitmap
            using (Graphics g = Graphics.FromImage(newEdgesImage))
            {
                g.DrawImage(processedImage, new Rectangle(0, 0, processedImage.Width, processedImage.Height));
            }

            using (Graphics g = Graphics.FromImage(newEdgesImage))
            {
                for (int y = 0; y < newEdgesImage.Height; y++)
                {
                    for (int x = 0; x < newEdgesImage.Width; x++)
                    {
                        Color pixelColor = newEdgesImage.GetPixel(x, y);
                        int red = pixelColor.R;
                        int green = pixelColor.G;
                        int blue = pixelColor.B;

                        if (red != 0 || green != 0 || blue != 0)
                        {
                            greenLineY = y;
                            int lineWidth = 5; // Ширина линии
                            g.DrawLine(new Pen(Color.Green, lineWidth), new System.Drawing.Point(0, y), new System.Drawing.Point(newEdgesImage.Width, y));
                            pixelFound_top = true;

                            // Начинаем поиск снизу после того, как найден пиксель сверху
                            for (int y_down = newEdgesImage.Height - 1; y_down >= 0; y_down--)
                            {
                                for (int x_down = 0; x_down < newEdgesImage.Width; x_down++)
                                {
                                    Color pixelColor_down = newEdgesImage.GetPixel(x_down, y_down);
                                    int red_down = pixelColor_down.R;
                                    int green_down = pixelColor_down.G;
                                    int blue_down = pixelColor_down.B;

                                    if (red_down != 0 || green_down != 0 || blue_down != 0)
                                    {
                                        blueLineY = y_down;
                                        g.DrawLine(new Pen(Color.Blue, lineWidth), new System.Drawing.Point(0, y_down), new System.Drawing.Point(newEdgesImage.Width, y_down));
                                        pixelFound_down = true;

                                        break;
                                    }
                                }

                                if (pixelFound_down)
                                    break;
                            }

                            break;
                        }
                    }

                    if (pixelFound_top)
                        break;
                }
            }
            Bitmap CropImage(Bitmap originalImage, int topLineY, int bottomLineY)
            {
                // Проверка на допустимые значения topLineY и bottomLineY
                if (topLineY < 0) topLineY = 0;
                if (bottomLineY >= originalImage.Height) bottomLineY = originalImage.Height - 1;

                // Создание прямоугольника для обрезки
                Rectangle cropRect = new Rectangle(0, topLineY, originalImage.Width, bottomLineY - topLineY);

                // Обрезка изображения
                Bitmap croppedImage = originalImage.Clone(cropRect, originalImage.PixelFormat);

                return croppedImage;
            }
            Bitmap croppedImage = CropImage(newEdgesImage, greenLineY + 2, blueLineY - 2);
            Invert invertFilter = new Invert();
            Bitmap newCroppedImage = croppedImage.Clone(new Rectangle(0, 0, croppedImage.Width, croppedImage.Height), PixelFormat.Format32bppArgb);



            //   Check if the pixel is black or gray
            for (int x = 0; x < newCroppedImage.Width; x++)
            {
                for (int y = 0; y < newCroppedImage.Height; y++)
                {
                    Color pixelColor = newCroppedImage.GetPixel(x, y);

                    // Check if the pixel is black or gray
                    if (pixelColor.R <= 128 && pixelColor.G <= 128 && pixelColor.B <= 128)
                    {
                        // Replace the pixel with red color
                        newCroppedImage.SetPixel(x, y, Color.Red);
                    }
                }
            }


            // Отображение результатов
            pictureBox1.Image = originalImage;
            pictureBox2.Image = croppedImage;
        }

        Bitmap CropImage(Bitmap originalImage, int topLineY, int bottomLineY)
        {
            // Проверка на допустимые значения topLineY и bottomLineY
            if (topLineY < 0) topLineY = 0;
            if (bottomLineY >= originalImage.Height) bottomLineY = originalImage.Height - 1;

            // Создание прямоугольника для обрезки
            Rectangle cropRect = new Rectangle(0, topLineY, originalImage.Width, bottomLineY - topLineY);

            // Обрезка изображения
            Bitmap croppedImage = originalImage.Clone(cropRect, originalImage.PixelFormat);

            return croppedImage;
        }

        private Bitmap Thresholding(Bitmap bmp)
        {
            for (int i = 0; i < bmp.Width; i++)
            {
                for (int j = 0; j < bmp.Height; j++)
                {
                    Color pixel = bmp.GetPixel(i, j);
                    int Y = (int)(0.299 * pixel.R + 0.587 * pixel.G + 0.114 * pixel.B);
                    bmp.SetPixel(i, j, Y > 128 ? Color.White : Color.Black);
                }
            }
            return bmp;
        }
        private Bitmap MedianFilter(Bitmap bmp, int size)
        {
            Bitmap copy = (Bitmap)bmp.Clone();
            int radius = size / 2;
            for (int i = radius; i < bmp.Width - radius; i++)
            {
                for (int j = radius; j < bmp.Height - radius; j++)
                {
                    List<int> redValues = new List<int>();
                    List<int> greenValues = new List<int>();
                    List<int> blueValues = new List<int>();
                    for (int k = i - radius; k <= i + radius; k++)
                    {
                        for (int l = j - radius; l <= j + radius; l++)
                        {
                            Color pixel = bmp.GetPixel(k, l);
                            redValues.Add(pixel.R);
                            greenValues.Add(pixel.G);
                            blueValues.Add(pixel.B);
                        }
                    }
                    redValues.Sort();
                    greenValues.Sort();
                    blueValues.Sort();
                    Color medianPixel = Color.FromArgb(redValues[redValues.Count / 2], greenValues[greenValues.Count / 2], blueValues[blueValues.Count / 2]);
                    copy.SetPixel(i, j, medianPixel);
                }
            }
            return copy;
        }
        Bitmap CropImage(Bitmap source, Rectangle cropRegion)
        {
            Bitmap croppedImage = new Bitmap(cropRegion.Width, cropRegion.Height);
            using (Graphics g = Graphics.FromImage(croppedImage))
            {
                g.DrawImage(source, 0, 0, cropRegion, GraphicsUnit.Pixel);
            }
            return croppedImage;
        }
        int UP, DOWM;
        int iter = 1;

        bool IsCloseToWhite(Color color)
        {
            int threshold = 10; // You can adjust this value
            return Math.Abs(color.R - 255) < threshold &&
                   Math.Abs(color.G - 255) < threshold &&
                   Math.Abs(color.B - 255) < threshold;
        }
        void starttobegin(string imagePath)
        {


            Bitmap originalImage = new Bitmap(imagePath);
            pictureBox1.Image = originalImage;

            Bitmap processedImage = new Bitmap(imagePath);
            Thresholding(processedImage);
            Graphics g = Graphics.FromImage(processedImage);

            int topPixelY = -1;
            int bottomPixelY = -1;

            for (int y = 0; y < processedImage.Height; y++)
            {
                for (int x = 0; x < processedImage.Width; x++)
                {
                    Color pixelColor = processedImage.GetPixel(x, y);

                    if (IsCloseToWhite(pixelColor))
                    {
                        if (topPixelY == -1)
                        {
                            topPixelY = y;
                            g.DrawLine(new Pen(Color.Green, 5), new System.Drawing.Point(0, y), new System.Drawing.Point(processedImage.Width, y));
                        }
                        bottomPixelY = y;
                    }
                }
            }

            if (topPixelY != -1 && bottomPixelY != -1)
            {
                g.DrawLine(new Pen(Color.Blue, 5), new System.Drawing.Point(0, bottomPixelY), new System.Drawing.Point(processedImage.Width, bottomPixelY));
            }
            Bitmap croppedImage = CropImage(processedImage, new Rectangle(0, topPixelY, processedImage.Width, bottomPixelY - topPixelY));

            Graphics g1 = Graphics.FromImage(croppedImage);
            Graphics g21 = Graphics.FromImage(croppedImage);




            double FindAngle(System.Drawing.Point point1, System.Drawing.Point point2)
            {
                // Вычисляем изменение координат по осям x и y
                double deltaX = point2.X - point1.X;
                double deltaY = point2.Y - point1.Y;

                // Вычисляем арктангенс отношения deltaY к deltaX и конвертируем результат в градусы
                int angle = (int)(Math.Atan2(deltaY, deltaX) * (180 / Math.PI));

                return angle;
            }
            double FindAngle_From_home(System.Drawing.Point A, System.Drawing.Point B)
            {
                int yq = croppedImage.Height - (croppedImage.Height / 12);
                int xq = croppedImage.Width - 1;
                System.Drawing.Point C = new System.Drawing.Point(xq, yq);

                double dotProduct = (B.X - A.X) * (C.X - A.X) + (B.Y - A.Y) * (C.Y - A.Y);
                double magnitudeAB = Math.Sqrt((B.X - A.X) * (B.X - A.X) + (B.Y - A.Y) * (B.Y - A.Y));
                double magnitudeAC = Math.Sqrt((C.X - A.X) * (C.X - A.X) + (C.Y - A.Y) * (C.Y - A.Y));

                double cosAngle = dotProduct / (magnitudeAB * magnitudeAC);

                // Преобразование радиан в градусы
                double angleInDegrees = Math.Acos(cosAngle) * (180.0 / Math.PI);

                return angleInDegrees;
            }

            int iter1 = 1, iter2 = 1;
            System.Drawing.Point point1;
            System.Drawing.Point point2;

            int X = 0;
            bool stp = true;





            List<System.Drawing.Point> pointsList1 = new List<System.Drawing.Point>();
            List<System.Drawing.Point> pointsList2 = new List<System.Drawing.Point>();

            for (int x = 0; x < croppedImage.Width; x++)
            {
                int y = croppedImage.Height / 12;
                Color pixelColor = croppedImage.GetPixel(x, y);

                if (IsCloseToWhite(pixelColor))
                {
                    textBox6.Text += $" {iter1}, - {x},{y} ;  ";
                    iter1++;

                    point1 = new System.Drawing.Point(x, y);
                    pointsList1.Add(point1);
                    g1.DrawLine(new Pen(Color.Yellow, 5), new System.Drawing.Point(x, 0), new System.Drawing.Point(x, croppedImage.Height));
                    x += croppedImage.Height / 15;
                }
            }
            for (int x = 0; x < croppedImage.Width; x++)
            {
                int y = croppedImage.Height - (croppedImage.Height / 12);
                Color pixelColor = croppedImage.GetPixel(x, y);

                if (IsCloseToWhite(pixelColor))
                {
                    textBox7.Text += $" {iter2}, - {x},{y} ;  ";
                    iter2++;

                    point2 = new System.Drawing.Point(x, y);
                    pointsList2.Add(point2);
                    g1.DrawLine(new Pen(Color.Green, 5), new System.Drawing.Point(x, 0), new System.Drawing.Point(x, croppedImage.Height));
                    x += croppedImage.Height / 15;
                }

            }



            for (int i = Math.Abs(pointsList1.Count - pointsList2.Count); i != pointsList1.Count; i++)
            {
                try
                {
                    if (pointsList1[i].X > pointsList2[i].X)
                    {
                        textBox5.Text += $"{i+1}) {FindAngle_From_home(pointsList2[i], pointsList1[i])},  ";
 
                    }
                    else
                    {

                        textBox5.Text += $"{i}) NUN,  ";
                    }
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // Обработка исключения
                    textBox5.Text += $"{i}) Exception: {ex.Message},  ";
                }

            }

            for (int i = 0; i != pointsList1.Count - 1; i++)
            {
                textBox8.Text += i+1 + " -" + (pointsList1[i + 1].X - pointsList1[i].X).ToString() + " ";

                System.Drawing.Point pn2 = new System.Drawing.Point(pointsList1[i + 1].X, pointsList1[i + 1].Y);
                System.Drawing.Point pn1 = new System.Drawing.Point(pointsList1[i].X, pointsList1[i].Y);
                g1.DrawLine(new Pen(Color.Pink, 5), pn1, pn2);


            }
            //for (int xa = 0; xa < croppedImage.Width; xa++)
            //{
            //    for (int y = 0; y < croppedImage.Height; y++)
            //    {



            //        Color pixelColor = croppedImage.GetPixel(xa, y);
            //        if (IsCloseToWhite(pixelColor))
            //        {
            //            for (int x = xa; x < croppedImage.Width; x += 130)
            //            {
            //                g1.DrawLine(new Pen(Color.Pink, 5), new System.Drawing.Point(x, 0), new System.Drawing.Point(x, croppedImage.Height));


            //            }
            //            xa = croppedImage.Width+1;
            //            y = croppedImage.Height + 1;

            //        }
            //    }
            //}

            pictureBox2.Image = croppedImage;

            //for (int x = 0; x < croppedImage.Width; x++)
            //{

            //    int y = croppedImage.Height / 12;

            //    Color pixelColor = croppedImage.GetPixel(x, y);

            //    if (IsCloseToWhite(pixelColor))
            //    {
            //        textBox6.Text += $" {iter1}, - {x},{y} ;  ";
            //        iter1++;
            //        point1 = new System.Drawing.Point(x, y);
            //        g1.DrawLine(new Pen(Color.Yellow, 5), new System.Drawing.Point(x, 0), new System.Drawing.Point(x, croppedImage.Height));





            //        while (stp) {

            //            X++;
            //            int Y = croppedImage.Height - (croppedImage.Height / 12);

            //            Color pixelColor2 = croppedImage.GetPixel(X, Y);

            //            if (IsCloseToWhite(pixelColor2))
            //            {
            //                textBox7.Text += $" {iter2})  {X},{Y} ;  ";
            //                iter2++;
            //                g1.DrawLine(new Pen(Color.Pink, 5), new System.Drawing.Point(X, 0), new System.Drawing.Point(X, croppedImage.Height));


            //                point2 = new System.Drawing.Point(X, Y);
            //                FindAngle(point2, point1);

            //                textBox5.Text += FindAngle(point1, point2).ToString() + " -- ";
            //                X += croppedImage.Width / 15;

            //                stp = false;
            //                break;
            //            }
            //        }

            //    }

            //    x += croppedImage.Width / 15;

            //}

        }

         
    }
}