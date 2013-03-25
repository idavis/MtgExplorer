### Building the Source
  - Execute bootstrap.ps1. This will look for Nuget.exe on your path. If it is not found, it will download the latest version. Once Nuget.exe is available, it will download all packages to the lib folder.

### Neo4j Config
in the conf folder open neo4j.properties and make sure these values are set (they may be commented out)
  - node_auto_indexing=true
  - node_keys_indexable=name
  - relationship_auto_indexing=true
  - relationship_keys_indexable=name