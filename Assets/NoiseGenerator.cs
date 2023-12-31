using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoiseGenerator : MonoBehaviour
{
    ComputeBuffer _weightsBuffer;
    public ComputeShader NoiseShader;


    private void Awake() {
        CreateBuffers();
    }


    private void OnDestroy() {
        ReleaseBuffers();
    }


    void CreateBuffers() {
        _weightsBuffer = new ComputeBuffer(GridMetrics.PointsPerChunk * GridMetrics.PointsPerChunk * GridMetrics.PointsPerChunk, sizeof(float));
    }


    void ReleaseBuffers() {
        _weightsBuffer.Release();
    }


    public float[] GetNoise() {
        float[] noiseValues = new float[GridMetrics.PointsPerChunk * GridMetrics.PointsPerChunk * GridMetrics.PointsPerChunk];
        NoiseShader.SetBuffer(0, "_Weights", _weightsBuffer);
        NoiseShader.Dispatch(0, GridMetrics.PointsPerChunk / GridMetrics.NumThreads, GridMetrics.PointsPerChunk / GridMetrics.NumThreads, GridMetrics.PointsPerChunk / GridMetrics.NumThreads);
        _weightsBuffer.GetData(noiseValues);
        return noiseValues;
    }
}
