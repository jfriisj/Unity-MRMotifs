#!/bin/bash
# Metrics Collection Script for Unity-MRMotifs
# Collects metrics data from Meta Quest 3 headsets
# Usage: ./collect_metrics.sh [session_name]

set -e

# Configuration
PACKAGE_NAME="com.Prototype.MRMotifs"
METRICS_PATH="/sdcard/Android/data/${PACKAGE_NAME}/files/metrics"
ADB_PATH="/c/Program Files/Unity/Hub/Editor/2022.3.62f2/Editor/Data/PlaybackEngines/AndroidPlayer/SDK/platform-tools/adb.exe"

# Colors for output
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
RED='\033[0;31m'
NC='\033[0m' # No Color

# Session name (default to timestamp if not provided)
SESSION_NAME=${1:-"session_$(date +%Y%m%d_%H%M%S)"}
OUTPUT_DIR="./metrics-data/${SESSION_NAME}"

echo -e "${GREEN}=== Unity-MRMotifs Metrics Collection ===${NC}"
echo "Session: ${SESSION_NAME}"
echo "Output Directory: ${OUTPUT_DIR}"
echo ""

# Check if ADB exists
if [ ! -f "$ADB_PATH" ]; then
    echo -e "${RED}Error: ADB not found at ${ADB_PATH}${NC}"
    echo "Please update ADB_PATH in the script to match your Unity installation."
    exit 1
fi

# Function to check if device is connected
check_device() {
    local serial=$1
    if ! MSYS_NO_PATHCONV=1 "$ADB_PATH" devices | grep -q "$serial"; then
        return 1
    fi
    return 0
}

# Function to get device label
get_device_label() {
    local serial=$1
    local device_name=$(MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$serial" shell getprop ro.product.model 2>/dev/null | tr -d '\r')
    echo "$device_name"
}

# Get list of connected devices
echo "Detecting connected Quest headsets..."
DEVICES=($(MSYS_NO_PATHCONV=1 "$ADB_PATH" devices | grep -E "^\w+" | awk '{print $1}'))

if [ ${#DEVICES[@]} -eq 0 ]; then
    echo -e "${RED}No devices found!${NC}"
    echo "Please connect Quest headsets via USB and enable USB debugging."
    exit 1
fi

echo -e "${GREEN}Found ${#DEVICES[@]} device(s):${NC}"
for i in "${!DEVICES[@]}"; do
    device_name=$(get_device_label "${DEVICES[$i]}")
    echo "  [$((i+1))] ${DEVICES[$i]} - $device_name"
done
echo ""

# Create output directory
mkdir -p "$OUTPUT_DIR"

# Collect data from each device
for i in "${!DEVICES[@]}"; do
    SERIAL="${DEVICES[$i]}"
    DEVICE_NUM=$((i+1))
    DEVICE_DIR="${OUTPUT_DIR}/H${DEVICE_NUM}"
    
    echo -e "${YELLOW}=== Collecting from Device H${DEVICE_NUM} (${SERIAL}) ===${NC}"
    
    # Check if metrics directory exists on device
    if ! MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" shell "ls ${METRICS_PATH} 2>/dev/null" > /dev/null 2>&1; then
        echo -e "${RED}Warning: Metrics directory not found on device H${DEVICE_NUM}${NC}"
        echo "Make sure the app has been run and metrics were logged."
        continue
    fi
    
    # Create device directory
    mkdir -p "$DEVICE_DIR"
    
    # Pull metrics files
    echo "  → Pulling metrics files..."
    MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" pull "${METRICS_PATH}/" "${DEVICE_DIR}/" 2>&1 | grep -E "^/|pulled|skipped" || true
    
    # Pull Unity logcat
    echo "  → Pulling Unity logs..."
    MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" logcat -d -s Unity:* > "${DEVICE_DIR}/unity_logcat.txt" 2>/dev/null || true
    
    # Get device info
    echo "  → Collecting device info..."
    cat > "${DEVICE_DIR}/device_info.txt" <<EOF
Device Serial: ${SERIAL}
Device Model: $(MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" shell getprop ro.product.model | tr -d '\r')
Android Version: $(MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" shell getprop ro.build.version.release | tr -d '\r')
Build Number: $(MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" shell getprop ro.build.display.id | tr -d '\r')
Collection Time: $(date +"%Y-%m-%d %H:%M:%S")
EOF
    
    # Get battery info
    echo "  → Collecting battery stats..."
    MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" shell dumpsys battery > "${DEVICE_DIR}/battery_info.txt" 2>/dev/null || true
    
    # Get thermal info
    echo "  → Collecting thermal data..."
    MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" shell "cat /sys/class/thermal/thermal_zone*/temp 2>/dev/null" > "${DEVICE_DIR}/thermal_zones.txt" 2>/dev/null || true
    MSYS_NO_PATHCONV=1 "$ADB_PATH" -s "$SERIAL" shell "cat /sys/class/thermal/thermal_zone*/type 2>/dev/null" > "${DEVICE_DIR}/thermal_types.txt" 2>/dev/null || true
    
    # Count files collected
    CSV_COUNT=$(find "${DEVICE_DIR}" -name "*.csv" 2>/dev/null | wc -l)
    JSON_COUNT=$(find "${DEVICE_DIR}" -name "*.json" 2>/dev/null | wc -l)
    
    echo -e "${GREEN}  ✓ Collected ${CSV_COUNT} CSV files and ${JSON_COUNT} JSON files from H${DEVICE_NUM}${NC}"
    echo ""
done

# Create collection summary
SUMMARY_FILE="${OUTPUT_DIR}/collection_summary.txt"
cat > "$SUMMARY_FILE" <<EOF
=== Metrics Collection Summary ===
Session: ${SESSION_NAME}
Collection Time: $(date +"%Y-%m-%d %H:%M:%S")
Devices Collected: ${#DEVICES[@]}

Files Collected:
EOF

for i in "${!DEVICES[@]}"; do
    DEVICE_NUM=$((i+1))
    DEVICE_DIR="${OUTPUT_DIR}/H${DEVICE_NUM}"
    if [ -d "$DEVICE_DIR" ]; then
        CSV_COUNT=$(find "${DEVICE_DIR}" -name "*.csv" 2>/dev/null | wc -l)
        JSON_COUNT=$(find "${DEVICE_DIR}" -name "*.json" 2>/dev/null | wc -l)
        echo "  H${DEVICE_NUM}: ${CSV_COUNT} CSV, ${JSON_COUNT} JSON" >> "$SUMMARY_FILE"
    fi
done

echo "" >> "$SUMMARY_FILE"
echo "Total Size: $(du -sh "$OUTPUT_DIR" | cut -f1)" >> "$SUMMARY_FILE"

# Display summary
echo -e "${GREEN}=== Collection Complete ===${NC}"
cat "$SUMMARY_FILE"
echo ""
echo "Data saved to: ${OUTPUT_DIR}"
echo ""
echo "Next steps:"
echo "  1. Review the collected CSV files in each H* directory"
echo "  2. Run analysis: python scripts/analyze_metrics.py ${OUTPUT_DIR}"
echo "  3. Check device_info.txt and battery_info.txt for device status"
