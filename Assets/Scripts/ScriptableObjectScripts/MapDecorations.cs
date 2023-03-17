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
    [Space]
    [SerializeField]
    public Material taigaTileMaterial;
    [SerializeField]
    public List<GameObject> taigaDecorations;
    [Space]
    [SerializeField]
    public Material fungalTileMaterial;
    [SerializeField]
    public List<GameObject> fungalDecorations;
    [Space]
    [SerializeField]
    public Material forestTileMaterial;
    [SerializeField]
    public List<GameObject> forestDecorations;
    [Space]
    [SerializeField]
    public Material tundraTileMaterial;
    [SerializeField]
    public List<GameObject> tundraDecorations;
    [Space]
    [SerializeField]
    public Material desertTileMaterial;
    [SerializeField]
    public List<GameObject> desertDecorations;
    [Space]
    [SerializeField]
    public Material volcanicTileMaterial;
    [SerializeField]
    public List<GameObject> volcanicDecorations;
    [Space]
    [SerializeField]
    public Material savannaTileMaterial;
    [SerializeField]
    public List<GameObject> savannaDecorations;
}
