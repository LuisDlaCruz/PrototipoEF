using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Odbc;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaVista
{
    public partial class frmDiasTrabajados : Form
    {
        private string usuario = "";
        public frmDiasTrabajados()
        {
            InitializeComponent();
            CenterToScreen();
            //Parametrización navegador
            /*arreglo de textboxs*/
            TextBox[] alias = navegador1.funAsignandoTexts(this);
            //definicion de textboxs, tabla y BD
            navegador1.funAsignarAliasVista(alias, "producto", "examenfinal");
            navegador1.funAsignarSalidadVista(this);

            //definicion del campo de estado / para dar de bajars
            navegador1.campoEstado = "estado";

            //el id de aplicacion para ayudas y reportes, quedara obsoleto en la nueva version
            navegador1.idAplicacion = "5004";//en la nueva se usara el nombre

            //-- Estos atributos no cambian
            //inicio de elementos para ejecutar la ayuda
            navegador1.tablaAyuda = "Aplicacion";
            navegador1.campoAyuda = "pkId";
            //fin de elementos para ejecutar la ayuda

            // Inicio datos para ejecurar reportes
            navegador1.funReportesVista("ruta", "idAplicacion", "Reporte");
            // Final datos para ejecutar reportes
            //-- fin de atributos no cambiantes

            //enviar el data grid view para la navegación
            navegador1.pideGrid(this.dgvProductos);
            //llena el data grid
            navegador1.llenaTabla();
            //pide la referencia a la form para poder cerrarla
            navegador1.pedirRef(this);
            //FALTA USUARIO Y ACTUALIZAR PERSMISOS

            //Para las combo boxes
            //Parametros: La combo box, la tabla, el id de la tabla, el valor a mostrar, y el campo de estado
            
            navegador1.funLlenarComboControl(cmbLinea, "linea", "idLinea", "nombre",  "estado");

        }
        //Actualización de permisos que se debe realizar después de tener la variable usuario ingresada
        public void funActualizarUsuario(string user)
        {
            //colocamos el nombre del usuario
            usuario = user;
            //y ya con el nombre del usuario podemos actualizar los permisos
            navegador1.usuario = user;
            navegador1.aplicacion = "Mantenimiento Dias Trabajados";//nombre de la app en seguridad
            navegador1.funActualizarPermisos();//actualizamos los permisos
            navegador1.idmodulo = "5";//7 es contabilidad
            navegador1.funReportesVista("ruta", "idAplicacion", "Reporte");

        }

        private void cmbLinea_SelectedIndexChanged(object sender, EventArgs e)
        {
            navegador1.funComboTextboxVista(cmbLinea, txtLinea);
        }
        private void txtLinea_TextChanged(object sender, EventArgs e)
        {
            navegador1.funTextboxComboVista(cmbLinea, txtLinea);
        }

        private void dvgCuentas_SelectionChanged(object sender, EventArgs e)
        {
            navegador1.funSeleccionarDTVista(dgvProductos);
        }

        private void rdbActivo_CheckedChanged(object sender, EventArgs e)
        {
            navegador1.funCambioEstatusRBVista(txtEstado, rdbActivo, "A");
        }

        private void rdbInactivo_CheckedChanged(object sender, EventArgs e)
        {
            navegador1.funCambioEstatusRBVista(txtEstado, rdbInactivo, "I");
        }


        private void txtEstatus_TextChanged(object sender, EventArgs e)
        {
            navegador1.funSetearRBVista(rdbActivo, rdbInactivo, txtEstado);
        }

      
    }
}
