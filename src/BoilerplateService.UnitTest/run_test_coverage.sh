#!/bin/bash
# Parameters - Colors
RED='\033[0;31m'
GREEN='\033[0;32m'
NC='\033[0m'

# Parameters - Solution
SOLUTION_FILE_NAME="BoilerplateService.sln"
COVERAGE_COVERLET_DIR=".coverage"
COVERAGE_REPORT_DIR=".coverage-report"
THRESHOLD=0
EXIT_CODE=0

# Parameters - Coverlet
DATA_COLLECTOR_FORMAT="cobertura"
COVERLET_OUTPUT_FORMAT="cobertura"
COVERLET_OUTPUT_EXTENSION=".xml"
COLLECT="XPlat Code Coverage"
COVERAGE_FILE_NAME="coverage.$COVERLET_OUTPUT_FORMAT$COVERLET_OUTPUT_EXTENSION"

# Parameters - Report Generator
REPORT_TYPES="HTML;cobertura;"
HTML_REPORT_INDEX_FILE_NAME="index.html"

# Calculated Parameters - Project
PROJECT_DIR="${PWD}"

# Calculated Parameters - Solution
SOLUTION_DIR="$(dirname $(cd "$( dirname "${BASH_SOURCE[0]}" )" && pwd))"
SOLUTION="$SOLUTION_DIR/$SOLUTION_FILE_NAME"

# Calculated Parameters - Coverlet
COVERLET_OUTPUT="$PROJECT_DIR/$COVERAGE_COVERLET_DIR"

# Delete previous test run results
if [ -d $COVERLET_OUTPUT ]
then
    rm -r $COVERLET_OUTPUT
fi

# Build and Test
dotnet test $SOLUTION --collect:"$COLLECT" --results-directory:"$COVERLET_OUTPUT" -- DataCollectionRunSettings.DataCollectors.DataCollector.Configuration.Format="$DATA_COLLECTOR_FORMAT"
retVal=$?

# Check Test results
if [ $retVal -ne 0 ]
then
    EXIT_CODE=1
else
    # Calculated Parameters - Report Generator
    TARGET_DIR="$PROJECT_DIR/$COVERAGE_REPORT_DIR"
    REPORT_HTML_FILE="$PROJECT_DIR/$COVERAGE_REPORT_DIR/$HTML_REPORT_INDEX_FILE_NAME"
    COVERAGE_FILES=$(find $COVERLET_OUTPUT/**/* -maxdepth 1 | awk -vORS=";" '{ print $1 }' | sed "s/\;$/\n/")

    # Delete previous test run reports
    if [ -d $TARGET_DIR ]
    then
        rm -r $TARGET_DIR
    fi

    # Extracte code coverage rates
    TOTAL_LINES=$(xmllint --xpath "//coverage/@lines-valid" $COVERAGE_FILES | awk -F'[="]' '!/>/{print $(NF-1)}')
    TOTAL_COVERED=$(xmllint --xpath "//coverage/@lines-covered" $COVERAGE_FILES | awk -F'[="]' '!/>/{print $(NF-1)}')
    COVERAGE=$((TOTAL_COVERED*100/TOTAL_LINES))

    # Generate Report
    dotnet reportgenerator "-reports:$COVERAGE_FILES" "-targetdir:$TARGET_DIR" "-reporttypes:$REPORT_TYPES"

    if [ $(($COVERAGE - $THRESHOLD)) -ge 0 ]
    then
        echo -e "${GREEN}Test coverage passed! Covered: $COVERAGE%, expected: $THRESHOLD%${NC}"
    else
        echo -e "${RED}Test coverage failed! Covered: $COVERAGE%, expected: $THRESHOLD%${NC}"
        # xdg-open $REPORT_HTML_FILE > /dev/null 2>&1
        EXIT_CODE=1
    fi
fi

exit $EXIT_CODE
