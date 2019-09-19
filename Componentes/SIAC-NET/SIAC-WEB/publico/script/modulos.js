/// <reference path="../../almacen/frmlistaitemsmantenimiento.aspx" />
/// <reference path="../../almacen/frmlistaitemsmantenimiento.aspx" />
// ************************************************************************************************
// Nombre       : genera_tabla
// Descripcion  : Genera tabla HTML dinamicamente
// Parametros   : Parametro         |  Descripcion 
//                =====================================================
//                nombrecontenedor  | Indica el nombre del contenedor donde sera mostrada la tabla 
//                                  | puede ser un DIV u otra TABLA
//                nombretabla       | Indica el id que se le asignara a la tabla 
//
// Creada       : 25/06/2015
// Autor        : epollongo
// ************************************************************************************************

function genera_t(nombrecontenedor, nombretabla) {
    //// Obtener la referencia del elemento body
    //var body = document.getElementsByTagName("body")[0];

    //// Crea un elemento <table> y un elemento <tbody>
    //var tabla = document.createElement("table");
    //var tblBody = document.createElement("tbody");

    //// Crea las celdas
    //for (var i = 0; i < 2; i++) {
    //    // Crea las hileras de la tabla
    //    var hilera = document.createElement("tr");

    //    for (var j = 0; j < 2; j++) {
    //        // Crea un elemento <td> y un nodo de texto, haz que el nodo de
    //        // texto sea el contenido de <td>, ubica el elemento <td> al final
    //        // de la hilera de la tabla
    //        var celda = document.createElement("td");
    //        var textoCelda = document.createTextNode("celda en la hilera " + i + ", columna " + j);
    //        celda.appendChild(textoCelda);
    //        hilera.appendChild(celda);
    //    }

    //    // agrega la hilera al final de la tabla (al final del elemento tblbody)
    //    tblBody.appendChild(hilera);
    //}

    //// posiciona el <tbody> debajo del elemento <table>
    //tabla.appendChild(tblBody);
    //// appends <table> into <body>
    //body.appendChild(tabla);
    //// modifica el atributo "border" de la tabla y lo fija a "2";
    //tabla.setAttribute("border", "2");

    //var resultArr = JSON.parse(datos);
    //alert(resultArr[0].name);
    alert("hola " + nombrecontenedor +" "+ nombretabla);

    var tabla = '<table id="' + nombretabla + '">';

    //var tabla = '<table id="tabla">';
    tabla += '<caption>Lista de Items</caption>';
    tabla += '<thead>';
    tabla += '<tr>';
    tabla += '<th>Codigo</th>';
    tabla += '<th>Descripcion</th>';

    tabla += '</tr>';
    tabla += '</thead>';

    tabla += '<tr class="color">';
    tabla += '<td>11</td>';
    tabla += '<td>Daniel</td>';
    tabla += '</tr>';
    tabla += '<tr class="coloralter">';
    tabla += '<td>12</td>';
    tabla += '<td>Carlos</td>';
    tabla += '</tr>';
    tabla += '<tr class="color"">';
    tabla += '<td>13</td>';
    tabla += '<td>kike</td>';
    tabla += '</tr>';
    tabla += '</table>';

    document.getElementById(nombrecontenedor).innerHTML = tabla;


    //var tabla = '<table id="' + nombretabla + '">';
    //tabla += '<thead>';
    //tabla += '<tr>';
    //tabla += '<th>Codigo</th>';
    //tabla += '<th>Descripcion</th>';
    //tabla += '<th>Tipo Item</th>';
    //tabla += '<th>Uni. Med.</th>';
    //tabla += '<th>Stck. Min.</th>';
    //tabla += '<th>Stck. Max</th>';
    //tabla += '<th>Cantidad</th>';
    //tabla += '<th>Id</th>';
    //tabla += '</tr>';
    //tabla += '</thead>';

    //tabla += '<tr><td>Celda 1</td><td>Celda 2</td><td> Celda 3</td></tr>';
    //tabla += '<tr><td>Celda 1</td><td>Celda 2</td><td> Celda 3</td></tr>';
    //tabla += '<tr><td>Celda 1</td><td>Celda 2</td><td> Celda 3</td></tr>';
    //tabla += '</table>';
    
    //document.getElementById(nombrecontenedor).innerHTML = tabla;
    
    //obj.employees[1].firstName + " " + obj.employees[1].lastName; 
}

function parsear(datos) {
    alert("kike dddddd lo maximo " + datos);

    var contact = JSON.parse(datos);
    alert(contact.tabla[1][1] + "_" + contact.tabla[1].c_despro);


    var tabla = '<table id="tabla">';
    tabla += '<caption>Lista de Items</caption>';
    tabla += '<thead>';
    tabla += '<tr>';
    tabla += '<th width="20"></th>';
    tabla += '<th width="20"></th>';
    tabla += '<th width="20"></th>';
    tabla += '<th width="40">Codigo</th>';
    tabla += '<th width="200">Descripcion</th>';
    tabla += '<th width="140">Tipo Item</th>';
    tabla += '<th width="40">Uni. Med.</th>';
    tabla += '<th width="40">Moneda</th>';
    tabla += '<th width="40">Stck. Min.</th>';
    tabla += '<th width="40">Stck. Max</th>';
    tabla += '<th width="40">Cantidad</th>';
    tabla += '<th width="40">estado</th>';
    tabla += '<th width="0">Id</th>';
    tabla += '</tr>';
    tabla += '</thead>';

    /*alert(contact.length);*/
    
    
    /*<td><img src='edit.png' alt='Edit"+i+"' class='btnEdit'/><img src='delete.png' alt='Delete"+i+"' class='btnDelete'/></td>" 
    */
    
    for( var i = 0; i < 20; i++ ) {  
        tabla += '<tr class=color">';
        tabla += '<td><img src="../publico/imagenes/editar.png" onclick="llamarformulario(' + contact.tabla[i].n_id + ')" class="btnEdit"/> </td>"';
        tabla += '<td><img src="../publico/imagenes/eliminar.png" onclick="reg_eliminar()"  class="btnDelete"/> </td>"';
        tabla += '<td><img src="../publico/imagenes/verregistro.png" onclick="reg_ver()"  class="btnDelete"/> </td>"';
        tabla += '<td>' + contact.tabla[i].c_codpro + '</td>';
        tabla += '<td>' + contact.tabla[i].c_despro + '</td>';
        tabla += '<td>' + contact.tabla[i].c_destipexi + '</td>';
        tabla += '<td>' + contact.tabla[i].c_desunimed + '</td>';
        tabla += '<td>' + contact.tabla[i].c_desmon + '</td>';
        tabla += '<td>' + contact.tabla[i].n_stkmin + '</td>';
        tabla += '<td>' + contact.tabla[i].n_stkmax + '</td>';
        tabla += '<td>' + contact.tabla[i].n_stkact + '</td>';
        tabla += '<td>' + contact.tabla[i].n_estado + '</td>';
        tabla += '<td>' + contact.tabla[i].n_id + '</td>';
        tabla += '</tr>';
    }
    /*
    tabla += '<tr class="coloralter">';
    tabla += '<td>12</td>';
    tabla += '<td>Carlos</td>';
    tabla += '</tr>';
    tabla += '<tr class="color"">';
    tabla += '<td>13</td>';
    tabla += '<td>kike</td>';
    tabla += '</tr>';
    tabla += '</table>';
    */

    document.getElementById("cuerpoform").innerHTML = tabla;
}

function llamarformulario(idproducto) {
    /*location.href = '../almacen/frmlistaitemsmantenimiento.aspx' + '?TextBox1=' + idproducto;*/
    /*alert("este es el id del producto" + idproducto);
    var x = document.getElementById('TextBox1');
    x.value = idproducto;*/
    var urlstring = '../almacen/frmlistaitemsmantenimiento.aspx' + '?TextBox1=' + idproducto;
    window.open(urlstring, '_new', 'width=350,height=150')

}

function VentanaDialogoModal(url, Arg, Ancho, Alto, idname) {
    x = (screen.width - Ancho) / 2;
    y = (screen.height - Alto) / 2;
    var resultado;
    resultado = window.showModalDialog(url, Arg, 'dialogHeight: ' + Alto + 'px; dialogWidth: ' + Alto + 'px; dialogTop: ' + y + 'px; dialogLeft: ' + x + 'px; edge: Raised; center: Yes; help: No; resizable: No; status: No;');
    document.getElementById(idname).value = resultado;
}


function demo() {
    alert("esto funciona");
}

function mensaje(texto) {
    alert(texto);
}

function ventana(archivo,ancho,alto){
    window.open(archivo,"Titulo de la ventana","toolbar=0, location=0, directories=0, status=0, menubar=0, scrollbars=yes, resizable=0, copyhistory=0, width=" + ancho + ", height=" + alto);
}