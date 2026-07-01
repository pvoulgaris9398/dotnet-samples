#!/usr/bin/env -S dotnet run

#:package BenchmarkDotNet@0.15.8
#:property Lang=csharp
#:property AllowUnsafeBlocks=true
#:property Optimize=true
#:property Configuration=Release

using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs; // Required for InProcess
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Toolchains.InProcess.NoEmit;
using ScriptInDockerSample;

// 1. Create a config using the No-Emit toolchain
var config = ManualConfig
    .CreateMinimumViable()
    .AddJob(Job.Default.WithToolchain(InProcessNoEmitToolchain.Instance));

var summary = BenchmarkRunner.Run<MemoryCopyBenchmarks>(config);

namespace ScriptInDockerSample
{
    // The magic attribute that stops BDN from looking for a .csproj file
    [InProcess]
    [MemoryDiagnoser]
    public class MemoryCopyBenchmarks
    {
        private byte[] _source = null!;
        private byte[] _destination = null!;

        [Params(32, 256, 4096, 1_048_576)]
        public int Size { get; set; }

        [GlobalSetup]
        public void Setup()
        {
            _source = new byte[Size];
            _destination = new byte[Size];

            Random.Shared.NextBytes(_source);
        }

        [Benchmark(Baseline = true)]
        public void SpanCopyTo()
        {
            _source.AsSpan().CopyTo(_destination);
        }

        [Benchmark]
        public void ManualLoop()
        {
            for (var i = 0; i < _source.Length; i++)
            {
                _destination[i] = _source[i];
            }
        }

        [Benchmark]
        public void ArrayCopy()
        {
            Array.Copy(_source, _destination, _source.Length);
        }

        [Benchmark]
        public void BufferBlockCopy()
        {
            Buffer.BlockCopy(_source, 0, _destination, 0, _source.Length);
        }

        [Benchmark]
        public unsafe void BufferMemoryCopy()
        {
            fixed (byte* src = _source)
            fixed (byte* dst = _destination)
            {
                Buffer.MemoryCopy(src, dst, _destination.Length, _source.Length);
            }
        }

        [Benchmark]
        public unsafe void UnsafeCopyBlockUnaligned()
        {
            fixed (byte* src = _source)
            fixed (byte* dst = _destination)
            {
                Unsafe.CopyBlockUnaligned(dst, src, (uint)_source.Length);
            }
        }
    }
}
