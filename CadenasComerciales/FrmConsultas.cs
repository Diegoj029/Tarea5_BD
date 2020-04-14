using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadenasComerciales {
    public partial class FrmConsultas : Form {
        //Variables locales:
        GestorBD.GestorBD gestorBD;
        int tipoCons;
        string cadSql;
        DataSet dsCadena = new DataSet(), dsSucursal = new DataSet(), dsProducto = new DataSet(), dsCliente = new DataSet();

        public FrmConsultas(int tipoCons, GestorBD.GestorBD gestorBD) {
            InitializeComponent();
            //Guardar el GestorBD y el tipo de consulta en una variable local.
            this.gestorBD = gestorBD;
            this.tipoCons = tipoCons;
        }

        //Acciones iniciales al cargar la forma.
        private void FrmConsultas_Load(object sender, EventArgs e) {
            //Evaluar que botón creó la forma y hacer una consulta acorde.
            //El resultado se almacena en el DataSet correspondiente y se carga en el DataGridView
            switch (tipoCons) {
                case 0:
                    cadSql = "select * from Cadena";
                    gestorBD.consBD(cadSql, dsCadena, "Cadena");
                    dtgGeneral.DataSource = dsCadena.Tables["Cadena"];
                    break;
                case 1:
                    cadSql = "select NomCad, IdSucursal, NomSuc, DomSuc from Cadena c, Sucursal s " +
                        "where s.RFC = c.RFC";
                    gestorBD.consBD(cadSql, dsSucursal, "Sucursal");
                    dtgGeneral.DataSource = dsSucursal.Tables["Sucursal"];
                    break;
                case 2:
                    cadSql = "select p.IdProd, NomProd, Marca, NomCad as 'De venta en:', PrecioUni as 'Precio Unitario ($)'  " +
                        "from Producto p, Cadena c, CadenaProducto cp " +
                        "where p.IdProd = cp.IdProd and c.RFC = cp.RFC";
                    gestorBD.consBD(cadSql, dsProducto, "Producto");
                    dtgGeneral.DataSource = dsProducto.Tables["Producto"];
                    break;
                case 3:
                    cadSql = "select * from Cliente";
                    gestorBD.consBD(cadSql, dsCliente, "Cliente");
                    dtgGeneral.DataSource = dsCliente.Tables["Cliente"];
                    break;
            }
        }
    }
}
