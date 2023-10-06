cmake_minimum_required(VERSION 3.26)
project(test)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTORCC ON)
set(CMAKE_AUTOUIC ON)


find_package(Qt6 COMPONENTS
        Core
        Gui
        Widgets
        REQUIRED)

qt_standard_project_setup()

# define the source and header files
file(GLOB SRC_FILES "src/*.cpp")
file(GLOB INCLUDE_FILES "include/*.h" "ui/*.ui")

qt_add_executable(test ${SRC_FILES} ${INCLUDE_FILES})
target_link_libraries(test
        PRIVATE
        Qt6::Core
        Qt6::Gui
        Qt6::Widgets
)
