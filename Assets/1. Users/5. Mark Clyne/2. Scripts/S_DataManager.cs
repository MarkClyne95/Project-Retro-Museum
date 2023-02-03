using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class S_DataManager : MonoBehaviour
{
    [DllImport("rust_num_management")]
    public static extern int add_sum(int a, int b);
    
    [DllImport("rust_num_management")]
    public static extern int double_num(int a);
    
    [DllImport("rust_num_management")]
    public static extern int triple_num(int a);
    
    [DllImport("rust_num_management")]
    public static extern int rnd_num(int upperRange, int lowerRange);
}
