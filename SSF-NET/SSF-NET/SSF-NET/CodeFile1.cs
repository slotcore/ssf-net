// STORE  = vta_guiasdet_obtenerregistro
// STORE  = vta_guiasdet_insertar


// para actualizar os items afects de las compras
//update log_comprasdet set log_comprasdet.n_idtipafeigv = 10

//UPDATE log_comprasdet 
//right JOIN 
//(
//    SELECT
//        `log_compras`.`n_id`,
//        `log_compras`.`n_idemp`,
//        `log_compras`.`n_impigv`
//    FROM
//        `log_compras`
//    WHERE
//        ((`log_compras`.`n_impigv` = 0) AND
//        (`log_compras`.`n_idemp` = 1))
//) AS `tabla` ON `log_comprasdet`.`n_idcom` = `tabla`.`n_id`


// modificar el store  vta_ventas_consulta9

// ejecutar lo siguiente pero ante cambiar el valor por defecto del campo n_conciliado a 0
// update tes_tesoreriaori SET n_conciliado = 0 WHERE COALESCE(n_conciliado,0) = 0


// actualizar tipo de cambio para que funciones la consulta de ventas
//update vta_ventas 
//LEFT JOIN `con_tc` ON `vta_ventas`.`d_fchdoc` = `con_tc`.`d_fecha`
//SET vta_ventas.n_tc = con_tc.n_impven
//WHERE
//((vta_ventas.n_tc = 0) AND
//(vta_ventas.n_anotra = 2017));



    // ****************************************************
    // CODIGO PARA CONCATENAR VARIAS FILAS EN UN SOLO CAMPO
    // ****************************************************
    //SET @c_docref = (
    //SELECT
    //    GROUP_CONCAT(Concat(`vta_guias`.`c_numser`, '-', `vta_guias`.`c_numdoc`) SEPARATOR ' | ') AS `c_numdoc`
    //FROM
    //    `vta_ventasdoc`
    //    INNER JOIN `vta_guias` ON `vta_ventasdoc`.`n_iddoc` = `vta_guias`.`n_id`
    //WHERE
    //    (`vta_ventasdoc`.`n_idvta` = n_idvta)
    //);


    //DatosMySql FunMysql = new DatosMySql();
    //mysConec = FunMysql.ReAbrirConeccion(mysConec);

    // ****************************************************
    // STORE QUE CONVIERTE CADENA EN UN SQL
    // ****************************************************
    //CREATE DEFINER=`root`@`%` PROCEDURE `vta_pedidocendet_traerpedidos`(
    //    IN n_idemp INT(11),
    //    IN c_cadin VARCHAR(100)
    //)
    //BEGIN
    //    SET @myquery = CONCAT(
    //      "SELECT
    //      `vta_pedidocendet`.`n_idped`,
    //      `vta_pedidocendet`.`c_coditecen`,
    //      `vta_pedidocendet`.`n_canpro`,
    //      `vta_pedidocendet`.`c_codunimedcen`,
    //      `alm_inventario`.`c_codpro`,
    //      `alm_inventario`.`c_despro`,
    //      `alm_inventariounimed`.`n_id` AS `n_idunimed`,
    //      `vta_itemscen`.`n_iditem`
    //    FROM
    //      `vta_pedidocendet`
    //      LEFT JOIN `vta_itemscen` ON `vta_pedidocendet`.`c_coditecen` =
    //        `vta_itemscen`.`c_codcen`
    //      LEFT JOIN `alm_inventario` ON `vta_itemscen`.`n_iditem` =
    //        `alm_inventario`.`n_id`
    //      LEFT JOIN `alm_inventariounimed` ON `alm_inventario`.`n_id` =
    //        `alm_inventariounimed`.`n_idite`
    //      RIGHT JOIN `vta_pedidocen` ON `vta_pedidocen`.`n_id` =
    //        `vta_pedidocendet`.`n_idped`
    //    WHERE
    //      ((`vta_pedidocendet`.`n_idped` IN (", c_cadin,")) AND
    //      (`alm_inventariounimed`.`n_default` = 1) AND
    //      (`vta_pedidocen`.`n_idemp` = ", n_idemp,"));
    //      ");
      
    //    PREPARE stmt2 FROM @myquery;
    //    EXECUTE stmt2;
    //END


    // ****************************************************
    // CONVERTIR CADENAS EN FECHA EN MYSQL
    // ****************************************************
    //DECLARE d_fecha DATE;
    //SET d_fecha =  STR_TO_DATE(c_fecha, '%d/%m/%Y');


// ****************************************************
// LIMPIAR BD
// ****************************************************
//delete from alm_inventariolotes;

//delete from alm_movimientosdet;
//delete from alm_movimientos;

//delete from con_diario;

//delete from con_regdetracciones;

//delete from con_regpercepciondet;
//delete from con_regpercepcion;

//delete from con_regretenciondet;
//delete from con_regretencion;

//delete from con_pcitems;

//delete from con_provicionesdet;
//delete from con_proviciones;

//delete from log_ordencompradet;
//delete from log_ordencompra;

//delete from log_comprasdoc;
//delete from log_comprasdet;
//delete from log_compras;

//delete from log_ordenrequerimientodet;
//delete from log_ordenrequerimiento;

//delete from pla_destajo;
//delete from pla_destajodet;

//delete from pla_destajouni;
//delete from pla_destajounidet;
//delete from pla_destajounipla;

//delete from pro_programadetprocron;
//delete from pro_programadetpro;
//delete from pro_programadet;
//delete from pro_programa;

//delete from pro_ordenproducciondet;
//delete from pro_ordenproduccion;

//delete from pro_produccionins;
//delete from pro_produccion;

//delete from pro_rendimiento;

//delete from pro_revision;

//delete from pro_solicitudmaterialesdet;
//delete from pro_solicitudmateriales;

//delete from pro_solicitudtareasdet;
//delete from pro_solicitudtareascab;
//delete from pro_solicitudtareas;

//delete from pro_solicitudtareasdiversas;
//delete from pro_solicitudtareasdiversascab;
//delete from pro_solicitudtareasdiversasdet;

//delete from tes_canjedet;
//delete from tes_canje;

//delete from tes_conciliaciondet;
//delete from tes_conciliacion;

//delete from tes_tesoreriadesdet;
//delete from tes_tesoreriades;
//delete from tes_tesoreriaoridet;
//delete from tes_tesoreriaori;
//delete from tes_tesoreria;

//delete from vta_guiasdatos
//delete from vta_guiasdoc;
//delete from vta_guiasdet;
//delete from vta_guias;

//delete from vta_pedidocendet;
//delete from vta_pedidocen;

//delete from vta_pedidoclidet;
//delete from vta_pedidocli;

//delete from vta_punvencli;

//delete from vta_ventasbaja;
//delete from vta_ventasoct;
//delete from vta_ventasdoc;
//delete from vta_ventasdet;
//delete from vta_ventas;

//delete from vta_itemscen

//delete from alm_inventariounimed;
//delete from alm_inventario;