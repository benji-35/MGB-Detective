using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "directory", menuName = "Computer/terminal/Create directory")]
public class directory : ScriptableObject
{
    public string nameDir;
    public List<fileTerm> files = new List<fileTerm>();
    public List<directory> dirs = new List<directory>();
}
