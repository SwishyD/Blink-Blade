﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyDeath
{
     void OnHit();
    IEnumerator Respawn();
}
