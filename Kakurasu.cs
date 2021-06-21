using System;
using static System.Console;

namespace Bme121
{
    static class Program
    {
        static bool useBoxDrawingChars = true;
        static string[ ] letters = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l" };
        static int boardSize = 4; // must be in the range 1..12.

        static double cellMarkProb = 0.2;
        static Random rGen = new Random( );

        static void Main( )
        {
            // TO DO: Game initialization
            bool [ , ] userBoard = new bool [ boardSize, boardSize ];
            bool [ , ] hiddenBoard = new bool [ boardSize, boardSize ];
            bool sameValues = true;
            bool gameWin = false;
            int[ ] userRowSums = new int[ boardSize ];
            int[ ] userColSums = new int[ boardSize ];
            int[ ] hiddenRowSums = new int[ boardSize ];
            int[ ] hiddenColSums = new int[ boardSize ];
            
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if(rGen.NextDouble( ) < cellMarkProb ) hiddenBoard[ i, j ] = true;
                }
            }
            
            //calculate hidden sums
            
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    if ( hiddenBoard [ i , j ] == true)
                    {
                        hiddenRowSums[ i ] += j + 1;
                        hiddenColSums[ j ] += i + 1;
                    }
                }
            }
            


            // This is the main game-play loop.

            bool gameNotQuit = true;
            while( gameNotQuit == true )
            {
                Console.Clear( );
                WriteLine( );
                WriteLine( "    Play Kakurasu!" );
                WriteLine( );
                
                if( useBoxDrawingChars )
                {
                    sameValues = true;
                    
                    // This is the userboard code
                    for( int row = 0; row < boardSize; row ++ )
                    {
                        if( row == 0 )
                        {
                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( "  {0} ", letters[ col ] );
                            WriteLine( );

                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " {0,2} ", col + 1 );
                            WriteLine( );

                            Write( "        \u250c" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u252c" );
                            WriteLine( "\u2500\u2500\u2500\u2510" );
                        }

                        Write( "   {0} {1,2} \u2502", letters[ row ], row + 1 );

                        for( int col = 0; col < boardSize; col ++ )
                        {
                            if( userBoard[ row, col ] == true ) Write( " X \u2502" );
                            else                                  Write( "   \u2502" );
                        }
                        
                        Write(     "{0,3}", userRowSums   [ row ] );
                        WriteLine( "{0,3}", hiddenRowSums [ row ] );
                        


                        if( row < boardSize - 1 )
                        {
                            Write( "        \u251c" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u253c" );
                            WriteLine( "\u2500\u2500\u2500\u2524" );
                        }
                        else
                        {
                            Write( "        \u2514" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u2534" );
                            WriteLine( "\u2500\u2500\u2500\u2518" );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " {0:d2} ", userColSums [ col] );
                            WriteLine( );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                            Write( " {0:d2} ", hiddenColSums [ col] );                            
                            WriteLine( );
                        }
                    }
            
                }
                else // ! useBoxDrawingChars
                {
                    for( int row = 0; row < boardSize; row ++ )
                    {
                        if( row == 0 )
                        {
                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( "  {0} ", letters[ col ] );
                            WriteLine( );

                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " {0,2} ", col + 1 );
                            WriteLine( );

                            Write( "        +" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "---+" );
                            WriteLine( "---+" );
                        }

                        Write( "   {0} {1,2} |", letters[ row ], row + 1 );

                        for( int col = 0; col < boardSize; col ++ )
                        {
                            if( row == 1 && col == 1 ) Write( " X |" );
                            else                       Write( "   |" );
                        }
                        WriteLine( " 00 00 " );

                        if( row < boardSize - 1 )
                        {
                            Write( "        +" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "---+" );
                            WriteLine( "---+" );
                        }
                        else
                        {
                            Write( "        +" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "---+" );
                            WriteLine( "---+" );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " 00 " );
                            WriteLine( );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " 00 " );
                            WriteLine( );
                        }
                    }
                }

                // Get the next move.

                WriteLine( );
                WriteLine( "   Toggle cells to match the row and column sums." );
                Write(     "   Enter a row-column letter pair or 'quit': " );
                string response = ReadLine( );

                if( response == "quit" ) gameNotQuit = false;
                else
                {
                    if( response.Length == 2 )
                    {
                        string rowPick = response.Substring( 0, 1 );
                        string colPick = response.Substring( 1, 1 );
                        
                        // going from the letters for rows and cols to numbers
                        int rowNum = Array.IndexOf( letters, rowPick );
                        int colNum = Array.IndexOf( letters, colPick );
                        
                        if( rowNum >= 0 && rowNum < boardSize && colNum >= 0 && colNum < boardSize)
                        {
                            if( userBoard [rowNum, colNum ] == true) 
                            {
                                userBoard [rowNum, colNum ] = false;
                                userRowSums[ rowNum ] -= colNum + 1;
                                userColSums[ colNum ] -= rowNum + 1;
                            }
                            else 
                            {
                                userBoard [ rowNum, colNum ] = true;  
                                userRowSums[ rowNum ] += colNum + 1;
                                userColSums[ colNum ] += rowNum + 1;
                            }                    
                        }
                    }
                }

                for (int i = 0; i < boardSize; i++)
                {
                    if (userRowSums[i] != hiddenRowSums[i] || userColSums [i] != hiddenColSums[i])
                    {
                        sameValues = false;
                        break;
                    }
                }
                
                if (sameValues == true)
                {
                    gameWin = true;
                    break;
                }
            }
            
            if (gameWin == true)
            { // displaying the winning gameboard
                for( int row = 0; row < boardSize; row ++ )
                    {;
                        if( row == 0 )
                        {
                            WriteLine();
                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( "  {0} ", letters[ col ] );
                            WriteLine( );

                            Write( "        " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " {0,2} ", col + 1 );
                            WriteLine( );

                            Write( "        \u250c" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u252c" );
                            WriteLine( "\u2500\u2500\u2500\u2510" );
                        }

                        Write( "   {0} {1,2} \u2502", letters[ row ], row + 1 );

                        for( int col = 0; col < boardSize; col ++ )
                        {
                            if( userBoard[ row, col ] == true ) Write( " X \u2502" );
                            else                                  Write( "   \u2502" );
                        }
                        
                        Write(     "{0,3}", userRowSums   [ row ] );
                        WriteLine( "{0,3}", hiddenRowSums [ row ] );
                        


                        if( row < boardSize - 1 )
                        {
                            Write( "        \u251c" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u253c" );
                            WriteLine( "\u2500\u2500\u2500\u2524" );
                        }
                        else
                        {
                            Write( "        \u2514" );
                            for( int col = 0; col < boardSize - 1; col ++ )
                                Write( "\u2500\u2500\u2500\u2534" );
                            WriteLine( "\u2500\u2500\u2500\u2518" );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                                Write( " {0:d2} ", userColSums [ col] );
                            WriteLine( );

                            Write( "         " );
                            for( int col = 0; col < boardSize; col ++ )
                            Write( " {0:d2} ", hiddenColSums [ col] );                            
                            WriteLine( );
                        }
                    }
                WriteLine ();
                WriteLine ("You completed this game of Kakurasu, Congratulations!!!");
            }

            WriteLine( );
        }
    }
}
