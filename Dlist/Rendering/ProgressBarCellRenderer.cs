using System;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Rendering
{
    public class ProgressBarCellRenderer : IComplexRenderer
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
        public int ChunkThickness { get; set; }
        public int ChunkSpaceThickness { get; set; }

        public bool Horizontal { get; set; }

        public ProgressBarCellRenderer(int minValue, int maxValue, int chunkThickness = 0, int chunkSpaceThickness = -1, bool horizontal = true)
        {
            if (maxValue <= minValue) throw new ArgumentException("Max value is less than or equal to min value.", nameof(maxValue));

            MinValue = minValue;
            MaxValue = maxValue;
            ChunkThickness = chunkThickness;
            ChunkSpaceThickness = chunkSpaceThickness;
            Horizontal = horizontal;
        }

        public void Draw(Graphics gfx, Rectangle bounds, RenderState state, object value, Color foreColor, Color backColor, Font font)
        {
            // HACK: 3 is subtracted here instead of 2 as a workaround to not draw under the grid if it's enabled.
            var Bounds = new Rectangle(bounds.Left + 1, bounds.Top + 1, bounds.Width - 3, bounds.Height - 3);

            using (var backgroundBrush = new SolidBrush(backColor))
            {
                gfx.FillRectangle(backgroundBrush, bounds);
            }

            int ActualChunkThickness = (ChunkThickness > 0) ? ChunkThickness : ProgressBarRenderer.ChunkThickness;
            int ActualChunkSpaceThickness = (ChunkSpaceThickness >= 0) ? ChunkSpaceThickness : ProgressBarRenderer.ChunkSpaceThickness;
            int TotalChunkThickness = ActualChunkThickness + ActualChunkSpaceThickness;
            double Value = Utils.Clamp((int)value, MinValue, MaxValue);
            double PercentageFactor = (Value + Math.Abs(MinValue)) / (MaxValue - MinValue);

            if (Horizontal)
            {
                ProgressBarRenderer.DrawHorizontalBar(gfx, Bounds);

                int BarWidth = Utils.Clamp((int)Math.Floor(PercentageFactor * (Bounds.Width - 2)), 0, Bounds.Width - 2);

                if (ActualChunkSpaceThickness <= 0)
                {
                    // No chunks
                    var BarBounds = new Rectangle(Bounds.Left + 1, Bounds.Top + 1, BarWidth, Bounds.Height - 2);
                    ProgressBarRenderer.DrawHorizontalChunks(gfx, BarBounds);
                }
                else
                {
                    int ChunkCount = (int)Math.Floor((BarWidth + ActualChunkSpaceThickness) / (double)TotalChunkThickness);
                    var ChunkBounds = new Rectangle(Bounds.Left + 1, Bounds.Top + 1, ActualChunkThickness, Bounds.Height - 2);
                    int RemainingBarWidth = BarWidth - ChunkCount * TotalChunkThickness;

                    for (int i = 0; i < ChunkCount; i++)
                    {
                        ProgressBarRenderer.DrawHorizontalChunks(gfx, ChunkBounds);
                        ChunkBounds.X += TotalChunkThickness;
                    }

                    if (RemainingBarWidth > 0)
                    {
                        ChunkBounds.Width = RemainingBarWidth;
                        ProgressBarRenderer.DrawHorizontalChunks(gfx, ChunkBounds);
                    }
                }
            }
            else
            {
                ProgressBarRenderer.DrawVerticalBar(gfx, Bounds);

                int BarHeight = Utils.Clamp((int)Math.Floor(PercentageFactor * (Bounds.Height - 2)), 0, Bounds.Height - 2);

                if (ActualChunkSpaceThickness <= 0)
                {
                    // No chunks
                    var BarBounds = new Rectangle(Bounds.Left + 1, Bounds.Top + 1 + (Bounds.Height - 2 - BarHeight), Bounds.Width - 2, BarHeight);
                    ProgressBarRenderer.DrawHorizontalChunks(gfx, BarBounds);
                }
                else
                {
                    int ChunkCount = (int)Math.Floor((BarHeight + ActualChunkSpaceThickness) / (double)TotalChunkThickness);
                    var ChunkBounds = new Rectangle(Bounds.Left + 1, Bounds.Top + 1 + (Bounds.Height - 2 - ActualChunkThickness), Bounds.Width - 2, ActualChunkThickness);
                    int RemainingBarHeight = BarHeight - ChunkCount * TotalChunkThickness;

                    for (int i = 0; i < ChunkCount; i++)
                    {
                        ProgressBarRenderer.DrawHorizontalChunks(gfx, ChunkBounds);
                        ChunkBounds.Y -= TotalChunkThickness;
                    }

                    if (RemainingBarHeight > 0)
                    {
                        ChunkBounds.Height = RemainingBarHeight;
                        ChunkBounds.Y += (TotalChunkThickness - RemainingBarHeight - 1);
                        ProgressBarRenderer.DrawHorizontalChunks(gfx, ChunkBounds);
                    }
                }
            }
        }
    }
}
