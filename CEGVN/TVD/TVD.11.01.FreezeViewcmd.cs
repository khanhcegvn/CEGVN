#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
#endregion

namespace CEGVN.TVD
{
    [Transaction(TransactionMode.Manual)]
    public class FreezeViewcmd : IExternalCommand
    {
        public Document doc;
        private List<View> m_ViewList = new List<View>();
        private List<string> m_ViewListStr = new List<string>();
        public List<Element> listelement = new List<Element>();
        public Result Execute(
          ExternalCommandData commandData,
          ref string message,
          ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            doc = uidoc.Document;
            View viewexpr = null;
            string file;
            //ViewSheet SheetImpr = null;
            using (Transaction T = new Transaction(doc, "Create View"))
            {
                T.Start();
                viewexpr = ExportDWG(doc, doc.ActiveView, out file);
                T.Commit();
            }
            var dfo = SettingFreeze.Instance.GetFolderPath();
            DirectoryInfo d = new DirectoryInfo(dfo);
            FileInfo[] files = d.GetFiles();
            try
            {
                foreach (var item in files)
                {
                    File.Delete(file + "\\" + item);
                }
            }
            catch
            {

            }
            uidoc.ActiveView = viewexpr;
            return Result.Succeeded;
        }
        public View ExportDWG(Document document, View view, out string file)
        {
            bool exported = false;
            ElementId outid = ElementId.InvalidElementId;
            DWGExportOptions dwgOptions = new DWGExportOptions();
            dwgOptions.FileVersion = ACADVersion.R2007;
            dwgOptions.ExportOfSolids = SolidGeometry.Polymesh;
            View v = null;
            ICollection<ElementId> views = new List<ElementId>();
            views.Add(view.Id);
            var pathfile = SettingFreeze.Instance.GetFolderPath();
            exported = document.Export(pathfile, "Forzen view", views, dwgOptions);
            file = Path.Combine(pathfile, "Forzen view" + ".dwg");
            if (exported)
            {
                TaskDialog taskDialog = new TaskDialog("Freeze View");
                taskDialog.Id = "Freeze";
                taskDialog.Title = "Freeze Drawing";
                taskDialog.TitleAutoPrefix = true;
                taskDialog.MainIcon = TaskDialogIcon.TaskDialogIconInformation;
                taskDialog.AllowCancellation = true;
                taskDialog.MainInstruction = ("Select View Type :");
                taskDialog.AddCommandLink((TaskDialogCommandLinkId)1001, "Drafting View");
                taskDialog.AddCommandLink((TaskDialogCommandLinkId)1002, "Legend");
                taskDialog.CommonButtons = TaskDialogCommonButtons.Cancel;
                taskDialog.DefaultButton = ((TaskDialogResult)2);
                TaskDialogResult taskDialogResult = taskDialog.Show();
                var dwgimport = new DWGImportOptions();
                dwgimport.ColorMode = ImportColorMode.BlackAndWhite;
                if (taskDialogResult == TaskDialogResult.CommandLink2)
                {
                    v = GetLegend(document);
                }
                else if (taskDialogResult == TaskDialogResult.CommandLink1)
                {
                    v = CreateDrafting(document);
                }
                if (v != null)
                {
                    document.Import(file, dwgimport, v, out outid);
                }
                string strPost = "(Forzen)";
                string newname = this.ReplaceForbiddenSigns(doc.ActiveView.Name);
                string tempName = newname;
                if (v != null)
                {
                    int j = 1;
                    for (; ; )
                    {
                        try
                        {
                            v.Name = newname + strPost;
                            break;
                        }
                        catch
                        {
                            bool flag2 = j > 10;
                            if (flag2)
                            {
                                try
                                {
                                    v.Name += strPost;
                                }
                                catch
                                {
                                }
                                break;
                            }
                            newname = tempName + "-" + j.ToString();
                            j++;
                        }
                    }
                }
            }
            return v;
        }
        public void CheckCompare(Document doc)
        {
            FilteredElementCollector val = new FilteredElementCollector(doc);
            IList<Element> list = val.OfClass(typeof(View)).ToElements();
            foreach (Element item in list)
            {
                View val2 = item as View;
                if (val2 != null && val2.CanBePrinted && (int)val2.ViewType != 6)
                {
                    m_ViewList.Add(val2);
                    m_ViewListStr.Add(val2.Name);
                }
            }
            m_ViewListStr.Sort();
            m_ViewList.Sort(CompareViewsByName);
        }
        private static int CompareViewsByName(View arg1, View arg2)
        {
            return arg1.Name.CompareTo(arg2.Name);
        }

        View GetLegend(Document doc)
        {
            var legend = new FilteredElementCollector(doc).OfClass(typeof(View)).Cast<View>().FirstOrDefault(x => x.ViewType == ViewType.Legend);
            return doc.GetElement(legend.Duplicate(ViewDuplicateOption.Duplicate)) as View;
        }
        View CreateDrafting(Document doc)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector.OfClass(typeof(ViewFamilyType));
            ViewFamilyType viewFamilyType = collector.Cast<ViewFamilyType>().First(vft => vft.ViewFamily == ViewFamily.Drafting);
            ViewDrafting drafting = ViewDrafting.Create(doc, viewFamilyType.Id);
            return (drafting as View);
        }
        private string ReplaceForbiddenSigns(string name)
        {
            name = name.Replace("[", "");
            name = name.Replace("]", "");
            name = name.Replace("}", "");
            name = name.Replace("{", "");
            name = name.Replace("|", "");
            name = name.Replace("?", "");
            name = name.Replace("'", "");
            name = name.Replace(":", "");
            name = name.Replace("\\", "");
            name = name.Replace("~", "");
            name = name.Replace(">", "");
            name = name.Replace("<", "");
            name = name.Replace(";", "");
            return name;
        }
        public ViewSheet ReferenceSheet(Document doc, View view)
        {
            ViewSheet viewhseetrf = null;
            DateTime start = DateTime.Now;
            FilterableValueProvider provider = new ParameterValueProvider(new ElementId(BuiltInParameter.VIEW_SHEET_VIEWPORT_INFO));
            FilterRule rule = new FilterStringRule(provider, new FilterStringGreater(), string.Empty, false);
            ElementParameterFilter epf = new ElementParameterFilter(rule, false);
            FilteredElementCollector viewCol = new FilteredElementCollector(doc).WherePasses(epf);
            viewCol.OfClass(typeof(ViewSheet));
            StringBuilder sb = new StringBuilder();
            foreach (ViewSheet v in viewCol)
            {
                try
                {
                    ISet<ElementId> col = v.GetAllPlacedViews();
                    foreach (var fg in col)
                    {
                        if (fg == view.Id)
                        {
                            sb.AppendLine("View: " + v.Name);
                            viewhseetrf = v;
                        }
                    }

                }
                catch { }
            }
            return viewhseetrf;
        }
    }
    public class SettingFreeze
    {
        private static SettingFreeze _instance;
        private SettingFreeze()
        {

        }
        public static SettingFreeze Instance => _instance ?? (_instance = new SettingFreeze());
        public string TypeText { get; set; }
        public string GetFolderPath()
        {
            string folderPath = SettingExtension.GetSettingPath() + "\\TVD.Freeze";
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            return folderPath;
        }
    }
}
