import java.util.Arrays;

import java.util.*;
public class Test {
    public static void main(String[] args){
        int[][] twoD = { {1, 2, 3},
                 {4, 5, 6},
                 {7, 8, 9} };
        twoD[2][1] = 12;
        System.out.println(Arrays.toString(twoD));
    }
}