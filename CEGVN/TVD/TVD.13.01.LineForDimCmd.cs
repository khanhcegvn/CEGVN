using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.Exceptions;
using Autodesk.Revit.UI;
using CEGVN.TVD.Extensions;
using Autodesk.Revit.UI.Selection;

namespace CEGVN.TVD
{
	[Transaction(TransactionMode.Manual)]
	public class LineForDimCmd : IExternalCommand
	{
		public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
		{
			UIApplication application = commandData.Application;
			UIDocument activeUIDocument = application.ActiveUIDocument;
			Document doc = activeUIDocument.Document;
			Selection selection = activeUIDocument.Selection;
			View activeView = doc.ActiveView;
			Plane plane = Plane.CreateByNormalAndOrigin(activeView.ViewDirection, activeView.Origin);
			List<Dimension> list = (from x in selection.GetElementIds()
			select doc.GetElement(x)).Cast<Dimension>().ToList<Dimension>();
			bool flag = list.Count == 2;
			Dimension dimension;
			Dimension dimension2;
			if (flag)
			{
				dimension = list[0];
				dimension2 = list[1];
			}
			else
			{
				dimension = (doc.GetElement(selection.PickObject(ObjectType.Element, new DimensionSelectionFilter())) as Dimension);
				dimension2 = (doc.GetElement(selection.PickObject(ObjectType.Element, new DimensionSelectionFilter())) as Dimension);
			}
			Line line = dimension.Curve as Line;
			line.MakeBound(0.0, 1.0);
			Line line2 = dimension2.Curve as Line;
			line2.MakeBound(0.0, 1.0);
			Line l = this.LineOnPlane(line, plane);
			Line ll = this.LineOnPlane(line2, plane);
			XYZ xyz = this.ExtendLineIntersection(l, ll);
			XYZ dimensionStartPoint = this.GetDimensionStartPoint(dimension);
			List<XYZ> dimensionPoints = this.GetDimensionPoints(dimension, dimensionStartPoint);
			XYZ dimensionStartPoint2 = this.GetDimensionStartPoint(dimension2);
			List<XYZ> dimensionPoints2 = this.GetDimensionPoints(dimension2, dimensionStartPoint2);
			XYZ xyz2 = xyz;
			XYZ xyz3 = XYZ.Zero;
			XYZ xyz4 = XYZ.Zero;
			double num = double.MaxValue;
			foreach (XYZ xyz5 in dimensionPoints)
			{
				double num2 = xyz2.DistanceTo(xyz5);
				bool flag2 = num2 < num;
				if (flag2)
				{
					xyz3 = xyz5;
					num = num2;
				}
			}
			double num3 = double.MaxValue;
			foreach (XYZ xyz6 in dimensionPoints2)
			{
				double num4 = xyz2.DistanceTo(xyz6);
				bool flag3 = num4 < num3;
				if (flag3)
				{
					xyz4 = xyz6;
					num3 = num4;
				}
			}
			xyz3 = plane.ProjectOnto(xyz3);
			xyz4 = plane.ProjectOnto(xyz4);
			using (Transaction transaction = new Transaction(doc))
			{
				transaction.Start("Draw Point Markers");
				doc.Create.NewDetailCurve(activeView, Line.CreateBound(plane.ProjectOnto(xyz), xyz3));
				doc.Create.NewDetailCurve(activeView, Line.CreateBound(plane.ProjectOnto(xyz), xyz4));
				transaction.Commit();
			}
			return 0;
		}

		private List<XYZ> GetDimensionPoints(Dimension dim, XYZ pStart)
		{
			Line line = dim.Curve as Line;
			bool flag = line == null;
			List<XYZ> result;
			if (flag)
			{
				result = null;
			}
			else
			{
				List<XYZ> list = new List<XYZ>();
				line.MakeBound(0.0, 1.0);
				XYZ endPoint = line.GetEndPoint(0);
				XYZ endPoint2 = line.GetEndPoint(1);
				XYZ xyz = endPoint2.Subtract(endPoint).Normalize();
				bool flag2 = dim.Segments.Size == 0;
				if (flag2)
				{
					XYZ xyz2 = 0.5 * dim.Value.Value * xyz;
					list.Add(pStart - xyz2);
					list.Add(pStart + xyz2);
				}
				else
				{
					XYZ xyz3 = pStart;
					foreach (object obj in dim.Segments)
					{
						DimensionSegment dimensionSegment = (DimensionSegment)obj;
						XYZ xyz4 = dimensionSegment.Value.Value * xyz;
						bool flag3 = list.Count == 0;
						if (flag3)
						{
							list.Add(xyz3 = pStart - 0.5 * xyz4);
						}
						list.Add(xyz3 = xyz3.Add(xyz4));
					}
				}
				result = list;
			}
			return result;
		}

		private XYZ GetDimensionStartPoint(Dimension dim)
		{
			XYZ p = null;

			try
			{
				p = dim.Origin;
			}
			catch (Autodesk.Revit.Exceptions.ApplicationException ex)
			{
				Debug.Assert(ex.Message.Equals("Cannot access this method if this dimension has more than one segment."));

				foreach (DimensionSegment seg in dim.Segments)
				{
					p = seg.Origin;
					break;
				}
			}
			return p;
		}

		private Line LineOnPlane(Line line, Plane plane)
		{
			XYZ xyz = plane.ProjectOnto(line.GetEndPoint(0));
			XYZ xyz2 = plane.ProjectOnto(line.GetEndPoint(1));
			return Line.CreateBound(xyz, xyz2);
		}

		public XYZ ExtendLineIntersection(Line l, Line ll)
		{
			IntersectionResultArray resultArray;
			if (Line.CreateBound(l.Origin + 10000.0 * l.Direction, l.Origin - 10000.0 * l.Direction).Intersect((Curve)Line.CreateBound(ll.Origin + 10000.0 * ll.Direction, ll.Origin - 10000.0 * ll.Direction), out resultArray) != SetComparisonResult.Overlap)
				throw new System.InvalidOperationException("Input lines did not intersect.");
			if (resultArray == null || resultArray.Size != 1)
				throw new System.InvalidOperationException("Could not extract line intersection point.");
			return resultArray.get_Item(0).XYZPoint;
		}
	}
}
