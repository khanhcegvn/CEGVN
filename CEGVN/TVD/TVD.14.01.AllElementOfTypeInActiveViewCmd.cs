#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Linq;
using Autodesk.Revit.UI.Selection;
using CEGVN.TVD.Extensions;
#endregion

namespace CEGVN.TVD
{
    [Transaction(TransactionMode.Manual)]
    public class AllElementOfTypeInActiveViewCmd : IExternalCommand
    {
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;

            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            try
            {
                var sel = uidoc.Selection;
                var selectedElements = doc.IdsToElements(sel.GetElementIds());
                var eles = new List<Element>();
                if (selectedElements.Count > 0)
                {
                    eles = selectedElements;
                }
                else
                {
                    eles = (from rf in (sel.PickObjects(ObjectType.Element, "Select Some Type of Element!"))
                            select doc.GetElement(rf)).ToList();
                }
                using (var tx = new Transaction(doc))
                {
                    var ids = new List<ElementId>();
                    tx.Start("Select All Element Of Type");
                    foreach (var ele in eles)
                    {
                        var fi = ele as FamilyInstance;
                        if (fi == null) continue;
                        doc.AllFamilyInstanceOfTypeInActiveView(fi.Symbol.Name).Where(x=>x.Symbol.Id.IntegerValue==fi.Symbol.Id.IntegerValue).ToList().ForEach(x => ids.Add(x.Id));
                    }
                    sel.SetElementIds(ids);
                    tx.Commit();
                }
                return Result.Succeeded;
            }
            catch (Exception)
            {
                return Result.Cancelled;
            }
           
        }

    }
}
