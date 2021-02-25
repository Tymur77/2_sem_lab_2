// gcc -c -fpic main.c
// gcc -shared -o libdict.so main.o
// rm main.o

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

#define TRUE_FALSE "True":"False"
#define INITIAL_SIZE 5
#define NOT_FOUND -1

typedef struct{
    int value;
    char *key;
} KeyValuePair;

KeyValuePair *Resize(KeyValuePair *old);


void CheckForNull(void *ptr){
    if (ptr == NULL){
        fprintf(stderr, "couldn't allocate memory");
        exit(1);
    }
}

void SetSize(KeyValuePair *pairs, int size){
    *(int *)pairs = size;
}

int GetSize(KeyValuePair *pairs){
    return *(int *)pairs;
}

KeyValuePair *Init(){
    KeyValuePair *pairs = malloc(sizeof(KeyValuePair) * (INITIAL_SIZE + 1));
    CheckForNull(pairs);
    SetSize(pairs, INITIAL_SIZE);

    KeyValuePair empty;
    empty.key = NULL;
    for (int i = 1; i <= INITIAL_SIZE; i++)
    {
        pairs[i] = empty;
    }
    return pairs;
}

int Hash(char *key, int size){
    int sum = 0;
    while (*key != 0){
        unsigned char c = *(key++);
        sum += c;
    }
    return sum % size;
}

int isEmpty(KeyValuePair pair){
    return pair.key == NULL;
}

KeyValuePair *Put(KeyValuePair *pairs, char *key, int value){
    int size = GetSize(pairs);
    int index = Hash(key, size);
    index++;

    KeyValuePair new;
    new.key = key;
    new.value = value;
    for (int i = index; i <= size; i++)
    {
        if (isEmpty(pairs[i])){
            pairs[i] = new;
            return pairs;
        }
    }
    for (int i = 1; i < index; i++)
    {
        if (isEmpty(pairs[i])){
            pairs[i] = new;
            return pairs;
        }
    }
    pairs = Resize(pairs);
    return Put(pairs, key, value);
}

KeyValuePair *Resize(KeyValuePair *pairs){
    printf("resizing ... \n");
    int oldSize = GetSize(pairs);
    int newSize = oldSize * 2;
    KeyValuePair *new = malloc(sizeof(KeyValuePair) * (newSize + 1));
    CheckForNull(new);
    SetSize(new, newSize);

    KeyValuePair empty;
    empty.key = NULL;
    for (int i = 1; i <= newSize; i++)
    {
        new[i] = empty;
    }
    for (int i = 1; i <= oldSize; i++)
    {
        char *key = pairs[i].key;
        int value = pairs[i].value;
        new = Put(new, key, value);
    }
    free(pairs);
    return new;
}

int Get(KeyValuePair *pairs, char *key){
    int size = GetSize(pairs);
    int index = Hash(key, size);
    index++;

    for (int i = index; i <= size; i++)
    {
        char *match = pairs[i].key;
        int value   = pairs[i].value;
        if (match != NULL && strcmp(match, key) == 0){
            return value;
        }
    }
    for (int i = 1; i < index; i++)
    {
        char *match = pairs[i].key;
        int value   = pairs[i].value;
        if (match != NULL && strcmp(match, key) == 0){
            return value;
        }
    }
    return NOT_FOUND;
}

int GetLength(KeyValuePair *pairs){
    int size = GetSize(pairs);
    int count = 0;
    for (int i = 1; i <= size; i++)
    {
        if (!isEmpty(pairs[i])){
            count++;
        }
    }
    return count;
}