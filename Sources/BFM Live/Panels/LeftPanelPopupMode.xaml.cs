﻿//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : LeftPanelPopupMode.xaml.cs
// Auteur        : Nicolas Dagnas
// Description   : Implémentation de la Popup LeftPanelPopupMode
// Créé le       : 08/05/2015
// Modifié le    : 19/05/2018
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Phone.Infos;
using System.Windows.Media.Imaging;
using System.Windows.Phone.Controls;
using System.Windows.Controls.Primitives;
//~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
using NextRadio.Service;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// Début du bloc "NextRadio.Popups"
//*******************************************************************************************************************************
namespace NextRadio.Popups
	{
	
	//  #      #####  #####  #####         ####    ###   #   #  #####  #    
	//  #      #      #        #           #   #  #   #  ##  #  #      #    
	//  #      ###    ###      #    #####  ####   #####  # # #  ###    #    
	//  #      #      #        #           #      #   #  #  ##  #      #    
	//  #####  #####  #        #           #      #   #  #   #  #####  #####

	//***************************************************************************************************************************
	// Classe LeftPanelPopupMode
	//***************************************************************************************************************************
	#region // Déclaration et Implémentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Fournit une fenêtre de zoom sur une photo.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public partial class LeftPanelPopupMode : UserControl
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>LeftPanelPopupMode</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public LeftPanelPopupMode ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			this.InitializeComponent ();
			
			this.OnOrientationChanged ( DeviceInfos.Orientation );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Anime l'apparition du menu.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void AnimateOpen ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			foreach ( UIElement Item in this.Layout.Children )
				{ if ( Item is TileButton ) ((TileButton)Item).AnimateOpen (); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Anime la fermeture du menu.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void AnimateClose ()
			{
			//-------------------------------------------------------------------------------------------------------------------
			foreach ( UIElement Item in this.Layout.Children )
				{ if ( Item is TileButton ) ((TileButton)Item).AnimateClose (); }
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Appelé après la modification de la propriété Orientation.
		/// </summary>
		/// <param name="Args">Arguments d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		public void OnOrientationChanged ( PhoneOrientation Value )
			{
			//-------------------------------------------------------------------------------------------------------------------
			double ButtonCount = 3;
			double ScreenWidth = 480;
			
			if ( Value == PhoneOrientation.Portrait )
				{
				//---------------------------------------------------------------------------------------------------------------
				ButtonCount = 3;
				ScreenWidth = Application.Current.Host.Content.ActualWidth;
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			else
				{
				//---------------------------------------------------------------------------------------------------------------
				ButtonCount = 5;
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				#if WP80
				//---------------------------------------------------------------------------------------------------------------
				ScreenWidth = Application.Current.Host.Content.ActualHeight - DeviceInfos.ApplicationBarHeight;
				//---------------------------------------------------------------------------------------------------------------
				#endif
				//---------------------------------------------------------------------------------------------------------------

				//---------------------------------------------------------------------------------------------------------------
				#if WP81
				//---------------------------------------------------------------------------------------------------------------
				ScreenWidth = Application.Current.Host.Content.ActualHeight - DeviceInfos.ApplicationBarHeight * 2;
				//---------------------------------------------------------------------------------------------------------------
				#endif
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------

			//-------------------------------------------------------------------------------------------------------------------
			double ItemWidth = ( ScreenWidth - ( 4 * ( ButtonCount + 1 ) ) ) / ButtonCount;

			foreach ( FrameworkElement Child in this.Layout.Children ) Child.Width = ItemWidth;
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Est appelé lors d'un clic sur un lien interne.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>RoutedEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnSectionClick ( object Sender, RoutedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			var Self = Sender as FrameworkElement;

			if ( Self != null && this.Click != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				SectionType Section = SectionType.All;
				//---------------------------------------------------------------------------------------------------------------
				
				//---------------------------------------------------------------------------------------------------------------
				switch ( Self.Tag.ToString () )
					{
					//-----------------------------------------------------------------------------------------------------------
//					case "SectionType.TodayQuestion"  : Section = SectionType.TodayQuestion; break;
					case "SectionType.Politics"       : Section = SectionType.Politics;      break;
					case "SectionType.Society"        : Section = SectionType.Society;       break;
					case "SectionType.International"  : Section = SectionType.International; break;
					case "SectionType.Economy"        : Section = SectionType.Economy;       break;
					case "SectionType.Sport"          : Section = SectionType.Sport;         break;
					case "SectionType.HighTech"       : Section = SectionType.HighTech;      break;
					case "SectionType.Divertissement" : Section = SectionType.Divertissement;break;
					case "SectionType.Planete"        : Section = SectionType.Planete;       break;
					case "SectionType.InfosLive"      : Section = SectionType.InfosLive;     break;
					//-----------------------------------------------------------------------------------------------------------
					case "SectionType.Bookmarks"      : Section = SectionType.Bookmarks;     break;
					//-----------------------------------------------------------------------------------------------------------
					}
				//---------------------------------------------------------------------------------------------------------------
				
				//---------------------------------------------------------------------------------------------------------------
				this.Click ( this, new ObjectEventArgs<SectionType> ( Section ) );
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************
		
		//***********************************************************************************************************************
		/// <summary>
		/// Est appelé lors d'un clic sur un lien interne.
		/// </summary>
		/// <param name="Sender">Objet source de l'appel.</param>
		/// <param name="Args"><b>RoutedEventArgs</b> qui contient les données d'événement.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		private void OnOptionsButtonClick ( object Sender, RoutedEventArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( this.Click != null ) this.Click ( this, EventArgs.Empty );
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Se produit lors d'un clic sur un élément de la page.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public event EventHandler Click;
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "NextRadio.Popups"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************