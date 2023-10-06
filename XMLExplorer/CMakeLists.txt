cmake_minimum_required(VERSION 3.16)

# Add folder where are supportive functions
set(CMAKE_MODULE_PATH ${CMAKE_CURRENT_SOURCE_DIR}/cmake)
set(CMAKE_INCLUDE_CURRENT_DIR ON)

# Include Qt basic functions
include(QtCommon)

# Basic information about project

project(XMLExplorer VERSION 1.0)

file(GLOB SHARED_LIBRARIES ${CMAKE_CURRENT_SOURCE_DIR}/lib/*.so)

# define the source and header files
set(SRC_FILES
        src/main.cpp
        src/XMLExplorer.cpp
)

add_project_meta(META_FILES_TO_INCLUDE)

set(RESOURCE_FILES XMLExplorer.qrc)

find_package(Qt6 COMPONENTS
        Core
        Gui
        Widgets
        REQUIRED)

# source_group("UI Files" FILES ${UI_FILES})

add_executable(${PROJECT_NAME} ${OS_BUNDLE} # Expands to WIN32 or MACOS_BUNDLE depending on OS
        ${SRC_FILES} ${META_FILES_TO_INCLUDE} ${RESOURCE_FILES}
)

target_precompile_headers(${PROJECT_NAME} INTERFACE QtWidgets.h)

target_link_libraries(${PROJECT_NAME} PRIVATE Qt6::Core Qt6::Gui Qt6::Widgets ${SHARED_LIBRARIES})

install(TARGETS ${PROJECT_NAME})
