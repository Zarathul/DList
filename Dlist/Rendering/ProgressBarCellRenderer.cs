/*
 * Copyright 2019 Zarathul
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

using System;
using System.Drawing;
using System.Windows.Forms;

namespace InCoding.DList.Rendering
{
    public class ProgressBarCellRenderer : CellRendererBase, IRenderer
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

            bounds.Inflate(-1, -1); // Leave a 1 pixel wide border around the progressbar.

            if (Horizontal)
            {
                BarThickness = (int)Math.Floor(PercentageFactor * (bounds.Width - 2));
                BarThickness = Utils.Clamp(BarThickness, 0, bounds.Width - 2);
                BarBounds = new Rectangle(bounds.Left + 1, bounds.Top + 1, BarThickness, bounds.Height - 2);
                ChunkBounds = new Rectangle(bounds.Left + 1, bounds.Top + 1, ActualChunkThickness, bounds.Height - 2);
                ProgressBarRenderer.DrawHorizontalBar(gfx, bounds);
            }
            else
            {
                BarThickness = (int)Math.Floor(PercentageFactor * (bounds.Height - 2));
                BarThickness = Utils.Clamp(BarThickness, 0, bounds.Height - 2);
                BarBounds = new Rectangle(bounds.Left + 1, bounds.Top + 1 + (bounds.Height - 2 - BarThickness), bounds.Width - 2, BarThickness);
                ChunkBounds = new Rectangle(bounds.Left + 1, bounds.Top + 1 + (bounds.Height - 2 - ActualChunkThickness), bounds.Width - 2, ActualChunkThickness);
                ProgressBarRenderer.DrawVerticalBar(gfx, bounds);
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
                // No chunks, so draw one big chunk which should be a solid bar.
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
