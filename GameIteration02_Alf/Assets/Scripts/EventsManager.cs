﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour {
	/*
	 * Event Cards
	 * 1. King's Recoginition
	 * - This next player(s) to complete a Quest will receive 2 extra shields.
	 * 2. Queen's Favor
	 * - The lowest rank player(s) immediately receives 2 Adventure Cards.
	 * 3. Court Called to Camelot
	 * - All Allies in play must be discarded.
	 * 4. Pox
	 * - All players except the player drawing this card lose 1 shield.
	 * 5. Plague
	 * - Drawer loses 2 shields if possible.
	 * 6. Chivalrous Deed
	 * - Player(s) with both lowest rank and least amount of shields, receives 3 shields.
	 * 7. Prosperity Throughout the Realm
	 * - All players may immediately draw 2 Adventure Cards.
	 * 8. King's Call to Arms
	 * - The Highest Ranked Player(s) must place 1 weapon in the discard pile. If unable to do so 2 Foe Cards must be discarded.
	 *
	 * Shields
	 * Allys
	 * Adventure Cards
	 * Weapon Cards
	 * 2 Foe Cards
	*/

	// - This next player(s) to complete a Quest will receive 2 extra shields.
	public void Kings_Recoginition(uint id){
		Debug.Log("EventsManager:: Kings_Recoginition :: setting shields for " + id);
		User user = GameObject.Find("PlayerObject(Clone)" + id).GetComponent<User>();
		int shields = user.getShields() + 2;
		user.setShields(shields);
	}

	// 2. Queen's Favor
	// - The lowest rank player(s) immediately receives 2 Adventure Cards.
	public List<GameObject> Queens_Favor(Users players){
		Debug.Log("Queens Favor has been called " );
		List<GameObject> lowestRankPlayers = players.getLowestRankUser();
		return lowestRankPlayers;
	}
	// 3. Court Called to Camelot
	// - All Allies in play must be discarded.
	public void Court_Called_To_Camelot(Users players){
		Debug.Log ("EventsManager.cs :: Event :: Running Court Called To Camelot.");
		// Call Hand Manager from each user to discard all cards.
		foreach (GameObject i in players.GetUsers()) {
			string rank = i.GetComponent<User> ().getRank ();
			// int baseAttack = i.GetComponent<User> ().getbaseAttack ();
			// if (rank == "Squire") {
			// 	i.GetComponent<User> ().setBaseAttack (5);
			// } else if (rank == "Knight") {
			// 	i.GetComponent<User> ().setBaseAttack (10);
			// } else if (rank == "Champion Knight") {
			// 	i.GetComponent<User> ().setBaseAttack (20);
			// }
		}
	}
	// 4. Pox
	// - All players except the player drawing this card lose 1 shield.
	public void Pox(User player, Users players){
		foreach (GameObject i in players.GetUsers()) {
			int shields = i.GetComponent<User> ().getShields ();
			if(!player.Equals(i.GetComponent<User>())){
				if (shields != 0) {
					i.GetComponent<User> ().setShields (shields - 1);
				}
			}
		}
	}
	// 5. Plague
	// - Drawer loses 2 shields if possible.
	public void Plague(User player){
		int shields = player.getShields ();
		if (shields != 0) {
			player.setShields (shields - 2);
		}
	}
	// 6. Chivalrous Deed
	// - Player(s) with both lowest rank and least amount of shields, receives 3 shields.
	public void Chivalrous_Deed(User player, Users players){
		List<GameObject> lowestRankPlayer = players.getLowestRankUser();
		foreach (GameObject i in lowestRankPlayer) {
			int shields = i.GetComponent<User>().getShields () ;
			i.GetComponent<User>().setShields (shields + 3);
		}
	}

	// 7. Prosperity Throughout the Realm
	// - All players may immediately draw 2 Adventure Cards.
	public List<GameObject> Prosperity_Throughout_The_Realm(Users players){
		List<GameObject> allPlayers = players.GetUsers();
		return allPlayers ;
	}
	// 8. King's Call to Arms
	// - The Highest Ranked Player(s) must place 1 weapon in the discard pile. If unable to do so 2 Foe Cards must be discarded.
	public List<GameObject> Kings_Call_To_Arms(Users players){
		List<GameObject> highestRankPlayer = players.getHighestRankUser();
		// highestRankPlayer.DiscardWeaponFunction();
		return highestRankPlayer;
	}

}
