<%@ Page Title="" Language="C#" MasterPageFile="~/publico/principal.Master" AutoEventWireup="true" CodeBehind="frmlistaitems.aspx.cs" Inherits="SIAC_WEB.almacen.frmlistaitems" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 23px;
        }
        #Text5 {
            width: 77px;
        }
        #Text7 {
            width: 69px;
        }
        #Text9 {
            width: 67px;
        }
        #Text6 {
            width: 60px;
        }
        #Text8 {
            width: 52px;
        }
        #Text10 {
            width: 49px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contenedorprincipal" runat="server">
    <link href="../publico/estilos/tablas.css" rel="stylesheet" type ="text/css"/>
    <link href="../publico/estilos/flotante.css" rel="stylesheet" type ="text/css"/>
    <link href="../publico/estilos/tablaformulario.css" rel="stylesheet" type ="text/css"/>
    <script type="text/javascript" src="../publico/script/modulos.js"></script>        

    <div id="lista">
        <div id="cabeceraform">
            <div id="tituloform">
              
              
            </div>

            <div id="toobarform">
                
                <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click" />
                
                <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
                
            </div>
        </div>

        <div id="cuerpoform">
            <div id='ventana-flotante'>
                <a class='cerrar' href='javascript:void(0);' onclick='document.getElementById(&apos;ventana-flotante&apos;).className = &apos;oculto&apos;'>x</a>
                <div id='contenedor'>
                    <div class='contenido'>
                        <asp:Button ID="Button3" runat="server" Text="Button" />
                        <div>
                            <table class='TablaFormulario'>
                                <tr>
                                    <td class="tdcol1">Codigo</td>
                                    <td class="tdcol3"; colspan="5">
                                        <input id="Text1" type="text"/>
                                    </td>

                                    <td class="tdcol4"> </td>
                                    <td class="tdcol5"; colspan="2" rowspan="12"></td>
                                </tr>

                                <tr>
                                    <td class="tdcol1" >Tipo de Item</td>
                                    <td class="tdcol3"  colspan="5">
                                        <select id="Select1" name="D1" >
                                            <option></option>
                                        </select></td>
                                </tr>

                                <tr>
                                    <td class="tdcol1" style="height: 26px">Familia</td>
                                    <td class="tdcol3" colspan="5">
                                        <select id="Select2" name="D2">
                                            <option></option>
                                        </select></td>
                                </tr>

                                <tr>
                                    <td class="tdcol1">Clase</td>
                                    <td class="tdcol3" colspan="5">
                                        <select id="Select3" name="D3">
                                            <option></option>
                                        </select></td>
                                </tr>

                                <tr>
                                    <td class="tdcol1" style="height: 25px">Sub Clase</td>
                                    <td class="tdcol3" colspan="5" style="height: 25px">
                                        <select id="Select4" name="D4">
                                            <option></option>
                                        </select></td>
                                </tr>
                                
                                <tr>
                                    <td class="tdcol1">Descripcion</td>
                                    <td class="tdcol3" colspan="5">
                                        <input id="Text2" type="text"/>
                                    </td>
                                </tr>

                                <tr>
                                    <td class="tdcol1" style="height: 25px">Desc. Tecnica</td>
                                    <td class="tdcol3" colspan="5" style="height: 25px">
                                        <input id="Text3" type="text" /></td>
                                </tr>

                                <tr>
                                    <td class="tdcol1">Caracteristicas</td>
                                    <td class="tdcol3" colspan="5">
                                        <textarea id="TextArea1" cols="20" name="S1" rows="3"></textarea></td>
                                </tr>
                                
                                <tr>
                                    <td class="tdcol1">Moneda</td>
                                    <td class="tdcol3" colspan="5">
                                        <select id="Select5" name="D5">
                                            <option></option>
                                        </select></td>
                                </tr>

                                <tr>
                                    <td class="tdcol1" style="height: 25px">Stock Inicial</td>
                                    <td class="tdcol2" style="height: 25px">
                                        <input id="Text5" type="text" /></td>
                                    <td class="tdcol4"></td>
                                    <td class="tdcol4"></td>
                                    <td class="tdcol1">Stock Actual;</td>
                                    <td class="tdcol2">
                                        <input id="Text6" type="text" /></td>                                    
                                </tr>

                                <tr>
                                    <td class="tdcol1">Stock Minimo</td>
                                    <td class="tdcol2">
                                        <input id="Text7" type="text" /></td>
                                    <td class="tdcol4"></td>
                                    <td class="tdcol4"></td>
                                    <td class="tdcol1">Stock Maximo</td>
                                    <td class="tdcol2">
                                        <input id="Text8" type="text" /></td>
                                </tr>
                                
                                <tr>
                                    <td class="tdcol1">Precio Inicial</td>
                                    <td class="tdcol2">
                                        <input id="Text9" type="text" /></td>
                                    <td class="tdcol4"></td>
                                    <td class="tdcol4"></td>
                                    <td class="tdcol1">Precio Actual</td>
                                    <td class="tdcol2">
                                        <input id="Text10" type="text" /></td>
                                </tr>

                                <tr>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td></td>
                                    <td class="tdcol4" ></td>
                                    <td></td>                                  
                                </tr>

                                <tr>
                                    <td colspan="6">
                                       
                                        <table class='tablaunidad'>
                                            <tr>
                                                <td>Uni. Med</td>
                                                <td>Descripcion</td>
                                                <td>Uni. Med. Basica;</td>
                                                <td>Can. Uni. Basica</td>
                                                <td>Uni. Principal</td>
                                            </tr>
                                            
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="auto-style1"></td>
                                                <td class="auto-style1"></td>
                                                <td class="auto-style1"></td>
                                                <td class="auto-style1"></td>
                                                <td class="auto-style1"></td>
                                            </tr>
                                            
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </table>
                                    </td>
                                    
                                    <td>
                                    </td>
                                          
                                    <td colspan="2">
                                        <table class='tablaunidad'>
                                            <tr>
                                                <td>Archivo</td>
                                                <td>id</td>
                                            </tr>
                                            
                                            <tr>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            
                                            <tr>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                            
                                            <tr>
                                                <td></td>
                                                <td></td>
                                            </tr>
                                        </table>                                    
                                    </td>
                                    
                                </tr>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <div id="pieform">
        </div>
    </div>
</asp:Content>
