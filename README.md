# WitsParser
Simple WITS(Wellsite Information Transfer Specification) parser

This is an example of WITS parser.

Usage example:

```C#
var witsStr = "&&\n01021000\n01031000\n!!";
var parser = new WitsParser.Wits.WitsDataSerializer();
parser.StringToSentenceCollection(witsStr);
```
