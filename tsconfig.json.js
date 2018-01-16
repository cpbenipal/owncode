{
  "compilerOptions": {
    "allowJs": true,            // These settings apply to .js files as well as .ts files
    "noEmit":  true             // Do not compile the JS (or TS) files in this project on build
  },
  "exclude": [
    "node_modules",             // Don't include any JavaScript found under "node_modules"
    "Scripts/Office/1"          // Suppress loading all the .js files from the Office NuGet package
  ],
  "typeAcquisition": {
    "enable": true,             // Enable automatic fetching of type definitions for .js libraries
    "include": [ "office-js" ]  // Ensure the "Office-js" type definition is fetched
  }
}