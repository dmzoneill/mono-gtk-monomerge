# MonoMerge

A Mono GTK# tool for joining AVI video files.

## What it does

You pick the AVI files to join and where to save the result. MonoMerge runs
`avimerge` from transcode and shows its progress output. The job can be started
and stopped.

## Tech

- C# with Mono and GTK#
- `avimerge` (transcode)

## Building

Open the `.sln` file in MonoDevelop, or build it with Mono and run the resulting
`.exe` with `mono`. Requires GTK# 2.

## License

Licensed under the Apache License 2.0. See [LICENSE](LICENSE).
