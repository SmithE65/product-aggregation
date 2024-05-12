cmake_minimum_required(VERSION 3.22)

project(MyProject C)

set(CMAKE_C_COMPILER gcc)
set(CMAKE_C_STANDARD 23)
set(CMAKE_C_STANDARD_REQUIRED ON)
set(CMAKE_C_EXTENSIONS OFF)

add_executable(products
    src/main.c
    src/hashtable.c)