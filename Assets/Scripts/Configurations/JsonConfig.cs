using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Linq;

public static class JsonConfig {
    public static bool hasInitialized = false;
    private static JObject config;

    private static void ensureInitialization() {
        if (hasInitialized) 
            return;
        loadFile();
        hasInitialized = true;
    }

    private static string getFileName() {
        return Application.dataPath + "/../config.json";
    }

    private static void saveFile() {
        File.WriteAllText(getFileName(), config.ToString());
    }

    private static void loadFile() {
        if (File.Exists(getFileName())) 
            config = JObject.Parse(File.ReadAllText(getFileName()));
        else {
            config = new JObject();
            saveFile();
        }
    }

    public static void DeleteAll() {
        ensureInitialization();
        config.RemoveAll();
        saveFile();
    }
    public static void DeleteKey(string key) {
        ensureInitialization();
        config.Remove(key);
        saveFile();
    }

    public static bool HasKey(string key) {
        ensureInitialization();
        return config.ContainsKey(key);
    }

    public static void SetBoolean(string key, bool boolean) {
        ensureInitialization();
        config[key] = boolean;
        saveFile();
    }
    public static void SetString(string key, string text) {
        ensureInitialization();
        config[key] = text;
        saveFile();
    }
    public static void SetInt(string key, int number) {
        ensureInitialization();
        config[key] = number;
        saveFile();
    }
    public static void SetDouble(string key, double number) {
        ensureInitialization();
        config[key] = number;
        saveFile();
    }
    public static void SetFloatArray(string key, float[] numbers) {
        ensureInitialization();
        config[key] = JArray.FromObject(numbers);
        saveFile();
    }
    public static void SetVector3(string key, Vector3 vector) {
        SetFloatArray(key, new float[] { vector.x, vector.y, vector.z });
    }
    public static void SetQuaternion(string key, Quaternion quaternion) {
        SetFloatArray(key, new float[] { quaternion.x, quaternion.y, quaternion.z, quaternion.w });
    }

    public static bool GetBoolean(string key) {
        ensureInitialization();
        return config.Value<bool>(key);
    }
    public static string GetString(string key) {
        ensureInitialization();
        return config.Value<string>(key);
    }
    public static int GetInt(string key) {
        ensureInitialization();
        return config.Value<int>(key);
    }
    public static double GetDouble(string key) {
        ensureInitialization();
        return config.Value<double>(key);
    }
    public static float[] GetFloatArray(string key) {
        ensureInitialization();
        return config.Value<JArray>(key).ToObject<float[]>();
    }
    public static Vector3 GetVector3(string key) {
        return new Vector3(GetFloatArray(key)[0], GetFloatArray(key)[1], GetFloatArray(key)[2]);
    }
    public static Quaternion GetQuaternion(string key) {
        return new Quaternion(GetFloatArray(key)[0], GetFloatArray(key)[1], GetFloatArray(key)[2], GetFloatArray(key)[3]);
    }
}
