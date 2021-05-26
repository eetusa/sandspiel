using UnityEngine;
public struct cell 
{
   public byte value {get;set;}
   public byte extradata;

   public int x {get;set;}
   public int y {get;set;}
   public bool flaggedOnOdd;
   public cell[,] array;



   public cell(int x, int y, byte value, cell[,] array, bool oddRunner, byte extradata = 0){
       this.x = x;
       this.y = y;
       this.value = value;
       this.array = array;
        this.extradata = extradata;
        this.flaggedOnOdd = oddRunner;

   }

   public void update(bool odd){

       switch (this.value){
            case 0:
                break;
            case 1:
                updateSand(odd);
                break;
            case 2:
                break;
            case 3:
                updateWater(odd);
                break;
       }
        
        this.flaggedOnOdd = odd;
       
       }

       void updateSand(bool odd){

            // if (array[x, y-1].value == 0){
            //     array[x, y-1].value = this.value;
            //     array[x, y-1].extradata = this.extradata;
            //     array[x, y-1].flaggedOnOdd = odd;
            //     this.empty();
            //     return;
            // } 

            int pseudoRandomSign = (odd == true ? 1 : -1); 

            if (array[x, y-1].value == 0){

                byte tempval = array[x, y-1].value;
                byte tempdata = array[x, y-1].extradata;

                array[x, y-1].value = this.value;
                array[x, y-1].extradata = this.extradata;
                array[x, y-1].flaggedOnOdd = odd;

                this.value = tempval;
                this.extradata = tempdata;

                
               // this.empty();
                return;
            } else if (array[x, y-1].value == 3){
                if (array[x+pseudoRandomSign, y].value == 0){
                    byte tempval = array[x, y-1].value;
                    byte tempdata = array[x, y-1].extradata;

                    array[x, y-1].value = this.value;
                    array[x, y-1].extradata = this.extradata;
                    array[x, y-1].flaggedOnOdd = odd;

                    array[x+pseudoRandomSign, y].value = tempval;
                    array[x+pseudoRandomSign, y].extradata = tempdata;
                //    array[x+pseudoRandomSign, y].flaggedOnOdd = odd;

                    this.empty();
                    return;
                    
                } else if (array[x-pseudoRandomSign, y].value == 0){
                    byte tempval = array[x, y-1].value;
                    byte tempdata = array[x, y-1].extradata;

                    array[x, y-1].value = this.value;
                    array[x, y-1].extradata = this.extradata;
                    array[x, y-1].flaggedOnOdd = odd;

                    array[x-pseudoRandomSign, y].value = tempval;
                    array[x-pseudoRandomSign, y].extradata = extradata;
               //     array[x-pseudoRandomSign, y].flaggedOnOdd = odd;

                    this.empty();
                    return;
                }
            }

            

            if (array[x-pseudoRandomSign, y-1].value == 0 || array[x-pseudoRandomSign, y-1].value == 3){

                byte tempval = array[x-pseudoRandomSign, y-1].value;
                byte tempdata = array[x-pseudoRandomSign, y-1].extradata;
                
                array[x-pseudoRandomSign, y-1].value = this.value; 
                array[x-pseudoRandomSign, y-1].extradata = this.extradata;
                array[x-pseudoRandomSign, y-1].flaggedOnOdd = odd;

                this.value = tempval;
                this.extradata = tempdata;
                return;

            }

       }

       void updateWater(bool odd){
            if (array[x, y-1].value == 0){
                array[x, y-1].value = this.value;
                array[x, y-1].extradata = this.extradata;
                array[x, y-1].flaggedOnOdd = odd;
                this.empty();
                return;
            } 
            int pseudoRandomSign = (odd == true ? 1 : -1); 

            if (array[x-pseudoRandomSign, y-1].value == 0){
                array[x-pseudoRandomSign, y-1].value = this.value; 
                array[x-pseudoRandomSign, y-1].extradata = this.extradata;
                array[x-pseudoRandomSign, y-1].flaggedOnOdd = odd;
                this.empty();
                return;
            }

            if (array[x+pseudoRandomSign, y-1].value == 0){
                array[x+pseudoRandomSign, y-1].value = this.value; 
                array[x+pseudoRandomSign, y-1].extradata = this.extradata;
                array[x+pseudoRandomSign, y-1].flaggedOnOdd = odd;
                this.empty();
                return;
            }

            if (array[x-pseudoRandomSign, y].value == 0){
                array[x-pseudoRandomSign, y].value = this.value;
                array[x-pseudoRandomSign, y].extradata = this.extradata;
                array[x-pseudoRandomSign, y].flaggedOnOdd = odd;
                this.empty();
                return;
            }

            if (array[x+pseudoRandomSign, y].value == 0){
                array[x+pseudoRandomSign, y].value = this.value;
                array[x+pseudoRandomSign, y].extradata = this.extradata;
                array[x+pseudoRandomSign, y].flaggedOnOdd = odd;
                this.empty();
                return;
            }
       }

        public void empty(){
            this.value = 0;
            this.extradata = 0;
        }
       void copyCell(cell originCell, cell targetCell){
           targetCell.value = originCell.value;
           targetCell.extradata = originCell.extradata;
       }
//        if (oddRun){
//             if (array[x, y-1].value == 128){
//                 array[x, y-1].value = this.value;
//                 array[x, y-1].modifyBit(0);
//                 this.value = 0;
//                 return;
//             } 
//             if (array[x-1, y-1].value == 128){
//                     array[x-1, y-1].value = this.value;
//                     array[x-1, y-1].modifyBit(0);
//                     this.value = 0;
//                     return;
//             }
//             if (array[x+1, y-1].value == 128){
//                     array[x+1, y-1].value = this.value;
//                     array[x+1, y-1].modifyBit(0);
//                     this.value = 0;
//                     return;
//             }
//        } else {
//             if (array[x, y-1].value == 0){
//                 array[x, y-1].value = this.value;
//                 array[x, y-1].modifyBit(1);
//                 this.value = 0;
//                 return;
//             } 
//             if (array[x-1, y-1].value == 0){
//                     array[x-1, y-1].value = this.value;
//                     array[x-1, y-1].modifyBit(1);
//                     this.value = 128;
//                     return;
//             }
//             if (array[x+1, y-1].value == 0){
//                     array[x+1, y-1].value = this.value;
//                     array[x+1, y-1].modifyBit(1);
//                     this.value = 0;
//                     return;
//             }
//        }
//    }

   public void changeMSB(){
        if (this.IsMSBOne()){
           // int mask = 1 << 7;
           // value = (byte) ((value & ~mask) | ((0 << 7) & mask));
            value = (byte) (value - 128);
        } else {
           // int mask = 1 << 7;
          //  value = (byte) ((value & ~mask) | ((1 << 7) & mask));
            value = (byte) (value + 128);
        }
   }

   public bool IsMSBOne(){
       if (value > 127) return true;
       return false;
   }

   public void modifyBit(int b)
{

    int mask = 1 << 7;
    value = (byte) ((value & ~mask) | ((b << 7) & mask));

}
    
}