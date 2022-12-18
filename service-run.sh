#!/bin/bash
serviceName="the_arc_project"

# ====================
# 
# ====================
dotnet publish "src/Arc/Arc.csproj" --output "bin" --runtime linux-x64 --self-contained \
  "-p:Configuration=Release" \
  "-p:DebugType=None" \
  "-p:GenerateRuntimeConfigurationFiles=true" \
  "-p:PublishSingleFile=true"

# ====================
# 
# ====================
if [ $serviceName != "Arc" ]; then
  mv "bin/Arc" "bin/${serviceName}"
fi

# ====================
# 
# ====================
"bin/${serviceName}"
