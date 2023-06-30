using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    // Start is called before the first frame update
    public void SetMaxSKillBar(int skill)
    {
        slider.maxValue = skill;
        slider.value = skill;
    }
    public void SetSkill(int skill)
    {
        slider.value = skill;
    }
}
