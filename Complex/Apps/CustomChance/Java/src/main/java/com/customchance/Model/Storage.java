package com.customchance.Model;

import java.util.List;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.util.LinkedList;

public class Storage {
    public int Selection;

    public List<Member> Members;

    public Storage() {
        Members = new LinkedList<Member>();
        deserialize();
    }

    public void serialize() {
        String path = "save.data";

        try {
            FileOutputStream fos = new FileOutputStream(path);
            ObjectOutputStream oos = new ObjectOutputStream(fos);
            oos.writeObject(Members);
            oos.flush();
            oos.close();
        } catch (Exception e) {
            System.out.println("exception while try serializing.");
        }
    }

    public void deserialize() {
        String path = "save.data";
        try {
            FileInputStream fis = new FileInputStream(path);
            ObjectInputStream oin = new ObjectInputStream(fis);
            Members = (LinkedList<Member>) oin.readObject();
        } catch (Exception ex) {
            System.out.println("exception while try deserializing.");
        }
    }
}
