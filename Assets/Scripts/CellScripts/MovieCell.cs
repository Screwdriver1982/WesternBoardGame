using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovieCell : Cell
{
    public override bool CheckCellStartingChoose()
    {


        if (GameManager.Instance.WhoIsPlayerMVMNT().turnMiss > 0)
        {
            return false;

        }
        else
        {
            bool suitablePlayer = GameManager.Instance.AreThereAnyPlayerForMovie();
            if (suitablePlayer)
            {
                UIManager.Instance.ShowMovieWindow( cellIcon,
                                                    cellTitle,
                                                    cellDescription,
                                                    cellWayTitle);
            }
            return suitablePlayer;
            
        }


    }
}
