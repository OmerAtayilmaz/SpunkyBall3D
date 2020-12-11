using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchClass 
{
    private int award_id;
    private int award_durum;
    private int award_miktar;

    public AchClass()
    {

    }
    public AchClass(int award_id, int award_durum, int award_miktar)
    {
        this.Award_id = award_id;
        this.Award_durum = award_durum;
        this.Award_miktar = award_miktar;
    }

    public int Award_id { get => award_id; set => award_id = value; }
    public int Award_durum { get => award_durum; set => award_durum = value; }
    public int Award_miktar { get => award_miktar; set => award_miktar = value; }


}
