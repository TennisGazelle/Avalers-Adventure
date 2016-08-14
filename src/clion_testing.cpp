//
// Created by Daniel Lopez on 8/14/2016.
//

#include <stdio.h>

int main (int argc, const char *argv[]) {
    // start up code
    if (argc < 2) {
        printf("Usage: ./BigProject <your name>");
        return 1;
    }

    printf("Hello, %s!", argv[1]);
    return 0;
}

