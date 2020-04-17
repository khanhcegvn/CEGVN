#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CEGVN.TVD;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#endregion

namespace CEGVN.TVD
{
    [Transaction(TransactionMode.Manual)]
    public class FindGavitycmd : IExternalCommand
    {
        public Document doc;
        public ExternalEvent _event;
        public UIDocument uidoc;
        public Selection sel;
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            doc = uidoc.Document;
            sel = uidoc.Selection;
            Reference reference = sel.PickObject(ObjectType.Element, new AssemblySelectionfilter(), "Select Element");
            AssemblyInstance assemblyInstance = doc.GetElement(reference) as AssemblyInstance;
            using (var form = new FrmFindGravity())
            {
                if (form.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                {
                    if (form.Allelement)
                    {
                        Excute(doc, assemblyInstance);

                    }
                    if (form.Structuralframming)
                    {
                        ExcuteForStructuralFramming(doc, assemblyInstance);
                    }
                }
            }
            return Result.Succeeded;
        }
        public FamilyInstance FIlterstructuralframming(AssemblyInstance assemblyInstance)
        {
            FamilyInstance familyInstance = null;
            var col = assemblyInstance.GetMemberIds();
            foreach (var item in col)
            {
                if (doc.GetElement(item).Name.Contains("NE") || doc.GetElement(item).Name.Contains("Touchdown") || doc.GetElement(item).Name.Contains("SE") || doc.GetElement(item).Name.Contains("NW") || doc.GetElement(item).Name.Contains("3") || doc.GetElement(item).Name.Contains("G3"))
                {
                    familyInstance = doc.GetElement(item) as FamilyInstance;
                    break;
                }
            }
            return familyInstance;
        }
        public List<FamilyInstance> FIlterAllstructuralFramming(AssemblyInstance assemblyInstance)
        {
            List<FamilyInstance> list = new List<FamilyInstance>();
            var col = assemblyInstance.GetMemberIds();
            foreach (var item in col)
            {
                if (doc.GetElement(item).Category.Id.IntegerValue == (int)BuiltInCategory.OST_StructuralFraming)
                {
                    FamilyInstance familyInstance = doc.GetElement(item) as FamilyInstance;
                    list.Add(familyInstance);
                }
            }
            return list;
        }
        public void Excute(Document doc, AssemblyInstance assemblyInstance)
        {
            FamilyInstance Skin = FIlterstructuralframming(assemblyInstance);
            List<FamilyInstance> Frames = FIlterFrame(assemblyInstance);
            List<FamilyInstance> Connecions = FIlterConnections(assemblyInstance);
            var symbol = Get3dsymbol(doc);
            XYZ Center1;
            double Volumns1;
            GetCenterPoinSkintSolids(Solidhelper.AllSolids(Skin), out Center1, out Volumns1);
            XYZ Center2;
            double Volumns2;
            CaculatorSolidFrame(Frames, out Center2, out Volumns2);
            XYZ Center3;
            double Volumns3;
            CaculatorSolidConnections(Connecions, out Center3, out Volumns3);
            double x = 0;
            double y = 0;
            double z = 0;
            x = ((Center1.X) * Volumns1 * 150 + (Center2.X) * Volumns2 * 490 + (Center3.X) * Volumns3 * 490) / (Volumns1 * 150 + Volumns2 * 490 + Volumns3 * 490);
            y = ((Center1.Y) * Volumns1 * 150 + (Center2.Y) * Volumns2 * 490 + (Center3.Y) * Volumns3 * 490) / (Volumns1 * 150 + Volumns2 * 490 + Volumns3 * 490);
            z = ((Center1.Z) * Volumns1 * 150 + (Center2.Z) * Volumns2 * 490 + (Center3.Z) * Volumns3 * 490) / (Volumns1 * 150 + Volumns2 * 490 + Volumns3 * 490);
            var ft = new XYZ(x, y, z);
            if (symbol != null)
            {
                PlaceSymbol(doc, symbol, ft, Skin);
            }
            else
            {
                DrawingModelline(doc, ft);
            }
        }
        public void ExcuteForStructuralFramming(Document doc, AssemblyInstance assemblyInstance)
        {
            List<FamilyInstance> familyInstances = FIlterAllstructuralFramming(assemblyInstance);
            var symbol = Get3dsymbol(doc);
            XYZ tong = XYZ.Zero;
            for (int i = 0; i < familyInstances.Count; i++)
            {
                var value = familyInstances[i];
                XYZ Center1;
                double Volumns1;
                GetCenterPoinSkintSolids(Solidhelper.AllSolids(value), out Center1, out Volumns1);
                tong = tong + Center1;
            }
            XYZ Center = tong / familyInstances.Count;
            if (symbol != null)
            {
                PlaceSymbol(doc, symbol, Center, familyInstances.First());
            }
            else
            {
                DrawingModelline(doc, Center);
            }
        }
        public List<FamilyInstance> FIlterFrame(AssemblyInstance assemblyInstance)
        {
            List<FamilyInstance> List = new List<FamilyInstance>();
            var col = assemblyInstance.GetMemberIds();
            foreach (var item in col)
            {
                if (doc.GetElement(item).Name.Contains("FRAME"))
                {
                    var familyInstance = doc.GetElement(item) as FamilyInstance;
                    List.Add(familyInstance);
                }
            }
            return List;
        }
        public List<FamilyInstance> FIlterConnections(AssemblyInstance assemblyInstance)
        {
            List<FamilyInstance> List = new List<FamilyInstance>();
            var col = assemblyInstance.GetMemberIds();
            foreach (var item in col)
            {
                if (doc.GetElement(item).Name.Contains("CONN"))
                {
                    var familyInstance = doc.GetElement(item) as FamilyInstance;
                    List.Add(familyInstance);
                }
            }
            return List;
        }
        public void CaculatorSolidFrame(List<FamilyInstance> familyInstances, out XYZ Center, out double Volumn)
        {
            Dictionary<XYZ, double> dic = new Dictionary<XYZ, double>();
            double Volumncount = 0;
            double x = 0;
            double y = 0;
            double z = 0;
            foreach (var item in familyInstances)
            {
                double volumn;
                XYZ center;
                var solids = Solidhelper.AllSolids(item);
                GetCenterPointFrameSolids(solids, out center, out volumn);
                dic.Add(center, volumn);
            }
            foreach (var item in dic.Values.ToList())
            {
                Volumncount = Volumncount + Math.Abs(item);
            }
            for (int i = 0; i < dic.Keys.Count; i++)
            {
                if (dic.Keys.ToList()[i] != null)
                {
                    x = x + dic.Keys.ToList()[i].X * Math.Abs(dic.Values.ToList()[i]) / Volumncount;
                    y = y + dic.Keys.ToList()[i].Y * Math.Abs(dic.Values.ToList()[i]) / Volumncount;
                    z = z + dic.Keys.ToList()[i].Z * Math.Abs(dic.Values.ToList()[i]) / Volumncount;
                }
            }
            Center = new XYZ(x, y, z);
            Volumn = Volumncount;
        }
        public void CaculatorSolidConnections(List<FamilyInstance> familyInstances, out XYZ Center, out double Volumn)
        {
            Dictionary<XYZ, double> dic = new Dictionary<XYZ, double>();
            double Volumncount = 0;
            double x = 0;
            double y = 0;
            double z = 0;
            foreach (var item in familyInstances)
            {
                double volumn;
                XYZ center;
                var solids = Solidhelper.AllSolids(item);
                GetCenterPointFrameSolids(solids, out center, out volumn);
                dic.Add(center, volumn);
            }
            foreach (var item in dic.Values.ToList())
            {
                Volumncount = Volumncount + Math.Abs(item);
            }
            for (int i = 0; i < dic.Keys.Count; i++)
            {
                if (dic.Keys.ToList()[i] != null)
                {
                    x = x + dic.Keys.ToList()[i].X * Math.Abs(dic.Values.ToList()[i]) / Volumncount;
                    y = y + dic.Keys.ToList()[i].Y * Math.Abs(dic.Values.ToList()[i]) / Volumncount;
                    z = z + dic.Keys.ToList()[i].Z * Math.Abs(dic.Values.ToList()[i]) / Volumncount;
                }
            }
            Center = new XYZ(x, y, z);
            Volumn = Volumncount;
        }
        public void GetCenterPointFrameSolids(List<Solid> solids, out XYZ Center, out double Volumn)
        {
            List<Solid> list1 = new List<Solid>();
            double Volumncount = 0;
            double x = 0;
            double y = 0;
            double z = 0;
            for (int i = 0; i < solids.Count; i++)
            {
                try
                {
                    if (solids[i] != null)
                    {
                        if (solids[i].ComputeCentroid() != null)
                        {
                            list1.Add(solids[i]);
                        }
                    }
                }
                catch
                {

                }
            }
            try
            {
                foreach (var item in list1)
                {
                    Volumncount = Volumncount + Math.Abs(item.Volume);
                }
                for (int i = 0; i < list1.Count; i++)
                {
                    if (list1[i] != null)
                    {
                        x = x + list1[i].ComputeCentroid().X * Math.Abs(list1[i].Volume) / Volumncount;
                        y = y + list1[i].ComputeCentroid().Y * Math.Abs(list1[i].Volume) / Volumncount;
                        z = z + list1[i].ComputeCentroid().Z * Math.Abs(list1[i].Volume) / Volumncount;
                    }
                }
                Center = new XYZ(x, y, z);
            }
            catch
            {
                Center = new XYZ(x, y, z);
            }
            Volumn = Volumncount;
        }
        public void GetCenterPoinSkintSolids(List<Solid> solids, out XYZ Center, out double Volumn)
        {
            List<Solid> list1 = new List<Solid>();
            double Volumncount = 0;
            double x = 0;
            double y = 0;
            double z = 0;
            for (int i = 0; i < solids.Count; i++)
            {
                try
                {
                    if (solids[i] != null)
                    {
                        if (solids[i].ComputeCentroid() != null)
                        {
                            list1.Add(solids[i]);
                        }
                    }
                }
                catch
                {

                }
            }
            try
            {
                foreach (var item in list1)
                {
                    Volumncount = Volumncount + Math.Abs(item.Volume);
                }
                for (int i = 0; i < list1.Count; i++)
                {
                    if (list1[i] != null)
                    {
                        x = x + list1[i].ComputeCentroid().X * Math.Abs(list1[i].Volume) / Volumncount;
                        y = y + list1[i].ComputeCentroid().Y * Math.Abs(list1[i].Volume) / Volumncount;
                        z = z + list1[i].ComputeCentroid().Z * Math.Abs(list1[i].Volume) / Volumncount;
                    }
                }
                Center = new XYZ(x, y, z);
            }
            catch
            {
                Center = new XYZ(x, y, z);
            }
            Volumn = Volumncount;
        }
        public FamilySymbol Get3dsymbol(Document doc)
        {
            FamilySymbol symbol = null;
            var col = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_GenericModel).OfClass(typeof(FamilySymbol)).Cast<FamilySymbol>().ToList();
            foreach (var item in col)
            {
                if(item.Name.Contains("Spot3d"))
                {
                    symbol = item;
                    break;
                }
            }
            return symbol;
        }
        public void PlaceSymbol(Document doc, FamilySymbol familySymbol, XYZ point, FamilyInstance familyInstance)
        {
            Transform transform = familyInstance.GetTransform();
            XYZ xYZ = transform.BasisX;
            using (Transaction tran = new Transaction(doc, "Place Symbol"))
            {
                tran.Start();
                FamilyInstance center = doc.Create.NewFamilyInstance(point, familySymbol, xYZ, familyInstance, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                center.Pinned = true;
                tran.Commit();
            }
        }
        public void DrawingModelline(Document doc, XYZ point)
        {
            XYZ p1 = point + 1.2 * XYZ.BasisX;
            XYZ p2 = point + 1.2 * XYZ.BasisY;
            XYZ p3 = point + 1.2 * XYZ.BasisZ;
            Line line1 = Line.CreateBound(point, p1);
            Line line2 = Line.CreateBound(point, p2);
            Line line3 = Line.CreateBound(point, p3);
            using (Transaction tran = new Transaction(doc, "Draw Modelline"))
            {
                tran.Start();
                Plane pl1 = Plane.CreateByOriginAndBasis(point, XYZ.BasisX, XYZ.BasisY);
                Plane pl2 = Plane.CreateByOriginAndBasis(point, XYZ.BasisX, XYZ.BasisZ);
                Plane pl3 = Plane.CreateByOriginAndBasis(point, XYZ.BasisY, XYZ.BasisZ);
                SketchPlane sk1 = SketchPlane.Create(doc, pl1);
                SketchPlane sk2 = SketchPlane.Create(doc, pl2);
                SketchPlane sk3 = SketchPlane.Create(doc, pl3);
                ModelLine m1 = doc.Create.NewModelCurve(line1, sk1) as ModelLine;
                ModelLine m2 = doc.Create.NewModelCurve(line2, sk3) as ModelLine;
                ModelLine m3 = doc.Create.NewModelCurve(line3, sk2) as ModelLine;
                m1.Pinned = true;
                m2.Pinned = true;
                m3.Pinned = true;
                tran.Commit();
            }
        }
    }
}
