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

function genera_tabla(nombrecontenedor, nombretabla) {
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
    
    var tabla = '<table id="' + nombretabla + '">';
    tabla += '<caption>Lista de Items</caption>';
    tabla += '<thead>';
    tabla += '<tr>';
    tabla += '<th>Codigo</th>';
    tabla += '<th>Descripcion</th>';
    tabla += '<th>Tipo Item</th>';
    tabla += '<th>Uni. Med.</th>';
    tabla += '<th>Stck. Min.</th>';
    tabla += '<th>Stck. Max</th>';
    tabla += '<th>Cantidad</th>';
    tabla += '<th>Id</th>';
    tabla += '</tr>';
    tabla += '</thead>';

    tabla += '<tr class="color">';
    tabla += '<td>11' + datos + '</td>';
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

    //var tabla = '<table id="table">';
    //tabla += '<tr><td>Celda 1</td><td>Celda 2</td><td> Celda 3</td></tr>';
    //tabla += '<tr><td>Celda 1</td><td>Celda 2</td><td> Celda 3</td></tr>';
    //tabla += '<tr><td>Celda 1</td><td>Celda 2</td><td> Celda 3</td></tr>';
    //tabla += '</table>';
    document.getElementById(nombrecontenedor).innerHTML = tabla;

    //alert('hola');
}


function ver_mensaje() {

    alert('hola');

    var tabla = '<table id="' + nombretabla + '">';
    tabla += '<caption>esto es una prueba de lo que se hace</caption>';
    tabla += '<thead>';
    tabla += '<tr>';
    tabla += '<th>Codigo</th>';
    tabla += '<th>Descripcion</th>';
    tabla += '<th>Tipo Item</th>';
    tabla += '<th>Uni. Med.</th>';
    tabla += '<th>Stck. Min.</th>';
    tabla += '<th>Stck. Max</th>';
    tabla += '<th>Cantidad</th>';
    tabla += '<th>Id</th>';
    tabla += '</tr>';
    tabla += '</thead>';

    tabla += '<tr class="color">';
    tabla += '<td>11' + datos + '</td>';
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
}