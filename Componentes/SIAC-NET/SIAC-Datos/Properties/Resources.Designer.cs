﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIAC_DATOS.Properties {
    using System;
    
    
    /// <summary>
    ///   Clase de recurso fuertemente tipado, para buscar cadenas traducidas, etc.
    /// </summary>
    // StronglyTypedResourceBuilder generó automáticamente esta clase
    // a través de una herramienta como ResGen o Visual Studio.
    // Para agregar o quitar un miembro, edite el archivo .ResX y, a continuación, vuelva a ejecutar ResGen
    // con la opción /str o recompile su proyecto de VS.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Devuelve la instancia de ResourceManager almacenada en caché utilizada por esta clase.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SIAC_DATOS.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Reemplaza la propiedad CurrentUICulture del subproceso actual para todas las
        ///   búsquedas de recursos mediante esta clase de recurso fuertemente tipado.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO pla_empleados
        ///	(
        ///	n_idemp,
        ///	n_id,
        ///	n_idtipdocide,
        ///	c_numdocide,
        ///	c_ape1,
        ///	c_ape2,
        ///	c_nom1,
        ///	c_nom2,
        ///	d_fchnac,
        ///	n_idsex,
        ///	c_numtel,
        ///	c_numesa,
        ///	d_fching,
        ///	n_asigfam,
        ///	n_suebas,
        ///	n_bon,
        ///	n_imphornor,
        ///	n_imphorext,
        ///	n_activo,
        ///	n_destajo,
        ///	c_dir,
        ///	c_email,
        ///	n_idnacpro,
        ///	n_idnacdep,
        ///	n_idnacdis,
        ///	n_idrespro,
        ///	n_idresdep,
        ///	n_idresdis,
        ///	n_destacado,
        ///	d_fchbaj
        ///	)
        ///VALUES 
        ///	(
        ///	@n_idemp,
        ///	@n_id,
        ///	@n_idtipdocide,
        ///	@c_numdocide,
        ///	@c_ape1,
        ///	@c_ape2,
        ///	@c_nom1,
        ///	@c_nom [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string q_pla_empleado_insert {
            get {
                return ResourceManager.GetString("q_pla_empleado_insert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE pla_empleados
        ///SET n_idemp = @n_idemp,
        ///	n_idtipdocide = @n_idtipdocide,
        ///	c_numdocide = @c_numdocide,
        ///	c_ape1 = @c_ape1,
        ///	c_ape2 = @c_ape2,
        ///	c_nom1 = @c_nom1,
        ///	c_nom2 = @c_nom2,
        ///	d_fchnac = @d_fchnac,
        ///	n_idsex = @n_idsex,
        ///	c_numtel = @c_numtel,
        ///	c_numesa = @c_numesa,
        ///	d_fching = @d_fching,
        ///	n_asigfam = @n_asigfam,
        ///	n_suebas = @n_suebas,
        ///	n_bon = @n_bon,
        ///	n_imphornor = @n_imphornor,
        ///	n_imphorext = @n_imphorext,
        ///	n_activo = @n_activo,
        ///	n_destajo = @n_destajo,
        ///	c_dir = @c_dir,
        ///	c_emai [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string q_pla_empleado_update {
            get {
                return ResourceManager.GetString("q_pla_empleado_update", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a DELETE FROM pla_periodolaboral
        ///WHERE n_idperiodolaboral = @n_idperiodolaboral.
        /// </summary>
        internal static string q_pla_periodolaboral_delete {
            get {
                return ResourceManager.GetString("q_pla_periodolaboral_delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT pla_periodolaboral.*
        ///, mae_categoria.c_descripcion categoria
        ///, mae_finperiodo.c_descripcion finperiodo
        ///FROM pla_periodolaboral 
        ///INNER JOIN mae_categoria ON mae_categoria.n_idcategoria = pla_periodolaboral.n_idcategoria 
        ///INNER JOIN mae_finperiodo ON mae_finperiodo.n_idfinperiodo = pla_periodolaboral.n_idfinperiodo 
        ///WHERE pla_periodolaboral.n_idempleado=@n_idempleado.
        /// </summary>
        internal static string q_pla_periodolaboral_get {
            get {
                return ResourceManager.GetString("q_pla_periodolaboral_get", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO pla_periodolaboral
        ///	(
        ///	n_idperiodolaboral,
        ///	n_idempleado,
        ///	n_idcategoria,
        ///	n_idfinperiodo,
        ///	n_corr,
        ///	d_fchini,
        ///	d_fchfin
        ///	)
        ///VALUES 
        ///	(
        ///	@n_idperiodolaboral,
        ///	@n_idempleado,
        ///	@n_idcategoria,
        ///	@n_idfinperiodo,
        ///	@n_corr,
        ///	@d_fchini,
        ///	@d_fchfin
        ///	).
        /// </summary>
        internal static string q_pla_periodolaboral_insert {
            get {
                return ResourceManager.GetString("q_pla_periodolaboral_insert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE pla_periodolaboral
        ///SET n_idempleado = @n_idempleado,
        ///	n_idcategoria = @n_idcategoria,
        ///	n_idfinperiodo = @n_idfinperiodo,
        ///	n_corr = @n_corr,
        ///	d_fchini = @d_fchini,
        ///	d_fchfin = @d_fchfin
        ///WHERE n_idperiodolaboral = @n_idperiodolaboral.
        /// </summary>
        internal static string q_pla_periodolaboral_update {
            get {
                return ResourceManager.GetString("q_pla_periodolaboral_update", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a DELETE FROM pro_produccionnotaing
        ///WHERE c_idproduccionnotaing = @c_idproduccionnotaing.
        /// </summary>
        internal static string q_pro_produccionnotaing_delete {
            get {
                return ResourceManager.GetString("q_pro_produccionnotaing_delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a SELECT 
        ///	pro_produccionnotaing.c_idproduccionnotaing,
        ///	pro_produccionnotaing.n_idproduccion,
        ///	pro_produccionnotaing.n_idnoting,
        ///	CONCAT(alm_movimientos.c_numser, &apos; - &apos;, alm_movimientos.c_numdoc) numnoting,
        ///	alm_movimientosdet.n_can cantidadmateriaprima,
        ///	alm_inventario.c_despro materiaprima
        ///FROM pro_produccionnotaing 
        ///INNER JOIN alm_movimientos ON alm_movimientos.n_id = pro_produccionnotaing.n_idnoting 
        ///INNER JOIN alm_movimientosdet ON alm_movimientosdet.n_idmov = alm_movimientos.n_id 
        ///INNER JOIN  [resto de la cadena truncado]&quot;;.
        /// </summary>
        internal static string q_pro_produccionnotaing_fetch {
            get {
                return ResourceManager.GetString("q_pro_produccionnotaing_fetch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a INSERT INTO pro_produccionnotaing
        ///	(
        ///	c_idproduccionnotaing,
        ///	n_idproduccion,
        ///	n_idnoting
        ///	)
        ///VALUES 
        ///	(
        ///	@c_idproduccionnotaing,
        ///	@n_idproduccion,
        ///	@n_idnoting
        ///	).
        /// </summary>
        internal static string q_pro_produccionnotaing_insert {
            get {
                return ResourceManager.GetString("q_pro_produccionnotaing_insert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Busca una cadena traducida similar a UPDATE pro_produccionnotaing
        ///SET n_idproduccion = @n_idproduccion,
        ///	n_idnoting = @n_idnoting
        ///WHERE c_idproduccionnotaing = @c_idproduccionnotaing.
        /// </summary>
        internal static string q_pro_produccionnotaing_update {
            get {
                return ResourceManager.GetString("q_pro_produccionnotaing_update", resourceCulture);
            }
        }
    }
}
