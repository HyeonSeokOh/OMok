using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateDoll : MonoBehaviour
{
    protected GameObject Object_Name;
    //Main

    public enum CostType
    {
        Cost01, Cost02, Cost03, Cost04, Cost05
    }

    //흑돌 생성함수
    public void CreateBlackdoll(CostType BlackcostType)
    {
        if (GameManager.Instance.mainTurn == 1) // 흑돌 놓을 차례에만 가능
        {
            if (DollMove.Instance.Xup >= TileManager.Instance.BoardSizeX || DollMove.Instance.Xup < 0 ||
                    DollMove.Instance.Yup >= TileManager.Instance.BoardSizeY || DollMove.Instance.Yup < 0)  // 범위 넘어가도 리턴임
            { return; }
            else if (TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].RullTile != 0)
            { return; }   // 이미 돌이 있으면 못 놓음
            else if (TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].RullTile == 0)
            {
                if (GameManager.Instance.BuilDollstate == false)
                {
                    switch (BlackcostType)
                    {
                        case CostType.Cost01:
                            GameManager.Instance.MainCost -= 1;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case CostType.Cost02:
                            GameManager.Instance.MainCost -= 2;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case CostType.Cost03:
                            GameManager.Instance.MainCost -= 3;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case CostType.Cost04:
                            GameManager.Instance.MainCost -= 4;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case CostType.Cost05:
                            GameManager.Instance.MainCost -= 5;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                    }
                    AudioManager.Instance.DollSt(0);
                    GameManager.Instance.BuilDollstate = true;  // 돌 더 놓지 못도로 막음.
                }
            }
        }
        else if (GameManager.Instance.mainTurn != 1)
        {
            // 중복 돌놓기 금지 텍스트
        }
    }

    //백돌 생성 함수
    public void CreateWhitedoll(CostType WhitecostType)
    {
        if (GameManager.Instance.mainTurn == 3)
        {
            if (TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].RullTile != 0) { return; }

            else if (TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].RullTile == 0)
            {
                if (GameManager.Instance.BuilDollstate == false)
                {
                    switch (WhitecostType)
                    {
                        case CostType.Cost01:
                            GameManager.Instance.MainCost -= 1;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case CostType.Cost02:
                            GameManager.Instance.MainCost -= 2;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case CostType.Cost03:
                            GameManager.Instance.MainCost -= 3;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case CostType.Cost04:
                            GameManager.Instance.MainCost -= 4;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                        case CostType.Cost05:
                            GameManager.Instance.MainCost -= 5;
                            Instantiate(Object_Name, new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                            ShowTileEffect();
                            break;
                    }
                    AudioManager.Instance.DollSt(0);
                    GameManager.Instance.BuilDollstate = true;  // 돌 더 놓지 못도로 막음.
                }
            }
        }
        else if (GameManager.Instance.mainTurn != 3)
        {
            // 중복 돌놓기 금지 텍스트
        }
    }

    //이펙트 표현 함수
    public void ShowTileEffect()
    {
        if (TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].RullTile == 0)
        {
            if(TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].TileAttribute == 1)
            {
                GameObject EffectTile = Instantiate(GameManager.Instance.Tile_Effect[0], new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                EffectTile.AddComponent<DestroyAni>();
            }
            else if (TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].TileAttribute == 2)
            {
                GameObject EffectTile = Instantiate(GameManager.Instance.Tile_Effect[1], new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                EffectTile.AddComponent<DestroyAni>();
            }
            else if (TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].TileAttribute == 3)
            {
                GameObject EffectTile = Instantiate(GameManager.Instance.Tile_Effect[2], new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                EffectTile.AddComponent<DestroyAni>();
            }
            else if (TileManager.TileMain[DollMove.Instance.Xup, DollMove.Instance.Yup].TileAttribute == 4)
            {
                GameObject EffectTile = Instantiate(GameManager.Instance.Tile_Effect[3], new Vector3(DollMove.Instance.Xup, DollMove.Instance.Yup, -1), Quaternion.identity);
                EffectTile.AddComponent<DestroyAni>();
            }
        }
    }


}
