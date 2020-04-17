using System;
using System.Windows.Forms;
using a = Autodesk.Revit.DB;
using c = Autodesk.Revit.ApplicationServices;


namespace CEGVN.TVD
{
    public partial class FrmModify : Form
    {
        private AddParameterFamily _data;
        private a.Document _doc;
        private c.Application _app;
        public FrmModify(AddParameterFamily data, a.Document doc, c.Application app)
        {
            _data = data;
            _doc = doc;
            _app = app;
            InitializeComponent();
        }

        private void FrmModify_Load(object sender, EventArgs e)
        {

        }

        private void Btn_OK_Click(object sender, EventArgs e)
        {
            lbr ORI = new lbr();
            if (EMBEDSTANDARD.Checked)
            {
                ORI.RemoveShareParameter(_doc);
                ORI.EmbedStandard(_doc, _app);
            }
            if (EMBEDCUSTOM.Checked)
            {
                ORI.RemoveShareParameter(_doc);
                ORI.EmbedCustom(_doc, _app);
            }
            if (CIPSTANDARD.Checked)
            {
                ORI.RemoveShareParameter(_doc);
                ORI.CIPSTANDARD(_doc, _app);
            }
            if (CIPCUSTOM.Checked)
            {
                ORI.RemoveShareParameter(_doc);
                ORI.CIPCUSTOM(_doc, _app);
            }
            if (ERECTIONSTANDARD.Checked)
            {
                ORI.RemoveShareParameter(_doc);
                ORI.ERECTIONSTANDARD(_doc, _app);
            }
            if (ERECTIONCUSTOM.Checked)
            {
                ORI.RemoveShareParameter(_doc);
                ORI.ERECTIONCUSTOM(_doc, _app);
            }
            if (Keep_data.Checked)
            {
                ORI.RemoveShareParameterkeep(_doc);
            }
            Close();

        }

        private void EMBED_CUSTTOM_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
