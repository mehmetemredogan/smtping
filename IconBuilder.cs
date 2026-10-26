using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Collections.Generic;

namespace SMTPing;

public static class IconBuilder
{
    public static void GenerateAppIcon(string outputPath)
    {
        int[] sizes = { 256, 48, 32, 16 };
        var pngDataList = new List<byte[]>();

        foreach (var size in sizes)
        {
            using var bmp = new Bitmap(size, size, PixelFormat.Format32bppArgb);
            using (var g = Graphics.FromImage(bmp))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.Clear(Color.Transparent);

                float scale = size / 256.0f;

                // 1. Arka plan yuvarlatılmış kare (Modern gradient)
                using var path = CreateRoundedRectangle(8 * scale, 8 * scale, 240 * scale, 240 * scale, 48 * scale);
                using var bgBrush = new LinearGradientBrush(
                    new PointF(0, 0),
                    new PointF(size, size),
                    Color.FromArgb(37, 99, 235), // Royal Blue
                    Color.FromArgb(15, 23, 42)   // Slate 900
                );
                g.FillPath(bgBrush, path);

                using var borderPen = new Pen(Color.FromArgb(96, 165, 250), 4 * scale);
                g.DrawPath(borderPen, path);

                // 2. E-posta Zarfı Çizimi
                float envX = 44 * scale;
                float envY = 72 * scale;
                float envW = 168 * scale;
                float envH = 112 * scale;
                float envRadius = 14 * scale;

                using var envPath = CreateRoundedRectangle(envX, envY, envW, envH, envRadius);
                using var envBrush = new SolidBrush(Color.FromArgb(248, 250, 252));
                g.FillPath(envBrush, envPath);

                // Zarf kapağı çizgileri
                using var penEnvelope = new Pen(Color.FromArgb(148, 163, 184), 5 * scale);
                penEnvelope.StartCap = LineCap.Round;
                penEnvelope.EndCap = LineCap.Round;
                penEnvelope.LineJoin = LineJoin.Round;

                // Üst kapak üçgeni
                PointF pTopLeft = new PointF(envX + 6 * scale, envY + 6 * scale);
                PointF pTopRight = new PointF(envX + envW - 6 * scale, envY + 6 * scale);
                PointF pCenter = new PointF(envX + envW / 2, envY + envH * 0.62f);
                g.DrawLine(penEnvelope, pTopLeft, pCenter);
                g.DrawLine(penEnvelope, pTopRight, pCenter);

                // Alt köşelerden hafif çizgiler
                PointF pBottomLeft = new PointF(envX + 8 * scale, envY + envH - 8 * scale);
                PointF pBottomRight = new PointF(envX + envW - 8 * scale, envY + envH - 8 * scale);
                PointF pInnerLeft = new PointF(envX + envW * 0.36f, envY + envH * 0.52f);
                PointF pInnerRight = new PointF(envX + envW * 0.64f, envY + envH * 0.52f);
                g.DrawLine(penEnvelope, pBottomLeft, pInnerLeft);
                g.DrawLine(penEnvelope, pBottomRight, pInnerRight);

                // 3. Şimşek / Yıldırım (Hız & Ping sembolü)
                // Parlak sarı/amber renk
                var boltPoints = new PointF[]
                {
                    new PointF(152 * scale, 34 * scale),
                    new PointF(118 * scale, 126 * scale),
                    new PointF(142 * scale, 126 * scale),
                    new PointF(112 * scale, 218 * scale),
                    new PointF(174 * scale, 108 * scale),
                    new PointF(148 * scale, 108 * scale),
                    new PointF(168 * scale, 34 * scale)
                };

                // Şimşek arkasına hafif koyu gölge
                using var shadowBrush = new SolidBrush(Color.FromArgb(160, 15, 23, 42));
                var shadowPoints = new PointF[boltPoints.Length];
                for (int i = 0; i < boltPoints.Length; i++)
                {
                    shadowPoints[i] = new PointF(boltPoints[i].X + 3 * scale, boltPoints[i].Y + 3 * scale);
                }
                g.FillPolygon(shadowBrush, shadowPoints);

                // Şimşeğin kendisi
                using var boltBrush = new LinearGradientBrush(
                    new PointF(110 * scale, 30 * scale),
                    new PointF(180 * scale, 220 * scale),
                    Color.FromArgb(254, 240, 138), // Açık sarı
                    Color.FromArgb(245, 158, 11)   // Koyu amber
                );
                g.FillPolygon(boltBrush, boltPoints);

                using var boltBorder = new Pen(Color.FromArgb(255, 255, 255), 2.5f * scale);
                g.DrawPolygon(boltBorder, boltPoints);
            }

            using var ms = new MemoryStream();
            bmp.Save(ms, ImageFormat.Png);
            pngDataList.Add(ms.ToArray());
        }

        // ICO dosyasını oluştur
        using var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write);
        using var bw = new BinaryWriter(fs);

        // ICO Header
        bw.Write((short)0); // Reserved
        bw.Write((short)1); // Type 1 = Icon
        bw.Write((short)sizes.Length); // Image count

        int offset = 6 + (16 * sizes.Length);

        // Directory entries
        for (int i = 0; i < sizes.Length; i++)
        {
            int s = sizes[i];
            byte widthByte = (byte)(s >= 256 ? 0 : s);
            byte heightByte = (byte)(s >= 256 ? 0 : s);
            byte[] data = pngDataList[i];

            bw.Write(widthByte);
            bw.Write(heightByte);
            bw.Write((byte)0); // Color count
            bw.Write((byte)0); // Reserved
            bw.Write((short)1); // Color planes
            bw.Write((short)32); // Bits per pixel
            bw.Write(data.Length); // Image size in bytes
            bw.Write(offset); // Image offset

            offset += data.Length;
        }

        // Image data
        for (int i = 0; i < sizes.Length; i++)
        {
            bw.Write(pngDataList[i]);
        }
    }

    private static GraphicsPath CreateRoundedRectangle(float x, float y, float width, float height, float radius)
    {
        var path = new GraphicsPath();
        float diameter = radius * 2;
        path.AddArc(x, y, diameter, diameter, 180, 90);
        path.AddArc(x + width - diameter, y, diameter, diameter, 270, 90);
        path.AddArc(x + width - diameter, y + height - diameter, diameter, diameter, 0, 90);
        path.AddArc(x, y + height - diameter, diameter, diameter, 90, 90);
        path.CloseFigure();
        return path;
    }
}
