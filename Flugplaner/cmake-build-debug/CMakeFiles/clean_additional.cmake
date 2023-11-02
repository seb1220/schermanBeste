# Additional clean files
cmake_minimum_required(VERSION 3.16)

if("${CONFIG}" STREQUAL "" OR "${CONFIG}" STREQUAL "Debug")
  file(REMOVE_RECURSE
  "CMakeFiles/flugplaner_autogen.dir/AutogenUsed.txt"
  "CMakeFiles/flugplaner_autogen.dir/ParseCache.txt"
  "flugplaner_autogen"
  )
endif()
