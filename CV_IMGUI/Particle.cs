using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class ParticleSystem
{
    private const int ParticleCount = 100;
    private const int DrawCount = 90;
    private readonly Random _random = new Random();
    private readonly PointF[] _particlePositions = new PointF[ParticleCount];
    private readonly PointF[] _particleTargetPositions = new PointF[ParticleCount];
    private readonly float[] _particleSpeeds = new float[ParticleCount];
    private readonly float[] _particleSizes = new float[ParticleCount];
    private readonly float[] _particleRotations = new float[ParticleCount];
    private int glowIndex;
    public Color particleColor { get; set; } = Color.FromArgb(192, 0, 192);
    public Color glowColor { get; set; } = Color.FromArgb(192, 0, 192);

    public ParticleSystem()
    {
        InitializeParticles();
    }

    private void InitializeParticles()
    {
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;
        for (int i = 0; i < ParticleCount; i++)
        {
            _particlePositions[i] = new PointF(0, 0);
            _particleTargetPositions[i] = new PointF(_random.Next(screenSize.Width), screenSize.Height * 2);

            _particleSpeeds[i] = 1 + _random.Next(25);
            _particleSizes[i] = 5 + _random.Next(3);
            _particleRotations[i] = 0;
        }
    }

    private PointF Lerp(PointF start, PointF end, float t)
    {
        return new PointF(start.X + (end.X - start.X) * t, start.Y + (end.Y - start.Y) * t);
    }

    public void UpdateParticles()
    {
        Size screenSize = Screen.PrimaryScreen.Bounds.Size;
        for (int i = 0; i < ParticleCount; i++)
        {
            if (_particlePositions[i].X == 0 || _particlePositions[i].Y == 0)
            {
                _particlePositions[i] = new PointF(_random.Next(screenSize.Width + 1), 15f);
                _particleSpeeds[i] = 1 + _random.Next(25);
                _particleTargetPositions[i] = new PointF(_random.Next(screenSize.Width), screenSize.Height * 2);
            }

            float deltaTime = 1.0f / 60;
            _particlePositions[i] = Lerp(_particlePositions[i], _particleTargetPositions[i], deltaTime * (_particleSpeeds[i] / 60));
            _particleRotations[i] += deltaTime;

            if (_particlePositions[i].Y > screenSize.Height)
            {
                _particlePositions[i] = new PointF(0, 0);
                _particleRotations[i] = 7;
            }
        }
    }

    public void DrawParticles(Graphics e)
    {
        for (int i = 0; i < DrawCount; i++)
        {
            DrawTriangleWithGlow(e, _particlePositions[i], _particleSizes[i], _particleRotations[i]);
        }
    }

    private void DrawGlowEffect(Graphics graphics, PointF position, float size)
    {
        int maxGlowLayers = 10;
        for (int j = 0; j < maxGlowLayers; j++)
        {
            int alpha = 25 - 2 * j;
            using (Brush glowBrush = new SolidBrush(Color.FromArgb(alpha, glowColor.R, glowColor.G, glowColor.B)))
            {
                float glowSize = size + j * 4;
                graphics.FillEllipse(glowBrush, position.X - glowSize / 2, position.Y - glowSize / 2, glowSize, glowSize);
            }
        }
    }

    private void DrawTriangleWithGlow(Graphics graphics, PointF position, float size, float rotation)
    {
        using (GraphicsPath path = new GraphicsPath())
        {
            PointF[] points = new PointF[3];
            double angle = Math.PI / 3;

            for (int i = 0; i < 3; i++)
            {
                points[i] = new PointF(
                    position.X + (float)(Math.Cos(i * 2 * angle + rotation) * size),
                    position.Y + (float)(Math.Sin(i * 2 * angle + rotation) * size)
                );
            }

            path.AddPolygon(points);

            DrawGlowEffect(graphics, position, size);

            using (Brush brush = new SolidBrush(particleColor))
            {
                graphics.FillPath(brush, path);
            }
        }
    }
}
