//*******************************************************************************************************************************
// DEBUT DU FICHIER
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// Nom           : DragEventArgs.cs
// Auteur        : Nicolas Dagnas
// Description   : Impl�mentation de l'objet DragEventArgs
// Cr�� le       : 17/01/2015
// Modifi� le    : 17/01/2015
//*******************************************************************************************************************************

//-------------------------------------------------------------------------------------------------------------------------------
#region Using directives
//-------------------------------------------------------------------------------------------------------------------------------
using System;
using System.Windows;
//-------------------------------------------------------------------------------------------------------------------------------
#endregion
//-------------------------------------------------------------------------------------------------------------------------------

//*******************************************************************************************************************************
// D�but du bloc "System.Windows.Phone.Gestures"
//*******************************************************************************************************************************
namespace System.Windows.Phone.Gestures
	{

	//  ####   ####    ###    ###  
	//  #   #  #   #  #   #  #   # 
	//  #   #  ####   #####  #     
	//  #   #  #   #  #   #  #   ##
	//  ####   #   #  #   #   ### #

	//***************************************************************************************************************************
	// Classe DragEventArgs
	//***************************************************************************************************************************
	#region // D�claration et Impl�mentation de l'Objet
	//---------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Impl�mentation de la gestion des mouvements.
	/// </summary>
	//---------------------------------------------------------------------------------------------------------------------------
	public class DragEventArgs : EventArgs
		{
		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>DragEventArgs</b>.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		internal DragEventArgs () {}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Initialise une nouvelle instance de l'objet <b>DragEventArgs</b>.
		/// </summary>
		/// <param name="Args">Ev�nement d'origine.</param>
		//-----------------------------------------------------------------------------------------------------------------------
		internal DragEventArgs ( InputDeltaArgs Args )
			{
			//-------------------------------------------------------------------------------------------------------------------
			if ( Args != null )
				{
				//---------------------------------------------------------------------------------------------------------------
				this.CumulativeDistance = Args.CumulativeTranslation;
				this.DeltaDistance      = Args.DeltaTranslation;
				this.Origin             = Args.Origin;
				//---------------------------------------------------------------------------------------------------------------
				}
			//-------------------------------------------------------------------------------------------------------------------
			}
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// D�finit l'�v�nement comme une terminaison.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public void MarkAsFinalTouchManipulation () { this.IsTouchComplete = true; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens la valeur cumul�e.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public Point CumulativeDistance { get; internal set; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens la valeur delta.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public Point DeltaDistance { get; private set; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Indique si l'�v�nement est une terminaison.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public bool IsTouchComplete { get; private set; }
		//***********************************************************************************************************************

		//***********************************************************************************************************************
		/// <summary>
		/// Obtiens la valeur d'origine.
		/// </summary>
		//-----------------------------------------------------------------------------------------------------------------------
		public Point Origin { get; private set; }
		//***********************************************************************************************************************
		}
	//---------------------------------------------------------------------------------------------------------------------------
	#endregion
	//***************************************************************************************************************************

	} // Fin du namespace "System.Windows.Phone.Gestures"
//*******************************************************************************************************************************

//*******************************************************************************************************************************
// FIN DU FICHIER
//*******************************************************************************************************************************
