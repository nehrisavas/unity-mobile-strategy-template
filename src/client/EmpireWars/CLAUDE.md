# Project Instructions

This Unity project uses Vibe Unity for automated development workflows.

## About Vibe Unity
Vibe Unity enables claude-code integration for Unity scene creation and project automation.

⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄VIBE-UNITY⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄⌄

# Vibe Unity Integration Guide (Auto-generated - v2.0.0)

## Claude-Code Automated Workflow

### Primary Development Pattern
```bash
# 1. Validate compilation after code changes
./claude-compile-check.sh
# Exit codes: 0=success, 1=errors, 2=timeout, 3=script error

# 2. Create scenes via JSON (automatic processing)
echo '{"action":"create-scene","name":"TestScene","path":"Assets/Scenes"}' > .vibe-unity/commands/test.json

# 3. Verify results (check logs after 3 seconds)
sleep 3 && cat .vibe-unity/commands/logs/latest.log
```

### Automated Success/Failure Detection
- ✅ **Success Indicators**: Log contains "Scene created successfully" or "STATUS: SUCCESS"
- ❌ **Failure Indicators**: Log contains "ERROR", "FAILED", or compilation errors
- 🔄 **Claude Action**: On failure, immediately report specific error and stop workflow

### File Locations for Claude-Code
- **Compilation Check**: `./claude-compile-check.sh` (auto-installed)
- **JSON Commands**: Drop files in `.vibe-unity/commands/` directory
- **Log Verification**: Check `.vibe-unity/commands/logs/latest.log`
- **Coverage Reports**: `.vibe-unity/commands/coverage-analysis/`
- **Test Template**: `.vibe-unity/commands/test-scene-creation.json`

### Current Component Support (v2.0.0)
- ✅ **UI**: Canvas, Button, Text, Image, ScrollView, TextMeshPro
- ✅ **3D**: Cube, Sphere, Plane, Cylinder, Capsule, Camera, Light
- ⚠️ **Partial**: Rigidbody, Colliders
- ❌ **Missing**: ParticleSystem, custom scripts, animations

### JSON Command Examples for Claude-Code
```json
// Basic scene creation
{"action":"create-scene","name":"MyScene","path":"Assets/Scenes"}

// Multiple commands in batch file
{
  "commands": [
    {"action":"create-scene","name":"MenuScene","path":"Assets/Scenes/UI"},
    {"action":"add-canvas","name":"MainCanvas"},
    {"action":"add-button","name":"PlayButton","parent":"MainCanvas","text":"Play"}
  ]
}

// Add 3D objects
{"action":"add-cube","name":"TestCube","position":[0,1,0],"scale":[2,2,2]}
```

### Claude-Code Decision Tree
1. **After C# changes**: Run `./claude-compile-check.sh`
   - Exit code 0: Proceed with scene creation
   - Exit code 1: Fix compilation errors immediately, report to user
   - Exit code 2+: Report timeout/system issues to user

2. **For scene operations**: Use JSON commands with automatic verification
   - Success: Continue workflow
   - Failure: Report specific error from logs, ask user for guidance

3. **Error Handling**: 
   - **Compilation errors**: STOP and fix errors
   - **Scene creation failures**: STOP, report error, ask user to check Unity Console
   - **Missing components**: Note in summary, continue with supported components

### Development Workflow Status
- **File Watcher**: ✅ ENABLED (automatic JSON processing)
- **Compilation Check**: ✅ AUTOMATED (`./claude-compile-check.sh`)
- **Log Verification**: ✅ AUTOMATED (structured log parsing)
- **Error Detection**: ✅ AUTOMATED (exit codes + log analysis)

## Automated Claude Instructions
* **ALWAYS** run `./claude-compile-check.sh` after modifying C# scripts
* **ONLY proceed** if compilation check returns exit code 0
* **VERIFY scene creation** by checking `.vibe-unity/commands/logs/latest.log` for success/error messages
* **REPORT failures immediately** with specific error details from logs
* **DO NOT** create .meta files unless explicitly requested
* **ASK USER** for guidance only when encountering system-level failures or unsupported features

## For Detailed Usage
- **Full Documentation**: [Package README](./Packages/com.ricoder.vibe-unity/README.md)
- **JSON Schema Examples**: [Package Test Files](./Packages/com.ricoder.vibe-unity/.vibe-unity/commands/)
- **Coverage Analysis**: Check latest report in `.vibe-unity/commands/coverage-analysis/`

^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^VIBE-UNITY^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^

