using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CEGVN.TVD
{
    public partial class FrmFindGravity : Form
    {
        public bool Structuralframming = false;
        public bool Allelement = false;
        public FrmFindGravity()
        {
            InitializeComponent();
        }

        private void FrmFindGravity_Load(object sender, EventArgs e)
        {

        }

        private void btn_Ok_Click(object sender, EventArgs e)
        {
            if(Rdt_Structuralframming.Checked)
            {
                Structuralframming = true;
            }
            if(Rdt_All.Checked)
            {
                Allelement = true;
            }
            Close();
        }
    }
}
