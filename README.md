# Hackney.Shared.Person
At Hackney, we have created NuGet Packages to prevent the duplication of common code when implementing our APIs.
This NuGet package will store the shared code related to a person that can then be used in the relevant projects. 

#### GitHub Actions Pipeline - Versioning
The pipeline automatically updates the package version number.

Version numbers use the following format:

Any specific version number follows the form Major.Minor.Patch[-Suffix], where the components have the following meanings:

* *Major*: Breaking changes
* *Minor*: New features, but backward compatible
* *Patch*: Backwards compatible bug fixes only
* *Suffix (optional)*: a hyphen followed by a string denoting a pre-release version

## Using the package
For full details on how to use the package(s) within this repository please read 
[this wiki page](https://github.com/LBHackney-IT/lbh-core/wiki/Using-the-package(s)-from-the-Hackney.Core-repository).