using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cells
{
    public int width;
    public int height;

    public bool oddRun;
    public cell[,] array;
    public cells(int width, int height){
        this.width = width;
        this.height = height;
        array = new cell[width+2,height+2];
        oddRun = false;
        initArray();
    }

    public void addCell(int x, int y, byte value, bool oddRunner = false, byte extradata = 0){
        if (array[x, y].value == 0){
            array[x, y] = new cell(x,y,value,array, !oddRunner, extradata);
        }
    }

    void initArray(){
        for (int i = 0; i < width + 2; i++){
            addCell(i,0,2);
            addCell(i,height+1,2);
        }
        for (int i = 0; i < height + 2; i++){
            addCell(0,i,2);
            addCell(width+1, i, 2);
        }
        for (int i = 1; i < width + 1; i++){
            for (int j = 1; j < height + 1; j++){
                addCell(i,j,0, false, 0);
            }
        }
    }

    public int cellCount(){
        int temp = 0;
        for (int i = 1; i < width + 1; i++){
            for (int j = 1; j < height + 1; j++){
                if (array[i,j].value == 1) {
                    Debug.Log("x,y: " + array[i,j].x + " " + array[i,j].y);
                    temp++;
                }
               
            }
        }
    return temp;
    }

    // public void iterateCells(bool oddRunner){
    //     for (int i = 1; i < width + 1; i++){
    //         for (int j = height; j > 0; j--){
                
    //             // if (array[i,j].value == 1){
    //             //     Debug.Log("1 cell value: " + array[i,j].value + ", oddRunner: " + oddRunner + ", flaggedonodd: "+array[i,j].flaggedOnOdd + " onecount: " + cellCount() + "coord: " + array[i,j].x + " " +array[i,j].y);
    //             // }
    //             if (array[i,j].flaggedOnOdd == !oddRunner){
    //                // if (array[i,j].value == 1){
    //                     array[i,j].update(oddRunner);
    //                     //  Debug.Log("2 cell value: " + array[i,j].value + ", oddRunner: " + oddRunner + ", flaggedonodd: "+array[i,j].flaggedOnOdd + " onecount: " + cellCount() + "coord: " + array[i,j].x + " " +array[i,j].y);
    //                // }
    //             }
    //         }
    //     }
    // }

    public void iterateCells(bool oddRunner){
        for (int i = 1; i < width + 1; i++){
            for (int j = 1; j < height + 1; j++){
                
                // if (array[i,j].value == 1){
                //     Debug.Log("1 cell value: " + array[i,j].value + ", oddRunner: " + oddRunner + ", flaggedonodd: "+array[i,j].flaggedOnOdd + " onecount: " + cellCount() + "coord: " + array[i,j].x + " " +array[i,j].y);
                // }
                if (array[i,j].flaggedOnOdd == !oddRunner){
                    
                        array[i,j].update(oddRunner);
                        //  Debug.Log("2 cell value: " + array[i,j].value + ", oddRunner: " + oddRunner + ", flaggedonodd: "+array[i,j].flaggedOnOdd + " onecount: " + cellCount() + "coord: " + array[i,j].x + " " +array[i,j].y);
                    
                }
            }
        }
    }
    
}
