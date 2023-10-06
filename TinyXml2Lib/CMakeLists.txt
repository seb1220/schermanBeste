cmake_minimum_required(VERSION 3.26)
project(TinyXml2Lib)

set(CMAKE_CXX_STANDARD 17)

add_library(TinyXml2Lib SHARED tinyxml2.cpp)
