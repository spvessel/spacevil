package com.customchance.Model;

import java.util.LinkedList;
import java.util.List;
import java.util.Random;

public class CommonLogic {
    static CommonLogic _instance;

    private CommonLogic() {
        Storage = new Storage();
    }

    public static CommonLogic getInstance() {
        if (_instance != null)
            return _instance;
        return _instance = new CommonLogic();
    }

    public Storage Storage;

    public void trySerialize() {
        Storage.serialize();
    }

    public void tryDeserialize() {
        Storage.deserialize();
    }

    public void startRandom(List<Member> members) {
        randomChoise(members);
        int max = 0;
        int index = 0;
        for (int i = 0; i < members.size(); i++) {
            if (max < members.get(i).value) {
                index = i;
                max = members.get(i).value;
            }
        }
        members.get(index).isWinner = true;
    }

    private void randomChoise(List<Member> variants) {
        // preparation
        if (variants.size() == 0)
            return;
        for (Member item : variants) {
            item.value = 0;
            item.isWinner = false;
        }

        // randomizing
        Random rnd = new Random();
        for (int i = 0; i < 100; i++) {
            int index = rnd.nextInt(variants.size());
            variants.get(index).value++;
        }
    }

    public boolean addMember(List<Member> members, String name) {
        if (members == null)
            members = new LinkedList<Member>();

        if (name.equals(""))
            return false;

        for (Member item : members) {
            if (item.name.equals(name))
                return false;
        }
        Member m = new Member();
        m.name = name;
        m.value = 0;
        m.isWinner = false;
        members.add(m);
        return true;
    }

    public void deleteMember(List<Member> members, String selected) {
        for (Member item : members) {
            if (item.name.equals(selected)) {
                members.remove(item);
                return;
            }
        }
    }

    public void printMembersStat() {
        for (Member item : Storage.Members) {
            System.out.println(item.name + " " + item.value + " " + item.isWinner);
        }
    }
}
