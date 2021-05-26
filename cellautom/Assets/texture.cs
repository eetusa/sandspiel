using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class texture : MonoBehaviour
{
    List<float> red;
    List<float> green;
    List<float> blue;
    public int pattern = 0;
    public string nameOfFile;
    Texture2D screen;
    public int IMG_WIDTH = 640;
    public int IMG_HEIGHT = 480;
    public GameObject thing;

    int addType = 1;

    int runner = 0;

    bool paused = false;

    private bool oddRunner;
    private int oddController = 0;

    List<Color32> sandColors;
    List<Color32> waterColors;

    Color drawcolor = new Color(255, 0, 0);
    Color sandcolor = new Color(30,30,0);
    Color bgcolor = new Color(50,50,50);
    cells cellArray;

    // Start is called before the first frame update
    void Start()
    {
        Screen.SetResolution(IMG_WIDTH, IMG_HEIGHT, false);
      //  screen = new Texture2D (IMG_WIDTH, IMG_HEIGHT);
        //screen = new Texture2D (IMG_WIDTH, IMG_HEIGHT, TextureFormat.ARGB32, false);
        screen = new Texture2D (IMG_WIDTH, IMG_HEIGHT, TextureFormat.RGBA32, false);
       // thing.GetComponent<Image>().sprite = Sprite.Create(screen, new Rect(0,0,IMG_WIDTH, IMG_HEIGHT), new Vector2(0.5f,0.5f));
       thing.GetComponent<Image>().sprite = Sprite.Create(screen, new Rect(0,0,IMG_WIDTH, IMG_HEIGHT), new Vector2(0,0));

        oddRunner = false;
        cellArray = new cells(IMG_WIDTH, IMG_HEIGHT);
        initiateColors();
       
    }

    // Update is called once per frame
    void Update()
    {
        runner++;
        if (Input.GetKeyUp(KeyCode.Return)){
          print(cellArray.cellCount());
            
        }
        
        if (Input.GetKey(KeyCode.Mouse0)){
            if (runner > 1){
                runner = 0;
              //  cellArray.addCell((int)Input.mousePosition.x, (int)Input.mousePosition.y, 1);
              if (addType == 0){
                    removeCells((int)Input.mousePosition.x, (int)Input.mousePosition.y, 10, oddRunner);
              } else {
                    addCells((int)Input.mousePosition.x, (int)Input.mousePosition.y, addType, 10, 2, oddRunner);

              }
               
            }
        }

         if (Input.GetKey(KeyCode.Alpha0)){
            addType = 0;
        }

        if (Input.GetKey(KeyCode.Alpha1)){
            addType = 1;
        }

        if (Input.GetKey(KeyCode.Alpha2)){
            addType = 2;
        }

        if (Input.GetKey(KeyCode.Alpha3)){
            addType = 3;
        }

        if (Input.GetKey(KeyCode.Alpha4)){
            addType = 4;
        }

        if (Input.GetKey(KeyCode.Alpha5)){
            addType = 5;
        }

        if (Input.GetKey(KeyCode.Mouse2)){
           // cellArray.addCell((int)Input.mousePosition.x, (int)Input.mousePosition.y, 2);
           addCells((int)Input.mousePosition.x, (int)Input.mousePosition.y, 2, 30, 1);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow)){
            cellArray.iterateCells(oddRunner);
           // oddRunner = !oddRunner;
            oddController++;
            if (oddController == 1) oddRunner = !oddRunner;
            if (oddController == 3) {oddRunner = !oddRunner; oddController = 0;}
        } else if (!paused){
            cellArray.iterateCells(oddRunner);
           // oddRunner = !oddRunner;
            oddController++;
            if (oddController == 1) oddRunner = !oddRunner;
            if (oddController == 3) {oddRunner = !oddRunner; oddController = 0;}
        }

        if (Input.GetKeyUp(KeyCode.P)){
            paused = !paused;
        }
        

        drawTexture2();

    }

    void removeCells(int corner_x, int corner_y, int amount, bool oddRun){

        double sqrtOfAmount = System.Math.Sqrt(amount);
        double floorOfSqrt = System.Math.Floor(sqrtOfAmount)+1;

        System.Random rand = new System.Random();
        int runner = 0;
        for (int i = 0; i < floorOfSqrt; i++){
            for (int j = 0; j < floorOfSqrt; j++){
                cellArray.array[corner_x+i,corner_y+j].value = 0;
                cellArray.array[corner_x+i,corner_y+j].extradata = 0;
                runner++;
                if (runner >= amount) return;                
            }
        }
       // print(floorOfSqrt);
    }

    void addCells(int corner_x, int corner_y, int type, int amount, int spacing, bool oddRun = false){
        double sqrtOfAmount = System.Math.Sqrt(amount);
        double floorOfSqrt = System.Math.Floor(sqrtOfAmount)+1;
        System.Random rand = new System.Random();
        int runner = 0;
        for (int i = 0; i < floorOfSqrt; i++){
            for (int j = 0; j < floorOfSqrt; j++){
                if (type==1){
                    int val  = rand.Next(sandColors.Count);
                    cellArray.addCell(corner_x + spacing*i,  corner_y + spacing*j, (byte)type, oddRun, (byte)val);
                } else if (type == 3){
                    int val  = rand.Next(waterColors.Count);
                    cellArray.addCell(corner_x + spacing*i,  corner_y + spacing*j, (byte)type, oddRun, (byte)val);
                }else {
                    cellArray.addCell(corner_x + spacing*i,  corner_y + spacing*j, (byte)type);
                }
                
                runner++;
                if (runner >= amount) return;                
            }
        }
       // print(floorOfSqrt);
    }

    void thinkTheArt(int type){
        red = new List<float>();
        green = new List<float>();
        blue = new List<float>();
        

        switch (type){
            case(0):
            default:
                for (int i = 0; i < IMG_WIDTH; i++){
                    for (int id = 0; id < IMG_HEIGHT; id++){
                        red.Add (Random.Range (0.0f, 1.0f));
                        green.Add (Random.Range (0.0f, 1.0f));
                        blue.Add (Random.Range (0.0f, 1.0f));
                    }
                }
                break;
            case(1):
            for (int i = 0; i < IMG_WIDTH; i++){
                for (int id = 0; id < IMG_HEIGHT; id++){
                        red.Add((float)id / IMG_HEIGHT);
                        green.Add(0);
                        blue.Add(0);
                }
            }
            break;
        }
    }


    void drawScreen(Color color){
        screen = new Texture2D (IMG_WIDTH, IMG_HEIGHT, TextureFormat.ARGB32, false);
        for (int i = 0; i < IMG_WIDTH; i++){
            for (int j = 0; j < IMG_HEIGHT; j++){
                screen.SetPixel(i,j, color);

            }
        }
        screen.Apply();
        thing.GetComponent<Image>().sprite = Sprite.Create(screen, new Rect(0,0,IMG_WIDTH, IMG_HEIGHT), new Vector2(0.5f,0.5f));
    }

    void doTheArt(){
        screen = new Texture2D (IMG_WIDTH, IMG_HEIGHT, TextureFormat.ARGB32, false);

        float[] redArr = red.ToArray();
        float[] greenArr = green.ToArray();
        float[] blueArr = blue.ToArray();

        for (int i = 0; i < IMG_WIDTH; i++){
            for (int j = 0; j < IMG_HEIGHT; j++){
                screen.SetPixel(i,j, new Color (redArr[i*IMG_HEIGHT+j], greenArr[i*IMG_HEIGHT + j], blueArr[i * IMG_HEIGHT +j]));

            }
        }
        screen.Apply();
        thing.GetComponent<Image>().sprite = Sprite.Create(screen, new Rect(0,0,IMG_WIDTH, IMG_HEIGHT), new Vector2(0.5f,0.5f));

    }

    void drawTexture2Test(){
       // var finalpixels = new Color[IMG_WIDTH * IMG_HEIGHT];
        GetComponent<Renderer>().material.mainTexture = screen;

        var data = screen.GetRawTextureData<Color32>();

        Color32 orange = new Color32(255, 165, 0, 255);
        Color32 teal = new Color32(0, 128, 128, 255);

        int index = 0;
        for (int y = 0; y < screen.height; y++)
        {
            for (int x = 0; x < screen.width; x++)
            {
                data[index++] = ((x & y) == 0 ? orange : teal);
            }
        }
        // upload to the GPU
        screen.Apply();

    }

    void initiateColors(){

        sandColors = new List<Color32>();

        sandColors.Add(new Color32(190, 152, 95, 255));
        sandColors.Add(new Color32(227, 181, 113, 255));
        sandColors.Add(new Color32(198, 158, 99, 255));
        sandColors.Add(new Color32(203, 162, 101, 255));
        sandColors.Add(new Color32(179, 144, 90, 255));

        waterColors = new List<Color32>();
        
        waterColors.Add(new Color32(87, 140, 219, 255));
        waterColors.Add(new Color32(90, 145, 226, 255));
        waterColors.Add(new Color32(73, 117, 183, 255));
        waterColors.Add(new Color32(76, 122, 190, 255));
        waterColors.Add(new Color32(60, 130, 170, 255));
        
    }

    void drawTexture2(){
    
        GetComponent<Renderer>().material.mainTexture = screen;

        var data = screen.GetRawTextureData<Color32>();

        Color32 orange = new Color32(255, 165, 0, 255);
        Color32 teal = new Color32(0, 128, 128, 255);
        Color32 white = new Color32(255,255,255, 255);

        int index = 0;
        for (int y = 0; y < screen.height; y++)
        {
            for (int x = 0; x < screen.width; x++)
            {
                cell cell = cellArray.array[x,y];
                if (cell.value == 1){
                    data[index++] = sandColors[cell.extradata];
                } else if (cell.value == 2){
                    data[index++] = teal;
                } else if (cell.value == 3) {
                    data[index++] = waterColors[cell.extradata];
                } else {
                    data[index++] = white;
                }
                
            }
        }
        // upload to the GPU
        screen.Apply();

    }

    void drawOnTexture(){
        //float mousex = Input.mousePosition.x;
        //float mousey = Input.mousePosition.y;
        
        int width = cellArray.width;
        int height = cellArray.height;

        for (int i = 1; i < width + 1; i++){
            for (int j = height; j > 0; j--){
                if (cellArray.array[i,j].value == 1){
                    screen.SetPixel(i,j, drawcolor);
                } else if (cellArray.array[i,j].value == 2){
                    screen.SetPixel(i,j, sandcolor);
                } else if (cellArray.array[i,j].value == 0){
                    screen.SetPixel(i,j, bgcolor);
                } 
            }
        }
       // screen.SetPixel((int)mousex,(int)mousey, drawcolor);
        screen.Apply();
    }

}
