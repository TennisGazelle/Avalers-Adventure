cmake_minimum_required(VERSION 2.6)
PROJECT(BigProject)

set(CMAKE_CXX_FLAGS "${CMAKE_CXX_FLAGS} -std=c++11")

set(SOURCE_FILES src/clion_testing.cpp)

add_executable(BigProject ${SOURCE_FILES})