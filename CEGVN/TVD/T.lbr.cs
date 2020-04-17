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
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.Set(p7, "EMBED STANDARD");
                    familyManager.Set(p8, int.Parse("100"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);
                }

            }
            tran.Commit();

        }
        public void EmbedStandardkeepdata(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.Set(p7, "EMBED STANDARD");
                    familyManager.Set(p8, int.Parse("100"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);
                }

            }
            tran.Commit();

        }
        public void EmbedStandard_finish(Document doc, c.Application app)
        {

            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            FamilyParameter p10 = doc.FamilyManager.AddParameter("desc_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p11 = doc.FamilyManager.AddParameter("desc_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p12 = doc.FamilyManager.AddParameter("desc_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p13 = doc.FamilyManager.AddParameter("desc_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p14 = doc.FamilyManager.AddParameter("desc_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p15 = doc.FamilyManager.AddParameter("desc_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p16 = doc.FamilyManager.AddParameter("finish_check", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p17 = doc.FamilyManager.AddParameter("manufacturer_plant_description_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p18 = doc.FamilyManager.AddParameter("manufacturer_plant_description_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p19 = doc.FamilyManager.AddParameter("manufacturer_plant_description_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p20 = doc.FamilyManager.AddParameter("manufacturer_plant_description_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p21 = doc.FamilyManager.AddParameter("manufacturer_plant_description_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p22 = doc.FamilyManager.AddParameter("manufacturer_plant_description_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p23 = doc.FamilyManager.AddParameter("manufacturer_plant_id_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p24 = doc.FamilyManager.AddParameter("manufacturer_plant_id_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p25 = doc.FamilyManager.AddParameter("manufacturer_plant_id_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p26 = doc.FamilyManager.AddParameter("manufacturer_plant_id_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p27 = doc.FamilyManager.AddParameter("manufacturer_plant_id_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p28 = doc.FamilyManager.AddParameter("manufacturer_plant_id_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p29 = doc.FamilyManager.AddParameter("mark_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p30 = doc.FamilyManager.AddParameter("mark_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p31 = doc.FamilyManager.AddParameter("mark_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p32 = doc.FamilyManager.AddParameter("mark_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p33 = doc.FamilyManager.AddParameter("mark_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p34 = doc.FamilyManager.AddParameter("mark_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.SetFormula(p1, "if(FINISH_GALVANIZED, mark_g, (if(FINISH_PAINTED, mark_pt, (if(FINISH_PRIMED, mark_pr, (if(FINISH_OTHER, mark_o, (if(FINISH_STAINLESS, mark_s, mark_b)))))))))");
                    familyManager.SetFormula(p2, "if(FINISH_GALVANIZED, desc_g, (if(FINISH_PAINTED, desc_pt, (if(FINISH_PRIMED, desc_pr, (if(FINISH_OTHER, desc_o, (if(FINISH_STAINLESS, desc_s, desc_b)))))))))");
                    familyManager.SetFormula(p3, "if(FINISH_GALVANIZED, mark_g, (if(FINISH_PAINTED, mark_pt, (if(FINISH_PRIMED, mark_pr, (if(FINISH_OTHER, mark_o, (if(FINISH_STAINLESS, mark_s, mark_b)))))))))");
                    familyManager.SetFormula(p4, "if(FINISH_GALVANIZED, manufacturer_plant_description_g, (if(FINISH_PAINTED, manufacturer_plant_description_pt, (if(FINISH_PRIMED, manufacturer_plant_description_pr, (if(FINISH_OTHER, manufacturer_plant_description_o, (if(FINISH_STAINLESS, manufacturer_plant_description_s, manufacturer_plant_description_b)))))))))");
                    familyManager.SetFormula(p5, "if(FINISH_GALVANIZED, manufacturer_plant_id_g, (if(FINISH_PAINTED, manufacturer_plant_id_pt, (if(FINISH_PRIMED, manufacturer_plant_id_pr, (if(FINISH_OTHER, manufacturer_plant_id_o, (if(FINISH_STAINLESS, manufacturer_plant_id_s, manufacturer_plant_id_b)))))))))");
                    familyManager.Set(p7, "EMBED STANDARD");
                    familyManager.Set(p8, int.Parse("100"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);

                }

            }
            tran.Commit();
        }
        public void EmbedCustom(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.Set(p7, "EMBED CUSTOM");
                    familyManager.Set(p8, int.Parse("102"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);
                }

            }
            tran.Commit();

        }

        public void EmbedCustom_finish(Document doc, c.Application app)
        {

            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            FamilyParameter p10 = doc.FamilyManager.AddParameter("desc_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p11 = doc.FamilyManager.AddParameter("desc_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p12 = doc.FamilyManager.AddParameter("desc_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p13 = doc.FamilyManager.AddParameter("desc_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p14 = doc.FamilyManager.AddParameter("desc_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p15 = doc.FamilyManager.AddParameter("desc_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p16 = doc.FamilyManager.AddParameter("finish_check", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p17 = doc.FamilyManager.AddParameter("manufacturer_plant_description_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p18 = doc.FamilyManager.AddParameter("manufacturer_plant_description_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p19 = doc.FamilyManager.AddParameter("manufacturer_plant_description_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p20 = doc.FamilyManager.AddParameter("manufacturer_plant_description_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p21 = doc.FamilyManager.AddParameter("manufacturer_plant_description_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p22 = doc.FamilyManager.AddParameter("manufacturer_plant_description_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p23 = doc.FamilyManager.AddParameter("manufacturer_plant_id_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p24 = doc.FamilyManager.AddParameter("manufacturer_plant_id_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p25 = doc.FamilyManager.AddParameter("manufacturer_plant_id_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p26 = doc.FamilyManager.AddParameter("manufacturer_plant_id_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p27 = doc.FamilyManager.AddParameter("manufacturer_plant_id_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p28 = doc.FamilyManager.AddParameter("manufacturer_plant_id_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p29 = doc.FamilyManager.AddParameter("mark_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p30 = doc.FamilyManager.AddParameter("mark_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p31 = doc.FamilyManager.AddParameter("mark_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p32 = doc.FamilyManager.AddParameter("mark_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p33 = doc.FamilyManager.AddParameter("mark_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p34 = doc.FamilyManager.AddParameter("mark_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.SetFormula(p1, "if(FINISH_GALVANIZED, mark_g, (if(FINISH_PAINTED, mark_pt, (if(FINISH_PRIMED, mark_pr, (if(FINISH_OTHER, mark_o, (if(FINISH_STAINLESS, mark_s, mark_b)))))))))");
                    familyManager.SetFormula(p2, "if(FINISH_GALVANIZED, desc_g, (if(FINISH_PAINTED, desc_pt, (if(FINISH_PRIMED, desc_pr, (if(FINISH_OTHER, desc_o, (if(FINISH_STAINLESS, desc_s, desc_b)))))))))");
                    familyManager.SetFormula(p3, "if(FINISH_GALVANIZED, mark_g, (if(FINISH_PAINTED, mark_pt, (if(FINISH_PRIMED, mark_pr, (if(FINISH_OTHER, mark_o, (if(FINISH_STAINLESS, mark_s, mark_b)))))))))");
                    familyManager.SetFormula(p4, "if(FINISH_GALVANIZED, manufacturer_plant_description_g, (if(FINISH_PAINTED, manufacturer_plant_description_pt, (if(FINISH_PRIMED, manufacturer_plant_description_pr, (if(FINISH_OTHER, manufacturer_plant_description_o, (if(FINISH_STAINLESS, manufacturer_plant_description_s, manufacturer_plant_description_b)))))))))");
                    familyManager.SetFormula(p5, "if(FINISH_GALVANIZED, manufacturer_plant_id_g, (if(FINISH_PAINTED, manufacturer_plant_id_pt, (if(FINISH_PRIMED, manufacturer_plant_id_pr, (if(FINISH_OTHER, manufacturer_plant_id_o, (if(FINISH_STAINLESS, manufacturer_plant_id_s, manufacturer_plant_id_b)))))))))");
                    familyManager.Set(p7, "EMBED CUSTOM");
                    familyManager.Set(p8, int.Parse("102"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);

                }

            }
            tran.Commit();
        }

        public void CIPCUSTOM(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.Set(p7, "CIP CUSTOM");
                    familyManager.Set(p8, int.Parse("301"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);
                }

            }
            tran.Commit();

        }

        public void CIPSTANDARD(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.Set(p7, "CIP STANDARD");
                    familyManager.Set(p8, int.Parse("300"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);
                }

            }
            tran.Commit();

        }
        public void ERCTIONSTANDARD_finish(Document doc, c.Application app)
        {

            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            FamilyParameter p10 = doc.FamilyManager.AddParameter("desc_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p11 = doc.FamilyManager.AddParameter("desc_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p12 = doc.FamilyManager.AddParameter("desc_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p13 = doc.FamilyManager.AddParameter("desc_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p14 = doc.FamilyManager.AddParameter("desc_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p15 = doc.FamilyManager.AddParameter("desc_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p16 = doc.FamilyManager.AddParameter("finish_check", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p17 = doc.FamilyManager.AddParameter("manufacturer_plant_description_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p18 = doc.FamilyManager.AddParameter("manufacturer_plant_description_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p19 = doc.FamilyManager.AddParameter("manufacturer_plant_description_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p20 = doc.FamilyManager.AddParameter("manufacturer_plant_description_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p21 = doc.FamilyManager.AddParameter("manufacturer_plant_description_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p22 = doc.FamilyManager.AddParameter("manufacturer_plant_description_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p23 = doc.FamilyManager.AddParameter("manufacturer_plant_id_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p24 = doc.FamilyManager.AddParameter("manufacturer_plant_id_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p25 = doc.FamilyManager.AddParameter("manufacturer_plant_id_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p26 = doc.FamilyManager.AddParameter("manufacturer_plant_id_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p27 = doc.FamilyManager.AddParameter("manufacturer_plant_id_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p28 = doc.FamilyManager.AddParameter("manufacturer_plant_id_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p29 = doc.FamilyManager.AddParameter("mark_b", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p30 = doc.FamilyManager.AddParameter("mark_g", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p31 = doc.FamilyManager.AddParameter("mark_o", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p32 = doc.FamilyManager.AddParameter("mark_pr", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p33 = doc.FamilyManager.AddParameter("mark_pt", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            FamilyParameter p34 = doc.FamilyManager.AddParameter("mark_s", BuiltInParameterGroup.INVALID, ParameterType.Text, true);
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.SetFormula(p1, "if(FINISH_GALVANIZED, mark_g, (if(FINISH_PAINTED, mark_pt, (if(FINISH_PRIMED, mark_pr, (if(FINISH_OTHER, mark_o, (if(FINISH_STAINLESS, mark_s, mark_b)))))))))");
                    familyManager.SetFormula(p2, "if(FINISH_GALVANIZED, desc_g, (if(FINISH_PAINTED, desc_pt, (if(FINISH_PRIMED, desc_pr, (if(FINISH_OTHER, desc_o, (if(FINISH_STAINLESS, desc_s, desc_b)))))))))");
                    familyManager.SetFormula(p3, "if(FINISH_GALVANIZED, mark_g, (if(FINISH_PAINTED, mark_pt, (if(FINISH_PRIMED, mark_pr, (if(FINISH_OTHER, mark_o, (if(FINISH_STAINLESS, mark_s, mark_b)))))))))");
                    familyManager.SetFormula(p4, "if(FINISH_GALVANIZED, manufacturer_plant_description_g, (if(FINISH_PAINTED, manufacturer_plant_description_pt, (if(FINISH_PRIMED, manufacturer_plant_description_pr, (if(FINISH_OTHER, manufacturer_plant_description_o, (if(FINISH_STAINLESS, manufacturer_plant_description_s, manufacturer_plant_description_b)))))))))");
                    familyManager.SetFormula(p5, "if(FINISH_GALVANIZED, manufacturer_plant_id_g, (if(FINISH_PAINTED, manufacturer_plant_id_pt, (if(FINISH_PRIMED, manufacturer_plant_id_pr, (if(FINISH_OTHER, manufacturer_plant_id_o, (if(FINISH_STAINLESS, manufacturer_plant_id_s, manufacturer_plant_id_b)))))))))");
                    familyManager.Set(p7, "ERECTION STANDARD");
                    familyManager.Set(p8, int.Parse("200"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);

                }

            }
            tran.Commit();
        }
        public void ERECTIONSTANDARD(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.Set(p7, "ERECTION STANDARD");
                    familyManager.Set(p8, int.Parse("200"));
                }
                if (i.Name == "DIMENSIONS_GENERAL")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "DIM_HEIGHT", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "DIM_THICKNESS", BuiltInParameterGroup.PG_GEOMETRY, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "DIM_WIDTH", BuiltInParameterGroup.PG_GEOMETRY, true);
                }
                if (i.Name == "FINISHES")
                {
                    FamilyParameter p1 = _AddParameter(doc, i, "FINISH_BLACK", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "FINISH_GALVANIZED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "FINISH_OTHER", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "FINISH_PAINTED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "FINISH_PRIMED", BuiltInParameterGroup.PG_MATERIALS, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "FINISH_STAINLESS", BuiltInParameterGroup.PG_MATERIALS, true);
                }

            }
            tran.Commit();

        }
        public void CONNECTION(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.Set(p7, "CONNECTION");
                }

            }
            tran.Commit();

        }
        public void ERECTIONCUSTOM(Document doc, c.Application app)
        {
            FamilyManager familyManager = doc.FamilyManager;
            string origfile = app.SharedParametersFilename;
            string tempFile = @"C:\ProgramData\Autodesk\ApplicationPlugins\TVD\Shared_Params_2015_v01.txt";
            Transaction tran = new Transaction(doc, "add parameter");
            tran.Start();
            app.SharedParametersFilename = tempFile;
            DefinitionFile shareParameterfile = app.OpenSharedParameterFile();
            foreach (DefinitionGroup i in shareParameterfile.Groups)
            {
                if (i.Name == "IDENTITY")
                {
                    //group data
                    FamilyParameter p1 = _AddParameter(doc, i, "CONTROL_MARK", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p2 = _AddParameter(doc, i, "IDENTITY_COMMENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p3 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p4 = _AddParameter(doc, i, "IDENTITY_DESCRIPTION_SHORT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p5 = _AddParameter(doc, i, "MANUFACTURER_PLANT_DESCRIPTION", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p6 = _AddParameter(doc, i, "MANUFACTURER_PLANT_ID", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p7 = _AddParameter(doc, i, "MANUFACTURE_COMPONENT", BuiltInParameterGroup.PG_DATA, true);
                    FamilyParameter p8 = _AddParameter(doc, i, "SORTING_ORDER", BuiltInParameterGroup.PG_DATA, true);
                    familyManager.Set(p7, "ERECTION CUSTOM");
                    familyManager.Set(p8, int.Parse("201"));
                }
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
