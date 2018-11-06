package server.model.players.skills;

import server.Config;
import server.util.Misc;
import server.model.players.Client;
import server.Server;

/**
 * Thieving.java
 *
 * @author Balla_
 **/

public class Thieving {

    private Client c;
    private SkillHandler skillHandler;
    private boolean stillWaiting = false;

    public Thieving(Client c) {
        this.c = c;
        this.skillHandler = new SkillHandler();
        c.lastThieve = System.currentTimeMillis();
    }

    public void stealFromNPC(int id) {
        if (System.currentTimeMillis() - c.lastThieve < 2000)
            return;
        for (int[] aNpcThieving : npcThieving) {
            if (aNpcThieving[0] == id) {
                if (c.playerLevel[c.playerThieving] >= aNpcThieving[1]) {
                    if (Misc.random(c.playerLevel[c.playerThieving] + 2 - aNpcThieving[1]) != 1) {
                        c.getPA().addSkillXP(aNpcThieving[2] * Config.THIEVING_EXPERIENCE, c.playerThieving);
                        c.getItems().addItem(995, aNpcThieving[3]);
                        c.startAnimation(881);
                        c.lastThieve = System.currentTimeMillis();
                        c.sendSystemMessage("You thieve some money...");
                        break;
                    } else {
                        c.setHitDiff(aNpcThieving[4]);
                        c.setHitUpdateRequired(true);
                        c.playerLevel[3] -= aNpcThieving[4];
                        c.getPA().refreshSkill(3);
                        c.lastThieve = System.currentTimeMillis() + 2000;
                        c.sendSystemMessage("You fail to thieve the NPC.");
                        break;
                    }
                } else {
                    c.sendSystemMessage("You need a thieving level of " + aNpcThieving[1] + " to thieve from this NPC.");
                }
            }
        }
    }

    public void stealFromStall(int id, int xp, int level) {
        // wait 2.5 seconds after last thieve
        if ((System.currentTimeMillis() - c.lastThieve) < 2500)
            return;

        if (c.playerLevel[c.playerThieving] >= level) { // if level is higher then required
            if (Misc.random(12) == 1) {
                c.sendSystemMessage("You get caught trying to thieve the stall..");
                c.startAnimation(3679);
                if (c.playerLevel[3] <= 30) {
                    appendHit(Misc.random(4), c);
                } else {
                    appendHit(Misc.random(10), c);
                }
            }
            else if (skillHandler.freeSlots(c) > 0) { // if there is space in inventory
                c.sendSystemMessage("You attempt to steal something from the stall...");
                c.getItems().addItem(id, 1);
                c.startAnimation(832);
                //c.getItems().addItem(995, c.playerLevel[c.playerThieving] * 150);
                c.getPA().addSkillXP(xp * Config.THIEVING_EXPERIENCE, c.playerThieving);
                c.sendSystemMessage("You steal a " + server.model.items.Item.getItemName(id) + ".");
            } else { // if inventory is full
                c.sendSystemMessage(skillHandler.getRandomInventoryFull_Message());
            }
        } else {
            c.sendSystemMessage("You need a thieving level of " + level + " to thief from this stall.");
        }
        c.lastThieve = System.currentTimeMillis();
    }

    @SuppressWarnings("static-access")
    public static void appendHit(int damage, Client c) {
        Server.playerHandler.players[c.playerId].setHitDiff(damage);
        Server.playerHandler.players[c.playerId].playerLevel[3] -= damage;
        c.getPA().refreshSkill(3);
        Server.playerHandler.players[c.playerId].setHitUpdateRequired(true);
        Server.playerHandler.players[c.playerId].updateRequired = true;
    }

    //npc, level, exp, coin amount
    public int[][] npcThieving = {{1, 1, 8, 200, 1}, {18, 25, 26, 500, 1}, {9, 40, 47, 1000, 2}, {26, 55, 85, 1500, 3}, {20, 70, 152, 2000, 4}, {21, 80, 273, 3000, 5}};


}