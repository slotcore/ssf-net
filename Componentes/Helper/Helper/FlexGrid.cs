using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public class FlexGrid
    {

        //#region FlexColumnaCombo
        ////*********************************************************************************************
        ////** NOMBRE      : FlexColumnaCombo
        ////** TIPO        : Metodo
        ////** DESCRIPCION : Carga un ComboList en una columna especificada del contro FlexGrid
        ////** PARAMETROS  : 
        ////**               TIPO           |  NOMBRE            | DESCRIPCION
        ////**               ----------------------------------------------------------------------------
        ////**               C1FlexGrid     |  objControl        | Control FlexGrid al que se le cargara el ComboList
        ////**               DataTable      |  dtDatos           | DataTable con los datos que se cargaran al ComboList
        ////**               string         |  StrCampoMostrar   | Nombre del campo que se utilizara para cargar los datos
        ////**               int            |  intColumnaNumero  | Numero de columna don de se creara el ComboList
        ////**
        ////** SINTAXIS    : 
        ////**               
        ////**               
        ////** 
        ////** DEVUELVE    : 
        ////*********************************************************************************************
        //public void FlexColumnaCombo(C1.Win.C1FlexGrid.C1FlexGrid objControl, DataTable dtDatos, string StrCampoMostrar, int intColumnaNumero)
        //{
        //    int intFila = 0;
        //    string strCadena = "";

        //    if (dtDatos.Rows.Count != 0)                              // SI EL NUMERO DE REGISTRO ES DIFERENTE A 0 CONTINUA CON LA CARGA DE DATOS
        //    {
        //        // GUARDAMOS EN EL PRIMER REGISTRO
        //        strCadena = dtDatos.Rows[intFila][StrCampoMostrar].ToString();

        //        for (intFila = 1; intFila <= dtDatos.Rows.Count - 1; intFila++)
        //        {
        //            strCadena = strCadena + "|" + dtDatos.Rows[intFila][StrCampoMostrar].ToString();
        //        }
        //    }
        //    else 
        //    {
        //        strCadena = "";
        //    }

        //    objControl.Cols[intColumnaNumero].ComboList = strCadena;   // CARGAMOS LOS DATOS EN EL COMBOLIST
        //}
        //#endregion FlexColumnaCombo

        //public double FlexSumarCol(C1.Win.C1FlexGrid.C1FlexGrid objControl, int intCol, int intRowIni, int intRowfin)
        //{
        //    double douResult = 0;
        //    double douValor = 0;
        //    int intFila = 0;

        //    for (intFila = intRowIni; intFila <= intRowfin; intFila++)
        //    {
        //        douValor = Convert.ToDouble(objControl.GetData(intFila, intCol));
        //        douResult = douResult + douValor;
        //    }

        //    return douResult;
        //}



    //    Function F_FGSumarCol(ByVal xFg As C1FlexGrid, ByVal xCol As Integer, ByVal xRowIni As Integer, ByVal xRowFin As Integer) As Double
    //    Dim A As Integer
    //    Dim xTotal As Double

    //    For A = xRowIni To xRowFin
    //        xTotal = xTotal + F_NulosN(xFg.GetDataDisplay(A, xCol))
    //    Next
    //    F_FGSumarCol = xTotal
    //End Function

    //Function F_FGSumarRow(ByVal xFg As C1FlexGrid, ByVal xRow As Integer, ByVal xColIni As Integer, ByVal xColFin As Integer) As Double
    //    Dim A As Integer
    //    Dim xTotal As Double

    //    For A = xColIni To xColFin
    //        xTotal = xTotal + F_NulosN(xFg.GetDataDisplay(xRow, A))
    //    Next
    //    F_FGSumarRow = xTotal
    //End Function
    }
}
