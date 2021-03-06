﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : UriUtils.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de l'objet StringUtils
// Créé le       : 21/06/2013
// Modifié le    : 13/04/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Net;
using System.Collections.Generic;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System"
//*******************************************************************************************************************************
namespace System
	{

	//  #   #  ####   #         #   #  #####  #  #       ####
	//  #   #  #   #  #         #   #    #    #  #      #    
	//  #   #  ####   #  #####  #   #    #    #  #       ### 
	//  #   #  #   #  #         #   #    #    #  #          #
	//   ###   #   #  #          ###     #    #  #####  #### 

	//***************************************************************************************************************************
	// Classe UriUtils
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit des méthodes utilisées pour manipuler les chaines.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public static class UriUtils
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Combine la racine de l'Uri avec le Path spécifié.
		/// </summary>
		/// <param name="Self">Objet <b>Uri</b> actuel.</param>
		/// <param name="Path"></param>
		/// <returns></returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static Uri Combine ( this Uri Self, string Path )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Self != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				string AbsoluteUri = Self.AbsoluteUri;

				if ( Path.StartsWith ( "/" ) || Path.StartsWith ( "\\" ) )
					Path = Path.Substring ( 1 );

				Self = new Uri ( Self.AbsoluteUri + Path );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return Self;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Transforme l'uri en absolue.
		/// </summary>
		/// <param name="BaseUri">Uri à transformer.</param>
		/// <returns>Uri transformée.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		private static Uri MakeAbsolute ( Uri BaseUri )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( BaseUri != null  && ! BaseUri.OriginalString.StartsWith ( "/", StringComparison.Ordinal ) )
				return new Uri ( "http://localhost/" + BaseUri, UriKind.Absolute );

			return new Uri ( "http://localhost" + BaseUri, UriKind.Absolute );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens le composant QueryString de l'Uri.
		/// </summary>
		/// <param name="Uri">Objet <b>Uri</b> contenant le composant QuaryString.</param>
		/// <returns>Composant QueryString de l'objet <b>Uri</b>.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		private static string InternalUriGetQueryString ( Uri Uri )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return MakeAbsolute (Uri).GetComponents (UriComponents.Query, UriFormat.SafeUnescaped);
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Transforme l'élément QueryString en tableau de valeurs.
		/// </summary>
		/// <param name="Self">Objet <b>Uri</b>.</param>
		/// <param name="Decode">Indique s'il faut décoder le résultat.</param>
		/// <returns>Tableau de valeurs.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static IDictionary<string, string> QueryString ( this Uri Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return QueryString ( InternalUriGetQueryString ( Self ), true );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Transforme l'élément QueryString en tableau de valeurs.
		/// </summary>
		/// <param name="Self">Objet <b>Uri</b>.</param>
		/// <param name="Decode">Indique s'il faut décoder le résultat.</param>
		/// <returns>Tableau de valeurs.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static IDictionary<string, string> QueryString ( this Uri Self, bool Decode )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return QueryString ( InternalUriGetQueryString ( Self ), Decode );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Transforme l'élément QueryString en tableau de valeurs.
		/// </summary>
		/// <param name="Self">Objet <b>Uri</b>.</param>
		/// <param name="Decode">Indique s'il faut décoder le résultat.</param>
		/// <returns>Tableau de valeurs.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static IDictionary<string, string> QueryString ( this string Self )
			{
			//-------------------------------------------------------------------------------------------------------------------
			return QueryString ( Self, true );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Transforme l'élément QueryString en tableau de valeurs.
		/// </summary>
		/// <param name="Self">Objet <b>Uri</b>.</param>
		/// <param name="Decode">Indique s'il faut décoder le résultat.</param>
		/// <returns>Tableau de valeurs.</returns>
		//-----------------------------------------------------------------------------------------------------------------------
		public static IDictionary<string, string> QueryString ( this string Self, bool Decode )
			{
			//-------------------------------------------------------------------------------------------------------------------
			IDictionary<string, string> Result = new Dictionary<string, string> ( StringComparer.Ordinal );
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			foreach ( string Str in Self.Split ( "&".ToCharArray (), StringSplitOptions.RemoveEmptyEntries ) )
				{
				//---------------------------------------------------------------------------------------------------------------
				int Index = Str.IndexOf ( "=", StringComparison.Ordinal );
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				if ( Index == -1 )
					{
					//-----------------------------------------------------------------------------------------------------------
					Result.Add ( Decode ? HttpUtility.UrlDecode ( Str ) : Str, string.Empty );
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				else
					{
					//-----------------------------------------------------------------------------------------------------------
					Result.Add ( Decode ? 
						HttpUtility.UrlDecode ( Str.Substring ( 0, Index ) ) : 
							Str.Substring ( 0, Index ), Decode ? 
								HttpUtility.UrlDecode ( Str.Substring ( Index + 1 ) ) : 
									Str.Substring ( Index + 1 ) );
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			return Result;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************
	
	} // Fin du namespace "System"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
