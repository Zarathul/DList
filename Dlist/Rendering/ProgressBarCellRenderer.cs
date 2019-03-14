using System;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Rendering
{
    public class ProgressBarCellRenderer : VisualStyleRendererBase, IComplexRenderer
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public bool Horizontal { get; set; }
        public int ChunkThickness { get; set; }
        public int ChunkSpaceThickness { get; set; }

        public ProgressBarCellRenderer(int minValue, int maxValue, bool horizontal = true, int chunkThickness = 0, int chunkSpaceThickness = -1)
        {
            if (maxValue <= minValue) throw new ArgumentException("Max value is less than or equal to min value.", nameof(maxValue));

            MinValue = minValue;
            MaxValue = maxValue;
            Horizontal = horizontal;
            ChunkThickness = chunkThickness;
            ChunkSpaceThickness = chunkSpaceThickness;
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            // HACK: @GRID 3 is subtracted here instead of 2 as a workaround to not draw under the grid if it's enabled.
            var Bounds = new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 3, bounds.Height - 3);

            var BackgroundBrush = GetBrush(backColor);
            gfx.FillRectangle(BackgroundBrush, bounds);

            int ActualChunkThickness = (ChunkThickness > 0) ? ChunkThickness : ProgressBarRenderer.ChunkThickness;
            int ActualChunkSpaceThickness = (ChunkSpaceThickness >= 0) ? ChunkSpaceThickness : ProgressBarRenderer.ChunkSpaceThickness;
            int TotalChunkThickness = ActualChunkThickness + ActualChunkSpaceThickness;
            int Value = Utils.Clamp((int)value, MinValue, MaxValue);
            double PercentageFactor = ((double)Value + Math.Abs(MinValue)) / (MaxValue - MinValue);

            int BarThickness = 0;
            Rectangle BarBounds;
            Rectangle ChunkBounds;

            if (Horizontal)
            {
                BarThickness = (int)Math.Floor(PercentageFactor * (Bounds.Width - 2));
                BarThickness = Utils.Clamp(BarThickness, 0, Bounds.Width - 2);
                BarBounds = new Rectangle(Bounds.Left + 1, Bounds.Top + 1, BarThickness, Bounds.Height - 2);
                ChunkBounds = new Rectangle(Bounds.Left + 1, Bounds.Top + 1, ActualChunkThickness, Bounds.Height - 2);
                ProgressBarRenderer.DrawHorizontalBar(gfx, Bounds);
            }
            else
            {
                BarThickness = (int)Math.Floor(PercentageFactor * (Bounds.Height - 2));
                BarThickness = Utils.Clamp(BarThickness, 0, Bounds.Height - 2);
                BarBounds = new Rectangle(Bounds.Left + 1, Bounds.Top + 1 + (Bounds.Height - 2 - BarThickness), Bounds.Width - 2, BarThickness);
                ChunkBounds = new Rectangle(Bounds.Left + 1, Bounds.Top + 1 + (Bounds.Height - 2 - ActualChunkThickness), Bounds.Width - 2, ActualChunkThickness);
                ProgressBarRenderer.DrawVerticalBar(gfx, Bounds);
            }

            if (BarThickness == 0 && Value > MinValue)
            {
                // If value is greater than the minimal value at least show something. Even if it's only one pixel.
                BarThickness = 1;
            }

            int ChunkCount = (int)Math.Floor((BarThickness + ActualChunkSpaceThickness) / (double)TotalChunkThickness);
            int RemainingBarThickness = BarThickness - ChunkCount * TotalChunkThickness;

            if (ActualChunkSpaceThickness <= 0)
            {
                // No chunks
                ProgressBarRenderer.DrawHorizontalChunks(gfx, BarBounds);
            }
            else
            {
                if (Horizontal)
                {
                    for (int i = 0; i < ChunkCount; i++)
                    {
                        ProgressBarRenderer.DrawHorizontalChunks(gfx, ChunkBounds);
                        ChunkBounds.X += TotalChunkThickness;
                    }

                    if (RemainingBarThickness > 0)
                    {
                        ChunkBounds.Width = RemainingBarThickness;
                        ProgressBarRenderer.DrawHorizontalChunks(gfx, ChunkBounds);
                    }
                }
                else
                {
                    for (int i = 0; i < ChunkCount; i++)
                    {
                        ProgressBarRenderer.DrawVerticalChunks(gfx, ChunkBounds);
                        ChunkBounds.Y -= TotalChunkThickness;
                    }

                    if (RemainingBarThickness > 0)
                    {
                        ChunkBounds.Height = RemainingBarThickness;
                        ChunkBounds.Y += (TotalChunkThickness - RemainingBarThickness - 1);
                        ProgressBarRenderer.DrawVerticalChunks(gfx, ChunkBounds);
                    }
                }
            }
        }
    }
}
