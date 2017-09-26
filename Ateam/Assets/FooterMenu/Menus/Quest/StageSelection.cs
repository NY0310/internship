using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * 具体的なステージ制作設計ができてから実装する。 
 * 基本的には、CSVなどでステージデータを管理することを想定している
 * 
 * 読み込んだ情報を元に自動でステージセレクションを作成。
 * タッチされたら、CSVのステージ情報をバトルシーンに与え、その通りに実行させる。
 * 
 * Starは、Imageをオン、オフをフラグに応じて切り替える形に
 * キャラクターは、一応ブラー用のImageを配置してあるが、デザイナー側でブラー付きのキャラ画像があるとプログラマーは楽
 * 要は、Unity側でコントロールするか、外部ツールで操作するかの違い
 * 
 * テキストは、CSVから読み込んだデータを元に自動で書き換える。
 * 
 * 現在は仮の処理として、タップされたらバトルシーンに飛ぶだけとする。
 * 
 * 最終的には、リストはスクロールできるようにすべきだが、今回はステージ数が１つということで気にしない
 * 
 */


public class StageSelection : MonoBehaviour {

    public void ClickedEvent()
    {
        CurrentStageData.Data = GetComponent<StageData>();
        GetComponent<FooterSceneChange>().ClickedEvent();
    }
}
