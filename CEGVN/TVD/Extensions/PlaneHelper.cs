using System;
using System.Diagnostics;
using Autodesk.Revit.DB;

namespace CEGVN.TVD.Extensions
{
	internal static class PlaneHelper
	{
		public static double SignedDistanceTo(this Plane plane, XYZ p)
		{
			Debug.Assert(Util.IsEqual(plane.Normal.GetLength(), 1.0), "expected normalised plane normal");
			XYZ xyz = p - plane.Origin;
			return plane.Normal.DotProduct(xyz);
		}

		public static XYZ ProjectOnto(this Plane plane, XYZ p)
		{
			double num = plane.SignedDistanceTo(p);
			XYZ xyz = p - num * plane.Normal;
			Debug.Assert(Util.IsZero(plane.SignedDistanceTo(xyz)), "expected point on plane to have zero distance to plane");
			return xyz;
		}

		public static XYZ ProjectOnto(this XYZ p, Plane plane)
		{
			double num = plane.SignedDistanceTo(p);
			XYZ xyz = p - num * plane.Normal;
			Debug.Assert(Util.IsZero(plane.SignedDistanceTo(xyz)), "expected point on plane to have zero distance to plane");
			return xyz;
		}

		public static bool IsPointOnPlane(this Plane plane, XYZ point)
		{
			return Math.Abs(plane.SignedDistanceTo(point)) < 0.0001;
		}

		public static Line ProjectLineOnPlane(this Plane plane, Line line)
		{
			XYZ xyz = line.GetEndPoint(0);
			XYZ xyz2 = line.GetEndPoint(1);
			xyz = xyz.ProjectOnto(plane);
			xyz2 = xyz2.ProjectOnto(plane);
			return Line.CreateBound(xyz, xyz2);
		}
	}
}
