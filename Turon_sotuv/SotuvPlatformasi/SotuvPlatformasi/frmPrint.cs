using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SotuvPlatformasi
{
    public partial class frmPrint : Form
    {
        List<SaleDetail> _list;
        public frmPrint(List<SaleDetail> list)
        {
            InitializeComponent();
            _list = list;
        }

        private void frmPrint_Load(object sender, EventArgs e)
        {
            basketReport1.SetDataSource(_list);
            basketReport1.SetParameterValue("pDate", Tul_qab_ucont.p_Date);
            basketReport1.SetParameterValue("pOmbor", Tul_qab_ucont.pOmbor);
            basketReport1.SetParameterValue("Pklient", Tul_qab_ucont.pKlient);
            basketReport1.SetParameterValue("pTel", Tul_qab_ucont.pTel);
            basketReport1.SetParameterValue("pQarz_Som", Tul_qab_ucont.pQarz_som);
            basketReport1.SetParameterValue("pQarz_Dollar", Tul_qab_ucont.pQarz_dollar);
            basketReport1.SetParameterValue("pResult_Qarz_Som", Tul_qab_ucont.pResult_Qarz_Som);
            basketReport1.SetParameterValue("pResult_Qarz_Dollar", Tul_qab_ucont.pResult_Qarz_Dollar);
            basketReport1.SetParameterValue("pJami_Som", Tul_qab_ucont.pJami_Som);
            basketReport1.SetParameterValue("pJami_Dollar", Tul_qab_ucont.pJami_Dollar);
            basketReport1.SetParameterValue("pSkidka_Som", Tul_qab_ucont.pSkidka_som);
            basketReport1.SetParameterValue("pSkidka_Dollar", Tul_qab_ucont.pSkidka_dollar);
            basketReport1.SetParameterValue("pTulov_Som", Tul_qab_ucont.pTulov_Som);
            basketReport1.SetParameterValue("pTulov_Dollar", Tul_qab_ucont.pTulov_Dollar);
            basketReport1.SetParameterValue("pSkidka_Foiz", Tul_qab_ucont.pSkidka_foiz);
            basketReport1.SetParameterValue("pShop_Id", Tul_qab_ucont.shopId);
            crystalReportViewer.ReportSource = basketReport1;
            crystalReportViewer.Refresh();
        }
    }
}
