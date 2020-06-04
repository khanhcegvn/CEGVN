using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Autodesk.Revit.DB;

namespace CEGVN.TVD.Extensions
{
	public static class Util
	{
		public static double Eps
		{
			get
			{
				return 1E-09;
			}
		}

		public static double MinLineLength
		{
			get
			{
				return 1E-09;
			}
		}

		public static double TolPointOnPlane
		{
			get
			{
				return 1E-09;
			}
		}

		public static bool IsZero(double a, double tolerance)
		{
			return tolerance > Math.Abs(a);
		}

		public static bool IsZero(double a)
		{
			return Util.IsZero(a, 1E-09);
		}

		public static bool IsEqual(double a, double b)
		{
			return Util.IsZero(b - a);
		}

		public static int Compare(double a, double b)
		{
			return (!Util.IsEqual(a, b)) ? ((a >= b) ? 1 : -1) : 0;
		}

		public static int Compare(XYZ p, XYZ q)
		{
			int num = Util.Compare(p.X, q.X);
			bool flag = num == 0;
			if (flag)
			{
				num = Util.Compare(p.Y, q.Y);
				bool flag2 = num == 0;
				if (flag2)
				{
					num = Util.Compare(p.Z, q.Z);
				}
			}
			return num;
		}

		public static int Compare(Plane a, Plane b)
		{
			int num = Util.Compare(a.Normal, b.Normal);
			bool flag = num == 0;
			if (flag)
			{
				num = Util.Compare(a.SignedDistanceTo(XYZ.Zero), b.SignedDistanceTo(XYZ.Zero));
				bool flag2 = num == 0;
				if (flag2)
				{
					num = Util.Compare(a.XVec.AngleOnPlaneTo(b.XVec, b.Normal), 0.0);
				}
			}
			return num;
		}

		public static bool IsEqual(XYZ p, XYZ q)
		{
			return Util.Compare(p, q) == 0;
		}

		public static bool BoundingBoxXyzContains(BoundingBoxXYZ bb, XYZ p)
		{
			return 0 < Util.Compare(bb.Min, p) && 0 < Util.Compare(p, bb.Max);
		}

		public static bool IsPerpendicular(XYZ v, XYZ w)
		{
			double length = v.GetLength();
			double length2 = v.GetLength();
			double num = Math.Abs(v.DotProduct(w));
			return 1E-09 < length && 1E-09 < length2 && 1E-09 > num;
		}

		public static bool IsParallel(XYZ p, XYZ q)
		{
			return p.CrossProduct(q).IsZeroLength();
		}

		public static bool IsHorizontal(XYZ v)
		{
			return Util.IsZero(v.Z);
		}

		public static bool IsHorizontal(Edge e)
		{
			XYZ xyz = e.Evaluate(0.0);
			XYZ xyz2 = e.Evaluate(1.0);
			return Util.IsHorizontal(xyz2 - xyz);
		}

		public static bool IsHorizontal(PlanarFace f)
		{
			return Util.IsVertical(f.FaceNormal);
		}

		public static bool IsVertical(XYZ v)
		{
			return Util.IsZero(v.X) && Util.IsZero(v.Y);
		}

		public static bool IsVertical(XYZ v, double tolerance)
		{
			return Util.IsZero(v.X, tolerance) && Util.IsZero(v.Y, tolerance);
		}

		public static bool IsVertical(PlanarFace f)
		{
			return Util.IsHorizontal(f.FaceNormal);
		}

		public static bool IsVertical(CylindricalFace f)
		{
			return Util.IsVertical(f.Axis);
		}
	
		public static double MmToFoot(double mm)
		{
			return mm / 304.79999999999995;
		}

		public static double MeterToFoot(double metter)
		{
			return metter / 0.30479999999999996;
		}

		public static double FootToMm(double feet)
		{
			return feet * 304.79999999999995;
		}

		public static double CubicFootToCubicMeter(double cubicFoot)
		{
			return cubicFoot * 0.02831684659199999;
		}

		public static double SquareFootToSquareMeter(double squareFoot)
		{
			return squareFoot * 0.092903039999999978;
		}

		public static double RadiansToDegrees(double rads)
		{
			return rads * 57.295779513082323;
		}

		public static double DegreesToRadians(double degrees)
		{
			return degrees * 0.017453292519943295;
		}

		public static XYZ Midpoint(XYZ p, XYZ q)
		{
			return 0.5 * (p + q);
		}

		public static XYZ Midpoint(Line line)
		{
			return Util.Midpoint(line.GetEndPoint(0), line.GetEndPoint(1));
		}

		public static XYZ Normal(Line line)
		{
			XYZ endPoint = line.GetEndPoint(0);
			XYZ endPoint2 = line.GetEndPoint(1);
			XYZ xyz = endPoint2 - endPoint;
			return xyz.CrossProduct(XYZ.BasisZ).Normalize();
		}

		public static List<XYZ> GetBottomCorners(BoundingBoxXYZ b)
		{
			double z = b.Min.Z;
			return new List<XYZ>
			{
				new XYZ(b.Min.X, b.Min.Y, z),
				new XYZ(b.Max.X, b.Min.Y, z),
				new XYZ(b.Max.X, b.Max.Y, z),
				new XYZ(b.Min.X, b.Max.Y, z)
			};
		}

		public static XYZ Intersection(Curve c1, Curve c2)
		{
			XYZ endPoint = c1.GetEndPoint(0);
			XYZ endPoint2 = c1.GetEndPoint(1);
			XYZ endPoint3 = c2.GetEndPoint(0);
			XYZ endPoint4 = c2.GetEndPoint(1);
			XYZ xyz = endPoint2 - endPoint;
			XYZ xyz2 = endPoint4 - endPoint3;
			XYZ xyz3 = endPoint3 - endPoint;
			XYZ result = null;
			double num = (xyz2.X * xyz3.Y - xyz2.Y * xyz3.X) / (xyz2.X * xyz.Y - xyz2.Y * xyz.X);
			bool flag = !double.IsInfinity(num);
			if (flag)
			{
				double num2 = endPoint.X + num * xyz.X;
				double num3 = endPoint.Y + num * xyz.Y;
				result = new XYZ(num2, num3, 0.0);
			}
			return result;
		}

		public static Solid CreateSphereAt(XYZ centre, double radius)
		{
			Frame frame = new Frame(centre, XYZ.BasisX, XYZ.BasisY, XYZ.BasisZ);
			Arc arc = Arc.Create(centre - radius * XYZ.BasisZ, centre + radius * XYZ.BasisZ, centre + radius * XYZ.BasisX);
			Line line = Line.CreateBound(arc.GetEndPoint(1), arc.GetEndPoint(0));
			CurveLoop curveLoop = new CurveLoop();
			curveLoop.Append(arc);
			curveLoop.Append(line);
			return GeometryCreationUtilities.CreateRevolvedGeometry(frame, new List<CurveLoop>(1)
			{
				curveLoop
			}, 0.0, 6.2831853071795862);
		}

		public static Solid CreateCube(double d)
		{
			return Util.CreateRectangularPrism(XYZ.Zero, d, d, d);
		}

		public static Solid CreateRectangularPrism(XYZ center, double d1, double d2, double d3)
		{
			List<Curve> list = new List<Curve>();
			XYZ xyz = new XYZ((0.0 - d1) / 2.0, (0.0 - d2) / 2.0, (0.0 - d3) / 2.0);
			XYZ xyz2 = new XYZ((0.0 - d1) / 2.0, d2 / 2.0, (0.0 - d3) / 2.0);
			XYZ xyz3 = new XYZ(d1 / 2.0, d2 / 2.0, (0.0 - d3) / 2.0);
			XYZ xyz4 = new XYZ(d1 / 2.0, (0.0 - d2) / 2.0, (0.0 - d3) / 2.0);
			list.Add(Line.CreateBound(xyz, xyz2));
			list.Add(Line.CreateBound(xyz2, xyz3));
			list.Add(Line.CreateBound(xyz3, xyz4));
			list.Add(Line.CreateBound(xyz4, xyz));
			CurveLoop curveLoop = CurveLoop.Create(list);
			SolidOptions solidOptions = new SolidOptions(ElementId.InvalidElementId, ElementId.InvalidElementId);
			return GeometryCreationUtilities.CreateExtrusionGeometry(new CurveLoop[]
			{
				curveLoop
			}, XYZ.BasisZ, d3, solidOptions);
		}

		public static Solid CreateSolidFromBoundingBox(Solid inputSolid)
		{
			BoundingBoxXYZ boundingBox = inputSolid.GetBoundingBox();
			XYZ xyz = new XYZ(boundingBox.Min.X, boundingBox.Min.Y, boundingBox.Min.Z);
			XYZ xyz2 = new XYZ(boundingBox.Max.X, boundingBox.Min.Y, boundingBox.Min.Z);
			XYZ xyz3 = new XYZ(boundingBox.Max.X, boundingBox.Max.Y, boundingBox.Min.Z);
			XYZ xyz4 = new XYZ(boundingBox.Min.X, boundingBox.Max.Y, boundingBox.Min.Z);
			Line item = Line.CreateBound(xyz, xyz2);
			Line item2 = Line.CreateBound(xyz2, xyz3);
			Line item3 = Line.CreateBound(xyz3, xyz4);
			Line item4 = Line.CreateBound(xyz4, xyz);
			List<Curve> list = new List<Curve>();
			list.Add(item);
			list.Add(item2);
			list.Add(item3);
			list.Add(item4);
			double num = boundingBox.Max.Z - boundingBox.Min.Z;
			CurveLoop item5 = CurveLoop.Create(list);
			Solid solid = GeometryCreationUtilities.CreateExtrusionGeometry(new List<CurveLoop>
			{
				item5
			}, XYZ.BasisZ, num);
			return SolidUtils.CreateTransformed(solid, boundingBox.Transform);
		}

		public static XYZ Greater(XYZ pt1, XYZ pt2, XYZ axis)
		{
			XYZ result = new XYZ();
			bool flag = axis.Equals(XYZ.BasisX);
			if (flag)
			{
				result = ((pt1.X <= pt2.X) ? pt2 : pt1);
			}
			bool flag2 = axis.Equals(XYZ.BasisY);
			if (flag2)
			{
				result = ((pt1.Y <= pt2.Y) ? pt2 : pt1);
			}
			bool flag3 = axis.Equals(XYZ.BasisZ);
			if (flag3)
			{
				result = ((pt1.Z <= pt2.Z) ? pt2 : pt1);
			}
			return result;
		}

		public static XYZ GetClosestPt(XYZ pt, List<XYZ> pts)
		{
			XYZ xyz = new XYZ();
			double num = 0.0;
			foreach (XYZ xyz2 in pts)
			{
				bool flag = !pt.Equals(xyz2);
				if (flag)
				{
					double num2 = Math.Sqrt(Math.Pow(pt.X - xyz2.X, 2.0) + Math.Pow(pt.Y - xyz2.Y, 2.0) + Math.Pow(pt.Z - xyz2.Z, 2.0));
					bool flag2 = xyz.IsZeroLength();
					if (flag2)
					{
						num = num2;
						xyz = xyz2;
					}
					else
					{
						bool flag3 = num2 < num;
						if (flag3)
						{
							num = num2;
							xyz = xyz2;
						}
					}
				}
			}
			return xyz;
		}

		private const double _eps = 1E-09;
		private const double _minimumSlope = 0.3;
		private const double _convertFootToMm = 304.79999999999995;
		private const double _convertFootToMeter = 0.30479999999999996;
		private const double _convertCubicFootToCubicMeter = 0.02831684659199999;
		private const double _convertSquareFootToSquareMeter = 0.092903039999999978;
		private const double kPi = 3.1415926535897931;
	}
}
