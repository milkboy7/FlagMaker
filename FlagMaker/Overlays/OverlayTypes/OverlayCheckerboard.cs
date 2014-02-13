﻿using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Shapes;
using FlagMaker.Localization;

namespace FlagMaker.Overlays.OverlayTypes
{
	internal class OverlayCheckerboard : Overlay
	{
		public OverlayCheckerboard(int maximumX, int maximumY)
			: base(new List<Attribute>
			{
				new Attribute(strings.X, true, 1, true),
				new Attribute(strings.Y, true, 1, false),
				new Attribute(strings.Width, true, 1, true),
				new Attribute(strings.Height, true, 1, false),
				new Attribute(strings.CountX, true, 4, true),
				new Attribute(strings.CountY, true, 4, true)
			}, maximumX, maximumY)
		{
		}

		public OverlayCheckerboard(Color color, double x, double y, double width, double height, double countX, double countY, int maximumX, int maximumY)
			: base(color, new List<Attribute>
			{
				new Attribute(strings.X, true, x, true),
				new Attribute(strings.Y, true, y, false),
				new Attribute(strings.Width, true, width, true),
				new Attribute(strings.Height, true, height, false),
				new Attribute(strings.CountX, true, countX, true),
				new Attribute(strings.CountY, true, countY, true)
			}, maximumX, maximumY)
		{
		}

		public override string Name { get { return "checkerboard"; } }

		public override void Draw(Canvas canvas)
		{
			var centerX = canvas.Width * (Attributes.Get(strings.X).Value / MaximumX);
			var centerY = canvas.Height * (Attributes.Get(strings.Y).Value / MaximumY);
			var width = canvas.Width * (Attributes.Get(strings.Width).Value / MaximumX);
			var height = canvas.Height * (Attributes.Get(strings.Height).Value / MaximumY);
			if (height == 0) height = width;
			var countX = Attributes.Get(strings.CountX).Value;
			var countY = Attributes.Get(strings.CountY).Value;

			var left = centerX - width / 2;
			var top = centerY - height / 2;
			var blockWidth = width / countX;
			var blockHeight = height / countY;

			for (int x = 0; x < countX; x++)
			{
				for (int y = 0; y < countY; y++)
				{
					if ((x + y) % 2 == 0)
					{
						canvas.Children.Add(new Rectangle
						{
							Width = blockWidth,
							Height = blockHeight,
							Margin = new Thickness(left + x * blockWidth, top + y * blockHeight, 0, 0),
							Fill = new SolidColorBrush(Color),
							SnapsToDevicePixels = true
						});
					}
				}
			}
		}

		public override void SetValues(List<double> values)
		{
			Attributes.Get(strings.X).Value = values[0];
			Attributes.Get(strings.Y).Value = values[1];
			Attributes.Get(strings.Width).Value = values[2];
			Attributes.Get(strings.Height).Value = values[3];
			Attributes.Get(strings.CountX).Value = values[4];
			Attributes.Get(strings.CountY).Value = values[5];
		}

		public override string ExportSvg(int width, int height)
		{
			throw new System.NotImplementedException();
		}

		public override IEnumerable<Shape> Thumbnail
		{
			get
			{
				var shapes = new List<Shape>();

				for (int x = 0; x < 5; x++)
				{
					for (int y = 0; y < 5; y++)
					{
						if ((x + y) % 2 == 0)
						{
							shapes.Add(new Rectangle
							{
								Width = 5,
								Height = 5,
								Margin = new Thickness(2.5 + x * 5, 2.5 + y * 5, 0, 0)
							});
						}
					}
				}

				return shapes;
			}
		}
	}
}
