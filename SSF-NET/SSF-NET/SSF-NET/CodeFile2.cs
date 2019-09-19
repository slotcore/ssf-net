
//https://www.youtube.com/watch?v=6SzSC6sJwaA

//http://www.sunat.gob.pe/w/wapS01Alias?ruc=10105203425

// ************************************
// ** INICIAMOS LOS PK DE LAS TABLAS **
// ************************************
//ALTER TABLE mae_clipro AUTO_INCREMENT = 0;

//ALTER TABLE alm_inventario AUTO_INCREMENT = 0;
//ALTER TABLE alm_movimientos AUTO_INCREMENT = 0;
//ALTER TABLE alm_responsable AUTO_INCREMENT = 0;

//ALTER TABLE con_diario AUTO_INCREMENT = 0;
//ALTER TABLE con_proviciones AUTO_INCREMENT = 0;
//ALTER TABLE con_regdetracciones AUTO_INCREMENT = 0;
//ALTER TABLE con_regpercepcion AUTO_INCREMENT = 0;
//ALTER TABLE con_regretencion AUTO_INCREMENT = 0;


//ALTER TABLE log_compras AUTO_INCREMENT = 0;
//ALTER TABLE log_honorarios AUTO_INCREMENT = 0;
//ALTER TABLE log_ordenrequerimiento AUTO_INCREMENT = 0;
//ALTER TABLE log_personal AUTO_INCREMENT = 0;

//ALTER TABLE pla_empleados AUTO_INCREMENT = 0;
//ALTER TABLE pla_personal AUTO_INCREMENT = 0;

//ALTER TABLE pro_programa AUTO_INCREMENT = 0;
//ALTER TABLE pro_ordenproduccion AUTO_INCREMENT = 0;
//ALTER TABLE pro_personal AUTO_INCREMENT = 0;
//ALTER TABLE pro_produccion AUTO_INCREMENT = 0;
//ALTER TABLE pro_revision AUTO_INCREMENT = 0;
//ALTER TABLE pro_solicitudmateriales AUTO_INCREMENT = 0;
//ALTER TABLE pro_solicitudtareas AUTO_INCREMENT = 0;
//ALTER TABLE pro_productos AUTO_INCREMENT = 0;

//ALTER TABLE tes_tesoreria AUTO_INCREMENT = 0;

//ALTER TABLE vta_chofer AUTO_INCREMENT = 0;
//ALTER TABLE vta_emptra AUTO_INCREMENT = 0;
//ALTER TABLE vta_guias AUTO_INCREMENT = 0;
//ALTER TABLE vta_itemscen AUTO_INCREMENT = 0;
//ALTER TABLE vta_pedidocen AUTO_INCREMENT = 0;
//ALTER TABLE vta_pedidocli AUTO_INCREMENT = 0;
//ALTER TABLE vta_punvencli AUTO_INCREMENT = 0;
//ALTER TABLE vta_vehiculo AUTO_INCREMENT = 0;
//ALTER TABLE vta_vendedor AUTO_INCREMENT = 0;
//ALTER TABLE vta_ventas AUTO_INCREMENT = 0;

//ALTER TABLE `data05`.`mae_subclase` AUTO_INCREMENT = 0;
//ALTER TABLE `data05`.`mae_clase` AUTO_INCREMENT = 0;
//ALTER TABLE `data05`.`mae_familia` AUTO_INCREMENT = 0;

// SELECT TABLE_NAME, VERSION, TABLE_ROWS  FROM INFORMATION_SCHEMA.tables WHERE TABLE_SCHEMA='data02' AND TABLE_TYPE = 'BASE TABLE';

// SELECT TABLE_NAME, COLUMN_NAME, DATA_TYPE, COLUMN_COMMENT FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_SCHEMA = 'data04' ORDER BY TABLE_NAME 



// select * from information_schema.routines
// select * from information_schema.parameters



// ACTUALIZAR EL SALDO DE LA FACTURA DE VENTA
//UPDATE vta_ventas
//INNER JOIN 
//(
//    SELECT
//        `tes_tesoreriadesdet`.`n_iddoc`,
//        `tes_tesoreriadesdet`.`n_idtipdoc`,
//        `tes_tesoreriadesdet`.`c_numser`,
//        `tes_tesoreriadesdet`.`c_numdoc`,
//        Sum(`tes_tesoreriadesdet`.`n_acuenta`) AS `n_impabono`
//    FROM
//        `tes_tesoreriadesdet`
//        LEFT JOIN `tes_tesoreriades` ON `tes_tesoreriadesdet`.`n_idtes` =
//        `tes_tesoreriades`.`n_idtes` AND `tes_tesoreriadesdet`.`n_iddes` =
//        `tes_tesoreriades`.`n_iddes`
//        LEFT JOIN `tes_tesoreria` ON `tes_tesoreriades`.`n_idtes` =
//        `tes_tesoreria`.`n_id`
//    WHERE
//        ((`tes_tesoreria`.`n_idemp` = 2) AND
//        (`tes_tesoreria`.`n_tipreg` = 1))
//    GROUP BY
//        `tes_tesoreriadesdet`.`n_iddoc`,
//        `tes_tesoreriadesdet`.`n_idtipdoc`,
//        `tes_tesoreriadesdet`.`c_numser`,
//        `tes_tesoreriadesdet`.`c_numdoc`,
//        `tes_tesoreria`.`n_idemp`,
//        `tes_tesoreria`.`n_tipreg`
//) AS abonos ON `abonos`.`n_iddoc` = `vta_ventas`.`n_id`
//SET `vta_ventas`.`n_impsal` = (`vta_ventas`.`n_imptotven` - `abonos`.`n_impabono`)
//WHERE 
//    (`vta_ventas`.`n_idemp` = 2)



// -- ACTUALIZAR EL SALDO DE LA FACTURA DE COMPRAS
//UPDATE log_compras
//INNER JOIN 
//(
//    SELECT
//        `tes_tesoreriadesdet`.`n_iddoc`,
//        `tes_tesoreriadesdet`.`n_idtipdoc`,
//        `tes_tesoreriadesdet`.`c_numser`,
//        `tes_tesoreriadesdet`.`c_numdoc`,
//        Sum(`tes_tesoreriadesdet`.`n_acuenta`) AS `n_impabono`
//    FROM
//        `tes_tesoreriadesdet`
//        LEFT JOIN `tes_tesoreriades` ON `tes_tesoreriadesdet`.`n_idtes` =
//        `tes_tesoreriades`.`n_idtes` AND `tes_tesoreriadesdet`.`n_iddes` =
//        `tes_tesoreriades`.`n_iddes`
//        LEFT JOIN `tes_tesoreria` ON `tes_tesoreriades`.`n_idtes` = `tes_tesoreria`.`n_id`
//    WHERE
//        (
//                (`tes_tesoreria`.`n_idemp` = 2) AND
//        (`tes_tesoreria`.`n_tipreg` = 2) AND 
//        (`tes_tesoreriadesdet`.`n_idlib` IN (8,34))
//                )
//    GROUP BY
//        `tes_tesoreriadesdet`.`n_iddoc`,
//        `tes_tesoreriadesdet`.`n_idtipdoc`,
//        `tes_tesoreriadesdet`.`c_numser`,
//        `tes_tesoreriadesdet`.`c_numdoc`,
//        `tes_tesoreria`.`n_idemp`,
//        `tes_tesoreria`.`n_tipreg`
//) AS abonos ON `abonos`.`n_iddoc` = `log_compras`.`n_id`
//SET `log_compras`.`n_impsal` = (`log_compras`.`n_imptotcom` - `abonos`.`n_impabono`)
//WHERE 
//    (`log_compras`.`n_idemp` = 2) 
//        -- AND		(	`log_compras`.`n_id` = 108)) 



 
// -- ACTUALIZAR EL SALDO DE LA FACTURA DE COMPRAS
//UPDATE con_regpercepcion
//INNER JOIN 
//(
//    SELECT
//        `tes_tesoreriadesdet`.`n_iddoc`,
//        `tes_tesoreriadesdet`.`n_idtipdoc`,
//        `tes_tesoreriadesdet`.`c_numser`,
//        `tes_tesoreriadesdet`.`c_numdoc`,
//        Sum(`tes_tesoreriadesdet`.`n_acuenta`) AS `n_impabono`
//    FROM
//        `tes_tesoreriadesdet`
//        LEFT JOIN `tes_tesoreriades` ON `tes_tesoreriadesdet`.`n_idtes` =
//        `tes_tesoreriades`.`n_idtes` AND `tes_tesoreriadesdet`.`n_iddes` =
//        `tes_tesoreriades`.`n_iddes`
//        LEFT JOIN `tes_tesoreria` ON `tes_tesoreriades`.`n_idtes` = `tes_tesoreria`.`n_id`
//    WHERE
//        ((`tes_tesoreria`.`n_idemp` = 2) AND
//        (`tes_tesoreria`.`n_tipreg` = 2) AND 
//        (`tes_tesoreriadesdet`.`n_idlib` = 16))
//    GROUP BY
//        `tes_tesoreriadesdet`.`n_iddoc`,
//        `tes_tesoreriadesdet`.`n_idtipdoc`,
//        `tes_tesoreriadesdet`.`c_numser`,
//        `tes_tesoreriadesdet`.`c_numdoc`,
//        `tes_tesoreria`.`n_idemp`,
//        `tes_tesoreria`.`n_tipreg`
//) AS abonos ON `abonos`.`n_iddoc` = `con_regpercepcion`.`n_id`
//SET `con_regpercepcion`.`n_saldo` = (`con_regpercepcion`.`n_impper` - `abonos`.`n_impabono`)
//WHERE 
//    (`con_regpercepcion`.`n_idemp` = 2)




// ACTUALIZAR LA DETRACCIONES
//UPDATE `log_compras`
//INNER JOIN `con_regdetracciones` ON `log_compras`.`n_id` = `con_regdetracciones`.`n_iddoccom`
//SET
//    `log_compras`.`n_impsal` = (`log_compras`.`n_impsal` - `con_regdetracciones`.`n_imp`)
//WHERE 
//    (`log_compras`.`n_idemp` = 1)





 //         Cls_Api o_api = new Cls_Api();
 //           BE_SYS_USUARIOS e_empresa = new BE_SYS_USUARIOS();
 //           e_empresa.n_idemp = 0;
 //           e_empresa.n_id = 0;
 //           e_empresa.c_nom = "eee";
 //           e_empresa.c_apepat = "ee";
 //           e_empresa.c_apemat = "ee";
 //           e_empresa.c_usuario = "ee";
 //           e_empresa.c_pass = "ee";
 //           e_empresa.n_activo = 0;
 //           e_empresa.n_idperfil = 0;
 //           e_empresa.c_email = "ee";
 //           string c_cad = o_api.ApiPost("http://localhost:30050/api/sys_usuario/", e_empresa);

 //           BE_SYS_USUARIOS e_emp = JsonConvert.DeserializeObject<BE_SYS_USUARIOS>(c_cad);




//CREATE TABLE `tes_letras` (
//  `n_idemp` int(11) DEFAULT NULL COMMENT 'ID DE LA EMPRESA (VER TABLA SYS_EMPRESA)',
//  `n_ano` int(11) DEFAULT NULL COMMENT 'ANO DE TRABAJO',
//  `n_mes` int(11) DEFAULT NULL COMMENT 'MES DE TRABAJO',
//  `n_idlib` int(11) DEFAULT NULL COMMENT 'ID DEL LIBRO (VER TABLA SUN_LIBROS)',
//  `n_id` int(11) NOT NULL AUTO_INCREMENT COMMENT 'ID DEL REGISTRO ',
//  `c_numreg` char(4) DEFAULT NULL,
//  `n_tiplet` int(11) DEFAULT NULL COMMENT 'INDICA EL TIPO DE LETRA (1 = LETRA DE CLIENTE;  2 = LETRA DE PROVEEDORES)',
//  `d_fchreg` date DEFAULT NULL COMMENT 'FECHA DE REGISTRO DE LA LETRA',
//  `d_fchini` date DEFAULT NULL COMMENT 'FECHA DE INICIO PARA EL CALCULO DE LOS VENCIMIENTOS DE LAS LETRAS',
//  `n_idclipro` int(11) DEFAULT NULL COMMENT 'ID DEL CLIENTE O DEL PROVEEDORE',
//  `n_idmon` int(11) DEFAULT NULL COMMENT 'ID DE LA MONEDA DE EMISION DE LA LETRA',
//  `n_numlet` int(11) DEFAULT NULL COMMENT 'NUMERO DE LETRAS QUE SE EMITIRAN',
//  `n_impcap` double(12,2) DEFAULT NULL COMMENT 'IMPORTE DEL CAPITAL A FRACCIONAR EN LETRAS',
//  `n_idtipint` int(11) DEFAULT NULL COMMENT 'ID DEL TIPO DE INTERES QUE SE APLICARA (VER TABLA TES_TIPOINTERES)',
//  `n_portas` double(10,2) DEFAULT NULL COMMENT 'PORCENTAJE DE LA TASA DE INTERES APLICADO A LAS LETRAS',
//  `c_glosa` varchar(200) DEFAULT NULL COMMENT 'GLOSA DE LA LETRA',
//  `n_numdiaint` int(11) DEFAULT NULL COMMENT 'NUMERO DE DIAS DE INTERVALO ENTRE LETRAS',
//  `n_tc` double(10,3) DEFAULT NULL COMMENT 'TIPO DE CAMBIO APLICADO',
//  `c_girnumdoc` varchar(45) DEFAULT NULL COMMENT 'NUMERO DE DOCUMENTO DEL GIRADOR',
//  `c_girnom` varchar(45) DEFAULT NULL COMMENT 'NOMBRE DEL GIRADOR',
//  `c_girtel` varchar(45) DEFAULT NULL COMMENT 'TELEFONO DEL GIRADOR',
//  `c_girdir` varchar(100) DEFAULT NULL COMMENT 'DIRECCION DEL GIRADOR',
//  `n_imppor` double(10,2) DEFAULT NULL COMMENT 'IMPORTE DE LOS PORTES APLICADOS A LA LETRA',
//  `n_idban` int(11) DEFAULT NULL COMMENT 'ID DEL BANCO ASIGNADO A LA LETRA (VER TABLLA TES_BANCO)',
//  `n_idcueban` int(11) DEFAULT NULL COMMENT 'ID DE LA CUENTA DE BANCO QUE SE ESTA ASIGNADO A LA LETRA (VER TABLA  TES_CUENTABANDO)',
//  `n_numdiapla` int(11) DEFAULT NULL COMMENT 'NUMDE DIAS DE PLAZO PARA EMITIR LA LETRA',
//  PRIMARY KEY (`n_id`)
//) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=latin1;
