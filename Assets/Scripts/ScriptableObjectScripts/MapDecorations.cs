using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/MapDecorationList")]
public class MapDecorations : ScriptableObject
{
    [SerializeField]
    public Material plainsTileMaterial;
    [SerializeField]
    public List<GameObject> plainsDecoration;
    [SerializeField]
    public Material taigaTileMaterial;
    [SerializeField]
    public List<GameObject> taigaDecorations;
    [SerializeField]
    public Material fungalTileMaterial;
    [SerializeField]
    public List<GameObject> fungalDecorations;
    [SerializeField]
    public Material forestTileMaterial;
    [SerializeField]
    public List<GameObject> forestDecorations;
    [SerializeField]
    public Material tundraTileMaterial;
    [SerializeField]
    public List<GameObject> tundraDecorations;
    [SerializeField]
    public Material desertTileMaterial;
    [SerializeField]
    public List<GameObject> desertDecorations;
}
