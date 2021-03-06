﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : ApplicationInfos.cs
// Auteur        : Nicolas Dagnas
// Description   : Déclaration de l'objet ApplicationInfos
// Créé le       : 09/02/2015
// Modifié le    : 18/11/2016
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Net;
using System.Xml.Linq;
using System.Globalization;
using System.IO.IsolatedStorage;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "System.MarketPlace"
//*******************************************************************************************************************************
namespace System.MarketPlace
	{
	
	//   ###   ####   ####          #  #   #  #####   ###    ####
	//  #   #  #   #  #   #         #  ##  #  #      #   #  #    
	//  #####  ####   ####   #####  #  # # #  ###    #   #   ### 
	//  #   #  #      #             #  #  ##  #      #   #      #
	//  #   #  #      #             #  #   #  #       ###   #### 

	//***************************************************************************************************************************
	// Classe ApplicationInfos
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	public static class ApplicationInfos
		{
		//***********************************************************************************************************************
		#region // Classe AsyncState
		//-----------------------------------------------------------------------------------------------------------------------
		private class AsyncState
			{
			//*******************************************************************************************************************
			/// <summary>
			/// Initialise une nouvelle instance de l'objet <b>AsyncState</b>.
			/// </summary>
			/// <param name="AppGuid">Identifiant de l'application.</param>
			/// <param name="Callback">Appelé pour retourner les informations.</param>
			/// <param name="CultureInfo">Culture de la région.</param>
			//-------------------------------------------------------------------------------------------------------------------
			public AsyncState ( Guid AppGuid, EventHandler<ApplicationInfosEventArgs> Callback, CultureInfo CultureInfo )
				{
				//---------------------------------------------------------------------------------------------------------------
				this.AppGuid     = AppGuid;
				this.Callback    = Callback;
				this.CultureInfo = CultureInfo;
				//---------------------------------------------------------------------------------------------------------------
				}
			//*******************************************************************************************************************

			//*******************************************************************************************************************
			/// <summary>
			/// Obtiens l'identifiant de l'application.
			/// </summary>
			//-------------------------------------------------------------------------------------------------------------------
			public Guid AppGuid { get; private set; }
			//*******************************************************************************************************************

			//*******************************************************************************************************************
			/// <summary>
			/// Délégué utilisé pour fournir les informations récupérées.
			/// </summary>
			//-------------------------------------------------------------------------------------------------------------------
			public EventHandler<ApplicationInfosEventArgs> Callback { get; private set; }
			//*******************************************************************************************************************

			//*******************************************************************************************************************
			/// <summary>
			/// Obtiens la culture déterminant le store.
			/// </summary>
			//-------------------------------------------------------------------------------------------------------------------
			public CultureInfo CultureInfo { get; private set; }
			//*******************************************************************************************************************
			}
		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		#region // Section des Procédures Privées
		//-----------------------------------------------------------------------------------------------------------------------

		//***********************************************************************************************************************
		/// <summary>
		/// Extrait les informations de la page Web.
		/// </summary>
		/// <param name="Sender">Source de l'appel.</param>
		/// <param name="Args">Données de l'appel Web.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private static void MarketPlaceAppContentCompleted ( object Sender, DownloadStringCompletedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			ApplicationInfosEventArgs Result = null;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			AsyncState State = Args.UserState as AsyncState;
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			try
				{
				//---------------------------------------------------------------------------------------------------------------
				if ( ! string.IsNullOrEmpty ( Args.Result ) )
					{
					//-----------------------------------------------------------------------------------------------------------
					Result = new ApplicationInfosEventArgs ( XElement.Parse ( Args.Result ), State.CultureInfo );

					if ( ! Result.IsEmpty )
						StorageSettings.SetValue ( "last-check-update", DateTime.Now.ToString ( "yyyy-MM-dd" ) );
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			catch {}
			//-------------------------------------------------------------------------------------------------------------------
			finally
				{
				//---------------------------------------------------------------------------------------------------------------
				var Callback = State.Callback;

				if ( State.Callback != null )
					{
					//-----------------------------------------------------------------------------------------------------------
					if ( Result == null )
						Result = new ApplicationInfosEventArgs ( State.AppGuid );

					Callback ( null, Result );
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//-----------------------------------------------------------------------------------------------------------------------
		#endregion
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Permet de récupérer les informations d'une application publiée sur le Store.
		/// </summary>
		/// <param name="Callback">Délégué utilisé pour retourner les informations.</param>
		/// <param name="AppGuid">Identifiant de l'application.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void AsyncGet ( EventHandler<ApplicationInfosEventArgs> Callback, string AppGuid )
			{
			//-------------------------------------------------------------------------------------------------------------------
			AsyncGet ( Callback, new Guid ( AppGuid ), CultureInfo.CurrentCulture );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Permet de récupérer les informations d'une application publiée sur le Store.
		/// </summary>
		/// <param name="Callback">Délégué utilisé pour retourner les informations.</param>
		/// <param name="AppGuid">Identifiant de l'application.</param>
		/// <param name="CultureInfo">Culture de la région associée à l'application.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void AsyncGet ( EventHandler<ApplicationInfosEventArgs> Callback, string AppGuid, CultureInfo CultureInfo )
			{
			//-------------------------------------------------------------------------------------------------------------------
			AsyncGet ( Callback, new Guid ( AppGuid ), CultureInfo );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Permet de récupérer les informations d'une application publiée sur le Store.
		/// </summary>
		/// <param name="Callback">Délégué utilisé pour retourner les informations.</param>
		/// <param name="AppGuid">Identifiant de l'application.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void AsyncGet ( EventHandler<ApplicationInfosEventArgs> Callback, Guid AppGuid )
			{
			//-------------------------------------------------------------------------------------------------------------------
			AsyncGet ( Callback, AppGuid, CultureInfo.CurrentCulture );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Permet de récupérer les informations d'une application publiée sur le Store.
		/// </summary>
		/// <param name="Callback">Délégué utilisé pour retourner les informations.</param>
		/// <param name="AppGuid">Identifiant de l'application.</param>
		/// <param name="CultureInfo">Culture de la région associée à l'application.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public static void AsyncGet ( EventHandler<ApplicationInfosEventArgs> Callback, Guid AppGuid, CultureInfo CultureInfo )
			{
			//-------------------------------------------------------------------------------------------------------------------
			string LastCheckUpdate = StorageSettings.GetValue ( "last-check-update", "" );

			if ( LastCheckUpdate == DateTime.Now.ToString ( "yyyy-MM-dd" ) )
				{
				//---------------------------------------------------------------------------------------------------------------
				if ( Callback != null )
					Callback ( null, new ApplicationInfosEventArgs ( AppGuid ) );

				return;
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			try
				{
				//---------------------------------------------------------------------------------------------------------------
				var Request = new WebClient ();

				Request.Headers["User-Agent"] = "MarketPlace Application Infos Both/2.0";

				Request.DownloadStringCompleted += MarketPlaceAppContentCompleted;

				string Mask = "http://api.on-va-sortir.com/GetAppInfos?AppID={0}";

				string Uri = string.Format ( Mask, AppGuid );

				Request.DownloadStringAsync ( new Uri ( Uri ), new AsyncState ( AppGuid, Callback, CultureInfo ) );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			catch {}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************
	
	} // Fin du namespace "System.MarketPlace"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
