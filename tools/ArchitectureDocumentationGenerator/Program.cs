// See https://aka.ms/new-console-template for more information
using ArchitectureDocumentationGenerator;

if(args.Length != 2)
{
  Console.WriteLine("Usage: ArchitectureDocumentationGenerator <assemblyFilesPath> <outputPath>");
  return;
}

ArchitectureGenerator.GenerateComponentDiagrams(args[0], args[1]);

ArchitectureGenerator.GenerateDocumentationFile(@"..\..\..\..\..\src\SFC.Tests\bin\debug\net6.0\", @"..\..\..\..\..\docs\ArchitectureDocumentation\");

