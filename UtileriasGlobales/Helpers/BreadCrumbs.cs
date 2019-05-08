/* ================================================================================================
 * BreadCrumbs.cs
 * 
 * This file contains the code that creates the breadcrumb navigation on my Web site.  For an 
 * article describing this control's functionality, please see
 * http://www.codeproject.com/aspnet/breadcrumbs.asp.
 * 
 * This file is provided "as is" with no expressed or implied warranty.  The author accepts no 
 * liability for any damage/loss of business that this product may cause.
 * 
 * Author:		Jon Sagara (jonsagara@hotmail.com)
 * History:
 *		1.0.0
 *			08/23/2003	- Released as an include file.
 * 
 *		2.0.0
 *			09/23/2003	- Converted to a custom control.
 * 
 * Compile: csc /out:..\bin\BreadCrumbs.dll /target:library BreadCrumbs.cs
 * ============================================================================================= */

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;


namespace UtileriasGlobales.Helpers
{
	namespace BreadCrumbs
	{
		public class BreadCrumbControl : System.Web.UI.WebControls.WebControl
		{
			StringBuilder sbResult = new StringBuilder ();				// Holds the Breadcrumb HTML.
			StringBuilder sbBcUrl = new StringBuilder ();				// Holds the URL of the breadcrumb.  Directories are appended in succession to the root.
			private HybridDictionary labels = new HybridDictionary ();	// Holds the "friendly" directory names.


			/*
			 *	Constructor
			 *	Hook up the Control_Load event handler.
			 */
			public BreadCrumbControl ()
			{
				Load += new EventHandler (Control_Load);

				//
				// Initialize properties to default values.
				//
				ShowFileName	= false;
				Separator		= ">";
				RootUrl			= "/";
				RootName		= "Inicio";

				//
				// Give the directories "friendly" names.  Given the dynamic nature of directory structures, list them 
				// alphabetically by key.
				//
				labels.Add("catalogos", "Catalogos");
                labels.Add("banners", "Banners");
                labels.Add("categorias", "Categorias");
                labels.Add("productos", "productos");
                labels.Add("promociones", "Promociones");
                labels.Add("clientes", "Clientes");
                labels.Add("clientesDirecciones", "Clientes > Direccion");

                labels.Add("usuarios", "Usuarios");
                labels.Add("inventario", "Inventario");
                labels.Add("ventas", "Ventas");
                labels.Add("boletin", "Boletin");
			}


			// PROPERTIES

			// ShowFileName.  Set to true if you want to append an extra separator character and
			// the current file's name; false if not.  Default is false.  
			private bool showFileName;
			public bool ShowFileName
			{
				get
				{
					return showFileName;
				}
				set
				{
					showFileName = value;
				}
			}

			// Separator.  Contains the character(s) that separate each directory and/or file name
			// in the breadcrumb HTML.  Default is ">".
			private string separator;
			public string Separator
			{
				get
				{
					return separator;
				}
				set
				{
					separator = value;
				}
			}

			// RootUrl.  URL of the root directory.  Default is "/".
			private string rootUrl;
			public string RootUrl
			{
				get
				{
					return rootUrl;
				}
				set
				{
					rootUrl = value;
				}
			}

			// RootName.  "Friendly" name of the root directory.  Default is "Home".
			private string rootName;
			public string RootName
			{
				get
				{
					return rootName;
				}
				set
				{
					rootName = value;
				}
			}


			// EVENT HANDLERS

			protected override void Render (HtmlTextWriter output)
			{
				output.Write (sbResult.ToString () + "<br>");
			}

			/*
			 *	EventHandler: Control_Load
			 *	This event handler contains the meat of the control's functionality.
			 */
			protected void Control_Load (Object sender, EventArgs e)
			{
				//
				// Create the root breadcrumb (corresponds to the root directory).
				//
				sbResult.Append ("<a href=\"" + RootUrl + "\">" + RootName + "</a>");

				//
				// Get the site URL.  Use a StringBuilder to hold the URL so that we can append 
				// directory names in succession.
				//
				string strHostUrl = "http://" + Page.Request.ServerVariables["HTTP_HOST"] + "/";
				sbBcUrl.Append (strHostUrl);

				//
				// Break up the path parts into an array (directory name(s) and/or file name).
				//
				string scriptName = Page.Request.ServerVariables["SCRIPT_NAME"];
				string strScriptDir = Path.GetDirectoryName (scriptName);
				bool bHasExtension = Path.HasExtension (scriptName);

				//
				// Create breadcrumb HTML for the directory name(s).
				// *** 	Note: Remove the first "\"; otherwise, when you split the string, the first item in the
				// 		array is an empty string.
				//
				strScriptDir = strScriptDir.Substring (1);
				string[] strDirs = strScriptDir.Split ('\\');
				int nNumDirs = strDirs.Length;

				//
				// Splitting the string "\" (root directory) produces an array with an empty string as its only element.  If there is
				// only one element and it is the empty string, then we are in the root directory.  If the user has chosen
				// to display the file name, then the separator and the file name will be appended to the Web site root
				// Name/URL.  
				//
				// If, however, there is one element and it is NOT the empty string, then we are one directory deep.
				// The else statement is executed and the separator character is inserted.
				//
				string strSeparator = "";
				if (1 == nNumDirs && (strDirs[0] == ""))
				{
					strSeparator = "";
				}
				else
				{
					strSeparator = String.Format (" {0} ", Separator);
				}

                //foreach (string strDirName in strDirs)
                //{
                //    sbResult.Append (strSeparator + "<a href=\"" + sbBcUrl.ToString () + strDirName + "/\">" + labels[strDirName] + "</a>");
                //    sbBcUrl.Append (strDirName + "/");
                //}

                foreach (string strDirName in strDirs)
                {
                    sbResult.Append(strSeparator + labels[strDirName]);
                    sbBcUrl.Append(strDirName + "/");
                }

				//
				// If the user wants to display file names, do it now.
				//
				if (false != ShowFileName)
				{
					sbResult.Append (String.Format (" {0} {1}", Separator, Path.GetFileName (scriptName)));
				}
			}
		}
	}
}

