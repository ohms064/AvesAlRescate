using System;
using System.Collections.Generic;
using UnityEngine;

public struct IndexLocation {
    public float Index { get; set; }
    public GPSCoordinate Loc { get; set; }

    public IndexLocation( float i, GPSCoordinate t ) : this() {
        Index = i;
        Loc = t;
    }
}

/// <summary>
/// Tools for the AI module of the simulator.
/// </summary>
/// 
public class LocationProximityTools {

    /// <summary>
    /// Sorts a List of Transforms by closest distance from another Transform position.
    /// </summary>
    /// <param name="origin">The Transform which will be referenced to.</param>
    /// <param name="destiny">The list of Transform which will be sorted.</param>
    /// <returns>A sorted array with the Transforms in destiny.</returns>
    public static GPSCoordinate[] SortByProximity( GPSCoordinate origin, List<GPSCoordinate> destiny ) {
        GPSCoordinate[] sortedArray = new GPSCoordinate[destiny.Count];
        List<IndexLocation> indexList = new List<IndexLocation>();
        int i = 0;

        foreach ( GPSCoordinate itervar in destiny ) {//Get the values for MinimumDistance for each Transform in destiny
            indexList.Add( new IndexLocation( MinimumDistanceCondition( origin, itervar ), itervar ) );
        }

        indexList.Sort( delegate ( IndexLocation index1, IndexLocation index2 ) { //Sort in descendant order
            if ( index1.Index > index2.Index )
                return -1;
            else
                return 1;
        } );
        foreach ( IndexLocation itervar in indexList ) { // Create an
            sortedArray[i] = itervar.Loc;
            i++;
        }
        return sortedArray;
    }

    public static void SortByProximity( GPSCoordinate origin, ref GPSCoordinate[] destiny ) {
        float[] keys = new float[destiny.Length];
        for ( int i = 0; i < destiny.Length; i++ ) {
            keys[i] = MinimumDistanceCondition( origin, destiny[i] );
        }
        Array.Sort( keys, destiny );
        Array.Reverse( destiny );
    }

    /// <summary>
    /// Sorts a List of Transforms by fartest distance from another Transform position.
    /// </summary>
    /// <param name="origin">The Transform which will be referenced to.</param>
    /// <param name="destiny">The list of Transform which will be sorted.</param>
    /// <returns>A sorted array with the Transforms in destiny.</returns>
    public static GPSCoordinate[] SortByRemoteness( GPSCoordinate origin, List<GPSCoordinate> destiny ) {
        GPSCoordinate[] sortedArray = new GPSCoordinate[destiny.Count];
        List<IndexLocation> indexList = new List<IndexLocation>();
        int i = 0;

        foreach ( GPSCoordinate itervar in destiny ) {//Get the values for MinimumDistance for each Transform in destiny
            indexList.Add( new IndexLocation( MinimumDistanceCondition( origin, itervar ), itervar ) );
        }

        indexList.Sort( delegate ( IndexLocation index1, IndexLocation index2 ) { //Sort in descendant order
            if ( index1.Index < index2.Index )
                return -1;
            else
                return 1;
        } );
        foreach ( IndexLocation itervar in indexList ) { // Create an
            sortedArray[i] = itervar.Loc;
            i++;
        }
        return sortedArray;
    }

    public static void SortByRemoteness( GPSCoordinate origin, ref GPSCoordinate[] destiny ) {
        float[] keys = new float[destiny.Length];
        for ( int i = 0; i < destiny.Length; i++ ) {
            keys[i] = MinimumDistanceCondition( origin, destiny[i] );
        }
        Array.Sort( keys, destiny );
    }

    /// <summary>
    /// Get the transform from an array that is closest from an origin.
    /// </summary>
    /// <param name="origin">The transformm that will be referenced.</param>
    /// <param name="destiny">The array of transforms that will be searched.</param>
    /// <returns>The closest transform to origin.</returns>
    public static GPSCoordinate GetClosest( GPSCoordinate origin, GPSCoordinate[] destiny ) {
        float[] keys = new float[destiny.Length];
        for ( int i = 0; i < destiny.Length; i++ ) {
            keys[i] = MinimumDistanceCondition( origin, destiny[i] );
        }
        return destiny[GetMaxIndex( keys )];
    }

    /// <summary>
    /// Get the transform from an array that is farthest from an origin.
    /// </summary>
    /// <param name="origin">The transformm that will be referenced.</param>
    /// <param name="destiny">The array of transforms that will be searched.</param>
    /// <returns>The farthest transform to origin.</returns>
    public static GPSCoordinate GetFarthest( GPSCoordinate origin, GPSCoordinate[] destiny ) {
        float[] keys = new float[destiny.Length];
        for ( int i = 0; i < destiny.Length; i++ ) {
            keys[i] = MinimumDistanceCondition( origin, destiny[i] );
        }
        return destiny[GetMinIndex( keys )];
    }

    /// <summary>
    /// The value to determine if other position is close to an origin position, the higher the value the closer they are.
    /// </summary>
    /// <remarks>The equation is: d(x) = x'z - 0.5(z'z); where x = origin and z = other and both are vectors and x' and z' are the transposed vectors.</remarks>
    /// <param name="origin">The origin position. For multiple other positions this should be the reference value.</param>
    /// <param name="other">The position that will be compared.</param>
    /// <returns>Value of the formula.</returns>
    public static float MinimumDistanceCondition( GPSCoordinate origin, GPSCoordinate other ) {
        return (float)(GPSCoordinate.Dot( origin, other ) - 0.5f * GPSCoordinate.Dot( other, other ));
    }

    /// <summary>
    /// Gets the index of an array of floats which has the biggest value.
    /// </summary>
    /// <param name="array">The array to search.</param>
    /// <returns>The index of an array which has the max value.</returns>
    private static int GetMaxIndex( float[] array ) {
        float max = array[0];
        int index = 0;
        for ( int i = 1; i < array.Length; i++ ) {
            if ( array[i] > max ) {
                max = array[i];
                index = i;
            }
        }
        return index;
    }

    /// <summary>
    /// Gets the index of an array of floats which has the lowest value.
    /// </summary>
    /// <param name="array">The array to search.</param>
    /// <returns>The index of an array which has the lowest value.</returns>
    private static int GetMinIndex( float[] array ) {
        float min = array[0];
        int index = 0;
        for ( int i = 1; i < array.Length; i++ ) {
            if ( array[i] < min ) {
                min = array[i];
                index = i;
            }
        }
        return index;
    }
}