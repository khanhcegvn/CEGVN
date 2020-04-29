using Autodesk.Revit.DB;
using System.Collections.Generic;
using c = Autodesk.Revit.ApplicationServices;


namespace CEGVN.TVD
{
    public class lbr
    {
        public void RemoveShareParameter(Document doc)
        {
            IList<FamilyParameter> list = new List<FamilyParameter>();
            FamilyManager familyManager = doc.FamilyManager;
            list = familyManager.GetParameters();
            Transaction newtran = new Transaction(doc, "Remove Parameter");
            newtran.Start();
            foreach (var i in list)
            {
                if (!i.Definition.ParameterGroup.ToString().Equals("PG_CONSTRAINTS") && !i.Definition.ParameterGroup.ToString().Equals("PG_IDENTITY_DATA"))
                {
                    familyManager.RemoveParameter(i);
                }
            }
            newtran.Commit();
        }
        public void RemoveShareParameterkeep(Document doc)
        {
            IList<FamilyParameter> list = new List<FamilyParameter>();
            FamilyManager familyManager = doc.FamilyManager;
            list = familyManager.GetParameters();
            Transaction newtran = new Transaction(doc, "Remove Parameter");
            newtran.Start();
            foreach (var i in list)
            {
                if (!i.Definition.ParameterGroup.ToString().Equals("PG_CONSTRAINTS") && !i.Definition.ParameterGroup.ToString().Equals("PG_IDENTITY_DATA") && !i.Definition.ParameterGroup.ToString().Equals("PG_GEOMETRY") && !i.Definition.ParameterGroup.ToString().Equals("PG_DATA"))
                {
                    familyManager.RemoveParameter(i);
                }
            }
            newtran.Commit();
        }
        public FamilyParameter _AddParameter(Document doc, DefinitionGroup i, string Nameitem, BuiltInParameterGroup builtInParameterGroup, bool po)
        {
            FamilyManager familyManager = doc.FamilyManager;
            ExternalDefinition p = i.Definitions.get_Item(Nameitem) as ExternalDefinition;
            FamilyParameter pa = familyManager.AddParameter(p, builtInParameterGroup, po);
            return pa;
        }
        public void EmbedStandard(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\Program Files\Autodesk\CEGCustomMenu\Shared_Params_2015_v01.json";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, false);
                    familyManager.Set(p7, "EMBED STANDARD");
                    familyManager.Set(p8, int.Parse("100"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter _p3 = _AddParameter(doc, i, "DBA_Length", BuiltInParameterGroup.PG_GEOMETRY, false);
                }
                //if (i.Name == "FINISHES")
                //{
                //    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, false);
                //}

            }
            tran.Commit();

        }
        public void EmbedStandardkeepdata(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\Program Files\Autodesk\CEGCustomMenu\Shared_Params_2015_v01.json";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, false);
                    familyManager.Set(p7, "EMBED STANDARD");
                    familyManager.Set(p8, int.Parse("100"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter _p3 = _AddParameter(doc, i, "DBA_Length", BuiltInParameterGroup.PG_GEOMETRY, false);
                }
                //if (i.Name == "FINISHES")
                //{
                //    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, false);
                //}

            }
            tran.Commit();

        }
        public void EmbedCustom(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\Program Files\Autodesk\CEGCustomMenu\Shared_Params_2015_v01.json";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, false);
                    familyManager.Set(p7, "EMBED CUSTOM");
                    familyManager.Set(p8, int.Parse("102"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter _p3 = _AddParameter(doc, i, "DBA_Length", BuiltInParameterGroup.PG_GEOMETRY, false);
                }
                //if (i.Name == "FINISHES")
                //{
                //    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, false);
                //}

            }
            tran.Commit();

        }

        public void CIPCUSTOM(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\Program Files\Autodesk\CEGCustomMenu\Shared_Params_2015_v01.json";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, false);
                    familyManager.Set(p7, "CIP CUSTOM");
                    familyManager.Set(p8, int.Parse("301"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter _p3 = _AddParameter(doc, i, "DBA_Length", BuiltInParameterGroup.PG_GEOMETRY, false);
                }
                //if (i.Name == "FINISHES")
                //{
                //    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, false);
                //}

            }
            tran.Commit();

        }

        public void CIPSTANDARD(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\Program Files\Autodesk\CEGCustomMenu\Shared_Params_2015_v01.json";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, false);
                    familyManager.Set(p7, "CIP STANDARD");
                    familyManager.Set(p8, int.Parse("300"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter _p3 = _AddParameter(doc, i, "DBA_Length", BuiltInParameterGroup.PG_GEOMETRY, false);
                }
                //if (i.Name == "FINISHES")
                //{
                //    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, false);
                //}

            }
            tran.Commit();

        }
        public void ERECTIONSTANDARD(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\Program Files\Autodesk\CEGCustomMenu\Shared_Params_2015_v01.json";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, false);
                    familyManager.Set(p7, "ERECTION STANDARD");
                    familyManager.Set(p8, int.Parse("200"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter _p3 = _AddParameter(doc, i, "DBA_Length", BuiltInParameterGroup.PG_GEOMETRY, false);
                }
                //if (i.Name == "FINISHES")
                //{
                //    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, false);
                //}

            }
            tran.Commit();

        }
        public void CONNECTION(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\Program Files\Autodesk\CEGCustomMenu\Shared_Params_2015_v01.json";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, false);
                    familyManager.Set(p7, "CONNECTION");
                }

            }
            tran.Commit();

        }
        public void ERECTIONCUSTOM(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\Program Files\Autodesk\CEGCustomMenu\Shared_Params_2015_v01.json";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, false);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, false);
                    familyManager.Set(p7, "ERECTION CUSTOM");
                    familyManager.Set(p8, int.Parse("201"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, false);
                    FamilyParameter _p3 = _AddParameter(doc, i, "DBA_Length", BuiltInParameterGroup.PG_GEOMETRY, false);
                }
                //if (i.Name == "FINISHES")
                //{
                //    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, false);
                //    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, false);
                //}
            }
            tran.Commit();

        }
        public void Changetypefamily(Document doc)
        {
            IList<Category> list = new List<Category>();
            Family f = doc.OwnerFamily;
            var f2 = f.GetType();
            BuiltInParameter _bip = BuiltInParameter.OMNICLASS_CODE;
            Category category = f.FamilyCategory;
            var t = f.StructuralFamilyNameKey;

        }
    }

}
