export default [
{
    files: ["**/*.js"],

    rules: {
      // ====== Bugs / logic mistakes ======

      // Catch missing return in map/filter/reduce callbacks
      "array-callback-return": "error",

      // Prevent unused variables
      "no-unused-vars": "warn",

      // Catch code that can never execute
      "no-unreachable": "error",

      // Require === instead of ==
      "eqeqeq": "error",

      // Prevent accidental assignments in conditions
      "no-cond-assign": "error",

      // Catch duplicated conditions
      "no-dupe-else-if": "error",

      // Catch duplicate object keys
      "no-dupe-keys": "error",

      // Catch duplicate case labels
      "no-duplicate-case": "error",

      // Catch invalid regex
      "no-invalid-regexp": "error",

      // Prevent calling something that is obviously not a function
      "no-unsafe-call": "off",


      // ====== Async / Promise mistakes ======

      // Catch async functions without await
      "require-await": "warn",

      // Catch forgotten Promise handling
      "no-floating-promises": "off",


      // ====== Code quality ======

      // Prevent useless variables
      "no-self-assign": "error",

      // Prevent useless comparisons
      "no-self-compare": "error",

      // Catch empty blocks
      "no-empty": "warn",

      // Prevent unreachable loops
      "no-constant-condition": "warn",

      // Warn about useless expressions
      "no-unused-expressions": "error",


      // ====== Modern JS safety ======

      // Prevent using var
      "no-var": "error",

      // Prefer const when possible
      "prefer-const": "warn",

      // Prevent redeclaring variables
      "no-redeclare": "error",


      // ====== Browser specific ======

      // Prevent accidentally using deprecated APIs
      "no-restricted-globals": [
        "warn",
        "event"
      ]
    }
  }
];
