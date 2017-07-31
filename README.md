# Synthetic Data File Generator (sdfg)

## Summary
**_What is sdfg?_**

A tool to generate synthetic data files to your exact specifications.

**_Why did I write sdfg?_**

There are scenarios where non-trivial data files with specific layouts are needed, but where existing/production/real data cannot be used - even with masking and/or scrubbing, as the concern  is "we may miss some sensitive data in our mask/scrub steps".

I wrote sdfg to allow for simple specification of the layout and other options for completely synthetic data file generation, to address these requirements from real-world engagements:
1. We need data files with realistic sizes and data characteristics which cannot be in any way based on production data, but must _resemble_ production data.
2. We need to specify exact file layouts. Generic examples with layouts different from our files will not work.
3. We need to specify other options including file date ranges, number of records per file, and details about different fields (columns) in the files. (Details below)

**_How can you use sdfg?_**

1. You can download the `run/syndatafilegen` folder. This is the main tool. You provide a run file with the needed configuration and invoke the tool, and it does the rest. See the README in that folder for command-line example. Multiple run file samples, as well as a full run file template with all options (and comments), are provided under /samples.
2. You can clone (or fork - I'm happy to consider pull requests) the repo. `/src` contains the full Visual Studio 2017 solution. You can write your own specific wrapper around sdfg; for example, the solution contains a CCLF17 project which I wrote to generate fairly complex and inter-related Medicare CCLF 2017 file sets, where data in one file (for example) is referenced in other files. The CCLF17 tool is also available for command-line invocation in `/run/cclf17`; note that as with the core sdfg tool, you need to provide a run file but again, there is a sample under `/samples/runfile_cclfgenerator`.

---

## Structure

sdfg consists of four functional areas:
* FileSpec: where output file-specific options are set. Currently, sdfg supports output to these formats: .arff; .avro; delimited (e.g. .csv, .tsv, etc.); fixed-width; and JSON.
* FieldSpec: this is used to configure each field in a file. Currently, sdfg supports these field types: categorical (where you provide a list of categories); date/time (where you provide a min/max date); numeric (where you specify a numeric distribution to use); dynamic (where you provide a C# Func<> to use). In each case, a FieldSpec generates a unique, random value for each record using the info you provide.
* Distribution: for numeric fields, this is used to specify the numeric distribution to use plus any needed parameters. For example, if you specify a Normal distribution, you'll also need to specify the mean and standard deviation. (See the sample run files.)
* Writer. Currently, only output to local file-system files is supported.

All the above are loosely coupled and work together through interfaces, so that extension and addition are as easy as possible.

---

## Detailed Configuration

Each of the structural areas has specific settings. The best way to gain quick insight into these is to review `/samples/runfile_FULL_TEMPLATE/runFile.json`, which contains every possible option and extensive comments for each option including when to use it, allowable values, and more.

---

# PLEASE NOTE FOR THE ENTIRETY OF THIS REPOSITORY AND ALL ASSETS
## 1. No warranties or guarantees are made or implied.
## 2. All assets here are provided by me "as is". Use at your own risk.
## 3. I am not representing my employer with these assets, and my employer assumes no liability whatsoever for any use of these assets.
## 4. DO NOT USE ANY ASSET HERE IN A PRODUCTION ENVIRONMENT WITHOUT APPROPRIATE REVIEWS, TESTS, and APPROVALS IN YOUR ENVIRONMENT.

---

Unless otherwise noted, all assets here are authored by me. Feel free to examine, learn from, comment, and re-use (subject to the above) as needed and without intellectual property restrictions.

If anything here helps you, attribution and/or a quick note is much appreciated.