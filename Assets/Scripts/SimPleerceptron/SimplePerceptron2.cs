using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimplePerceptron2 : MonoBehaviour
{
    // 重みとバイアス
    private float[] _weights = { 0.0f, 0.0f ,0.0f}; // 3つの入力に対する重み
    private float _bias = 0.0f; // バイアス項

    // 学習率
    private float _learningRate = 0.1f;

    void Start()
    {
        // _weightsと_biasに-1から1の範囲でランダムな値を与える
        for (int i = 0; i < _weights.Length; i++)
        {
            _weights[i] = Random.Range(-1.0f, 1.0f);
        }

        _bias = Random.Range(-1.0f, 1.0f);

        // ANDゲートの学習データ (入力, 出力)
        float[,] inputs = {
            { 0f, 0f, 0f},
            { 0f, 1f, 0f },
            { 1f, 0f, 0f },
            { 1f, 1f, 0f },
            { 0f, 0f, 1f},
            { 0f, 1f, 1f },
            { 1f, 0f, 1f },
            { 1f, 1f, 1f }
        };
        float[] outputs = { 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f }; // ANDゲートの出力

        // 学習
        Train(inputs, outputs, 100);

        // 学習後にパーセプトロンをテスト
        Test(inputs);
    }

    // 学習メソッド
    void Train(float[,] inputs, float[] outputs, int epochs)
    {
        for (int epoch = 0; epoch < epochs; epoch++)
        {
            for (int i = 0; i < inputs.GetLength(0); i++)
            {
                float[] input = { inputs[i, 0], inputs[i, 1], inputs[i, 2] };
                float target = outputs[i];

                // 出力を計算
                float output = CalculateOutput(input);

                // エラー計算
                float error = target - output;

                // 重みとバイアスの更新
                for (int j = 0; j < input.Length; j++)
                {
                    _weights[j] += _learningRate * error * input[j];
                }
                _bias += _learningRate * error;
            }
        }
    }

    // 出力を計算するメソッド
    float CalculateOutput(float[] input)
    {
        var total = input.Zip(_weights, (a, b) => a * b).Sum();
        total += _bias;

        // 活性化関数 (階段関数)
        return ActivationFunction(total);
    }

    // 階段関数 (活性化関数)
    float ActivationFunction(float sum)
    {
        return sum >= 0 ? 1f : 0f; // 0以上なら1、負の値なら0
    }

    // 学習後のテスト
    void Test(float[,] inputs)
    {
        Debug.Log("Testing after training:");
        Debug.Log($"Train result weight[0] = {_weights[0]}, weight[1] = {_weights[1]}, weight[2] = {_weights[2]}, bias = {_bias}");

        for (int i = 0; i < inputs.GetLength(0); i++)
        {
            float[] input = { inputs[i, 0], inputs[i, 1], inputs[i, 2] };
            float output = CalculateOutput(input);
            Debug.Log($"Input: {input[0]}, {input[1]}, {input[2]} -> Output: {output}");
        }
    }
}
