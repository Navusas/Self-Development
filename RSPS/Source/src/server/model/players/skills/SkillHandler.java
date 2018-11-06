package server.model.players.skills;

import server.model.players.Client;

/**
 * @author Domantas Giedraitis (student id: u1757704 )
 * @version 1
 * @since 2018-11-05
 */
public class SkillHandler {
    public static final String SKILLS_IN_STRING[] = {"attack","defence","strength","hitpoints","range","prayer","magic","cooking"
            ,"woodcutting","fletching","fishing","firemaking","crafting","smithing","mining",
            "herbloring","agility","thieving","slayer","runecrafting","hunting","summonning",
            "PK'ing","Dungeoneering"};

    public String getRandomInventoryFull_Message() {
        String[] inventoryIsFull = {"Not enough space in inventory",
                "Your inventory is full",
                "Please empty your inventory first",
                "There is no space in your inventory",
                "All space used in your inventory"};

        // randomly select one of the following options
        return (inventoryIsFull[new java.util.Random().nextInt(inventoryIsFull.length)]);
    }
    public String getRandomFastClicking_Message() {
        String[] fastClicking = {"You need to wait for a moment before attempting again",
                "Slow down a little bit !",
                "Speed does not always mean fast!",
                "There is no way to be quicker here",
                "Allow couple seconds before trying again"};

        // randomly select one of the following options
        return (fastClicking[new java.util.Random().nextInt(fastClicking.length)]);
    }
    public int freeSlots(Client c) {
        int freeS = 0;
        for (int i = 0; i < c.playerItems.length; i++) {
            if (c.playerItems[i] <= 0) {
                freeS++;
            }
        }
        return freeS;
    }
}
