﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
using UnityEngine.UI;

public class User : NetworkBehaviour {
	public int shields;
	public int baseAttack;
	public int bids;
	public int totalBP;
	public int AllyBattlePoints;

	public string rank;
	public string username;
	public uint id;

	public List<AdventureCard> AlliesInHand;
	public List<AdventureCard> TournmanetCards;
	public int TourniBP;

	protected Sprite Squire;
	protected Sprite Knight;
	protected Sprite ChampionKnight;
	// public // logger // logger;


	// Use this for initialization
	void Start () {
		// logger = GameObject.Find("// loggerManager").GetComponent<// logger>().// logger;

		this.id = netId.Value;
		this.username 			  = "player_" + netId.Value;
		this.rank 				  = "Squire";
		this.totalBP 		 	  = 0;
		this.baseAttack			  = 5;
		this.AllyBattlePoints	  = 0;
		this.shields			  = 3;
		this.totalBP 			  = 0;
		Debug.Log ("username : " + username);
		// logger.info ("User.cs :: Start() :: Creating player " + username);

	}

	// Update is called once per frame
	void Update () {

	}
	public void CheckCardInHand(){
		if (GameObject.Find("HandCanvas").transform.childCount > 12) {
			// Debug.Log ("Too Many Cards In Hand");
		}
	}

	public uint GetNetID(){
		return this.id;
	}

	//*********************************************************************//
		//						GET METHODS                         //

		public int numOfcards(){
			GameObject hand = GameObject.Find ("HandCanvas"+netId.Value);
			return hand.transform.childCount;
		}
		public List<GameObject> getCardsInHand(){
			List<GameObject> CIH = new List<GameObject> ();
			GameObject hand = GameObject.Find ("HandCanvas"+netId.Value);
			int HandCount = hand.transform.childCount;
			for(int counter=0;counter<HandCount;counter++){
				CIH.Add(hand.transform.GetChild (counter).gameObject);
			}
			return CIH;
		} //End of get cards in hand.

		public GameObject getHand(){
			foreach (Transform temp in transform) {
				if (temp.name == "Hand") {
					return temp.gameObject;
					}
			}
			return null;
		}	//End of get Hand

		public List<AdventureCard> getAllies(){
			return AlliesInHand;
		}//End of Get Allies


		public string GetUsername(){
			return username;
		}//End of Get username

		public int getShields(){
			// logger.info ("User.cs :: getShields() :: " + this.shields);
			return this.shields;
		}// End of get shields

		public int getBaseAttack(){
			// logger.info ("User.cs :: getBaseAttack() :: " + this.baseAttack);

			return this.baseAttack;
		}//End of Get Base Attack

		public string getRank(){

			return this.rank;
		}
		public int getTotalBattlePoints(){
			// logger.info ("User.cs :: getTotalBattlePoints() :: " + this.totalBP);

			return this.totalBP;
		}
		public void UpdateRank(){

			if (12 > this.shields && this.shields >= 5) {
				//// logger.info ("User.cs :: Ranking Up: " + this.username);
				this.rank = "Knight";
				this.baseAttack = 5;
				//// logger.info("User.cs :: BaseATTACK:" + this.baseAttack);
				this.gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = Knight;

			} else if (22 > this.shields && this.shields >= 12) {
				//// logger.info ("User.cs :: Ranking Up: " + this.username);
				this.gameObject.transform.GetChild (1).GetComponent<Image> ().sprite = ChampionKnight;
				this.baseAttack = 10;
				//// logger.info("User.cs :: BaseATTACK:" + this.baseAttack);
				this.rank = "Champion Knight";

			} else if (this.shields >= 22) {
				//	// logger.info ("User.cs :: Ranking Up: " + this.username);
				this.rank = "Knight Of the Round Table";
				this.baseAttack = 20;
				//// logger.info("User.cs :: BaseATTACK:" + this.baseAttack);
			}

			//this.gameObject.transform.GetChild(4).GetComponent<Text>().text =  ("Rank: " + this.rank);

		}//end of updating rank
		public int getAllyBattlePoints(){

			int returnPoints = 0;
			foreach (AdventureCard CurrentCard in this.AlliesInHand){
				returnPoints+= CurrentCard.getBattlePoints();
			}
			//// logger.info ("User.cs :: getAllyBattlePoints function has been called for Player:  " + this.user_name + " BattlePoints: " + totalBattlePoints);
			//	// logger.info ("User.cs :: getAllyBattlePoints function has been called for Player:  " + this.user_name + " New BattlePoints: " + totalBattlePoints);
			// logger.info ("User.cs :: getAllyBattlePoints() :: " + returnPoints);

			return returnPoints;
		}
		public List<AdventureCard> GetTournmanetCards(){
			return TournmanetCards;

		}

		public void SetTourni(AdventureCard Temp){
			// logger.info ("User.cs :: SetTourni() :: " + Temp.getName());

			TournmanetCards.Add(Temp);
		}

		public int getTourniBP(){
			// logger.info ("User.cs :: getTourniBP() :: " + TourniBP);

			return TourniBP;
		}
		public void setTourniBP(int points){
			// logger.info ("User.cs :: setTourniBP() :: " + points);

			TourniBP = points;
		}





		//*****************************************************************************************************//
			//											Set Functions


		public void setName(string GivenUsername){
			this.username=GivenUsername;
		}//End of setName


		public void setShields(int NewShieldAmount){
			// logger.info ("User.cs :: setShields() :: " + NewShieldAmount);

			//this.gameObject.transform.GetChild(5).GetComponent<Text>().text =  ("Shields: " + this.shields);
			this.shields = NewShieldAmount;
		}

		public void setTotalBattlePoints(){

		this.totalBP = getAllyBattlePoints()+ baseAttack;
		// logger.info ("User.cs :: setTotalBattlePoints() :: " + this.totalBP);

			//if (isServer) 	{RpcSetBP ((int)netId.Value,getAllyBattlePoints()+baseAttack);}
			//else 	{CmdSetBP((int)netId.Value,getAllyBattlePoints()+baseAttack));}
		}//end of Setting total battle points

		public void setbids(AdventureCard CurrentAlly){
			AlliesInHand.Add(CurrentAlly);
				foreach (AdventureCard CurrentCard in this.AlliesInHand){
			bids+=CurrentCard.getBidPoints();
			}
		}

		// [Command]
		// public void CmdSetBP(int id, int score){
		// 	RpcSetBP(id, score);
		// }
		//
		// [ClientRpc]
		// public void RpcSetBP(int id,int score){
		// 	GameObject.Find("PlayerObject(Clone)"+id).GetComponent<User>().setTotalBattlePointstotalBP(score);
		// }


		//***********************************************************************************************//
				// Remove and Add Allies//
		public void PlayAllies(AdventureCard CurrentAlly){
		AlliesInHand.Add(CurrentAlly);
			setTotalBattlePoints();
		//	// logger.info ("User.cs :: addAlly function has been called for Player:  " + this.user_name + " Adding Ally: " + Ally.getName());
		//// logger.info ("User.cs :: addAlly function has been called for Player:  " + this.user_name + " Ally Battle Points: " + Ally.getBonusBattlePoints());
		}//End of playallies
		// public void SetOtherBP(){
		// 	if (isServer) 	{RpcSetBP ((int)netId.Value,getAllyBattlePoints()+baseAttack);}
		// 	else 	{CmdSetBP((int)netId.Value,getAllyBattlePoints()+baseAttack));}
		// }
		//
		// [Command]
		// public void CmdSetOtherBP(){
		//
		// }
		//
		// [ClientRpc]
		// public void RpcSetOtherBP(){
		//
		// }
		/*
		public void removeAllies(AdventureCard AllyToBeRemoved){
			foreach(AdventureCard CurrentCard int AlliesInHand){
			//	if(CurrentCard.getName()==ally){
					AlliesInHand.remove(CurrentCard);
				//}
			}
		}//End of removeAllies

		*/
}
